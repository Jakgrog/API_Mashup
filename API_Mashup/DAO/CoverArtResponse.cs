﻿using ApiMashup.Validation;

namespace ApiMashup.DAO
{
    /// <summary>
    /// Contains the response from cover art.
    /// </summary>
    public class CoverArtResponse : IResponse
    {
        public Image[] Images { get; set; }

        public string getImageUrl { get; }
        public CoverArtResponse(string m)
        {
            Images = new Image[1];
            Images[0] = new Image(m);
        }
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