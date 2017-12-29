using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;

namespace MusicHubAPI.ViewModels
{
    public class MusicalProjectModel : MusicalProject
    {
        public string confirmation_password { get; set; }

        internal MusicalProject Create()
        {
            MusicalProjectBusiness musicalProjectBusiness = new MusicalProjectBusiness();

            var retorno = musicalProjectBusiness.Create(this);

            return retorno;
        }
    }
}