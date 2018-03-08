using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class MusicalGenreController : RestfulApiController
    {
        // GET: MusicalGenre
        [HttpPost]
        [Route("musicalgenre")]
        public IHttpActionResult GetAll()
        {
            IEnumerable<MusicalGenre> retorno = null;

            try
            {
                MusicalGenreBusiness musicalGenreBusiness = new MusicalGenreBusiness();
                retorno = musicalGenreBusiness.GetAll();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(retorno);
        }
    }
}