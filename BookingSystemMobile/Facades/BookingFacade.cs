﻿using BookingSystemMobile.Facades.Core;
using LibBookingService.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookingSystemMobile.Facades
{
    public class BookingFacade : GenericFacade
    {
        /// <summary>
        /// Default controller that sets the api controller used by the facade.
        /// </summary>
        public BookingFacade() : base("Booking/")
        {
        }

        /// <summary>
        /// Returns an IEnumerable of bookings from the web api.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Booking>> Get()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "Get")
                };

                IEnumerable<Booking> res = await ExecuteRequestList<Booking>(request);

                return res.Any()
                    ? res.ToList()
                    : new List<Booking>();
            }
            catch (Exception ex)
            {
                return new List<Booking>();
            }
        }

        /// <summary>
        /// Returns a booking model by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Booking> FindById(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "Get/" + id)
                };

                return await ExecuteRequest<Booking>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns an IEnumerable of bookings by customer id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Booking>> FindByCustomerId(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "GetByCustomer/" + id)
                };

                IEnumerable<Booking> res = await ExecuteRequestList<Booking>(request);

                return res.Any()
                    ? res.ToList()
                    : new List<Booking>();
            }
            catch (Exception ex)
            {
                return new List<Booking>();
            }
        }

        /// <summary>
        /// Returns an available table.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Table> GetAvailableTable(Booking model)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_baseUrl + "GetAvailableTable"),
                    Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                };

                return await ExecuteRequest<Table>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Posts a booking model to the web api and returns the saved model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Booking> Create(Booking model)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_baseUrl + "Post"),
                    Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                };

                return await ExecuteRequest<Booking>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Puts a booking model to the web api and returns the updated model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Booking> Update(Booking model)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(_baseUrl + "Update/" + model.Id),
                    Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                };

                return await ExecuteRequest<Booking>(request);
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

        /// <summary>
        /// Sends a cancel request to the web api and returns true if successful.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Cancel(int id)
        {
            return await ExecuteRemove(new Uri(_baseUrl + "Cancel/" + id));
        }
    }
}