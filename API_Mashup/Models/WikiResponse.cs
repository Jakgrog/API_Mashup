using System.Linq;
using Newtonsoft.Json.Linq;

namespace ApiMashup.Models
{
    /// <summary>
    /// Contains the response from wikipedia.
    /// </summary>
    public class WikipediaResponse : IResponse
    {
        public JObject Query { get; set; }
        /// <summary>
        /// Extracts the artist description from the JQuery response.
        /// </summary>
        public string GetDescriptionPage()
        {
            JToken page = Query.Last.First().First.First["extract"];
            return page != default(JToken) ? page.ToString() : "Description could not be found";
        }
    }

    /// <summary>
    /// Contains the wikidata response
    /// </summary>
    public class WikidataResponse : IResponse
    {
        public JObject Entities { get; set; }

        /// <summary>
        /// Extracts the band name needed to find the description in wikipedia
        /// </summary>
        public string GetWikipediaID()
        {
            return Entities.First.First["sitelinks"]["enwiki"]["url"].ToString().Split('/').Last();
        }
    }
}