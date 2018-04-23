using MusicHubAPI.Enum;
using MusicHubBusiness;
using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicHubAPI.ViewModels
{
    public class MusicalProjectModel : MusicalProject
    {
        public int[] instruments { get; set; }
        public int base_instrument_id { get; set; }
        public byte[] base_insrument { get; set; }

        internal MusicalProject Create()
        {
            if (instruments.Length < 1) throw new ValidateException("É necessário ter no mínimo um instrumento!");
            if (base_instrument_id == 0) throw new ValidateException("É necessário preencher o instrumento base!");

            MusicalProjectBusiness musicalProjectBusiness = new MusicalProjectBusiness();

            var retorno = musicalProjectBusiness.Create(this);

            MusicalProjectInstrumentBusiness musicalProjectInstrumentBusiness = new MusicalProjectInstrumentBusiness();
            MusicalProjectInstrument baseMusicalProjectInstrument = new MusicalProjectInstrument();

            foreach (var item in instruments)
            {
                MusicalProjectInstrument musicalProjectInstrument = new MusicalProjectInstrument()
                {
                    instrument_id = item,
                    musical_project_id = retorno.id,
                    is_base_instrument = item == base_instrument_id
                };

                var tempMusicalProjectInstrument = musicalProjectInstrumentBusiness.Create(musicalProjectInstrument);

                if (item == base_instrument_id)
                {
                    baseMusicalProjectInstrument = tempMusicalProjectInstrument;
                }
            }

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string audioPath = string.Empty;
            int idBaseMusicalProjectInstrument = baseMusicalProjectInstrument.id;

            if (instruments.Any(i => i == (int)EInstruments.Lead_Guitar || i == (int)EInstruments.Rhythm_Guitar))
            {
                audioPath = Path.Combine(baseDirectory, "PlaceholderAudio\\guitar.mp3");
            }
            else if (instruments.Any(i => i == (int)EInstruments.Piano))
            {
                audioPath = Path.Combine(baseDirectory, "PlaceholderAudio\\synth.mp3");
            }
            else
            {
                throw new NotImplementedException();
            }

            string folderSave = Path.Combine(baseDirectory, "UploadedAudios");

            musicalProjectInstrumentBusiness.SaveAudio(audioPath, folderSave, idBaseMusicalProjectInstrument);

            return retorno;
        }

        internal IEnumerable<MusicalProject> MyProjects()
        {
            MusicalProjectBusiness musicalProjectBusiness = new MusicalProjectBusiness();

            int owner_id = Utitilities.GetLoggedUserId();

            IEnumerable<MusicalProject> projects = musicalProjectBusiness.GetProjectsByOwnerId(owner_id);

            return projects;
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