﻿using MusicHubAPI.Enum;
using MusicHubBusiness;
using MusicHubBusiness.Audio;
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

            ContributionModel contributionModel = new ContributionModel()
            {
                instrument_id = baseMusicalProjectInstrument.instrument_id,
                base_instrument = true,
                musical_project_id = retorno.id,
                musical_genre_id = retorno.musical_genre_id,
                musician_id = Utitilities.GetLoggedUserId(),
            };

            contributionModel.Create();

            //if (instruments.Any(i => i == (int)EInstruments.Lead_Guitar || i == (int)EInstruments.Rhythm_Guitar))
            //{
            //    audioPath = Path.Combine(baseDirectory, "PlaceholderAudio\\guitar.mp3");
            //}
            //else if (instruments.Any(i => i == (int)EInstruments.Piano))
            //{
            //    audioPath = Path.Combine(baseDirectory, "PlaceholderAudio\\synth.mp3");
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}

            //string folderSave = Path.Combine(baseDirectory, "UploadedAudios");

            //musicalProjectInstrumentBusiness.SaveAudio(audioPath, folderSave, idBaseMusicalProjectInstrument);

            return retorno;
        }

        internal MemoryStream Download(int id)
        {
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectsAudio", $"{id}.mp3");

            byte[] bytes = File.ReadAllBytes(file);
            MemoryStream stream = new MemoryStream(bytes, 0, bytes.Length, true, true);

            return stream;
        }

        internal void Finish(int id)
        {
            MusicalProjectBusiness musicalProjectBusiness = new MusicalProjectBusiness();
            musicalProjectBusiness.Finish(id);

            var contributions = Contributions(id).ToList();
            List<string> files = new List<string>();

            for (int i = 0; i < contributions.Count; i++)
            {
                files.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedAudios", $"{contributions[i].id}.mp3"));
            }

            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectsAudio");

            AudioHelper.CreateMashup(id, folder, files.ToArray());
        }

        internal IEnumerable<Contribution> Contributions(int musicalProjectId, int instrumentId)
        {
            ContributionBusiness contributionBusiness = new ContributionBusiness();
            IEnumerable<Contribution> contributions = contributionBusiness.GetByMusicalProjectAndIntrument(musicalProjectId, instrumentId);

            return contributions;
        }

        internal IEnumerable<Contribution> Contributions(int musicalProjectId)
        {
            ContributionBusiness contributionBusiness = new ContributionBusiness();
            IEnumerable<Contribution> contributions = contributionBusiness.GetByMusicalProjectId(musicalProjectId);

            return contributions;
        }

        internal IEnumerable<MusicalProjectInstrument> Instruments(int musicalProjectId)
        {
            MusicalProjectInstrumentBusiness musicalProjectInstrumentBusiness = new MusicalProjectInstrumentBusiness();
            IEnumerable<MusicalProjectInstrument> retorno = musicalProjectInstrumentBusiness.GetByMusicalProject(musicalProjectId);

            return retorno;
        }

        internal MusicalProject Get(int id)
        {
            MusicalProjectBusiness musicalProjectBusiness = new MusicalProjectBusiness();
            MusicalProject retorno = musicalProjectBusiness.Get(id);

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
            MusicianBusiness contributionBusiness = new MusicianBusiness();
            return contributionBusiness.GetByMusicalProjectId(musical_project_id);
        }
    }
}