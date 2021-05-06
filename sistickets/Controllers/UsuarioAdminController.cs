using sistickets.Filters;
using sistickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace sistickets.Controllers
{
    public class UsuarioAdminController : Controller
    {
        ticketEntities bd;

        public UsuarioAdminController()
        {
            bd = new ticketEntities();
        }

        [AuthorizeUser(idOperacion: 7)]
        public ActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Guardar(UsuarioCLS oUsuarioCLS)
        {
            int nRegistrosEncontrados = 0;
            string nombre_usuario = oUsuarioCLS.usuario;
            nRegistrosEncontrados = bd.usuario.Where(p => p.nombre_usuario.Equals(nombre_usuario)).Count();
            if (!ModelState.IsValid || nRegistrosEncontrados >= 1)
            {
                if (nRegistrosEncontrados >= 1) oUsuarioCLS.mensajeError = "El nombre de usuario ya existe, intente con otro";
                return View(oUsuarioCLS);
            }
            else
            {
                try{
                    usuario oUsuario = new usuario();
                    oUsuario.nombre_completo = oUsuarioCLS.nombre;
                    oUsuario.nombre_usuario = oUsuarioCLS.usuario;
                    SHA256Managed sha = new SHA256Managed();
                    byte[] byteContra = Encoding.Default.GetBytes(oUsuarioCLS.clave);
                    byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                    string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                    oUsuario.clave = cadenaContraCifrada;
                    oUsuario.email_cliente = oUsuarioCLS.correo;
                    oUsuario.id_Rol = 2;
                    bd.usuario.Add(oUsuario);
                    bd.SaveChanges();
                    ViewBag.Alert = "Usuario agregado con exito";
                }
                catch (Exception)
                {
                    ViewBag.Alert = "Ocurrio un error";
                }
            }
            return View();
        }
    }
}