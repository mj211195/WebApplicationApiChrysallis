//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class eventos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public eventos()
        {
            this.asistir = new HashSet<asistir>();
            this.documentos = new HashSet<documentos>();
            this.notificaciones = new HashSet<notificaciones>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public byte[] imagen { get; set; }
        public System.DateTime fecha { get; set; }
        public string ubicacion { get; set; }
        public System.TimeSpan hora { get; set; }
        public Nullable<System.DateTime> fechaLimite { get; set; }
        public int numAsistentes { get; set; }
        public string descripcion { get; set; }
        public string nombreImagen { get; set; }
        public int id_comunidad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<asistir> asistir { get; set; }
        public virtual comunidades comunidades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<documentos> documentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notificaciones> notificaciones { get; set; }
    }
}
