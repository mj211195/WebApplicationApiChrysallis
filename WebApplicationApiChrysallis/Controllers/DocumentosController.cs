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
    public class DocumentosController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/Documentos
        public IQueryable<documentos> Getdocumentos()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.documentos;
        }

        // GET: api/Documentos/5
        [ResponseType(typeof(documentos))]
        public IHttpActionResult Getdocumentos(int id)
        {

            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            documentos _documento= (from d in db.documentos
                               where d.id == id
                               select d).FirstOrDefault();
            if (_documento == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_documento);
            }
            return result;
        }

        // PUT: api/Documentos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdocumentos(int id, documentos documentos)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documentos.id)
            {
                return BadRequest();
            }

            db.Entry(documentos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!documentosExists(id))
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

        // POST: api/Documentos
        [ResponseType(typeof(documentos))]
        public IHttpActionResult Postdocumentos(documentos documentos)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.documentos.Add(documentos);
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

            return CreatedAtRoute("DefaultApi", new { id = documentos.id }, documentos);
        }

        // DELETE: api/Documentos/5
        [ResponseType(typeof(documentos))]
        public IHttpActionResult Deletedocumentos(int id)
        {
            documentos _documento = db.documentos.Find(id);
            String mensaje;
            if (_documento == null)
            {
                return NotFound();
            }

            db.documentos.Remove(_documento);
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
            return Ok(_documento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool documentosExists(int id)
        {
            return db.documentos.Count(e => e.id == id) > 0;
        }

        [HttpGet]
        [Route("api/Documentos/SearchEvent/{id_evento}")]
        public IHttpActionResult busquedaDocumentosEvento(int id_evento)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<documentos> _documentos = (
                from d in db.documentos
                where d.id_evento == id_evento
                select d).ToList();

            if (_documentos == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_documentos);
            }
            return result;
        }
    }
}