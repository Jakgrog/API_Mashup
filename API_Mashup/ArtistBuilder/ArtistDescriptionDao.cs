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
        private void CreateValidationList(WikidataResponse wikiDataContext, WikipediaResponse wikipediaContext)
        {
            validationList.Add(new WikidataUrlValidation(wikiDataContext));
        }
        public async Task<WikipediaResponse> GetAsync(string id)
        {
            try
            {
                WikidataResponse wikiData = await GetResponseAsync<WikidataResponse>(string.Format(wikidataUrl, id));
                description = await GetResponseAsync<WikipediaResponse>(string.Format(wikipediaUrl, wikiData.GetWikipediaID()));

                // Add validation objects that you want to validate to the validation list
                CreateValidationList(wikiData, description);

                // Validate all validation objects
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

            return description;
        }
    }

}