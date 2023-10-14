using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoU1GrupoQWERTY.Models
{
    public enum Opcion { Piedra, Papel, Tijeras };
    public class ClsPPT
    {
        public Opcion opcion { get; set; }
    }
}