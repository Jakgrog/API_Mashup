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

        private void CreateValidationList(MusicBrainzResponse context)
        {
            validationList = new ValidationList{
                new MusicBrainzRelationsValidation(context),
                new MusicBrainzWikipediaValidation(context),
                new MusicBrainzReleaseGroupsValidation(context)
            };
        }

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