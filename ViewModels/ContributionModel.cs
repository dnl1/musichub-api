using MusicHubAPI.Enum;
using MusicHubBusiness;
using MusicHubBusiness.Audio;
using MusicHubBusiness.Business;
using MusicHubBusiness.Enum;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicHubAPI.ViewModels
{
    public class ContributionModel : Contribution
    {
        public int instrument_id { get; set; }
        public string song_base64 { get; set; }
        public bool base_instrument { get; set; } = true;
        public eContributionType contribution_type_id { get; set; } = eContributionType.PrivateContribution;

        private FileArchive song;

        internal Contribution Create()
        {
            Mock();

            Contribution retorno = null;

            using (AudioHelper audioHelper = new AudioHelper())
            {
                FileHandling(audioHelper);

                MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();
                MusicalProject musicalProject = musicalProjectRepository.Get(musical_project_id);

                MusicalProjectInstrumentBusiness musicalProjectInstrumentBusiness = new MusicalProjectInstrumentBusiness();
                MusicalProjectInstrument musicalProjectInstrument = new MusicalProjectInstrument
                {
                    instrument_id = instrument_id,
                    is_base_instrument = base_instrument,
                    musical_project_id = musical_project_id
                };

                var projectInstruments = musicalProjectInstrumentBusiness.GetByMusicalProject(musical_project_id);
                musicalProjectInstrument = projectInstruments.Where(p => p.instrument_id == instrument_id).FirstOrDefault();

                if (projectInstruments is null || musicalProjectInstrument == null)
                {
                    musicalProjectInstrument = musicalProjectInstrumentBusiness.Create(musicalProjectInstrument);
                }

                timing = song.TotalTime.ToString(@"hh\:mm\:ss");
                musician_id = Utitilities.GetLoggedUserId();
                musical_genre_id = musicalProject.musical_genre_id;
                musical_project_instrument_id = musicalProjectInstrument.id;

                if (base_instrument)
                {
                    status_id = eContributionStatus.Approved;
                    type_id = eContributionType.PrivateContribution;
                }
                else
                {
                    status_id = eContributionStatus.WaitingForApproval;
                    type_id = contribution_type_id;
                }

                ContributionBusiness contributionBusiness = new ContributionBusiness();

                var contributions = contributionBusiness.GetByMusicalProjectId(musical_project_id);
                var userContribution = contributions.Where(c => c.musician_id == musician_id && c.musical_project_instrument_id == musical_project_instrument_id).FirstOrDefault();

                if (userContribution != null) throw new ValidateException("You have already contribuited to this insturment on this project");

                retorno = contributionBusiness.Create(this);

                string keyName = string.Format("{0}{1}", retorno.id.ToString(), song.Extension);

                string audioPath = GetAudioPath();

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string folderSave = Path.Combine(baseDirectory, "UploadedAudios");

                musicalProjectInstrumentBusiness.SaveAudio(audioPath, folderSave, retorno.id);

                //audioHelper.UploadToAmazon(keyName);
            }

            return retorno;
        }

        internal void Approve(int contributionId)
        {
            ContributionBusiness contributionBusiness = new ContributionBusiness();
            contributionBusiness.Approve(contributionId);
        }

        private void Mock()
        {
            string audioPath = GetAudioPath();
            byte[] baites = File.ReadAllBytes(audioPath);
            this.song_base64 = Convert.ToBase64String(baites);
        }

        public string GetAudioPath()
        {
            string audioPath = string.Empty;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            switch ((EInstruments)instrument_id)
            {
                case EInstruments.Lead_Guitar:
                    audioPath = Path.Combine(baseDirectory, "PlaceholderAudio\\guitar.mp3");
                    break;

                case EInstruments.Piano:
                    audioPath = Path.Combine(baseDirectory, "PlaceholderAudio\\synth.mp3");
                    break;

                default:
                    throw new NotImplementedException();
            }

            return audioPath;
        }

        internal IEnumerable<Contribution> GetFreeContributions(int id)
        {
            ContributionBusiness contributionBusiness = new ContributionBusiness();
            return contributionBusiness.GetFreeContributions(id);
        }

        private void FileHandling(AudioHelper audioConverter)
        {
            song = new FileArchive(song_base64);

            //if (song.ContentLength == 0)
            //    throw new Exception("O áudio recebido pelo nosso sistema está corrompido...");

            //if (!song.ContentType.Contains("mp3") && !song.ContentType.Contains("wav"))
            //    throw new Exception("A extensão do audio não é suportada, suportamos somente wav e mp3");

            audioConverter.SaveSong(song);
        }
    }
}