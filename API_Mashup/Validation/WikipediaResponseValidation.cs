using System;
using ApiMashup.DAO;

namespace ApiMashup.Validation
{
    public class WikipediaPagesExtractValidation : ValidationBase<WikipediaResponse>
    {
        public WikipediaPagesExtractValidation(WikipediaResponse context) : base(context) { }
        public override bool IsValid => throw new NotImplementedException();

        public override string Message => throw new NotImplementedException();
    }
}