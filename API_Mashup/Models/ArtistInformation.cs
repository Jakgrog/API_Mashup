using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Mashup
{
    /// <summary>
    /// Artist information
    /// </summary>
    public class ArtistInformation
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ArtistInformation()
        {
        }

        /// <summary>
        /// Constructor for creating entries.
        /// </summary>
        /// <param name="mbid">The mbid number that identifies the Artist.</param>
        public ArtistInformation(string mbid, string description, IEnumerable<Album> albums)
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
        public IEnumerable<Album> Albums { get; set; }
    }

    /// <summary>
    /// Holds album information.
    /// </summary>
    public struct Album
    {
        public readonly string Title;
        public readonly string Id;
        public readonly string Image;

        public Album(string title, string id, string image)
        {
            Title = title;
            Id = id;
            Image = image;
        }
    }
}