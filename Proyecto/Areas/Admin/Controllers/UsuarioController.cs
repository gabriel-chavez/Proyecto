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
    [Autenticado]
    public class UsuarioController : Controller
    {
        private Usuario usuario = new Usuario();
        private TablaDato dato = new TablaDato();
        // GET: Admin/Usuario
        public ActionResult Index()
        {
            ViewBag.paises = dato.Listar("pais");
            return View(usuario.Obtener(SessionHelper.GetUser()));
        }
        public JsonResult Guardar(Usuario model,HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();
            //quitamos la validacion de password
            ModelState.Remove("Password");
            if(ModelState.IsValid)
            {
                rm = model.Guardar(Foto);
            }
            return Json(rm);
        }
    }
}