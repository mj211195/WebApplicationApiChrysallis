﻿using System;
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
    public class EventosController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/Eventos
        public IQueryable<eventos> Geteventos()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.eventos;
        }

        // GET: api/Eventos/5
        [ResponseType(typeof(eventos))]
        public IHttpActionResult Geteventos(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            eventos _evento = (from e in db.eventos.Include("comunidades").Include("asistir").Include("notificaciones")
                               where e.id == id
                             select e).FirstOrDefault();
            if (_evento == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_evento);
            }
            return result;
        }

        [HttpGet]
        [Route("api/Eventos/SearchSocio/{id_socio}")]
        public IHttpActionResult GetEventosApuntado(int id_socio)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<eventos> _eventos = (
                from e in db.eventos.Include("comunidades").Include("asistir").Include("notificaciones")
                join c in db.comunidades on e.comunidades.id equals c.id
                join a in db.asistir on e.id equals a.id_evento
                where a.id_socio == id_socio
                select e).ToList();

            if (_eventos == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_eventos);
            }

            return result;
        }

        // PUT: api/Eventos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puteventos(int id, eventos eventos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventos.id)
            {
                return BadRequest();
            }

            db.Entry(eventos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eventosExists(id))
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

        // POST: api/Eventos
        [ResponseType(typeof(eventos))]
        public IHttpActionResult Posteventos(eventos eventos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.eventos.Add(eventos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventos.id }, eventos);
        }

        // DELETE: api/Eventos/5
        [ResponseType(typeof(eventos))]
        public IHttpActionResult Deleteeventos(int id)
        {
            eventos eventos = db.eventos.Find(id);
            if (eventos == null)
            {
                return NotFound();
            }

            db.eventos.Remove(eventos);
            db.SaveChanges();

            return Ok(eventos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool eventosExists(int id)
        {
            return db.eventos.Count(e => e.id == id) > 0;
        }

        [HttpGet]
        [Route("api/Eventos/SearchNC/{nombre}/{id_comunidad}/{date}")]
        public IHttpActionResult BusquedaEventosNombreComunidad(String nombre,int id_comunidad, DateTime date)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<eventos>_eventos = (
                from e in db.eventos.Include("comunidades").Include("asistir").Include("notificaciones")
                where e.id_comunidad == id_comunidad && e.nombre.Contains(nombre) && e.fecha >= date
                select e).ToList();

            if (_eventos == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_eventos);
            }

            return result;
        }


       


        [HttpGet]
        [Route("api/Eventos/SearchCD/{id_comunidad}/{date}")]
        public IHttpActionResult BusquedaEventosComDate(int id_comunidad, DateTime date)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<eventos> _eventos = (
                from e in db.eventos.Include("comunidades").Include("asistir").Include("notificaciones")
                where e.id_comunidad == id_comunidad && e.fecha.Equals(date)
                select e).ToList();

            if (_eventos == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_eventos);
            }
            return result;
        }


        [HttpGet]
        [Route("api/Eventos/Search/{nombre}/{id_comunidad}/{date}")]
        public IHttpActionResult BusquedaEventosAll(String nombre, int id_comunidad, DateTime date)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<eventos> _eventos = (
                from e in db.eventos.Include("comunidades").Include("asistir").Include("notificaciones")
                where e.nombre.Contains(nombre) && e.id_comunidad == id_comunidad && e.fecha.Equals(date)
                select e).ToList();

            if (_eventos == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_eventos);
            }
            return result;
        }


        [HttpGet]
        [Route("api/Eventos/SearchC/{id_comunidad}/{date}")]
        public IHttpActionResult BusquedaEventosComunidad(int id_comunidad, DateTime date)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<eventos> _eventos = (
                from e in db.eventos.Include("comunidades").Include("asistir").Include("notificaciones")
                where e.id_comunidad == id_comunidad && e.fecha >= date
                select e).ToList();

            if (_eventos == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_eventos);
            }
            return result;
        }
    }
}