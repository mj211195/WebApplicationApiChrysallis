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
    public class SociosController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/Socios
        public IQueryable<socios> Getsocios()
        {
            return db.socios;
        }

        // GET: api/Socios/5
        [ResponseType(typeof(socios))]
        public IHttpActionResult Getsocios(int id)
        {
            socios socios = db.socios.Find(id);
            if (socios == null)
            {
                return NotFound();
            }

            return Ok(socios);
        }

        // PUT: api/Socios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsocios(int id, socios socios)
        {
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
            catch (DbUpdateConcurrencyException)
            {
                if (!sociosExists(id))
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

        // POST: api/Socios
        [ResponseType(typeof(socios))]
        public IHttpActionResult Postsocios(socios socios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.socios.Add(socios);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = socios.id }, socios);
        }

        // DELETE: api/Socios/5
        [ResponseType(typeof(socios))]
        public IHttpActionResult Deletesocios(int id)
        {
            socios socios = db.socios.Find(id);
            if (socios == null)
            {
                return NotFound();
            }

            db.socios.Remove(socios);
            db.SaveChanges();

            return Ok(socios);
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
    }
}