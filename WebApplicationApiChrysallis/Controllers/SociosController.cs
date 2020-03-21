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
    public class SociosController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/Socios
        public IQueryable<socios> Getsocios()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.socios;
        }

        // GET: api/Socios/5
        [ResponseType(typeof(socios))]
        public IHttpActionResult Getsocios(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            socios _socio = (from s in db.socios.Include("comunidades")
                             where s.id == id
                             select s).FirstOrDefault();
            if (_socio == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_socio);
            }
            return result;
        }

        // PUT: api/Socios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsocios(int id, socios socios)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socios.id)
            {
                return BadRequest();
            }

            db.Entry(socios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!sociosExists(id))
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

        // POST: api/Socios
        [ResponseType(typeof(socios))]
        public IHttpActionResult Postsocios(socios socios)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.socios.Add(socios);
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
            return CreatedAtRoute("DefaultApi", new { id = socios.id }, socios);
        }

        // DELETE: api/Socios/5
        [ResponseType(typeof(socios))]
        public IHttpActionResult Deletesocios(int id)
        {
            socios _socio = db.socios.Find(id);
            String mensaje;
            if (_socio == null)
            {
                return NotFound();
            }

            db.socios.Remove(_socio);
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

            return Ok(_socio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sociosExists(int id)
        {
            return db.socios.Count(e => e.id == id) > 0;
        }

        [HttpGet]
        [Route("api/Socios/{telefono}/{password}")]
        public IHttpActionResult SocioLogin(String telefono,String password)
        {
            db.Configuration.LazyLoadingEnabled = false;

            socios _socio = (
                from s in db.socios
                where s.telefono.Equals(telefono) && s.password.Equals(password)
                select s).FirstOrDefault();

            return Ok(_socio);
        }

        [HttpGet]
        [Route("api/Socios/busquedaRecuperar/{telefono}/{mail}")]
        public IHttpActionResult SocioRecuperar(String telefono, String mail)
        {
            db.Configuration.LazyLoadingEnabled = false;

            socios _socio = (
                from s in db.socios
                where s.telefono.Equals(telefono) && s.mail.Equals(mail)
                select s).FirstOrDefault();

            return Ok(_socio);
        }
    }
}