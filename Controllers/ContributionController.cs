using MusicHubAPI.ViewModels;
using MusicHubBusiness;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class ContributionController : RestfulApiController
    {
        [HttpPost]
        public IHttpActionResult Create(ContributionModel contributionModel)
        {
            Contribution contribution = null;

            try
            {
                contribution = contributionModel.Create();
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

        [Route("contribution/{id}/download")]
        [HttpGet]
        public HttpResponseMessage Download([FromUri] int id)
        {
            HttpResponseMessage result = null;

            try
            {
                ContributionModel model = new ContributionModel();
                MemoryStream stream = model.Download(id);

                var content = new StreamContent(stream);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Headers.ContentLength = stream.GetBuffer().Length;

                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = content
                };
            }
            catch (Exception ex)
            {
                result = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return result;
        }

        [HttpPost]
        [Route("contribution/{contributionId}/approve")]
        public IHttpActionResult Approve([FromUri] int contributionId)
        {
            ContributionModel contributionModel = new ContributionModel();

            try
            {
                contributionModel.Approve(contributionId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { ok = true });
        }

        [HttpGet]
        public IHttpActionResult FreeContributions(int id)
        {
            IEnumerable<Contribution> contributions = null;
            ContributionModel model = new ContributionModel();

            try
            {
                contributions = model.GetFreeContributions(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(contributions);
        }
    }
}