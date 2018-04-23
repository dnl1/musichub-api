using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

                int[] available_instruments = new int[] { 1, 5 };

                retorno = retorno.Where(i => available_instruments.Contains(i.id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(retorno);
        }
    }
}