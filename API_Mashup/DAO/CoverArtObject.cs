using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiMashup.DAO
{

    public class CoverArt : IResponseObject
    {
        public Image[] Images { get; set; }

        public string getImageUrl { get; }
    }

    public class Image
    {
        public string image { get; set; }
    }
}