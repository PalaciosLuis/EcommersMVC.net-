using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyEcomersMVC.Models
{
    public class Carrito
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal importe { get {

                return precio * cantidad;
            } 
        }

    }
}