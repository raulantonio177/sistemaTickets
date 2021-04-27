using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistickets.Models;

namespace sistickets.Controllers
{
    public class TicketController : Controller
    {
        ticketEntities bd;

        public TicketController()
        {
            bd = new ticketEntities();
        }
        // GET: Ticket
        public ActionResult Index(TicketCLS oTicketCLS)
        {
            List<TicketCLS> listaTickets = null;
            if (oTicketCLS.estado_ticket == null)
            {
                listaTickets = (from ticket in bd.ticket
                                    select new TicketCLS
                                    {
                                        id = ticket.id,
                                        fecha = (DateTime)ticket.fecha,
                                        serie = ticket.serie,
                                        estado_ticket = ticket.estado_ticket,
                                        nombre_usuario = ticket.nombre_usuario,
                                        email_cliente = ticket.email_cliente,
                                        asunto = ticket.asunto,
                                        mensaje = ticket.mensaje,
                                        solucion = ticket.solucion,
                                        prioridad = ticket.prioridad
                                    }).ToList();
            }
            else if (oTicketCLS.estado_ticket == "Pendiente")
            {
                listaTickets = (from ticket in bd.ticket
                                where ticket.estado_ticket == "Pendiente"
                                orderby ticket.prioridad
                                select new TicketCLS
                                {
                                    id = ticket.id,
                                    fecha = (DateTime)ticket.fecha,
                                    serie = ticket.serie,
                                    estado_ticket = ticket.estado_ticket,
                                    nombre_usuario = ticket.nombre_usuario,
                                    email_cliente = ticket.email_cliente,
                                    asunto = ticket.asunto,
                                    mensaje = ticket.mensaje,
                                    solucion = ticket.solucion,
                                    prioridad = ticket.prioridad
                                }).ToList();
            }
            else if (oTicketCLS.estado_ticket == "En Proceso")
            {
                listaTickets = (from ticket in bd.ticket
                                where ticket.estado_ticket == "En Proceso"
                                select new TicketCLS
                                {
                                    id = ticket.id,
                                    fecha = (DateTime)ticket.fecha,
                                    serie = ticket.serie,
                                    estado_ticket = ticket.estado_ticket,
                                    nombre_usuario = ticket.nombre_usuario,
                                    email_cliente = ticket.email_cliente,
                                    asunto = ticket.asunto,
                                    mensaje = ticket.mensaje,
                                    solucion = ticket.solucion,
                                    prioridad = ticket.prioridad
                                }).ToList();
            }
            else if (oTicketCLS.estado_ticket == "Resuelto")
            {
                listaTickets = (from ticket in bd.ticket
                                where ticket.estado_ticket == "Resuelto"
                                select new TicketCLS
                                {
                                    id = ticket.id,
                                    fecha = (DateTime)ticket.fecha,
                                    serie = ticket.serie,
                                    estado_ticket = ticket.estado_ticket,
                                    nombre_usuario = ticket.nombre_usuario,
                                    email_cliente = ticket.email_cliente,
                                    asunto = ticket.asunto,
                                    mensaje = ticket.mensaje,
                                    solucion = ticket.solucion,
                                    prioridad = ticket.prioridad
                                }).ToList();
            }

            return View(listaTickets);
        }
        public ActionResult Soporte()
        {
            return View();
        }
        public ActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Guardar(TicketCLS oTicketCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(oTicketCLS);
            }
            else
            {
                //Generar numero de serie aleatorio del ticket
                Random rand = new Random();
                string codigo = "";
                int longitud = 2;
                for (int i = 0; i < longitud; i++)
                {
                    int numero = rand.Next(0, 9);
                    codigo += numero;
                }

                int numero_filas = (from ticket in bd.ticket select ticket).Count()+1;
                string serie = "TK" + codigo + "N" + numero_filas;


                ticket oTicket = new ticket();
                oTicket.fecha = DateTime.Now;
                oTicket.serie = serie;
                oTicket.nombre_usuario = oTicketCLS.nombre_usuario;
                oTicket.email_cliente = oTicketCLS.email_cliente;
                oTicket.departamento = oTicketCLS.departamento;
                oTicket.asunto = oTicketCLS.asunto;
                oTicket.mensaje = oTicketCLS.mensaje;
                oTicket.estado_ticket = "Pendiente";

                bd.ticket.Add(oTicket);
                bd.SaveChanges();
            }
            return View();
        }
    }
}