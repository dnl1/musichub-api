using MusicHubBusiness.Business;
using MusicHubBusiness.Models;

namespace MusicHubAPI.ViewModels
{
    public class RateMusicalProjectModel : RateMusicalProject
    {
        internal RateMusicalProject Create()
        {
            RateMusicalProjectBusiness rateContributionBusiness = new RateMusicalProjectBusiness();

            var retorno = rateContributionBusiness.Create(this);

            return retorno;
        }
    }
}