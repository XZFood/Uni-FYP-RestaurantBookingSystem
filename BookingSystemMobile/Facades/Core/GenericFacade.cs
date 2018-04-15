﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LibBookingService;
using Newtonsoft.Json;

namespace BookingSystemMobile.Facades.Core
{
    public class GenericFacade
    {
        /// <summary>
        /// Http client used to send http requests.
        /// </summary>
        //protected readonly HttpClient _client;

        /// <summary>
        /// Json serializer settings used to define how received data is serialized.
        /// </summary>
        protected JsonSerializerSettings _serializerSettings;

        /// <summary>
        /// Readonly string which defines the base url for the web api.
        /// </summary>
        protected readonly string _baseUrl = "http://192.168.148.121:64577/api/";

        /// <summary>
        /// Default constructor that sets up the HttpClient and JsonSerializerSettings.
        /// </summary>
        public GenericFacade(string apiController)
        {
            //_client = new HttpClient();
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
            _baseUrl += apiController;
        }

        /// <summary>
        /// Task to execute a HttpRequestMessage and return a single model. Uses T type parameter to declare the return type of the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async Task<T> ExecuteRequest<T>(HttpRequestMessage request) where T : Dto
        {
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content, _serializerSettings);
            }
        }

        /// <summary>
        /// Task to execute a HttpRequestMessage and return a list of models. Uses T type parameter to declare the return type of the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> ExecuteRequestList<T>(HttpRequestMessage request) where T : Dto
        {
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(content, _serializerSettings);
            }
        }

        /// <summary>
        /// Async task to remove a model from the database.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected async Task<bool> ExecuteRemove(Uri uri)
        {
            using (HttpClient _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = uri
                    };

                    HttpResponseMessage response = await _client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}