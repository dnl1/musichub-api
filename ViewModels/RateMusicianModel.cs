using MusicHubBusiness.Business;
using MusicHubBusiness.Models;

namespace MusicHubAPI.ViewModels
{
    public class RateMusicianModel : RateMusician
    {
        internal RateMusician Create()
        {
            RateMusicianBusiness rateMusicianBusiness = new RateMusicianBusiness();

            var retorno = rateMusicianBusiness.Create(this);

            return retorno;
        }
    }
}