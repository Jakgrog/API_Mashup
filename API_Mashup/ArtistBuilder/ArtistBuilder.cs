using ApiMashup.Models;
using System.Threading.Tasks;
using ApiMashup.ArtistBuilder;
using System.Linq;


namespace ApiMashup.ArtistBuilder
{
    /// <summary>
    /// Uses DAO:s to receive information from the different API:s 
    /// and agregates it into an Artist object.
    /// </summary>
    public interface IArtistBuilder
    {
        Task<Artist> RunGetArtistAsync(string mbid);
    }
    /// <summary>
    /// Implements IArtistBuilder
    /// </summary>
    public class ArtistBuilderObject : IArtistBuilder
    {
        /// <summary>
        /// Creates an Album from a cover art response.
        /// </summary>
        /// <param name="releaseGroups"></param>
        /// <returns></returns>
        private async Task<Album> CreateAlbumAsync(string id, string title)
        {
            CoverArtResponse coverArtResponse = await new ArtistAlbumsDao().GetAsync(id);

            return new Album(title, id, coverArtResponse.Images);
        }
        /// <summary>
        /// Creates an Artist object from the information 
        /// retrieved from the different API:s. 
        /// </summary>
        /// <param name="mbid"></param>
        public async Task<Artist> RunGetArtistAsync(string mbid)
        {
            MusicBrainzResponse musicBrainz;
            string description;
            Album[] albums;

            // Sends request to the API:s by calling the Dao:s GetAsync functions. 
            musicBrainz = await new MusicBrainzDao().GetAsync(mbid);
            description = (await new ArtistDescriptionDao().
                GetAsync(musicBrainz.GetWikidataID())).GetDescriptionPage();
            albums = await Task.WhenAll(musicBrainz.ReleaseGroups.
                Select(x => CreateAlbumAsync(x.Id, x.Title)));

            return new Artist(mbid, description, albums);
        }
    }
}