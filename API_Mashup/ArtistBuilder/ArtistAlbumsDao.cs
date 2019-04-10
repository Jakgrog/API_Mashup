using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using ApiMashup.Validation;
using ApiMashup.Models;

namespace ApiMashup.ArtistBuilder
{
    public class ArtistAlbumsDao : ArtistDao
    {
        private CoverArtResponse coverArtResponse;

        /// <summary>
        /// Create list of validation objects
        /// </summary>
        private void CreateValidationList(CoverArtResponse context)
        {
            validationList = new ValidationList{
                new CoverArtImagesValidation(context),
            };
        }
        /// <summary>
        /// Sends a request to Cover art archive and returns a coverArtResponse object.
        /// </summary>
        /// <param name="id"></param>
        public async Task<CoverArtResponse> GetAsync(string id)
        {  
            try
            {
                coverArtResponse = await GetResponseAsync<CoverArtResponse>(string.Format(coverArtUrl, id));

                CreateValidationList(coverArtResponse);
                validationList.Validate();
            }
            catch (ValidationException ve)
            {
                coverArtResponse = new CoverArtResponse(new Image("Images could not be found"));
                Debug.WriteLine(ve.Message);
            }
            catch(Exception e)
            {
                coverArtResponse = new CoverArtResponse(new Image("Images could not be found"));
                Debug.WriteLine(e.Message);
            }

            return coverArtResponse;
        }
    }
}