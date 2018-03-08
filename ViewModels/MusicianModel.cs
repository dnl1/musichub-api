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
        public MusicianModel()
        {
            musicianBusiness = new MusicianBusiness();
        }

        private MusicianBusiness musicianBusiness;
        public string confirmation_password { get; set; }

        internal Musician Create()
        {
            if (password != confirmation_password) throw new ValidateException("Senhas não coincidem");

            var retorno = musicianBusiness.Create(this);

            return retorno;
        }

        internal IEnumerable<Musician> SearchByName(string name)
        {
            IEnumerable<Musician> retorno = musicianBusiness.SearchByName(name);

            return retorno;
        }

        internal Musician Update(int id)
        {
            this.id = id;

            Musician retorno = musicianBusiness.Update(this);

            return retorno;
        }
    }
}