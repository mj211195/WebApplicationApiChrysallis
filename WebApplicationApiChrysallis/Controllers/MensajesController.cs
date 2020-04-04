using System;
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
    public class MensajesController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/Mensajes
        public IQueryable<mensajes> Getmensajes()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.mensajes;
        }

        // GET: api/Mensajes/5
        [ResponseType(typeof(mensajes))]
        public IHttpActionResult Getmensajes(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            mensajes _mensaje = db.mensajes.Find(id);
            if (_mensaje == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_mensaje);
            }
            return result;
        }

        // PUT: api/Mensajes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmensajes(int id, mensajes mensajes)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mensajes.id)
            {
                return BadRequest();
            }

            db.Entry(mensajes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!mensajesExists(id))
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

        // POST: api/Mensajes
        [ResponseType(typeof(mensajes))]
        public IHttpActionResult Postmensajes(mensajes mensajes)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mensajes.Add(mensajes);
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
            return CreatedAtRoute("DefaultApi", new { id = mensajes.id }, mensajes);
        }

        // DELETE: api/Mensajes/5
        [ResponseType(typeof(mensajes))]
        public IHttpActionResult Deletemensajes(int id)
        {
            mensajes _mensaje = db.mensajes.Find(id);
            String mensaje;
            if (_mensaje == null)
            {
                return NotFound();
            }

            db.mensajes.Remove(_mensaje);
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

            return Ok(_mensaje);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mensajesExists(int id)
        {
            return db.mensajes.Count(e => e.id == id) > 0;
        }
    }
}