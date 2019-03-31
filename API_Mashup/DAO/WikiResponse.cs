using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ApiMashup.DAO
{
    /// <summary>
    /// Holds the wikipedia response
    /// </summary>
    public class WikipediaResponse : IResponse
    {
        public string GetExceptionMessage()
        {
            return "Artist not found in wikipedia";
        }
        public JObject Query { get; set; }

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
    /// Holds the wikidata response
    /// </summary>
    public class WikidataResponse : IResponse
    {
        public string GetExceptionMessage()
        {
            return "Artist not found in wikidata";
        }
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