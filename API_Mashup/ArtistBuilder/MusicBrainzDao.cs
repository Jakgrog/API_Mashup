using System;
using System.Threading.Tasks;
using System.Diagnostics;
using ApiMashup.Validation;
using ApiMashup.Models;

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
            validationList.Add(new MusicBrainzRelationsValidation(context));
        }

        public async Task<MusicBrainzResponse> GetAsync(string mbid)
        {
            try
            {
                musicBrainsResponse = await
                    GetResponseAsync<MusicBrainzResponse>(string.Format(musicBrainzUrl, mbid));

                CreateValidationList(musicBrainsResponse);

                if (!IsValid())
                {
                    throw new Exception(ErrorMessage());
                }
            }
            catch (Exception we)
            {
                Debug.WriteLine(we.Message);
                throw;
            }

            return musicBrainsResponse;
        }
    }
}