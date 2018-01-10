using BearerAuthentication;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System.Web;

namespace MusicHubAPI.ViewModels
{
    public class MusicalProjectModel : MusicalProject
    {
        internal MusicalProject Create()
        {
            MusicalProjectBusiness musicalProjectBusiness = new MusicalProjectBusiness();

            BearerToken bearerToken = new BearerToken();
            var token = bearerToken.GetActiveToken();

            this.owner_id = int.Parse(token.client);    

            var retorno = musicalProjectBusiness.Create(this);

            return retorno;
        }
    }
}