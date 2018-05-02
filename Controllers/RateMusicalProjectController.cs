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
    public class RateMusicalProjectController : RestfulApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody] RateMusicalProjectModel rate_musical_project)
        {
            RateMusicalProject rateMusicalProject = null;
            try
            {
                rateMusicalProject = rate_musical_project.Create();
            }
            catch(ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", rateMusicalProject);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] int id)
        {
            RateMusicalProject rateMusicalProject = null;
            RateMusicalProjectModel rateMusicianModel = new RateMusicalProjectModel();
            try
            {
                rateMusicalProject = rateMusicianModel.GetByUserAndProjectId(musicalProjectId: id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(rateMusicalProject);
        }
    }
}