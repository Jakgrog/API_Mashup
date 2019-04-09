using ApiMashup.Models;
using System.Threading.Tasks;
using ApiMashup.ArtistBuilder;

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
        /// Creates an Artist object from the information 
        /// retrieved from the different API:s. 
        /// </summary>
        /// <param name="mbid"></param>
        public async Task<Artist> RunGetArtistAsync(string mbid)
        {
            WikipediaResponse description;
            Album[] albums;
            MusicBrainzResponse musicBrainz;

            musicBrainz = await new MusicBrainzDao().GetAsync(mbid);
            description = await new ArtistDescriptionDao().GetAsync(musicBrainz.GetWikidataID());
            albums = await new ArtistAlbumsDao().GetAsync(musicBrainz.ReleaseGroups);

            return new Artist(mbid, description.GetDescriptionPage(), albums);
        }
    }
}