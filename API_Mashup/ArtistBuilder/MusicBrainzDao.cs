using System.Threading.Tasks;
using System.Diagnostics;
using ApiMashup.Validation;
using ApiMashup.Models;
using System;

namespace ApiMashup.ArtistBuilder
{
    /// <summary>
    /// Sends a request to Music brainz
    /// </summary>
    /// <param name="mbid"></param>
    public class MusicBrainzDao : ArtistDao
    {
        private MusicBrainzResponse musicBrainsResponse;
        
        /// <summary>
        /// Create list of validation objects
        /// </summary>
        private void CreateValidationList(MusicBrainzResponse context)
        {
            validationList = new ValidationList{
                new MusicBrainzRelationsValidation(context),
                new MusicBrainzWikipediaValidation(context),
                new MusicBrainzReleaseGroupsValidation(context)
            };
        }

        /// <summary>
        /// Sends a request to music brainz API with the specific mbid.
        /// Stores release groups and relations in a music brainz response. 
        /// </summary>
        /// <param name="mbid"></param>
        /// <returns></returns>
        public async Task<MusicBrainzResponse> GetAsync(string mbid)
        {
            try
            {
                musicBrainsResponse = await
                    GetResponseAsync<MusicBrainzResponse>(string.Format(musicBrainzUrl, mbid));

                CreateValidationList(musicBrainsResponse);
                validationList.Validate();
            }
            catch (ValidationException ve)
            {
                Debug.WriteLine(ve.Message);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("An error occured when requesting data from MusicBrainz, " +
                    "please make sure that the mbid is valid"
                    , e.InnerException);
            }

            return musicBrainsResponse;
        }
    }
}