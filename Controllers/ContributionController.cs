using MusicHubAPI.ViewModels;
using MusicHubBusiness;
using MusicHubBusiness.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class ContributionController : RestfulApiController
    {
        [HttpPost]
        public IHttpActionResult Create(ContributionModel contributionModel)
        {
            Contribution contribution = null;

            try
            {
                contribution = contributionModel.Create();
            }
            catch (ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", contribution);
        }

        [HttpGet]
        public IHttpActionResult FreeContributions(int id)
        {
            IEnumerable<Contribution> contributions = null;
            ContributionModel model = new ContributionModel();

            try
            {
                contributions = model.GetFreeContributions(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(contributions);
        }
    }
}