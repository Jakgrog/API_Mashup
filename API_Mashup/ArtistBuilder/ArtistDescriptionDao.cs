using System;
using System.Threading.Tasks;
using System.Diagnostics;
using ApiMashup.Validation;
using ApiMashup.Models;

namespace ApiMashup.ArtistBuilder
{
    /// <summary>
    /// Sends a request to Wikidata, uses the response from wikidata
    /// to retriev the artist description from wikipedia. This is because
    /// there are not always a relation between Music Brainz and wikipedia.
    /// </summary>
    /// <param name="id"></param>
    public class ArtistDescriptionDao : ArtistDao
    {
        private WikipediaResponse description;

        /// <summary>
        /// Create list of validation objects
        /// </summary>
        private void CreateValidationList(WikidataResponse wikiDataContext, WikipediaResponse wikipediaContext)
        {
            validationList = new ValidationList{
                new WikidataUrlValidation(wikiDataContext),
                new WikipediaPagesExtractValidation(wikipediaContext)
            };
        }

        /// <summary>
        /// Sends a request to wikidata to recieve a wikipedia ID.
        /// The wikipedia ID is then used to request the artist 
        /// description from wikipedia.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WikipediaResponse> GetAsync(string id)
        {
            try
            {
                WikidataResponse wikiData = await GetResponseAsync<WikidataResponse>(string.Format(wikidataUrl, id));
                description = await GetResponseAsync<WikipediaResponse>(string.Format(wikipediaUrl, wikiData.GetWikipediaID()));

                // Add validation objects that you want to validate to the validation list
                CreateValidationList(wikiData, description);
                validationList.Validate();
            }
            catch (ValidationException we)
            {
                description = new WikipediaResponse();
                Debug.WriteLine(we.Message);
            }
            catch (Exception e)
            {
                throw new Exception("An error occured when requesting data from Wikipedia or wikidata, "
                    , e.InnerException);
            }

            return description;
        }
    }

}