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
    public class AsistirController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/Asistir
        public IQueryable<asistir> Getasistir()
        {
            return db.asistir;
        }

        // GET: api/Asistir/5
        [ResponseType(typeof(asistir))]
        public IHttpActionResult Getasistir(int id)
        {
            asistir asistir = db.asistir.Find(id);
            if (asistir == null)
            {
                return NotFound();
            }

            return Ok(asistir);
        }

        // PUT: api/Asistir/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putasistir(int id, asistir asistir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asistir.id_socio)
            {
                return BadRequest();
            }

            db.Entry(asistir).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!asistirExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Asistir
        [ResponseType(typeof(asistir))]
        public IHttpActionResult Postasistir(asistir asistir)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.asistir.Add(asistir);
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

            return CreatedAtRoute("DefaultApi", new { id = asistir.id_socio }, asistir);
        }

        // DELETE: api/Asistir/5
        [ResponseType(typeof(asistir))]
        public IHttpActionResult Deleteasistir(int id)
        {
            asistir asistir = db.asistir.Find(id);
            if (asistir == null)
            {
                return NotFound();
            }

            db.asistir.Remove(asistir);
            db.SaveChanges();

            return Ok(asistir);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asistirExists(int id)
        {
            return db.asistir.Count(e => e.id_socio == id) > 0;
        }
    }
}