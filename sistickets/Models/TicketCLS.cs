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

        [Display(Name = "Fecha de creación")]
        public DateTime fecha { get; set; }

        [Display(Name = "Código del ticket")]
        public string serie { get; set; }

        [Display(Name = "Estado")]
        public string estado_ticket { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z ]{1,100}", ErrorMessage = "Debe contener letras y un máximo de 100 caracteres")]
        [Display(Name = "Nombre")]
        public string nombre_usuario { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo no es válido")]
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

        public int idEmpresa { get; set; }
    }

    public enum Departamento{
        Contabilidad,
        [Display(Name ="Recursos Humanos")]
        RecursosHumanos,
        Marketing,
        Sistemas,
        Compras,
        Mantenimiento,
        [Display(Name = "Empresa Externa")]
        Empresa
    }
}
