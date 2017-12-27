using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MusicHubAPI.Controllers
{
    public class MusicianController : ApiController
    {
        public IHttpActionResult Index()
        {
            return Ok();
        }

    }
}