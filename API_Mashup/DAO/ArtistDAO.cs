using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;
using ApiMashup.Models;
using Newtonsoft.Json;

namespace ApiMashup.DAO
{
    /// <summary>
    /// Fetches information from the different API:s and agregates it
    /// into an Artist object.
    /// </summary>
    public interface IArtistDao
    {
        Task<Artist> RunGetArtistAsync(string mbid);
    }
    /// <summary>
    /// Implements IArtistDao
    /// </summary>
    public class ArtistDao : IArtistDao
    {
        private static HttpClient client = new HttpClient();

        private readonly string musicBrainzUrl;
        private readonly string coverArtUrl;
        private readonly string wikipediaUrl;
        private readonly string wikidataUrl;

        /// <summary>
        /// ArtistDao constructor, reads the Url templates located in
        /// the web.config file and stores them as readonly strings.
        /// </summary>
        public ArtistDao()
        {
            NameValueCollection settings = 
                ConfigurationManager.AppSettings;

            if (settings != null)
            {
                musicBrainzUrl = settings["musicbrainz"].ToString();
                coverArtUrl = settings["coverart"].ToString();
                wikidataUrl = settings["wikidata"].ToString();
                wikipediaUrl = settings["wikipedia"].ToString();
            }
        }

        /// <summary>
        /// Creates an Artist object from the information 
        /// retrieved from the different API:s. 
        /// </summary>
        /// <param name="mbid"></param>
        public async Task<Artist> RunGetArtistAsync(string mbid)
        {
            string description = string.Empty;
            Album[] albums = null;
            MusicBrainzResponse musicBrainz;

            musicBrainz = await GetMusicBrainzAsync(mbid);
            description = await GetArtistDescriptionAsync(musicBrainz.GetWikidataID());
            albums = await GetAlbumsAsync(musicBrainz.ReleaseGroups);

            return new Artist(mbid, description, albums);
        }

        /// <summary>
        /// Sends a request to Music brainz
        /// </summary>
        /// <param name="mbid"></param>
        private async Task<MusicBrainzResponse> GetMusicBrainzAsync(string mbid)
        {
            try
            {
                return await GetResponseAsync<MusicBrainzResponse>(string.Format(musicBrainzUrl, mbid));
            }
            catch(Exception we)
            {
                Debug.WriteLine(we.Message); 
                throw;
            }
        }

        /// <summary>
        /// Sends a request to Wikidata, uses the ID from wikidata
        /// to send a request to wikipedia.
        /// </summary>
        /// <param name="id"></param>
        private async Task<string> GetArtistDescriptionAsync(string id)
        {
            try
            {
                WikidataResponse wikiData = await GetResponseAsync<WikidataResponse>(string.Format(wikidataUrl, id));
                WikipediaResponse description = await GetResponseAsync<WikipediaResponse>(string.Format(wikipediaUrl, wikiData.GetWikipediaID()));

                return description.GetDescriptionPage();
            }
            catch(Exception we)
            {
                Debug.WriteLine(we.Message);
                return we.Message;
            }
        }

        /// <summary>
        /// Selects all release groups in the release groups list and runs the 
        /// GetAlbumAsync function with each groups Id and Title as in parameters.
        /// </summary>
        /// <param name="releaseGroups"></param>
        private async Task<Album[]> GetAlbumsAsync(IList<ReleaseGroups> releaseGroups)
        {
            return await Task.WhenAll(releaseGroups.Select(x => GetAlbumAsync(x.Id, x.Title)));
        }

        /// <summary>
        /// Sends a request to Cover art archive and generates
        /// an Album object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        private async Task<Album> GetAlbumAsync(string id, string title)
        {
            Image[] albumImages = default(Image[]);
            try
            {
                CoverArtResponse albumCoversRespons = await GetResponseAsync<CoverArtResponse>(string.Format(coverArtUrl, id));
                albumImages = albumCoversRespons?.Images;
            }
            catch(Exception we)
            {
                Debug.WriteLine(we.Message);
            }

            return new Album(title, id, albumImages);
        }

        /// <summary>
        /// A generic function that sends a request to a specific url
        /// and stores it as a IResponse object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        private async Task<T> GetResponseAsync<T>(string url) where T : IResponse
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);
            string product = await response.Content.ReadAsStringAsync();
            T responseObject = JsonConvert.DeserializeObject<T>(product);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString() + "-" + responseObject.GetExceptionMessage());
            }

            return responseObject;
        }
    }
}
