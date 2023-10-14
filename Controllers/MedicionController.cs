using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoU1GrupoQWERTY.Controllers
{
    public class MedicionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(double txtpul)
        {
            if (txtpul > 0)
            {
                ViewBag.mili = txtpul * 25.4 + " mm";
                ViewBag.pul = txtpul;
            }
            return View();
        }
    }
}