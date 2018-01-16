using BearerAuthentication;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        internal IEnumerable<MusicalProject> SearchByMusicalGenre(int musical_genre_id)
        {
            MusicalProjectBusiness musicalProjectBusiness = new MusicalProjectBusiness();
            return musicalProjectBusiness.SearchByMusicalGenre(musical_genre_id);
        }

        internal IEnumerable<Musician> Musicians(int musical_project_id)
        {
            ContributionBusiness contributionBusiness = new ContributionBusiness();
            return contributionBusiness.GetByMusicalProjectId(musical_project_id);
        }
    }
}