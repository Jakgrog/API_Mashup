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
    public class ArtistController : ApiController
    {
        [HttpGet]
        public async Task<Artist> Get(string id)
        {
            return await (new ArtistDAO()).RunGetArtistAsync(id);
        }
    }
}