using BearerAuthentication;
using MusicHubBusiness;
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
        public int[] instruments { get; set; }
        public int base_instrument_id { get; set; }

        internal MusicalProject Create()
        {
            if (instruments.Length < 2) throw new ValidateException("É necessário ter no mínimo dois instrumentos!");
            if (base_instrument_id == 0) throw new ValidateException("É necessário preencher o instrumento base!");

            MusicalProjectBusiness musicalProjectBusiness = new MusicalProjectBusiness();

            BearerToken bearerToken = new BearerToken();
            var token = bearerToken.GetActiveToken();

            this.owner_id = int.Parse(token.client);    

            var retorno = musicalProjectBusiness.Create(this);

            MusicalProjectInstrumentBusiness musicalProjectInstrumentBusiness = new MusicalProjectInstrumentBusiness();

            foreach (var item in instruments)
            {
                MusicalProjectInstrument musicalProjectInstrument = new MusicalProjectInstrument()
                {
                    instrument_id = item,
                    musical_project_id = retorno.id,
                    is_base_instrument = item == base_instrument_id
                };

                musicalProjectInstrumentBusiness.Create(musicalProjectInstrument);
            }

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