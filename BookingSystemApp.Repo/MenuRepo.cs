﻿using LibBookingService.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemApp.Repo.Core
{
    public class MenuRepo : GenericRepo, IRepository<MenuItem>
    {
        /// <summary>
        /// Default controller that sets the api controller used by the repo.
        /// </summary>
        public MenuRepo() : base("Menu/")
        {
        }

        /// <summary>
        /// Returns an IEnumerable of menu items from the web api.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MenuItem>> Get()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "Get")
                };

                IEnumerable<MenuItem> res = await ExecuteRequestAsyncList<MenuItem>(request);

                return res.Any()
                    ? res
                    : Enumerable.Empty<MenuItem>();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<MenuItem>();
            }
        }

        /// <summary>
        /// Returns a menu item model by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuItem> FindById(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "Get/" + id)
                };

                return await ExecuteRequestAsync<MenuItem>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Posts a menu item model to the web api and returns the saved model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MenuItem> Create(MenuItem model)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_baseUrl + "Create"),
                    Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                };

                return await ExecuteRequestAsync<MenuItem>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Puts a menu item model to the web api and returns the updated model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MenuItem> Update(MenuItem model)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(_baseUrl + "Update/" + model.Id),
                    Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                };

                return await ExecuteRequestAsync<MenuItem>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Sends a delete request to the web api and returns true if successful.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            return await ExecuteRemove(new Uri(_baseUrl + "Delete/" + id));
        }
    }
}
