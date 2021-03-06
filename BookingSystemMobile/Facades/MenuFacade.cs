﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BookingSystemMobile.Facades.Core;
using LibBookingService.Dtos;

namespace BookingSystemMobile.Facades
{
    class MenuFacade : GenericFacade
    {
        /// <summary>
        /// Default controller that sets the api controller used by the facade.
        /// </summary>
        public MenuFacade() : base("Menu/")
        {
        }

        /// <summary>
        /// Returns an IEnumerable of menu items from the web api.
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuItem>> Get()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "Get")
                };

                IEnumerable<MenuItem> res = await ExecuteRequestList<MenuItem>(request);

                return res.Any() 
                    ? res .ToList()
                    : new List<MenuItem>();
            }
            catch (Exception ex)
            {
                return new List<MenuItem>();
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

                return await ExecuteRequest<MenuItem>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns an IEnumerable of menu items by restaurant id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<MenuItem>> FindByRestaurantId(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_baseUrl + "GetByRestaurant/" + id)
                };

                IEnumerable<MenuItem> res = await ExecuteRequestList<MenuItem>(request);

                return res.Any()
                    ? res.ToList()
                    : new List<MenuItem>();
            }
            catch (Exception ex)
            {
                return new List<MenuItem>();
            }
        }
    }
}