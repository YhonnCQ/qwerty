using ProyectoU1GrupoQWERTY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoU1GrupoQWERTY.Controllers
{
    public class CajeroController : Controller
    {
        List<ClassDinero> listDinero;
        List<ClassMovimiento> listMovimiento;

        public CajeroController()
        {
            if (System.Web.HttpContext.Current.Session["dinero"] == null)
            {
                listDinero = new List<ClassDinero>();
                ClassDinero soles200 = new ClassDinero();
                soles200.nombre = 200;
                soles200.cantidad = 0;
                soles200.limite = 100;
                listDinero.Add(soles200);
                ClassDinero soles100 = new ClassDinero();
                soles100.nombre = 100;
                soles100.cantidad = 0;
                soles100.limite = 100;
                listDinero.Add(soles100);
                ClassDinero soles50 = new ClassDinero();
                soles50.nombre = 50;
                soles50.cantidad = 0;
                soles50.limite = 100;
                listDinero.Add(soles50);
                ClassDinero soles20 = new ClassDinero();
                soles20.nombre = 20;
                soles20.cantidad = 0;
                soles20.limite = 100;
                listDinero.Add(soles20);
                ClassDinero soles10 = new ClassDinero();
                soles10.nombre = 10;
                soles10.cantidad = 0;
                soles10.limite = 100;
                listDinero.Add(soles10);
            }
            else
            {
                listDinero = System.Web.HttpContext.Current.Session["dinero"] as List<ClassDinero>;

            }
            if (System.Web.HttpContext.Current.Session["movimiento"] == null)
            {
                listMovimiento = new List<ClassMovimiento>();
            }
            else 
            {
                listMovimiento = System.Web.HttpContext.Current.Session["movimiento"] as List<ClassMovimiento>;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int monto)
        {
            List<ClassDinero> retiro = new List<ClassDinero>();
            int dineroCajero = listDinero.Sum(x => x.nombre * x.cantidad);
            if (dineroCajero >= monto)
            {
                ClassMovimiento movimiento = new ClassMovimiento();
                movimiento.tipo = "RETIRO";
                movimiento.fecha = DateTime.Now.ToString("yyyy MM dd HH:mm:ss");
                movimiento.importe = 0;
                int res = monto;
                foreach (var item in listDinero)
                {
                    int cantidadDescontar = res / item.nombre;
                    if (item.cantidad >= cantidadDescontar)
                    {
                        ClassDinero dineroRetiro = new ClassDinero();
                        dineroRetiro.nombre = item.nombre;
                        dineroRetiro.cantidad = cantidadDescontar;
                        item.cantidad -= cantidadDescontar;
                        retiro.Add(dineroRetiro);
                        res = res % item.nombre;
                        movimiento.importe += cantidadDescontar * item.nombre;
                    }
                }
                if (movimiento.importe > 0)
                {
                    listMovimiento.Add(movimiento);
                }
            }
            System.Web.HttpContext.Current.Session["movimiento"] = listMovimiento;
            System.Web.HttpContext.Current.Session["dinero"] = listDinero;
            return View(retiro);
        }

        [HttpGet]
        public ActionResult Cargar()
        {
            return View(listDinero);
        }

        [HttpPost]
        public ActionResult Cargar(ClassDinero dinero)
        {
            int importe = 0;
            foreach (var item in listDinero)
            {
                item.cantidad += Convert.ToInt32(Request[item.nombre.ToString()]);
                importe += item.nombre * Convert.ToInt32(Request[item.nombre.ToString()]);
            }
            if (importe > 0)
            {
                ClassMovimiento movimiento = new ClassMovimiento();
                movimiento.tipo = "DEPOSITO";
                movimiento.fecha = DateTime.Now.ToString("yyyy MM dd HH:mm:ss");
                movimiento.importe = importe;
                listMovimiento.Add(movimiento);
            }
            System.Web.HttpContext.Current.Session["movimiento"] = listMovimiento;
            System.Web.HttpContext.Current.Session["dinero"] = listDinero;
            return View(listDinero);
        }

        public ActionResult Movimientos()
        {
            return View(listMovimiento);
        }
    }
}