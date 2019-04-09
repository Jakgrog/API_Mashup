using ApiMashup.Validation;

namespace ApiMashup.Models
{
    /// <summary>
    /// Contains the response from cover art.
    /// </summary>
    public class CoverArtResponse : IResponse
    {
        // Array with all cover images
        public Image[] Images { get; set; }

    }

    public class Image
    {
        public string image { get; set; }

        public Image(string m)
        {
            image = m;
        }
    }
}