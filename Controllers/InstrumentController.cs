using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class InstrumentController : RestfulApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            IEnumerable<Instrument> retorno = null;

            try
            {
                InstrumentBusiness instrumentBusiness = new InstrumentBusiness();
                retorno = instrumentBusiness.GetAll();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(retorno);
        }
    }
}