using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public abstract class RestfulApiController : ApiController
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