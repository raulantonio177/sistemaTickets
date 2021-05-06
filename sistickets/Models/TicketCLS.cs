using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sistickets.Models
{
    public class TicketCLS
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        [Display(Name = "Código")]
        public string serie { get; set; }

        public int idEstado { get; set; }



        [RegularExpression("[a-zA-Z ]{1,100}", ErrorMessage = "Debe contener letras y un máximo de 100 caracteres")]
        [Display(Name = "Nombre")]
        public string nombre_usuario { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo no es válido")]
        [Display(Name = "Correo")]
        public string email_cliente { get; set; }

        [Display(Name = "Departamento")]
        public string departamento { get; set; }

        [Display(Name = "Asunto")]
        public string asunto { get; set; }

        [Display(Name = "Mensaje")]
        public string mensaje { get; set; }

        [Display(Name = "Solución")]
        public string solucion { get; set; }

        [Display(Name = "Prioridad")]
        public string prioridad { get; set; }

        public int idEmpresa { get; set; }

        [Display(Name = "Empresa")]
        public string nombreEmpresa{ get; set; }

        [Display(Name = "Estado")]
        public string estado_ticket { get; set; }
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
    public enum Prioridad
    {
        Alta,
        Media,
        Baja
    }
}
