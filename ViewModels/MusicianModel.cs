using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHubBusiness;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;

namespace MusicHubAPI.ViewModels
{
    public class MusicianModel:Musician
    {
        public string confirmation_password { get; set; }

        internal Musician Create()
        {
            if (password != confirmation_password) throw new ValidateException("Senhas não coincidem");

            MusicianBusiness musicianBusiness = new MusicianBusiness();

            var retorno = musicianBusiness.Create(this);

            return retorno;
        }

        internal IEnumerable<Musician> SearchByName(string name)
        {
            MusicianBusiness musicianBusiness = new MusicianBusiness();
            IEnumerable<Musician> retorno = musicianBusiness.SearchByName(name);

            return retorno;
        }
    }
}