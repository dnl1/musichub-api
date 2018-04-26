using MusicHubAPI.ViewModels;
using MusicHubBusiness;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class MusicianController : RestfulApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Create([FromBody] MusicianModel model)
        {
            Musician musician = null;
            try
            {
                musician = model.Create();
            }
            catch (ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", musician);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            IEnumerable<Musician> musicians = null;
            MusicianModel model = new MusicianModel();
            try
            {
                musicians = model.GetAll();
            }
            catch (ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(musicians);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] int id)
        {
            Musician musician = null;
            MusicianModel model = new MusicianModel();
            try
            {
                musician = model.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(musician);
        }

        [HttpPut]
        public IHttpActionResult Update([FromUri] int id, [FromBody] MusicianModel model)
        {
            Musician musician = null;
            try
            {
                musician = model.Update(id);
            }
            catch (ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(musician);
        }

        [HttpPost]
        public IHttpActionResult SearchByName(string name)
        {
            IEnumerable<Musician> musicians = null;
            try
            {
                MusicianModel model = new MusicianModel();
                musicians = model.SearchByName(name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", musicians);
        }
    }
}