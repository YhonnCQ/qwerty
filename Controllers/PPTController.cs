using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoU1GrupoQWERTY.Models;

namespace ProyectoU1GrupoQWERTY.Controllers
{
    public class PPTController : Controller
    {
        // GET: PPT
        public ActionResult Index()
        {
            return View();
        }
        public  ActionResult JugadorVsPC(int id)
        {
            ClsPPT opcionjugador = new ClsPPT() { opcion = (Opcion)id };
            ClsPPT pcopcion = new ClsPPT() { opcion = (Opcion)PCopcion() };
            string str;
            str = GanaoPierde(opcionjugador, pcopcion, true);

            ViewBag.Opciones = "Jugador: " + opcionjugador.opcion.ToString() + "\nPC: " + pcopcion.opcion.ToString();
            ViewBag.Message = str;

            return View("Result");
        }
        public ActionResult PCvsPC()
        {
            ClsPPT pc1opcion = new ClsPPT() { opcion = PCopcion() };
            ClsPPT pc2opcion = new ClsPPT() { opcion = PCopcion() };
            string str = GanaoPierde(pc1opcion, pc2opcion, false);
            ViewBag.Opciones = "PC 1: " + pc1opcion.opcion.ToString() + "\nPC 2: " + pc2opcion.opcion.ToString();
            ViewBag.Message = str;
            return View("Result");
        }
        Random rnd = new Random();
        private Opcion PCopcion()
        {
            List<Opcion> opciones = new List<Opcion>();
            opciones.Add(Opcion.Piedra);
            opciones.Add(Opcion.Papel);
            opciones.Add(Opcion.Tijeras);
            int index = rnd.Next(0, 3);
            return opciones[index];
        }
        private string GanaoPierde(ClsPPT p1, ClsPPT p2, bool type)
        {
            //el parametro type es para regular el output adecuadamente, ya sea si es jugador vs pc o pc vs pc
            /*
              0. Piedra
              1. Papel
              2. Tijeras
              */
            /*De las opciones, cada una de las opciones solo puede perder con la siguiente, por ejemplo la piedra (index 0) pierde con la siguiente la cuál es papel (index 0+1) el módulo %3 es para las tijeras, ya que no tiene indice siguiente pero pierde contra la piedra (indice 0), así que si p1 = 2, y p2 = 0, se calcula*/

            string jugador = type ? "Jugador" : "PC";
            string output = "El ganador es ";
            if ((int)p2.opcion == (int)(p1.opcion + 1) % 3) output += "PC 2";
            else if ((int)p1.opcion == (int)(p2.opcion + 1) % 3) output += $"{jugador} 1";
            else output = "Es un empate";
            return output;
        }
    }
}