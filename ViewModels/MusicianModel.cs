using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;

namespace MusicHubAPI.ViewModels
{
    public class MusicianModel:Musician
    {
        public string confirmation_password { get; set; }

        internal Musician Create()
        {
            MusicianBusiness musicianBusiness = new MusicianBusiness();

            var retorno = musicianBusiness.Create(this);

            return retorno;
        }
    }
}