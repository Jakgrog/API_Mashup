using ApiMashup.Models;

namespace ApiMashup.Validation
{
    /// <summary>
    /// Checks if the artist description is null or empty. 
    /// If it is, it checks what element in the wikipedia
    /// response that is empty by starting from the root
    /// of the Json object.
    /// </summary>
    public class WikipediaPagesExtractValidation : ValidationBase<WikipediaResponse>
    {
        public WikipediaPagesExtractValidation(WikipediaResponse context) : base(context) { }
        public override bool IsValid => Context.GetDescriptionPage() != "" && Context.GetDescriptionPage() != null;
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