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
        [Route("musicalproject/create")]
        public IHttpActionResult Create([FromBody] MusicalProjectModel musical_project)
        {
            MusicalProject musicalProject = null;
            try
            {
                musicalProject = musical_project.Create();
            }
            catch (ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", musicalProject);
        }

        [HttpGet]
        [Route("musicalproject/my-projects")]
        public IHttpActionResult MyProjects()
        {
            IEnumerable<MusicalProject> projects = new List<MusicalProject>();

            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                projects = model.MyProjects();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(projects);
        }

        public struct SearchByMusicalGenreObj {
            public int musical_genre_id { get; set; }
        }

        [HttpPost]
        [Route("musicalproject/search-by-musical-genre")]
        public IHttpActionResult SearchByMusicalGenre([FromBody] SearchByMusicalGenreObj obj)
        {
            IEnumerable<MusicalProject> projects = new List<MusicalProject>();

            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                projects = model.SearchByMusicalGenre(obj.musical_genre_id);
            }
            catch (ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(projects);
        }

        [HttpGet]
        public IHttpActionResult Musicians(int id)
        {
            IEnumerable<Musician> musicians = null;
            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                musicians = model.Musicians(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(musicians);
        }
    }
}