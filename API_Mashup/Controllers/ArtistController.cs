using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using ApiMashup.Models;
using ApiMashup.DAO;
using Newtonsoft.Json.Linq;

namespace ApiMashup.Controllers
{
    public class Response
    {
        public JObject Pages { get; set; }
    }

    public class ArtistController : ApiController
    {
        [HttpGet]
        public async Task<string> Get(string id)
        {
            List<Album> Albums = new List<Album>();
            Albums.Add(new Album("Songalong", "ididid", "http://image.jpg"));
            
            return await ArtistDAO.GetArtistAsync(id);

            //return await ArtistDAO.RunAsync("https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles=Nirvana_(band)");

            //return new Artist(id, "Jakob", Albums);
        }
    }
}