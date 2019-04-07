using System.Linq;
using Newtonsoft.Json.Linq;
using ApiMashup.Validation;

namespace ApiMashup.DAO
{
    /// <summary>
    /// Contains the response from wikipedia.
    /// </summary>
    public class WikipediaResponse : IResponse
    {
        public string GetExceptionMessage()
        {
            return "Something went wrong when retrieving artist description from wikipedia";
        }
        public JObject Query { get; set; }
        public IValidation Validation { get; set; }

        /// <summary>
        /// Extracts the artist description from the JQuery response.
        /// </summary>
        public string GetDescriptionPage()
        {
            JToken page = Query.Last.First().First.First["extract"];
            return page.ToString();
        }
    }

    /// <summary>
    /// Contains the wikidata response
    /// </summary>
    public class WikidataResponse : IResponse
    {
        public string GetExceptionMessage()
        {
            return "Something went wrong when retrieving wikipedia URL from Wikidata";
        }
        public JObject Entities { get; set; }
        public IValidation Validation { get; set; }

        /// <summary>
        /// Extracts the band name needed to find the description in wikipedia
        /// </summary>
        public string GetWikipediaID()
        {
            return Entities.First.First["sitelinks"]["enwiki"]["url"].ToString().Split('/').Last();
        }
    }
}