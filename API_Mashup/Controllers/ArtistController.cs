using System.Web.Http;
using System.Threading.Tasks;
using ApiMashup.Models;
using ApiMashup.DAO;

namespace ApiMashup.Controllers
{
    /// <summary>
    /// This <see cref="ApiController"/> takes a mbid as input and fetches information from
    /// Music brainz, Cover art archive and Wikipedia in order to aggregate the information
    /// and return an Artist object. 
    /// The information is fetched asynchronously from the sites.
    /// </summary>
    public class ArtistController : ApiController
    {
        [HttpGet]
        public async Task<Artist> Get(string id)
        {
            return await new ArtistDao().RunGetArtistAsync(id);
        }
    }
}
