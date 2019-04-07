using ApiMashup.Models;

namespace ApiMashup.Validation
{
    public class WikipediaPagesExtractValidation : ValidationBase<WikipediaResponse>
    {
        public WikipediaPagesExtractValidation(WikipediaResponse context) : base(context) { }
        public override bool IsValid => Context.Query == null;

        public override string Message => "Wikipedia response does not contain 'query'";
    }
}