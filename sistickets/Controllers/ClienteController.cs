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
    public class ClienteController : Controller
    {
        ticketEntities bd;
        public ClienteController()
        {
            bd = new ticketEntities();
        }
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Guardar(ClienteCLS oClienteCLS)
        {
            int nRegistrosEncontrados = 0;
            string nombre_usuario = oClienteCLS.usuario;
            nRegistrosEncontrados = bd.cliente.Where(p => p.nombre_usuario.Equals(nombre_usuario)).Count();
            if (!ModelState.IsValid || nRegistrosEncontrados>=1)
            {
                if (nRegistrosEncontrados >= 1) oClienteCLS.mensajeError = "El nombre de usuario ya existe, intente con otro";
                return View(oClienteCLS);
            }
            else
            {
                cliente oCliente = new cliente();
                oCliente.nombre_completo = oClienteCLS.nombre;
                oCliente.nombre_usuario = oClienteCLS.usuario;
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(oClienteCLS.clave);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                oCliente.clave = cadenaContraCifrada;
                oCliente.email_cliente = oClienteCLS.correo;
                bd.cliente.Add(oCliente);
                bd.SaveChanges();
            }
            return View();
        }
    }
}