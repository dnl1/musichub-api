using BearerAuthentication;
using MusicHubBusiness.Audio;
using MusicHubBusiness.Business;
using MusicHubBusiness.Enum;
using MusicHubBusiness.Models;
using MusicHubBusiness.Repository;
using System;
using System.Web;

namespace MusicHubAPI.ViewModels
{
    public class ContributionModel : Contribution
    {
        public int instrument_id { get; set; }
        public int musical_project_id { get; set; }

        private TimeSpan timingSpan;

        internal Contribution Create(bool baseInstrument, HttpPostedFile song)
        {
            Contribution retorno = null;

            AudioHelper audioHelper = new AudioHelper();

            FileHandling(song, audioHelper);

            BearerToken bearerToken = new BearerToken();
            var token = bearerToken.GetActiveToken();

            MusicalProjectRepository musicalProjectRepository = new MusicalProjectRepository();
            MusicalProject musicalProject = musicalProjectRepository.Get(musical_project_id);

            MusicalProjectInstrumentBusiness musicalProjectInstrumentBusiness = new MusicalProjectInstrumentBusiness();
            MusicalProjectInstrument musicalProjectInstrument = new MusicalProjectInstrument
            {
                instrument_id = instrument_id,
                is_base_instrument = baseInstrument,
                musical_project_id = musical_project_id
            };

            musicalProjectInstrument = musicalProjectInstrumentBusiness.Create(musicalProjectInstrument);

            timing = timingSpan.ToString("HH:mm:ss");
            musician_id = int.Parse(token.client);
            musical_genre_id = musicalProject.musical_genre_id;
            musical_project_instrument_id = musicalProjectInstrument.id;

            if(baseInstrument)
            {
                status_id = eContributionStatus.Approved;
                type_id = eContributionType.PrivateContribution;
            }

            ContributionBusiness contributionBusiness = new ContributionBusiness();

            retorno = contributionBusiness.Create(this);

            audioHelper.UploadToAmazon(retorno.id.ToString());
            audioHelper.ClearTempFiles();

            return retorno;
        }

        private void FileHandling(HttpPostedFile song, AudioHelper audioConverter)
        {
            if (song.ContentLength == 0)
                throw new Exception("O áudio recebido pelo nosso sistema está corrompido...");

            if (!song.ContentType.Contains("mp3") && !song.ContentType.Contains("wav"))
                throw new Exception("A extensão do audio não é suportada, suportamos somente wav e mp3");


            string mp3FileName = string.Empty;
            if (song.ContentType.Contains("wav"))
            {
                mp3FileName = audioConverter.WaveToMP3(song);
            }
            else //THEN IS MP3
            {
                mp3FileName = audioConverter.SaveSong(song);
            }

            timingSpan = audioConverter.GetTotalTime();
        }
    }
}