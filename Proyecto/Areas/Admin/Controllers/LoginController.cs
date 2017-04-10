using Helper;
using Proyecto.Areas.Admin.Filters;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto.Areas.Admin.Controllers
{
    
    public class LoginController : Controller
    {
        // GET: Admin/Login
        private Usuario usuario = new Usuario();
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Acceder(string Email, string Password)
        {
            var rm = usuario.Acceder(Email, Password);
            if(rm.response)
            {
                rm.href = Url.Content("~/admin/usuario");
            }
            return Json(rm);
        }
        public ActionResult Logout() 
        {
            // Eliminar la sesion actual
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }
    }
}