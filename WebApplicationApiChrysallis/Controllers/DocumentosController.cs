using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationApiChrysallis;

namespace WebApplicationApiChrysallis.Controllers
{
    public class DocumentosController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/Documentos
        public IQueryable<documentos> Getdocumentos()
        {
            return db.documentos;
        }

        // GET: api/Documentos/5
        [ResponseType(typeof(documentos))]
        public IHttpActionResult Getdocumentos(int id)
        {
            documentos documentos = db.documentos.Find(id);
            if (documentos == null)
            {
                return NotFound();
            }

            return Ok(documentos);
        }

        // PUT: api/Documentos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdocumentos(int id, documentos documentos)
        {
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
            catch (DbUpdateConcurrencyException)
            {
                if (!documentosExists(id))
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

        // POST: api/Documentos
        [ResponseType(typeof(documentos))]
        public IHttpActionResult Postdocumentos(documentos documentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.documentos.Add(documentos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = documentos.id }, documentos);
        }

        // DELETE: api/Documentos/5
        [ResponseType(typeof(documentos))]
        public IHttpActionResult Deletedocumentos(int id)
        {
            documentos documentos = db.documentos.Find(id);
            if (documentos == null)
            {
                return NotFound();
            }

            db.documentos.Remove(documentos);
            db.SaveChanges();

            return Ok(documentos);
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