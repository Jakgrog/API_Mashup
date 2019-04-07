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
        private void CreateValidationList(CoverArtResponse context)
        {
            validationList.Add(new CoverArtImagesValidation(context));
        }
        /// <summary>
        /// Sends a request to Cover art archive and generates
        /// an Album object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        private async Task<Album> GetAlbumAsync(string id, string title)
        {
            Image[] albumImages = new Image[] { new Image("No images found") };
            ;
            try
            {
                coverArtResponse = await GetResponseAsync<CoverArtResponse>(string.Format(coverArtUrl, id));
                albumImages = coverArtResponse?.Images;

                CreateValidationList(coverArtResponse);

                if (!IsValid())
                {
                    throw new Exception(ErrorMessage());
                }
            }
            catch (Exception we)
            {
                Debug.WriteLine(we.Message);
            }

            return new Album(title, id, albumImages);
        }

        public async Task<Album[]> GetAsync(IList<ReleaseGroups> releaseGroups)
        {
            return await Task.WhenAll(releaseGroups.Select(x => GetAlbumAsync(x.Id, x.Title)));
        }
    }
}