namespace ApiMashup.Models
{
    /// <summary>
    /// Contains album information.
    /// </summary>
    // TODO: A album should also constain information about its Artists. 
    public struct Album
    {
        public readonly string Title;
        public readonly string Id;
        public readonly Image[] Images;

        /// <summary>
        /// Constructor for creating Albums.
        /// </summary>
        /// <param name="title">The album title.</param>
        /// <param name="id">The Album ID, unique for every Album.</param>
        /// <param name="image">Link to the cover image.</param>
        public Album(string title, string id, Image[] images)
        {
            Title = title;
            Id = id;
            Images = images;
        }
    }
    /// <summary>
    /// Contains artist information
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Artist(string mbid)
        {
            Mbid = mbid;
        }

        /// <summary>
        /// Constructor for creating Artists.
        /// </summary>
        /// <param name="mbid">The mbid number that identifies the Artist.</param>
        public Artist(string mbid, string description, Album[] albums)
        {
            Mbid = mbid;
            Description = description;
            Albums = albums;
        }

        /// <summary>
        /// The mbid number that identifies the Artist.
        /// </summary>
        public string Mbid { get; set; }

        /// <summary>
        /// The Artist desciption.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Artist desciption.
        /// </summary>
        public Album[] Albums { get; set; }
    }
}
