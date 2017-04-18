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
    public class TestimoniosController : Controller
    {
        public Testimonio testimonio = new Testimonio();
        public TablaDato dato = new TablaDato();
        // GET: Admin/Testimonios
        public ActionResult Index()
        {
            ViewBag.estados = dato.Listar("testimonioestado");
            return View(testimonio.Listar());
        }
        public ActionResult Crud(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.estados = dato.Listar("testimonioestado");
                testimonio.Usuario_id = SessionHelper.GetUser();
            }
            else
            {
                ViewBag.estados = dato.Listar("testimonioestado");
                testimonio = testimonio.Obtener(id);
            }

            return View(testimonio);
        }
        public JsonResult Guardar(Testimonio model)
        {
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/testimonios/");
                }
            }
            return Json(rm);
        }

        public ActionResult Eliminar(int id)
        {
            var rm = testimonio.Eliminar(id);

            return Redirect("~/admin/testimonios/");
        }
    }
}