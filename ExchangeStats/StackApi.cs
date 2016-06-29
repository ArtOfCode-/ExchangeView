using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

using ExchangeStats.Contracts;

namespace ExchangeStats
{
    class StackApi
    {
        /// <summary>
        /// A request key for the Stack Exchange API; see https://api.stackexchange.com/docs.
        /// </summary>
        public static string RequestKey { get; set; }

        /// <summary>
        /// Creates a fully-qualified request URI for the Stack Exchange API based on the request path given.
        /// </summary>
        /// <param name="slug">The API path that should be included in the URI.</param>
        /// <returns>A (string) URI.</returns>
        public static string CreateRequestUri(string slug)
        {
            return CreateRequestUri(slug, new Dictionary<string, string>());
        }

        /// <summary>
        /// Creates a fully-qualified request URI for the Stack Exchange API based on the request path and parameters given.
        /// </summary>
        /// <param name="slug">The API path that should be included in the URI.</param>
        /// <param name="parameters">Additional parameters to be applied to the request.</param>
        /// <returns>A (string) URI.</returns>
        public static string CreateRequestUri(string slug, Dictionary<string, string> parameters)
        {
            string baseRequest = string.Format("https://api.stackexchange.com/2.2/{0}?", slug);

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                if (baseRequest.Contains("{" + parameter.Key + "}"))
                {
                    baseRequest = baseRequest.Replace("{" + parameter.Key + "}", parameter.Value);
                }
                else
                {
                    baseRequest += string.Format("{0}={1}&", parameter.Key, parameter.Value);
                }
            }

            if (!string.IsNullOrEmpty(RequestKey))
            {
                baseRequest += string.Format("key={0}", RequestKey);
            }

            return baseRequest;
        }

        /// <summary>
        /// Given a raw string response from the API, serializes it into a Response-type object.
        /// </summary>
        /// <typeparam name="T">The type of response expected from this API request. Must derive from Response.</typeparam>
        /// <param name="requestUri">The fully-qualified URI to request from.</param>
        /// <returns>A Response object containing the data returned from the API.</returns>
        public static T SerializeRawResponse<T>(string responseContent) where T : Response
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
            object responseObject = jsonSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(responseContent)));
            return responseObject as T;
        }

        /// <summary>
        /// Sends an HTTP request to the Stack Exchange API and returns the raw string without any additional parsing.
        /// </summary>
        /// <param name="requestUri">The fully-qualified URI to request from.</param>
        /// <returns>A raw string containing the data from the API.</returns>
        public static string RequestRawContent(string requestUri)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUri) as HttpWebRequest;
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(string.Format("API request failed with code {0}: {1}", response.StatusCode, response.StatusDescription));
                    }

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        Encoding responseEncoding;
                        if (string.IsNullOrEmpty(response.CharacterSet))
                        {
                            responseEncoding = Encoding.Unicode;
                        }
                        else
                        {
                            responseEncoding = Encoding.GetEncoding(response.CharacterSet);
                        }

                        StreamReader reader = new StreamReader(responseStream, responseEncoding);
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Sends an HTTP request to the Stack Exchange API and returns the response parsed into a Response-type DataContract.
        /// </summary>
        /// <typeparam name="T">The type of response expected from this API request. Must derive from Response.</typeparam>
        /// <param name="requestUri">The fully-qualified URI to request from.</param>
        /// <returns>A Response object containing the data returned from the API.</returns>
        public static T FireRequest<T>(string requestUri) where T : Response
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUri) as HttpWebRequest;
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(string.Format("API request failed with code {0}: {1}", response.StatusCode, response.StatusDescription));
                    }
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                    object responseObject = jsonSerializer.ReadObject(response.GetResponseStream());
                    return responseObject as T;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Sends an HTTP request to the Stack Exchange API and returns the response parsed into a Response-type DataContract.
        /// Additionally caches the response for <code>cacheExpiration</code> seconds.
        /// </summary>
        /// <typeparam name="T">The type of response expected from this API request. Must derive from Response.</typeparam>
        /// <param name="requestUri">The fully-qualified URI to request from.</param>
        /// <returns>A Response object containing the data returned from the API.</returns>
        public static T FireCacheableRequest<T>(string requestUri, int cacheExpiration) where T : Response
        {
            if (ApiCache.HasValidItem(requestUri))
            {
                Stream cacheStream = ApiCache.GetDataStream(requestUri);
                if (cacheStream != null)
                {
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                    object responseObject = jsonSerializer.ReadObject(cacheStream);
                    return responseObject as T;
                }
            }

            string responseData = RequestRawContent(requestUri);
            ApiCache.AddCacheItem(requestUri, cacheExpiration, responseData);
            return SerializeRawResponse<T>(responseData);
        }
    }
}
