using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sistickets.Models
{
    public class UsuarioCLS
    {
        [Display(Name="Id Cliente")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Nombre completo")]
        [StringLength(100, ErrorMessage ="El nombre completo debe tener menos de 100 caracteres")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Nombre de usuario")]
        [StringLength(100, ErrorMessage = "El nombre de usuario debe tener menos de 100 caracteres")]
        public string usuario { get; set; }

        [Required]
        [Display(Name = "Correo")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo no es válido")]
        public string correo { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string clave { get; set; }

        //propiedad para mostrar error
        public string mensajeError { get; set; }
    }
}