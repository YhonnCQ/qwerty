using ProyectoU1GrupoQWERTY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace ProyectoU1GrupoQWERTY.Controllers
{
    public class RuletaController : Controller
    {
        List<ClassEstudiante> listEstudiante;
        int limiteEstudiante = 0;
        int intentosTemp = 0;

        public RuletaController()
        {
            if (System.Web.HttpContext.Current.Session["listEstudiante"] == null)
            {
                listEstudiante = new List<ClassEstudiante>();
            }
            else
            {
                listEstudiante = System.Web.HttpContext.Current.Session["listEstudiante"] as List<ClassEstudiante>;
                limiteEstudiante = Convert.ToInt32(System.Web.HttpContext.Current.Session["limiteEstudiante"]);
                intentosTemp = Convert.ToInt32(System.Web.HttpContext.Current.Session["intentosTemp"]);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(listEstudiante);
        }

        [HttpPost]
        public ActionResult Index(ClassEstudiante estudiante)
        {
            if (Request.Form["GuardarLimite"] != null)
            {
                limiteEstudiante = Convert.ToInt32(Request.Form["limite"]);
                listEstudiante.Clear();
                System.Web.HttpContext.Current.Session["limiteMax"] = Convert.ToInt32(Request.Form["limite"]);
            }
            if (Request.Form["Guardar"] != null)
            {
                if (!estudiante.nombre.IsEmpty())
                {
                    if (limiteEstudiante > 0)
                    {
                        listEstudiante.Add(estudiante);
                        limiteEstudiante--;
                        System.Web.HttpContext.Current.Session["intentos"] = Convert.ToInt32(Request.Form["intentos"]);
                        System.Web.HttpContext.Current.Session["intentosTemp"] = Convert.ToInt32(Request.Form["intentos"]);
                    }
                }
            }
            if (Request.Form["Ruleta"] != null)
            {
                if (intentosTemp > 0)
                {
                    Random rnd = new Random();
                    int index = rnd.Next(listEstudiante.Count);
                    ViewData["Ganador"] = listEstudiante[index].nombre;
                    listEstudiante.RemoveAt(index);
                    intentosTemp--;
                    System.Web.HttpContext.Current.Session["intentosTemp"] = intentosTemp;
                }
            }
            System.Web.HttpContext.Current.Session["listEstudiante"] = listEstudiante;
            System.Web.HttpContext.Current.Session["limiteEstudiante"] = limiteEstudiante;
            return View(listEstudiante);
        }
    }
}