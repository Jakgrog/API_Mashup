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
    static class ArtistDAO
    {
        private static HttpClient client = new HttpClient();

        private static string musicBrainzUrl;
        private static string coverArtArchiveUrl;
        private static string wikipediaUrl;
        private static string wikidataUrl;

        public static async Task<string> GetArtistAsync(string mbid)
        {
            GetConnectionStrings();
            MusicBrainzObject musicBrainz = await GetMusicBrainzAsync(mbid);
            
            //var albums = GetArtistDescriptionAsync(musicBrainz.ReleaseGroups);
            var description = await GetArtistDescriptionAsync(musicBrainz.GetWikidataID());

            return description; 
        }

        private static void GetConnectionStrings()
        {
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                musicBrainzUrl = settings["musicbrainz"].ConnectionString;
                coverArtArchiveUrl = settings["coverart"].ConnectionString;
                wikidataUrl = settings["wikidata"].ConnectionString;
                wikipediaUrl = settings["wikipedia"].ConnectionString;
            }
        }

        private static async Task<MusicBrainzObject> GetMusicBrainzAsync(string mbid)
        {
            return await RunAsync<MusicBrainzObject>(string.Format(musicBrainzUrl, mbid));
        }

        private static async Task<string> GetArtistDescriptionAsync(string id)
        {
            WikidataObject wikiData = await RunAsync<WikidataObject>(string.Format(wikidataUrl, id));
            WikipediaObject description = await RunAsync<WikipediaObject>(string.Format(wikipediaUrl, wikiData.GetWikipediaID()));

            return description.GetDescriptionPage();
        }

        private static async Task<IResponseObject> GetArtistAlbumsAsync(string mbid)
        {
            return await RunAsync<WikipediaObject>(string.Format(wikipediaUrl, mbid));
        }

        private static async Task<T> RunAsync<T>(string url) where T : IResponseObject
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(product);
            }
            else
            {
                return default(T);
            }
        }
    }
}
