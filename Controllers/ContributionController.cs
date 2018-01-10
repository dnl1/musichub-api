using MusicHubAPI.ViewModels;
using MusicHubBusiness;
using MusicHubBusiness.Models;
using System;
using System.Web;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class ContributionController : RestfulApiController
    {
        [HttpPost]
        public IHttpActionResult Create(ContributionModel contributionModel, bool baseInstrument = false)
        {
            Contribution contribution = null;

            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count < 1)
                {
                    return BadRequest();
                }

                var postedFile = httpRequest.Files[0];

                contribution = contributionModel.Create(baseInstrument, postedFile);
            }
            catch (ValidateException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("api", contribution);
        }
    }
}