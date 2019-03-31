using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiMashup.DAO
{
    /// <summary>
    /// Holds the cover art response
    /// </summary>
    public class CoverArtResponse : IResponse
    {
        public string GetExceptionMessage()
        {
            return "Album not found in Cover Art Archive";
        }
        public Image[] Images { get; set; }

        public string getImageUrl { get; }
    }

    public class Image
    {
        public string image { get; set; }
    }
}