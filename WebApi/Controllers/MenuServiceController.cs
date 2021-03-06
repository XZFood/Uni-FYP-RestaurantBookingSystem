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
    /// Controller to communicate with the menu service facade.
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [RoutePrefix("api/Menu")]
    public class MenuServiceController : ApiController
    {
        /// <summary>
        /// Property of type MenuServiceFacade that the controller endpoints use to access the facade.
        /// </summary>
        protected readonly Facades.MenuService.MenuServiceFacade _facade;

        MenuServiceController()
        {
            _facade = new Facades.MenuService.MenuServiceFacade();
        }

        /// <summary>
        /// Endpoint to get a list of menu items.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Get")]
        public async Task<HttpResponseMessage> Get()
        {
            IEnumerable<MenuItem> menuItems = await _facade.GetMenuItems();

            if (menuItems.Any())
                return Request.CreateResponse(HttpStatusCode.OK, menuItems);

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Menu Items Found");
        }

        /// <summary>
        /// Endpoint to get a menu item by id.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Get/{id:int?}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            MenuItem menuItem = await _facade.GetMenuItemById(id);

            if (menuItem != null)
                return Request.CreateResponse(HttpStatusCode.OK, menuItem);

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Menu Item Found For Id");
        }

        /// <summary>
        /// Endpoint to get menu items by restaurant id.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetByRestaurant/{id:int?}")]
        public async Task<HttpResponseMessage> GetByRestaurant(int id)
        {
            IEnumerable<MenuItem> menuItems = await _facade.GetMenuItemsByRestaurantId(id);

            if (menuItems.Any())
                return Request.CreateResponse(HttpStatusCode.OK, menuItems);

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Menu Items Found For Restaurant Id");
            
        }

        /// <summary>
        /// Endpoint to post a menu item.
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Post")]
        public async Task<HttpResponseMessage> Post(MenuItem menuItem)
        {
            MenuItem newMenuItem = await _facade.PostMenuItem(menuItem);

            if (newMenuItem != null)
                return Request.CreateResponse(HttpStatusCode.OK, newMenuItem);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Saving");
        }

        /// <summary>
        /// Endpoint to update a menu item.
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> Update(MenuItem menuItem)
        {
            MenuItem updatedMenuItem = await _facade.UpdateMenuItem(menuItem);

            if (updatedMenuItem != null)
                return Request.CreateResponse(HttpStatusCode.OK, updatedMenuItem);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Updating");
        }

        /// <summary>
        /// Endpoint to add a diet info to a menu item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dietInfoId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("AddDietInfo/{id:int?}/{dietInfoId:int?}")]
        public async Task<HttpResponseMessage> AddDietInfo(int id, int dietInfoId)
        {
            bool res = await _facade.AddDietInfo(id, dietInfoId);

            if (res)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Updating");
        }

        /// <summary>
        /// Endpoint to remove a diet info from a menu item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dietInfoId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveDietInfo/{id:int?}/{dietInfoId:int?}")]
        public async Task<HttpResponseMessage> RemoveDietInfo(int id, int dietInfoId)
        {
            bool res = await _facade.RemoveDietInfo(id, dietInfoId);

            if (res)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Removing");
        }

        /// <summary>
        /// Endpoint to add a category to a menu item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuItemTypeId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("AddMenuItemType/{id:int?}/{menuItemTypeId:int?}")]
        public async Task<HttpResponseMessage> AddMenuItemType(int id, int menuItemTypeId)
        {
            bool res = await _facade.AddMenuItemType(id, menuItemTypeId);

            if (res)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Updating");
        }

        /// <summary>
        /// Endpoint to remove a category from a menu item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuItemTypeId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveMenuItemType/{id:int?}/{menuItemTypeId:int?}")]
        public async Task<HttpResponseMessage> RemoveMenuItemType(int id, int menuItemTypeId)
        {
            bool res = await _facade.RemoveMenuItemType(id, menuItemTypeId);

            if (res)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Removing");
        }

        /// <summary>
        /// Endpoint to delete a menu item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id:int?}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            bool res = await _facade.RemoveMenuItem(id);

            if (res)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An Error Occured Whilst Deleting");
        }
    }
}
