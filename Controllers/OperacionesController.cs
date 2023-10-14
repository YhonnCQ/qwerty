using ProyectoU1GrupoQWERTY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoU1GrupoQWERTY.Controllers
{
    public class OperacionesController : Controller
    {
        List<ClassAleatorio> listNumeros;

        public OperacionesController()
        {
            if (System.Web.HttpContext.Current.Session["listNumeros"] == null)
            {
                listNumeros = new List<ClassAleatorio>();
            }
            else
            {
                listNumeros = System.Web.HttpContext.Current.Session["listNumeros"] as List<ClassAleatorio>;
            }
        }
        // GET: Operaciones
        [HttpGet]
        public ActionResult Index()
        {
            return View(listNumeros);
        }

        [HttpPost]
        public ActionResult Index(List<ClassAleatorio> listNumero)
        {

            if (Request.Form["Generar"] != null)
            {
                listNumeros.Clear();
                Random rand = new Random();
                for (int i = 0; i < Convert.ToInt32(Request.Form["cantidad"]); i++)
                {
                    ClassAleatorio aleatorio = new ClassAleatorio();
                    aleatorio.numero = rand.Next(1000);
                    listNumeros.Add(aleatorio);
                }
            }
            if (Request.Form["Calcular"] != null)
            {
                if (listNumeros.Count > 0)
                {
                    int mayor = listNumeros.Max(x => Convert.ToInt32(x.numero));
                    int menor = listNumeros.Min(x => Convert.ToInt32(x.numero));
                    int suma = listNumeros.Sum(x => Convert.ToInt32(x.numero));
                    double promedio = suma / listNumeros.Count;

                    string operacion = Convert.ToString(Request.Form["opcion"]);

                    switch (operacion)
                    {
                        case "mayor":
                            ViewData["resultado"] = "El Mayor: " + mayor;
                            break;
                        case "menor":
                            ViewData["resultado"] = "El Menor: " + menor;
                            break;
                        case "suma":
                            ViewData["resultado"] = "El Suma: " + suma;
                            break;
                        case "promedio":
                            ViewData["resultado"] = "El Promedio: " + promedio;
                            break;
                    }
                }
            }
            System.Web.HttpContext.Current.Session["listNumeros"] = listNumeros;
            return View(listNumeros);
        }
    }
}