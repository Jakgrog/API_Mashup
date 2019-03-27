using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace ApiMashup.DAO
{
    public class MusicBrainzObject : IResponseObject
    {
        public IList<Relation> Relations { get; set; }

        [JsonProperty("release-groups")]
        public IList<ReleaseGroup> ReleaseGroups { get; set; }

        public string GetWikidataID()
        {
            Relation wikiRelation = Relations.FirstOrDefault(x => x.Type.Equals("wikidata", StringComparison.OrdinalIgnoreCase));

            string resource = String.Empty;
            if (!wikiRelation.Equals(default(Relation)))
            {
                resource = wikiRelation.Url.Resource.Split('/').Last();
            }
            //TODO: Throw exeption
            return resource;
        }
    }
    public struct Relation
    {
        public string Type { get; set; }
        public Url Url { get; set; }
    }

    public struct Url
    {
        public string Resource { get; set; }
    }

    public struct ReleaseGroup
    {
        public string Title { get; set; }
        public string Id { get; set; }
    }

}