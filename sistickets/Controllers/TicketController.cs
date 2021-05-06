using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using sistickets.Filters;
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

        List<SelectListItem> listaEstados;
        public void listarEstados()
        {
            listaEstados = (from estado in bd.estado_ticket
                            select new SelectListItem
                            {
                                Text = estado.nombre,
                                Value = estado.id_estado_ticket.ToString()
                            }).ToList();
            listaEstados.Insert(0, new SelectListItem { Text = "-----SELECCIONA-----", Value = "" });
            ViewBag.listaEstados = listaEstados;
        }
        // GET: Ticket
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult Index()
        {
            listarEstados();
            List<TicketCLS> listaTickets = null;
            listaTickets = (from ticket in bd.ticket
                            join empresa in bd.empresa
                            on ticket.id_empresa equals empresa.id
                            join estado_ticket in bd.estado_ticket
                            on ticket.id_estado_ticket equals estado_ticket.id_estado_ticket
                            select new TicketCLS
                            {
                                id = ticket.id,
                                fecha = (DateTime)ticket.fecha,
                                serie = ticket.serie,
                                estado_ticket = estado_ticket.nombre,
                                nombre_usuario = ticket.nombre_usuario,
                                email_cliente = ticket.email_cliente,
                                asunto = ticket.asunto,
                                mensaje = ticket.mensaje,
                                solucion = ticket.solucion,
                                prioridad = ticket.prioridad,
                                departamento = ticket.departamento,
                                nombreEmpresa = empresa.nombre
                            }).ToList(); 
            return View(listaTickets);
    }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Filtrar(int idEstado)
        {
            List<TicketCLS> listaTickets = null;
            if (idEstado==0)
            {
                listaTickets = (from ticket in bd.ticket
                                join empresa in bd.empresa
                                on ticket.id_empresa equals empresa.id
                                join estado_ticket in bd.estado_ticket
                                on ticket.id_estado_ticket equals estado_ticket.id_estado_ticket
                                select new TicketCLS
                                {
                                    id = ticket.id,
                                    fecha = (DateTime)ticket.fecha,
                                    serie = ticket.serie,
                                    estado_ticket = estado_ticket.nombre,
                                    nombre_usuario = ticket.nombre_usuario,
                                    email_cliente = ticket.email_cliente,
                                    asunto = ticket.asunto,
                                    mensaje = ticket.mensaje,
                                    solucion = ticket.solucion,
                                    prioridad = ticket.prioridad,
                                    departamento = ticket.departamento,
                                    nombreEmpresa = empresa.nombre
                                }).ToList();
            }
            else if (idEstado==1)
            {
                listaTickets = (from ticket in bd.ticket
                                join empresa in bd.empresa
                                on ticket.id_empresa equals empresa.id
                                join estado_ticket in bd.estado_ticket
                                on ticket.id_estado_ticket equals estado_ticket.id_estado_ticket
                                where ticket.id_estado_ticket == 1
                                orderby ticket.prioridad
                                select new TicketCLS
                                {
                                    id = ticket.id,
                                    fecha = (DateTime)ticket.fecha,
                                    serie = ticket.serie,
                                    estado_ticket = estado_ticket.nombre,
                                    nombre_usuario = ticket.nombre_usuario,
                                    email_cliente = ticket.email_cliente,
                                    asunto = ticket.asunto,
                                    mensaje = ticket.mensaje,
                                    solucion = ticket.solucion,
                                    prioridad = ticket.prioridad,
                                    departamento = ticket.departamento,
                                    nombreEmpresa = empresa.nombre
                                }).ToList();
            }
            else if (idEstado==2)
            {
                listaTickets = (from ticket in bd.ticket
                                join empresa in bd.empresa
                                on ticket.id_empresa equals empresa.id
                                join estado_ticket in bd.estado_ticket
                                on ticket.id_estado_ticket equals estado_ticket.id_estado_ticket
                                where ticket.id_estado_ticket == 2
                                select new TicketCLS
                                {
                                    id = ticket.id,
                                    fecha = (DateTime)ticket.fecha,
                                    serie = ticket.serie,
                                    estado_ticket = estado_ticket.nombre,
                                    nombre_usuario = ticket.nombre_usuario,
                                    email_cliente = ticket.email_cliente,
                                    asunto = ticket.asunto,
                                    mensaje = ticket.mensaje,
                                    solucion = ticket.solucion,
                                    prioridad = ticket.prioridad,
                                    departamento = ticket.departamento,
                                    nombreEmpresa = empresa.nombre
                                }).ToList();
            }
            else if (idEstado==3)
            {
                listaTickets = (from ticket in bd.ticket
                                join empresa in bd.empresa
                                on ticket.id_empresa equals empresa.id
                                join estado_ticket in bd.estado_ticket
                                on ticket.id_estado_ticket equals estado_ticket.id_estado_ticket
                                where ticket.id_estado_ticket == 3
                                select new TicketCLS
                                {
                                    id = ticket.id,
                                    fecha = (DateTime)ticket.fecha,
                                    serie = ticket.serie,
                                    estado_ticket = estado_ticket.nombre,
                                    nombre_usuario = ticket.nombre_usuario,
                                    email_cliente = ticket.email_cliente,
                                    asunto = ticket.asunto,
                                    mensaje = ticket.mensaje,
                                    solucion = ticket.solucion,
                                    prioridad = ticket.prioridad,
                                    departamento = ticket.departamento,
                                    nombreEmpresa = empresa.nombre
                                }).ToList();
            }

            return  PartialView("_TablaTicket",listaTickets);
        }

        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Soporte()
        {
            return View();
        }
        List<SelectListItem> lista;
        public void listarEmpresas()
        {
            lista = (from empresa in bd.empresa
                                          select new SelectListItem
                                          {
                                              Text = empresa.nombre,
                                              Value = empresa.id.ToString()
                                          }).ToList();
            lista.Insert(0, new SelectListItem { Text = "-----SELECCIONA-----", Value = "" });
            ViewBag.listaEmpresas = lista;
        }
        [HttpGet]
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Guardar()
        {
            listarEmpresas();
            ViewBag.listaEmpresas = lista;
            return View();
        }

        [HttpPost]
        public ActionResult Guardar(TicketCLS oTicketCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    listarEmpresas();
                    ViewBag.listaEmpresas = lista;
                    return View(oTicketCLS);
                }
                else
                {
                    listarEmpresas();
                    ViewBag.listaEmpresas = lista;
                    //Generar numero de serie aleatorio del ticket
                    Random rand = new Random();
                    string codigo = "";
                    int longitud = 2;
                    for (int i = 0; i < longitud; i++)
                    {
                        int numero = rand.Next(0, 9);
                        codigo += numero;
                    }

                    int numero_filas = (from ticket in bd.ticket select ticket).Count() + 1;
                    string serie = "TK" + codigo + "N" + numero_filas;


                    ticket oTicket = new ticket();
                    oTicket.fecha = DateTime.Now;
                    oTicket.serie = serie;
                    oTicket.nombre_usuario = oTicketCLS.nombre_usuario;
                    oTicket.email_cliente = oTicketCLS.email_cliente;
                    oTicket.departamento = oTicketCLS.departamento;
                    if (oTicketCLS.idEmpresa != 0)
                    {
                        oTicket.id_empresa = oTicketCLS.idEmpresa;
                    }
                    else
                    {
                        oTicket.id_empresa = null;
                    }
                    oTicket.asunto = oTicketCLS.asunto;
                    oTicket.mensaje = oTicketCLS.mensaje;
                    oTicket.id_estado_ticket = 1;

                    bd.ticket.Add(oTicket);
                    bd.SaveChanges();

                    var message = new MailMessage
                    {
                        Subject = oTicket.asunto,
                        Body = "¡Gracias por reportarnos su problema! estimad@ "+ oTicketCLS.nombre_usuario+" Buscaremos una solución para su servicio lo mas pronto posible. Su ID ticket es: " + oTicket.serie,
                        IsBodyHtml = true
                    };

                    message.From = new MailAddress("soportedotechmty@gmail.com", "Soporte Dotech");
                    message.To.Add(oTicket.email_cliente);


                    var client = new SmtpClient
                    {
                        EnableSsl = true//Si es SSL
                    };

                    client.Send(message);
                    message.Dispose();
                    client.Dispose();

                    ViewBag.Alert = "Ticket agregado con exito";
                }
            }
            catch (Exception)
            {
                ViewBag.Alert = "Ocurrio un error";
            }
            return View();
        }

        [HttpGet]
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Editar(int id)
        {
            listarEstados();
            ViewBag.listaEstados = listaEstados;
            TicketCLS oTicketCLS = new TicketCLS();
            ticket oTicket = bd.ticket.Where(p => p.id.Equals(id)).First();

            oTicketCLS.idEstado = (int)oTicket.id_estado_ticket;
            oTicketCLS.prioridad = oTicket.prioridad;
            oTicketCLS.solucion = oTicket.solucion;

            return View(oTicketCLS);
        }

        [HttpPost]
        public ActionResult Editar(TicketCLS oTicketCLS)
        {
            int id = oTicketCLS.id;
                if (!ModelState.IsValid)
                {
                    listarEstados();
                    ViewBag.listaEstados = listaEstados;
                return View(oTicketCLS);
                }
                else
                {
                    listarEstados();
                    ViewBag.listaEstados = listaEstados;
                ticket oTicket = bd.ticket.Where(p => p.id.Equals(id)).First();
                    oTicket.id_estado_ticket = oTicketCLS.idEstado;
                    oTicket.prioridad = oTicketCLS.prioridad;
                    oTicket.solucion = oTicketCLS.solucion;
                    bd.SaveChanges();

                    if (oTicketCLS.idEstado == 2)
                    {
                        var message = new MailMessage
                        {
                            Subject = oTicket.asunto,
                            Body = "Buen dia estimad@ " + oTicket.nombre_usuario + " su reporte ha comenzado a estar en proceso, pronto le daremos una solucion",
                            IsBodyHtml = true
                        };

                        message.From = new MailAddress("soportedotechmty@gmail.com", "Soporte Dotech");
                        message.To.Add(oTicket.email_cliente);


                        var client = new SmtpClient
                        {
                            EnableSsl = true//Si es SSL
                        };

                        client.Send(message);
                        message.Dispose();
                        client.Dispose();

                    }
                    else if (oTicketCLS.idEstado == 3)
                    {
                        var message = new MailMessage
                        {
                            Subject = oTicket.asunto,
                            Body = "Buen dia estimad@ " + oTicket.nombre_usuario + " la solucion a su problema es: " + oTicket.solucion,
                            IsBodyHtml = true
                        };

                        message.From = new MailAddress("soportedotechmty@gmail.com", "Soporte Dotech");
                        message.To.Add(oTicket.email_cliente);


                        var client = new SmtpClient
                        {
                            EnableSsl = true//Si es SSL
                        };

                        client.Send(message);
                        message.Dispose();
                        client.Dispose();
                    }
                }
            return RedirectToAction("Index");
        }
    }
}