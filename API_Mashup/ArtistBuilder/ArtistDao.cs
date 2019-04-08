﻿using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;
using ApiMashup.Models;
using Newtonsoft.Json;
using System.Net;
using ApiMashup.Validation;

namespace ApiMashup.ArtistBuilder
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

        protected ValidationList validationList;

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

            if (settings != null)
            {
                musicBrainzUrl = settings["musicbrainz"].ToString();
                coverArtUrl = settings["coverart"].ToString();
                wikidataUrl = settings["wikidata"].ToString();
                wikipediaUrl = settings["wikipedia"].ToString();
            }
        }
    }
}
