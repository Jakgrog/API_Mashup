using System.Web.Http;
using System.Threading.Tasks;
using ApiMashup.Models;
using ApiMashup.DAO;
using ApiMashup.Actionfilters;
using WebApi.OutputCache.V2;
using Microsoft.Web.Http;


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
        [ApiVersion("1.0")]
        [DeflateCompression]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public async Task<Artist> Get(string id)
        {
            return await new ArtistDao().RunGetArtistAsync(id);
        }
    }
}
