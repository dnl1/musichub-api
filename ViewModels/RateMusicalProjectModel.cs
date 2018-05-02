using System;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;

namespace MusicHubAPI.ViewModels
{
    public class RateMusicalProjectModel : RateMusicalProject
    {
        private RateMusicalProjectBusiness _rateContributionBusiness;

        public RateMusicalProjectModel()
        {
            _rateContributionBusiness = new RateMusicalProjectBusiness();
        }

        internal RateMusicalProject Create()
        {
            var retorno = _rateContributionBusiness.Create(this);

            return retorno;
        }

        internal RateMusicalProject GetByUserAndProjectId(int musicalProjectId)
        {
            int userId = Utitilities.GetLoggedUserId();

            RateMusicalProject retorno = _rateContributionBusiness.GetByUserAndProjectId(musicalProjectId, userId);

            return retorno;
        }
    }
}