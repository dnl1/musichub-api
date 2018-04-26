using MusicHubAPI.ViewModels;
using MusicHubBusiness;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class RateMusicianController : RestfulApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody] RateMusicianModel rate_musician)
        {
            RateMusician rateMusician = null;
            try
            {
                rateMusician = rate_musician.Create();
            }
            catch(ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", rateMusician);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] int id)
        {
            RateMusician rateMusician = null;
            RateMusicianModel rateMusicianModel = new RateMusicianModel();
            try
            {
                rateMusician = rateMusicianModel.GetByOwnerId(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(rateMusician);
        }
    }
}