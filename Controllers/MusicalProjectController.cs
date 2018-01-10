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
    public class MusicalProjectController : RestfulApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody] MusicalProjectModel musical_project)
        {
            MusicalProject musicalProject = null;
            try
            {
                musicalProject = musical_project.Create();
            }
            catch(ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", musicalProject);
        }
    }
}