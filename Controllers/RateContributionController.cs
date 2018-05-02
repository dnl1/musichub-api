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
    public class RateContributionController : RestfulApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody] RateContributionModel rate_musician)
        {
            RateContribution rateContribution = null;
            try
            {
                rateContribution = rate_musician.Create();
            }
            catch(ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", rateContribution);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] int id)
        {
            RateContribution rateContribution = null;
            RateContributionModel rateContributionModel = new RateContributionModel();
            try
            {
                rateContribution = rateContributionModel.GetByUserAndProjectId(musicalProjectId: id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(rateContribution);
        }
    }
}