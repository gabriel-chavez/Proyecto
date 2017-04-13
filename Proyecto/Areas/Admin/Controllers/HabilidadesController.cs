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
    public class HabilidadesController : Controller
    {
        private Habilidad habilidad = new Habilidad(); 
        // GET: Admin/Habilidades
        public ActionResult Index()
        {
            return View(habilidad.Listar());
        }
        public ActionResult Crud(int id = 0)
        {
            if (id == 0)
            {
                
                habilidad.Usuario_id = SessionHelper.GetUser();
            }
            else
            {
                habilidad = habilidad.Obtener(id);
            }

            return View(habilidad);
        }
        public JsonResult Guardar(Habilidad model)
        {
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/habilidades/");
                }
            }
            return Json(rm);
        }
        //public JsonResult Eliminar(int id)
        //{
        //    var rm = habilidad.Eliminar(id);
            
        //    return Json(rm);
        //}
        public ActionResult Eliminar(int id)
        {
            var rm = habilidad.Eliminar(id);

            return Redirect("~/admin/habilidades/");
        }
    }

}