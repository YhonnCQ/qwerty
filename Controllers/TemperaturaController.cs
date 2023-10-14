using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoU1GrupoQWERTY.Controllers
{
    public class TemperaturaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(double txtce)
        {
            if (txtce > 0)
            {
                ViewBag.fa = (txtce * 9 / 5) + 32 + " °F";
                ViewBag.ce = txtce + " °C";
            }
            return View();
        }
    
    }
}