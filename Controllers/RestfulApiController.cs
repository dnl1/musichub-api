using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MusicHubAPI.Controllers
{
    public class RestfulApiController : ApiController
    {
       public IHttpActionResult UnprocessableEntity(string message)
        {
            return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse(
                    (HttpStatusCode)422,
                    new HttpError(message)
                )
            );
        }
    }
}