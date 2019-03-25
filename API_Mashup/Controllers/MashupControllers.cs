using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_Mashup
{
    public class MashupController : ApiController
    {
        [HttpGet]
        public ArtistInformation Get(string id)
        {
            List<Album> Albums = new List<Album>();
            Albums.Add(new Album("Songalong","ididid", "http://image.jpg"));

            return new ArtistInformation(id, "Jakob", Albums);
        }
    }
}