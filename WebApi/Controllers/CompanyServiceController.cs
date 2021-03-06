﻿using LibBookingService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller to communicate with the company service facade.
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/Company")]
    public class CompanyServiceController : ApiController
    {
        /// <summary>
        /// Property of type RestaurantServiceFacade that the controller endpoints use to access the facade.
        /// </summary>
        protected readonly Facades.RestaurantService.RestaurantServiceCompanyFacade _facade;

        CompanyServiceController()
        {
            _facade = new Facades.RestaurantService.RestaurantServiceCompanyFacade();
        }

        /// <summary>
        /// Endpoint to get the company.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<HttpResponseMessage> Get()
        {
            Company company = await _facade.GetCompany();

            if (company != null)
                return Request.CreateResponse(HttpStatusCode.OK, company);

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Restaurant Found For Id");
        }
    }
}
