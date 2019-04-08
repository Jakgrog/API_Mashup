using ApiMashup.Models;

namespace ApiMashup.Validation
{
    public class WikipediaPagesExtractValidation : ValidationBase<WikipediaResponse>
    {
        public WikipediaPagesExtractValidation(WikipediaResponse context) : base(context) { }
        public override bool IsValid => Context.Query != null;

        public override string Message => "Wikipedia response does not contain any artist description. "
            + CheckResponset() + " could not be found in the Wikipedia response";
        private string CheckResponset()
        {
            if (Context.Query == null)
                return "Query";
            else if (Context.Query.Last == null)
                return "Last element of Query";
            else if (Context.Query.Last.First == null)
                return "First element of Last element of Query";
            else if (Context.Query.Last.First.First == null)
                return "First element of First element of Last element of Query";
            else if (Context.Query.Last.First.First.First == null)
                return "First element of First element of First element of Last element of Query";
            else
                return "extract";
        }
    }
}