﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationApiChrysallis;
using WebApplicationApiChrysallis.Utilidades;

namespace WebApplicationApiChrysallis.Controllers
{
    public class ComunidadesController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/Comunidades
        public IQueryable<comunidades> Getcomunidades()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.comunidades;
        }

        // GET: api/Comunidades/5
        [ResponseType(typeof(comunidades))]
        public IHttpActionResult Getcomunidades(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            comunidades _comunidad = (from c in db.comunidades
                             where c.id == id
                             select c).FirstOrDefault();

            if (_comunidad == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_comunidad);
            }
            return result;
        }

        // PUT: api/Comunidades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcomunidades(int id, comunidades comunidades)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comunidades.id)
            {
                return BadRequest();
            }

            db.Entry(comunidades).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!comunidadesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    SqlException sqlExc = (SqlException)ex.InnerException.InnerException;
                    mensaje = Utilidad.MensajeError(sqlExc);
                    return BadRequest(mensaje);
                }
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlExc = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidad.MensajeError(sqlExc);
                return BadRequest(mensaje);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Comunidades
        [ResponseType(typeof(comunidades))]
        public IHttpActionResult Postcomunidades(comunidades comunidades)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.comunidades.Add(comunidades);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlExc = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidad.MensajeError(sqlExc);
                return BadRequest(mensaje);
            }
  
            return CreatedAtRoute("DefaultApi", new { id = comunidades.id }, comunidades);
        }

        // DELETE: api/Comunidades/5
        [ResponseType(typeof(comunidades))]
        public IHttpActionResult Deletecomunidades(int id)
        {
            comunidades _comunidad = db.comunidades.Find(id);
            String mensaje;
            if (_comunidad == null)
            {
                return NotFound();
            }

            db.comunidades.Remove(_comunidad);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlExc = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidad.MensajeError(sqlExc);
                return BadRequest(mensaje);
            }

            return Ok(_comunidad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool comunidadesExists(int id)
        {
            return db.comunidades.Count(e => e.id == id) > 0;
        }
    }
}