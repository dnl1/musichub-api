using System;
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

        internal RateContribution GetByUserAndProjectId(int musicalProjectId)
        {
            int userId = Utitilities.GetLoggedUserId();

            //TODO
            RateContribution retorno = new RateContribution();// _rateContributionBusiness.GetByUserAndProjectId(musicalProjectId, userId);

            return retorno;
        }
    }
}