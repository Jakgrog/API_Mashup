using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ApiMashup.DAO
{
    public class WikipediaObject : IResponseObject
    {
        public JObject Query { get; set; }

        public string GetDescriptionPage()
        {
            JToken page = Query.Last.First().First.First["extract"];
            return page.ToString();
        }
    }

    public class WikidataObject : IResponseObject
    {
        public JObject Entities { get; set; }

        public string GetWikipediaID()
        {
            return Entities.First.First["sitelinks"]["enwiki"]["url"].ToString().Split('/').Last();
        }
    }
}