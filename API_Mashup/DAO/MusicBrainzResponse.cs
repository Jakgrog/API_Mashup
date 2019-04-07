using System;
using Newtonsoft.Json;
using System.Linq;
using ApiMashup.Validation;

namespace ApiMashup.DAO
{
    /// <summary>
    /// Contains the response from music brainz.
    /// </summary>
    public class MusicBrainzResponse : IResponse
    {
        public string GetExceptionMessage()
        {
            return "Invalid mbid";
        }

        public Relation[] Relations { get; set; }
        public IValidation Validation { get; set; }

        [JsonProperty("release-groups")]
        public ReleaseGroups[] ReleaseGroups { get; set; }

        /// <summary>
        /// Extracs the artist wikidata ID from the wikidata relation. 
        /// </summary>
        public string GetWikidataID()
        {
            Relation wikiRelation = Relations.FirstOrDefault(x => x.Type.Equals("wikidata", StringComparison.OrdinalIgnoreCase));

            string resource = String.Empty;
            if (!wikiRelation.Equals(default(Relation)))
            {
                resource = wikiRelation.Url.Resource.Split('/').Last();
            }
            return resource;
        }
    }

    public class Relation
    {
        public string Type { get; set; }
        public Url Url { get; set; }
    }


    public class Url
    {
        public string Resource { get; set; }
        public string Id { get; set; }
    }

    public class ReleaseGroups
    {
        public string Title { get; set; }
        public string Id { get; set; }
    }

}