using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyEcomersMVC.Models
{
    public class Clientes
    {

        public string cli_cod { get; set; }
        public string cli_nom {  get; set; }
        public string cli_tel { get; set; }
        public string cli_cor { get; set; }
        public string cli_dir { get; set; }
        public decimal cli_cre { get; set; }
    }
}