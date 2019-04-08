using ApiMashup.Models;

namespace ApiMashup.Validation
{
    public class WikidataUrlValidation : ValidationBase<WikidataResponse>
    {
        public WikidataUrlValidation(WikidataResponse context) : base(context) { }
        public override bool IsValid =>  Context.GetWikipediaID() != null;
        public override string Message => "Wikidata response does not contain Wikipedia Url. "
             + CheckResponse() + " could not be found in the Wikidata response";
        private string CheckResponse()
        {
            if (Context.Entities == null)
                return "Enteties";
            else if (Context.Entities.First == null)
                return "First element of Enteties";
            else if (Context.Entities.First.First == null)
                return "First element of First element of Enteties";
            else if (Context.Entities.First.First["sitelinks"] == null)
                return "sitelinks";
            else if (Context.Entities.First.First["sitelinks"]["enwiki"] == null)
                return "enwiki";
            else
                return "url";
        }
    }
}