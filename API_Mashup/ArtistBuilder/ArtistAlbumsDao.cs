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
        /// Sends a request to Cover art archive and generates
        /// an Album object with the album title, mbid and cover images.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        private async Task<Album> GetAlbumAsync(string id, string title)
        {
            Image[] albumImages;
            
            try
            {
                coverArtResponse = await GetResponseAsync<CoverArtResponse>(string.Format(coverArtUrl, id));
                albumImages = coverArtResponse?.Images;

                CreateValidationList(coverArtResponse);
                validationList.Validate();
            }
            catch (ValidationException ve)
            {
                albumImages = new Image[] { new Image("Images could not be found") };
                Debug.WriteLine(ve.Message);
            }
            catch(Exception e)
            {
                albumImages = new Image[] { new Image("Images could not be found") };
                Debug.WriteLine(e.Message);
            }

            return new Album(title, id, albumImages);
        }

        /// <summary>
        /// Runs GetAlbumAsync for all release groups. Returns an array of Albums.
        /// </summary>
        /// <param name="releaseGroups"></param>
        /// <returns></returns>
        public async Task<Album[]> GetAsync(IList<ReleaseGroups> releaseGroups)
        {
            return await Task.WhenAll(releaseGroups.Select(x => GetAlbumAsync(x.Id, x.Title)));
        }
    }
}