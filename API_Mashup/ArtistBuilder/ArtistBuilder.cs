using ApiMashup.DAO;
using System.Threading.Tasks;

namespace ApiMashup.Models
{
    /// <summary>
    /// Fetches information from the different API:s and agregates it
    /// into an Artist object.
    /// </summary>
    public interface IArtistBuilder
    {
        Task<Artist> RunGetArtistAsync(string mbid);
    }
    /// <summary>
    /// Implements IArtistDao
    /// </summary>
    public class ArtistBuilder : IArtistBuilder
    {
        /// <summary>
        /// Creates an Artist object from the information 
        /// retrieved from the different API:s. 
        /// </summary>
        /// <param name="mbid"></param>
        public async Task<Artist> RunGetArtistAsync(string mbid)
        {
            WikipediaResponse description = null;
            Album[] albums = null;
            MusicBrainzResponse musicBrainz;

            musicBrainz = await new MusicBrainzDao().GetAsync(mbid);
            description = await new ArtistDescriptionDao().GetAsync(musicBrainz.GetWikidataID());
            albums = await new ArtistAlbumsDao().GetAsync(musicBrainz.ReleaseGroups);

            return new Artist(mbid, description.GetDescriptionPage(), albums);
        }
    }
}