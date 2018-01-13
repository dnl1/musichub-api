using BearerAuthentication;
using MusicHubBusiness.Audio;
using MusicHubBusiness.Business;
using MusicHubBusiness.Enum;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;

namespace MusicHubAPI.ViewModels
{
    public class ContributionModel : Contribution
    {
        public int instrument_id { get; set; }
        public string song_base64 { get; set; }
        private bool _base_instrument = false;
        public bool base_instrument { get { return _base_instrument; } set { _base_instrument = value; } }
        private eContributionType _contribution_type_id = eContributionType.PrivateContribution;
        public eContributionType contribution_type_id { get { return _contribution_type_id; } set { _contribution_type_id = value; } }

        private FileArchive song;

        internal Contribution Create()
        {
            Contribution retorno = null;

            using (AudioHelper audioHelper = new AudioHelper())
            {
                FileHandling(audioHelper);

                BearerToken bearerToken = new BearerToken();
                var token = bearerToken.GetActiveToken();

                MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();
                MusicalProject musicalProject = musicalProjectRepository.Get(musical_project_id);

                MusicalProjectInstrumentBusiness musicalProjectInstrumentBusiness = new MusicalProjectInstrumentBusiness();
                MusicalProjectInstrument musicalProjectInstrument = new MusicalProjectInstrument
                {
                    instrument_id = instrument_id,
                    is_base_instrument = base_instrument,
                    musical_project_id = musical_project_id
                };

                musicalProjectInstrument = musicalProjectInstrumentBusiness.Create(musicalProjectInstrument);

                timing = song.TotalTime.ToString(@"hh\:mm\:ss");
                musician_id = int.Parse(token.client);
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

                retorno = contributionBusiness.Create(this);

                string keyName = string.Format("{0}{1}", retorno.id.ToString(), song.Extension);

                audioHelper.UploadToAmazon(keyName);
            }

            return retorno;
        }

        private void FileHandling(AudioHelper audioConverter)
        {
             song = new FileArchive(song_base64);

            if (song.ContentLength == 0)
                throw new Exception("O áudio recebido pelo nosso sistema está corrompido...");

            if (!song.ContentType.Contains("mp3") && !song.ContentType.Contains("wav"))
                throw new Exception("A extensão do audio não é suportada, suportamos somente wav e mp3");

            audioConverter.SaveSong(song);
        }
    }
}