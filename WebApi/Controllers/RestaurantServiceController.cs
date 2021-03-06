﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Controllers.Core;
using LibBookingService.Dtos;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller to communicate with the restaurant service facade.
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [RoutePrefix("api/Restaurant")]
    public class RestaurantServiceController : ApiController
    {
        /// <summary>
        /// Property of type RestaurantServiceFacade that the controller endpoints use to access the facade.
        /// </summary>
        protected readonly Facades.RestaurantService.RestaurantServiceFacade _facade;

        RestaurantServiceController()
        {
            _facade = new Facades.RestaurantService.RestaurantServiceFacade();
        }

        /// <summary>
        /// Endpoint to get a list of restaurants.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Get")]
        public async Task<HttpResponseMessage> Get()
        {
            IEnumerable<Restaurant> restaurants = await _facade.GetRestaurants();

            if (restaurants.Any())
                return Request.CreateResponse(HttpStatusCode.OK, restaurants);

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Restaurants Found");
        }

        /// <summary>
        /// Endpoint to get a restaurant by id.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Get/{id:int?}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            Restaurant restaurant = await _facade.GetRestaurantById(id);

            if (restaurant != null)
                return Request.CreateResponse(HttpStatusCode.OK, restaurant);

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Restaurant Found For Id");
        }

        /// <summary>
        /// Endpoint to post a restaurant.
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Post")]
        public async Task<HttpResponseMessage> Post(Restaurant restaurant)
        {
            Restaurant newRestaurant = await _facade.PostRestaurant(restaurant);

            if (newRestaurant != null)
                return Request.CreateResponse(HttpStatusCode.OK, newRestaurant);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Saving");
        }

        /// <summary>
        /// Endpoint to update a restaurant.
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> Update(Restaurant restaurant)
        {
            Restaurant updatedRestaurant = await _facade.UpdateRestaurant(restaurant);

            if (updatedRestaurant != null)
                return Request.CreateResponse(HttpStatusCode.OK, updatedRestaurant);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Updating");
        }

        /// <summary>
        /// Endpoint to delete a restaurant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id:int?}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            bool res = await _facade.RemoveRestaurant(id);

            if (res)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Deleting");
        }

        /// <summary>
        /// Endpoint to add a menu item to a restaurant.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("AddMenuItem/{id:int?}/{menuItemId:int?}")]
        public async Task<HttpResponseMessage> AddMenuItem(int id, int menuItemId)
        {
            bool res = await _facade.AddMenuItem(id, menuItemId);

            if (res)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Updating");
        }

        /// <summary>
        /// Endpoint to remove a menu item from a restaurant.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveMenuItem/{id:int?}/{menuItemId:int?}")]
        public async Task<HttpResponseMessage> RemoveMenuItem(int id, int menuItemId)
        {
            bool res = await _facade.RemoveMenuItem(id, menuItemId);

            if (res)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Removing");
        }
    }
}
