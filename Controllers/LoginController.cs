using BearerAuthentication;
using MusicHubAPI.ViewModels;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class LoginController : RestfulApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Index(LoginModel model)
        {
            try
            {
                MusicianBusiness musicianBusiness = new MusicianBusiness();

                Musician user = musicianBusiness.Login(model.email, model.password);

                if (user == null) return Unauthorized();

                BearerToken bearerLogin = new BearerToken(new BearerDatabaseManager(model.email));
                bearerLogin.GenerateHeaderToken(user.id.ToString(), user.email);

                return Ok(user);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Teste()
        {
            return Ok();
        }
    }
}