﻿using System;
using System.Linq;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;
using ApiMashup.Models;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using ApiMashup.Validation;

namespace ApiMashup.DAO
{
    public abstract class ArtistDao
    {
        protected readonly string musicBrainzUrl;
        protected readonly string coverArtUrl;
        protected readonly string wikipediaUrl;
        protected readonly string wikidataUrl;

        // A Client handler used if decompression of a response is needed
        protected static HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        // A reuseable http client for connection pooling.
        protected static HttpClient client = new HttpClient(handler);

        protected readonly ValidationList validationList;

        protected bool IsValid()
        {
            validationList.Validate();
            return validationList.IsValid;
        }

        protected string ErrorMessage() => validationList.Messages.ToString();

        /// <summary>
        /// A generic function that sends a request to a specific url
        /// and stores it as a IResponse object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        protected async Task<T> GetResponseAsync<T>(string url) where T : IResponse
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            string product = await response.Content.ReadAsStringAsync();
            T responseObject = JsonConvert.DeserializeObject<T>(product);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return responseObject;
        }

        /// <summary>
        /// ArtistDao constructor, reads the Url templates located in
        /// the web.config file and stores them as readonly strings.
        /// </summary>
        // TODO: Move to subclasses 
        public ArtistDao()
        {
            NameValueCollection settings =
                ConfigurationManager.AppSettings;

            validationList = new ValidationList();

            if (settings != null)
            {
                musicBrainzUrl = settings["musicbrainz"].ToString();
                coverArtUrl = settings["coverart"].ToString();
                wikidataUrl = settings["wikidata"].ToString();
                wikipediaUrl = settings["wikipedia"].ToString();
            }
        }
    }

    /// <summary>
    /// Sends a request to Music brainz
    /// </summary>
    /// <param name="mbid"></param>
    public class MusicBrainzDao : ArtistDao
    {
        private MusicBrainzResponse musicBrainsResponse;

        private void CreateValidationList(MusicBrainzResponse context)
        {
            validationList.Add(new MusicBrainzRelationsValidation(context));
        }

        public async Task<MusicBrainzResponse> GetAsync(string mbid)
        {
            try
            {
                musicBrainsResponse = await 
                    GetResponseAsync<MusicBrainzResponse>(string.Format(musicBrainzUrl, mbid));

                CreateValidationList(musicBrainsResponse);

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

            return musicBrainsResponse;
        }
    }

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