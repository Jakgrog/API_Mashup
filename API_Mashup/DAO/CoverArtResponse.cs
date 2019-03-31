namespace ApiMashup.DAO
{
    /// <summary>
    /// Contains the response from cover art.
    /// </summary>
    public class CoverArtResponse : IResponse
    {
        public string GetExceptionMessage()
        {
            return "Something went wrong when retrieving Album information from Cover Art Archive";
        }
        public Image[] Images { get; set; }

        public string getImageUrl { get; }
    }

    public class Image
    {
        public string image { get; set; }
    }
}