//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sistickets.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ticket
    {
        public int id { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string serie { get; set; }
        public string email_cliente { get; set; }
        public string asunto { get; set; }
        public string mensaje { get; set; }
        public string solucion { get; set; }
        public string prioridad { get; set; }
        public string departamento { get; set; }
        public Nullable<int> id_empresa { get; set; }
        public string nombre_usuario { get; set; }
        public Nullable<int> id_estado_ticket { get; set; }
    
        public virtual empresa empresa { get; set; }
        public virtual estado_ticket estado_ticket { get; set; }
    }
}
