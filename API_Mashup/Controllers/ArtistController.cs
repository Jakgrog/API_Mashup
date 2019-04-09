using System;
using System.Web.Http;
using System.Threading.Tasks;
using ApiMashup.ArtistBuilder;
using WebApi.OutputCache.V2;
using Microsoft.Web.Http;
using ApiMashup.Validation;
using ApiMashup.Actionfilters;

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
        public async Task<IHttpActionResult> Get(string id)
        {
            IValidation input = new MbidValidation(id);

            if (input.IsValid)
            {
                return Ok(await new ArtistBuilderObject().RunGetArtistAsync(id));
            }
            else
            {
                return BadRequest(input.Message);
            }
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public IHttpActionResult Get()
        {
            return BadRequest("Please enter a mbid");
        }
    }
}

