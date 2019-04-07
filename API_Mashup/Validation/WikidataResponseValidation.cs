using System;
using ApiMashup.DAO;

namespace ApiMashup.Validation
{
    public class WikidataUrlValidation : ValidationBase<WikidataResponse>
    {
        public WikidataUrlValidation(WikidataResponse context) : base(context) { }
        public override bool IsValid => !(Context.Entities == null);
        public override string Message => "Wikidata response does not contain 'Enteties'";
    }
}