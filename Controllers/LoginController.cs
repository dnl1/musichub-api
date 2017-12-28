using BearerAuthentication;
using MusicHubAPI.ViewModels;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class LoginController : RestfulApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Index(LoginModel model)
        {
            MusicianBusiness musicianBusiness = new MusicianBusiness();

            Musician user = musicianBusiness.Login(model.email, model.password);

            if (user == null) return Unauthorized();

            BearerToken bearerLogin = new BearerToken();
            bearerLogin.GenerateHeaderToken(user.id.ToString(), user.email);

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult TestMe()
        {
            return Ok();
        }
    }
}