using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyEcomersMVC.Models;
using System.Data.SqlClient;

namespace ProyEcomersMVC.DAO
{
    public class ArticulosDAO
    {


        public List<Articulos> ListarArticulos(string nom)
        {


            var Lista = new List<Articulos>();

            SqlDataReader dr = DBHelper.RetornaReader("USP_LISTAR_ARTICULOS", nom);

            Articulos obj;

            while (dr.Read())
            {

                obj = new Articulos()
                {
                    art_cod = dr.GetString(0),
                    art_nom = dr.GetString(1),
                    
                    art_pre = dr.GetDecimal(2),
                    art_stk = dr.GetInt32(3)


                };
                Lista.Add(obj);
            }

            dr.Close();

            return Lista;
        }



    }
}