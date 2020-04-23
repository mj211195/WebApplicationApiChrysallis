using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            db.Configuration.LazyLoadingEnabled = false;
            return db.asistir;
        }

        //// GET: api/Asistir/5
        //[ResponseType(typeof(asistir))]
        //public IHttpActionResult Getasistir(int id)
        //{
        //    asistir asistir = db.asistir.Find(id);
        //    if (asistir == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(asistir);
        //}

        [HttpGet]
        [Route("api/Asistir/{id_socio}/{id_evento}")]
        public IHttpActionResult getAsistir(int id_socio, int id_evento)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            asistir _asistir = (
                from a in db.asistir
                where a.id_socio == id_socio && a.id_evento == id_evento
                select a).FirstOrDefault();

            if (_asistir == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_asistir);
            }
            return result;
        }

        [HttpGet]
        [Route("api/Asistir/total/{id_evento}")]
        public IHttpActionResult getAsistentes(int id_evento)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<asistir> _asistir = (
                from a in db.asistir
                where a.id_evento == id_evento
                select a).ToList();
            int total = 0;
            foreach(asistir a in _asistir)
            {
                total += a.cuantos;
            }

            if (_asistir == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(total);
            }
            return result;
        }

        //// GET: api/Asistir/searchid
        //[ResponseType(typeof(asistir))]
        //public IHttpActionResult GetEventosApuntado(int id)
        //{

        //    asistir _asistir = new asistir();
        //    List<eventos> _evento = (
        //        from a in db.asistir
        //        where a.id_socio == id
        //        select a.eventos).ToList();

        //    if (_asistir == null)
        //    {
        //        return NotFound();
        //    }


        //    return Ok(_evento);
        //}

        //// PUT: api/Asistir/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putasistir(int id, asistir asistir)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != asistir.id_socio)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(asistir).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!asistirExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        [HttpPut]
        [Route("api/Asistir/{id_socio}/{id_evento}")]
        public IHttpActionResult PutAsistir(int id_socio, int id_evento, asistir asistir)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id_socio != asistir.id_socio || id_evento != asistir.id_evento)
            {
                return BadRequest();
            }

            db.Entry(asistir).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!asistirExists(id_socio, id_evento))
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

        //// DELETE: api/Asistir/5
        //[ResponseType(typeof(asistir))]
        //public IHttpActionResult Deleteasistir(int id)
        //{
        //    asistir _asistir = db.asistir.Find(id);
        //    String mensaje;
        //    if (_asistir == null)
        //    {
        //        return NotFound();
        //    }

        //    db.asistir.Remove(_asistir);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        SqlException sqlExc = (SqlException)ex.InnerException.InnerException;
        //        mensaje = Utilidad.MensajeError(sqlExc);
        //        return BadRequest(mensaje);
        //    }

        //    return Ok(_asistir);
        //}

        [HttpDelete]
        [Route("api/Asistir/{id_socio}/{id_evento}")]
        public IHttpActionResult deleteAsistir(int id_socio, int id_evento)
        {
            asistir _asistir = db.asistir.Find(id_socio,id_evento);
            String mensaje;
            if (_asistir == null)
            {
                return NotFound();
            }

            db.asistir.Remove(_asistir);
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

            return Ok(_asistir);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asistirExists(int id_socio, int id_evento)
        {
            return db.asistir.Count(e => e.id_socio == id_socio && e.id_evento == id_evento) > 0;
        }

        [HttpPost]
        [Route("api/Asistir/modificar/{id_socio}/{id_evento}")]
        public IHttpActionResult modificarAsistir(int id_socio, int id_evento, asistir asistir)
        {
            String mensaje;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id_socio != asistir.id_socio || id_evento != asistir.id_evento)
            {
                return BadRequest();
            }

            db.Entry(asistir).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!asistirExists(id_socio, id_evento))
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

        [HttpGet]
        [Route("api/Asistir/eliminar/{id_socio}/{id_evento}")]
        public IHttpActionResult eliminarAsistir(int id_socio, int id_evento)
        {
            asistir _asistir = db.asistir.Find(id_socio, id_evento);
            String mensaje;
            if (_asistir == null)
            {
                return NotFound();
            }

            db.asistir.Remove(_asistir);
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

            return Ok("Baja confirmada");
        }

        [HttpGet]
        [Route("api/Asistir/eliminar/{codigo_asistir}")]
        public HttpResponseMessage eliminarAsistirByCodigo(String codigo_asistir)
        {

            var response = new HttpResponseMessage();
            asistir _asistir = (
                from a in db.asistir
                where a.codigo_asistir.Equals(codigo_asistir)
                select a).FirstOrDefault();
            String mensaje;
            if (_asistir == null)
            {
                response.Content = new StringContent("<html><body>No se ha encontrado la asistencia</body></html>");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }

            db.asistir.Remove(_asistir);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlExc = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidad.MensajeError(sqlExc);

                response.Content = new StringContent("<html><body>Error al conectarse con la base de datos - " + mensaje + "</body></html>");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }
            response.Content = new StringContent("<html><body><img style=\"display: block; margin-left: auto; margin-right: auto; \"src=\"https://chrysallis.org.es/wp-content/uploads/2020/03/cropped-LOGOCHRYSALLIS_peque%C3%B1o.png\" alt=\"logo\" width=\"176\" height=\"171\"/><h1 style = \"text-align: center;\"><span style = \"color: #000080;\"><strong> Baja confirmada </strong></span></h1><h1 style = \"text-align: center;\" ><span style = \"color: #000080;\"><strong> Baixa confirmada </strong ></span></h1><h1 style = \"text-align: center;\"><span style = \"color: #000080;\"><strong> Unsubscribe confirmation </strong></span><h1 style = \"text-align: center;\"><span style = \"color: #000080;\"><strong> Baxua baieztatu da</strong></span></h1><h1 style = \"text-align: center;\"><span style = \"color: #000080;\"><strong> Baixo confirmado </strong></span></h1></body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
            //return Ok("Baja confirmada");
        }
    }
}