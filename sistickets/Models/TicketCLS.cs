using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sistickets.Models
{
    public class TicketCLS
    {
        [Display(Name ="Id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Fecha de creación")]
        public DateTime fecha { get; set; }

        [Display(Name = "Código del ticket")]
        public string serie { get; set; }

        [Display(Name = "Estado")]
        public string estado_ticket { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string nombre_usuario { get; set; }

        [Required]
        [Display(Name = "Correo del cliente")]
        public string email_cliente { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public string departamento { get; set; }

        [Required]
        [Display(Name = "Asunto")]
        public string asunto { get; set; }

        [Required]
        [Display(Name = "Mensaje")]
        public string mensaje { get; set; }

        [Display(Name = "Solución")]
        public string solucion { get; set; }

        [Display(Name = "Prioridad")]
        public string prioridad { get; set; }
    }
}