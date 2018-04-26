using MusicHubAPI.ViewModels;
using MusicHubBusiness;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        [Route("musicalproject/{id}")]
        public IHttpActionResult Get([FromUri] int id)
        {
            MusicalProject project = null;

            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                project = model.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(project);
        }

        [HttpPost]
        [Route("musicalproject/{id}/finish")]
        public IHttpActionResult Finish([FromUri] int id)
        {
            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                model.Finish(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { ok = true});
        }

        [Route("musicalproject/{id}/download")]
        [HttpGet]
        public IHttpActionResult Download([FromUri] int id)
        {
            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                MemoryStream stream = model.Download(id);

                var content = new StreamContent(stream);
                content.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg3");
                content.Headers.ContentLength = stream.GetBuffer().Length;
                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("musicalproject/{musicalProjectId}/instruments")]
        public IHttpActionResult Instruments(int musicalProjectId)
        {
            IEnumerable<MusicalProjectInstrument> instruments = null;

            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                instruments = model.Instruments(musicalProjectId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(instruments);
        }

        [HttpGet]
        [Route("musicalproject/{musicalProjectId}/contributions")]
        public IHttpActionResult Contributions(int musicalProjectId)
        {
            IEnumerable<Contribution> contributions = null;

            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                contributions = model.Contributions(musicalProjectId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(contributions);
        }

        [HttpGet]
        [Route("musicalproject/{musicalProjectId}/contributions/{instrumentId}")]
        public IHttpActionResult Contributions(int musicalProjectId, int instrumentId)
        {
            IEnumerable<Contribution> contributions = null;

            try
            {
                MusicalProjectModel model = new MusicalProjectModel();
                contributions = model.Contributions(musicalProjectId, instrumentId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(contributions);
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
        [Route("musicalproject/{id}/musicians")]
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