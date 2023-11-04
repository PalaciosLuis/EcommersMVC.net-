using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyEcomersMVC.Models;
using System.Data.SqlClient;

namespace ProyEcomersMVC.DAO
{
    public class ClientesDAO
    {


         public List<Clientes> ListarClientes()
        {


            var Lista = new List<Clientes>();

            SqlDataReader dr = DBHelper.RetornaReader("USP_LISTAR_CLIENTES");

            Clientes obj;

            while (dr.Read())
            {

                obj = new Clientes()
                {
                    cli_cod = dr.GetString(0),
                    cli_nom = dr.GetString(1)
                   


                };
                Lista.Add(obj);
            }

            dr.Close();

            return Lista;
        }
    }
}