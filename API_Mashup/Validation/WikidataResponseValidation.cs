using System;
using ApiMashup.DAO;

namespace ApiMashup.Validation
{
    public class WikidataUrlValidation : ValidationBase<WikidataResponse>
    {
        public WikidataUrlValidation(WikidataResponse context) : base(context) { }
        public override bool IsValid => throw new NotImplementedException();

        public override string Message => throw new NotImplementedException();
    }
}