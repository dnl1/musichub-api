using System;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;

namespace MusicHubAPI.ViewModels
{
    public class RateMusicianModel : RateMusician
    {
        private RateMusicianBusiness _rateMusicianBusiness;

        public RateMusicianModel()
        {
            _rateMusicianBusiness = new RateMusicianBusiness();
        }
        internal RateMusician Create()
        {
            RateMusician retorno = _rateMusicianBusiness.Create(this);
            return retorno;
        }

        internal RateMusician GetByOwnerId(int musician_target_id)
        {
            int musician_owner_id = Utitilities.GetLoggedUserId();

            RateMusician retorno = _rateMusicianBusiness.GetByOwnerId(musician_target_id, musician_owner_id);
            return retorno;
        }
    }
}