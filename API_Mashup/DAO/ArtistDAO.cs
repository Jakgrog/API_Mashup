using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using ApiMashup.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ApiMashup.DAO
{
    /// <summary>
    /// Implementing Data access objects pattern
    /// </summary>
    public interface IArtistDAO
    {

    }
    public class ArtistDAO : IArtistDAO
    {
        private static HttpClient client = new HttpClient();

        private readonly string musicBrainzUrl;
        private readonly string coverArtUrl;
        private readonly string wikipediaUrl;
        private readonly string wikidataUrl;

        public ArtistDAO()
        {
            ConnectionStringSettingsCollection settings = 
                ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                musicBrainzUrl = settings["musicbrainz"].ConnectionString;
                coverArtUrl = settings["coverart"].ConnectionString;
                wikidataUrl = settings["wikidata"].ConnectionString;
                wikipediaUrl = settings["wikipedia"].ConnectionString;
            }
        }

        public async Task<Artist> RunGetArtistAsync(string mbid)
        {
            MusicBrainzObject musicBrainz = await GetMusicBrainzAsync(mbid);
            string description = await GetArtistDescriptionAsync(musicBrainz.GetWikidataID());
            Album[] albums = await GetAlbumsAsync(musicBrainz.ReleaseGroups);

            return new Artist(mbid, description, albums);
        }

        private async Task<MusicBrainzObject> GetMusicBrainzAsync(string mbid)
        {
            return await GetResponseAsync<MusicBrainzObject>(string.Format(musicBrainzUrl, mbid));
            // Throw exception if bad response
        }

        private async Task<string> GetArtistDescriptionAsync(string id)
        {
            WikidataObject wikiData = await GetResponseAsync<WikidataObject>(string.Format(wikidataUrl, id));
            // Throw exception if bad response
            WikipediaObject description = await GetResponseAsync<WikipediaObject>(string.Format(wikipediaUrl, wikiData.GetWikipediaID()));
            // Throw exception if bad response

            return description.GetDescriptionPage();
        }

        private async Task<Album[]> GetAlbumsAsync(IList<ReleaseGroups> releaseGroups)
        {
            IList<Album> Albums = new List<Album>();

            //TODO: Speed this upp, multiple taskt to the same server (Create a collection of tasks?)
            foreach (ReleaseGroups group in releaseGroups)
            {
                CoverArt albumCoversRespons = (await GetResponseAsync<CoverArt>(string.Format(coverArtUrl, group.Id)));
                Image[] albumImages = albumCoversRespons?.Images;

                // Throw exception if bad response
                if (albumImages != null)
                    Albums.Add(new Album(group.Title, group.Id, albumImages));
            }

            return Albums.ToArray();
        }

        private async Task<IResponseObject> GetResponseAsync<IResponseObject>(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string product = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IResponseObject>(product);
            }
            else
            {
                //TODO: Exceptions
                return default(IResponseObject);
            }
        }
    }
}
