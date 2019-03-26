using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API_Mashup.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace API_Mashup
{
    public class Response
    {
        public JObject Pages { get; set; }
    }

    public class ArtistController : ApiController
    {

        [HttpGet]
        public async Task<JObject> Get(string id)
        {
            List<Album> Albums = new List<Album>();
            Albums.Add(new Album("Songalong", "ididid", "http://image.jpg"));

            return await RunAsync("https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles=Nirvana_(band)");

            //return new Artist(id, "Jakob", Albums);
        }

        static async Task<JObject> RunAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(url);
                var objectResponse = new JObject();
                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsStringAsync();
                    objectResponse = JsonConvert.DeserializeObject<JObject>(product);
                }

                return objectResponse;
            }
        }
    }
}