﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplicationApiChrysallis
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class chrysallisEntities : DbContext
    {
        public chrysallisEntities()
            : base("name=chrysallisEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<asistir> asistir { get; set; }
        public virtual DbSet<comunidades> comunidades { get; set; }
        public virtual DbSet<documentos> documentos { get; set; }
        public virtual DbSet<eventos> eventos { get; set; }
        public virtual DbSet<mensajes> mensajes { get; set; }
        public virtual DbSet<notificaciones> notificaciones { get; set; }
        public virtual DbSet<socios> socios { get; set; }
    }
}
