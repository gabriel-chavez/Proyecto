using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Proyecto.App_Start;
using Proyecto.ViewModels;
using System.Net.Mail;

namespace Proyecto.Controllers
{
    public class DefaultController : Controller
    {
        private Usuario usuario = new Usuario();
        // GET: Default
        public ActionResult Index()
        {

            return View(usuario.Obtener(FrontOfficeStartUp.UsuarioVizualizado(), true));
        }
        public JsonResult EnviarCorreo(ContactoViewModel model)
        {

            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                try
                {
                    var _usuario = usuario.Obtener(FrontOfficeStartUp.UsuarioVizualizado());
                    var mail = new MailMessage();
                    mail.From = new MailAddress(model.Correo, model.Nombre);
                    mail.To.Add(_usuario.Email);
                    mail.Subject = "Correo desde contacto";
                    mail.IsBodyHtml = true;
                    mail.Body = model.Mensaje;

                    var SmtpServer = new SmtpClient("smtp.live.com");/// o smtp.gmail.com
                    SmtpServer.Port = 587;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("g_lito@hotmail.com", "aknelim");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                }
                catch (Exception e)
                {
                    rm.SetResponse(false, e.Message);
                    return Json(rm);
                    throw;
                }
                rm.SetResponse(true);
                rm.function = "CerrarContacto();";

            }
            return Json(rm);
        }
        public JsonResult Comentar(Testimonio testimonio)
        {
            var rm = new ResponseModel();
            if(ModelState.IsValid)
            {
                rm = testimonio.Guardar();
                if (rm.response) rm.message = "Gracias por comentar";
            }
            return Json(rm);
        }
        /******************EXPORTAR A PDF*****************/
        public ActionResult ExportaAPDF()
        {
            return new Rotativa.MVC.ActionAsPdf("PDF"); //renderiza la accion PDF---(BUSCA PUBLIC ACTIONRESULT PDF())
        }
        public ActionResult PDF()
        {
            return View(usuario.Obtener(FrontOfficeStartUp.UsuarioVizualizado(), true));
        }
        /******************FIN EXPORTAR A PDF*****************/
    }
}