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
    public class MusicianController : RestfulApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [ActionName("")]
        public IHttpActionResult Create([FromBody] MusicianModel model)
        {
            Musician musician = null;
            try
            {
                if (model.password != model.confirmation_password) throw new Exception("Senhas não coincidem");

                musician = model.Create();
            }
            catch(ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", musician);
        }


    }
}