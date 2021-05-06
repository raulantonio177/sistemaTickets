using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using sistickets.Models;

namespace sistickets.Controllers
{
    public class AccesoController : Controller
    {
        ticketEntities bd;

        public AccesoController()
        {
            bd = new ticketEntities();
        }
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {

            try
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(pass);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                pass = cadenaContraCifrada;

                var oUser = (from usuario in bd.usuario
                                 where usuario.nombre_usuario == user.Trim() &&
                                 usuario.clave == pass.Trim()
                                 select usuario).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();
                    }

                    Session["User"] = oUser;
                    Session["id_rol"] = oUser.id_Rol;

                if (oUser.id_Rol == 1)
                {
                    return RedirectToAction("Soporte", "Ticket");
                }
                else
                {
                    return RedirectToAction("Index", "Ticket");
                }

                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            
        }
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
                try
                {
                    usuario oUsuario = new usuario();
                    oUsuario.nombre_completo = oUsuarioCLS.nombre;
                    oUsuario.nombre_usuario = oUsuarioCLS.usuario;
                    SHA256Managed sha = new SHA256Managed();
                    byte[] byteContra = Encoding.Default.GetBytes(oUsuarioCLS.clave);
                    byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                    string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                    oUsuario.clave = cadenaContraCifrada;
                    oUsuario.email_cliente = oUsuarioCLS.correo;
                    oUsuario.id_Rol = 1;
                    bd.usuario.Add(oUsuario);
                    bd.SaveChanges();
                    ViewBag.Alert = "Ha sido agregado con exito, inicie sesion";
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