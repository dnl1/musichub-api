﻿using MusicHubBusiness.Business;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MusicHubAPI.Controllers
{
    public class MusicalGenreController : RestfulApiController
    {
        // GET: MusicalGenre
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            IEnumerable<MusicalGenre> retorno = null;

            try
            {
                MusicalGenreBusiness musicalGenreBusiness = new MusicalGenreBusiness();
                retorno = musicalGenreBusiness.GetAll();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(retorno);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] int id)
        {
            MusicalGenre retorno = null;

            try
            {
                MusicalGenreBusiness musicalGenreBusiness = new MusicalGenreBusiness();
                retorno = musicalGenreBusiness.Get(id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(retorno);
        }
    }
}