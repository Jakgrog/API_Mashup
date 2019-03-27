using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiMashup.Models
{
    /// <summary>
    /// Holds album information.
    /// </summary>
    // TODO: A album should also constain information about its Artists. 
    // Use Aggregation relationship or monikers
    public struct Album
    {
        public readonly string Title;
        public readonly string Id;
        public readonly string Image;

        /// <summary>
        /// Constructor for creating Albums.
        /// </summary>
        /// <param name="title">The album title.</param>
        /// <param name="id">The Album ID, unique for every Album.</param>
        /// <param name="image">Link to the cover image.</param>
        public Album(string title, string id, string image)
        {
            Title = title;
            Id = id;
            Image = image;
        }
    }
    /// <summary>
    /// Artist information
    /// </summary>
    public class Artist : IMashupModel
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
        public Artist(string mbid, string description, IEnumerable<Album> albums)
        {
            Mbid = mbid;
            Description = description;
            Albums = albums;
        }

        public void PopulateModel()
        {
            
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
}