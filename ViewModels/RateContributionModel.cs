using MusicHubBusiness.Business;
using MusicHubBusiness.Models;

namespace MusicHubAPI.ViewModels
{
    public class RateContributionModel : RateContribution
    {
        internal RateContribution Create()
        {
            RateContributionBusiness rateContributionBusiness = new RateContributionBusiness();

            var retorno = rateContributionBusiness.Create(this);

            return retorno;
        }
    }
}