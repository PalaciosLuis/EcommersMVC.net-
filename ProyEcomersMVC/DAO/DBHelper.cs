using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration; // Leer la cadena de conexion
using System.Data;
using System.Data.SqlClient;


    public class DBHelper
    {
        private static string cad_cn =
            ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;

        // Para Procedimientos Almacenados Insert, Update y/o Delete
        // Que no devuelvan filas
        public static void EjecutarSP(string NombreSP, 
                          params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();
            //
            SqlCommand comando = new SqlCommand(NombreSP, cnx);
            comando.CommandType = CommandType.StoredProcedure;
            //
            if (Parametros.Length > 0)
            {
                LlenarParametrosSP(comando, Parametros);
            }
            //
            comando.ExecuteNonQuery();
            //
            cnx.Close();
        }



        public static void EjecutarSP_TRX(string NombreSP,
                          params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();
            //
            // definir el objeto SqlTransaction
            SqlTransaction trx = cnx.BeginTransaction();

            try
            {
                SqlCommand comando = 
                    new SqlCommand(NombreSP, cnx, trx);
                comando.CommandType = CommandType.StoredProcedure;
                //
                if (Parametros.Length > 0)
                {
                    LlenarParametrosSP(comando, Parametros);
                }
                //
                comando.ExecuteNonQuery();
                // sino se produjo alguna excepción,
                // entonces confirmamos la transacción
                trx.Commit();
            }
            catch (Exception ex)
            { 
                trx.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                cnx.Close();
            }
        }


        // Para Procedimientos Almacenados de Consultas: Select
        // Donde se devuelvan muchas filas y columnas
        public static SqlDataReader RetornaReader(string NombreSP,
                                       params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();
            //
            SqlCommand comando = new SqlCommand(NombreSP, cnx);
            comando.CommandType = CommandType.StoredProcedure;
            //
            if (Parametros.Length > 0)
            {
                LlenarParametrosSP(comando, Parametros);
            }
            //
            SqlDataReader lector = 
                comando.ExecuteReader(CommandBehavior.CloseConnection);
            //
            return lector;
        }



        // Para Procedimientos Almacenados de Consultas: Select
        // Donde sólo se devuelva 1 fila y 1 columna
        public static object RetornaScalar(string NombreSP,
                                params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();
            //
            SqlCommand comando = new SqlCommand(NombreSP, cnx);
            comando.CommandType = CommandType.StoredProcedure;
            //
            if (Parametros.Length > 0) {
                LlenarParametrosSP(comando, Parametros);
            }
            //
            object respuesta = comando.ExecuteScalar();
            //
            cnx.Close();
            //
            return respuesta;
        }

        private static void LlenarParametrosSP(SqlCommand cmd,
                                 params object[] argumentos)
        {
            int indice = 0;
            // descubiriendo y creando los parámetros que el
            // procedimiento almacenado de un sqlcommand necesita
            SqlCommandBuilder.DeriveParameters(cmd);

            foreach (SqlParameter prm in cmd.Parameters)
            {
                if (prm.ParameterName != "@RETURN_VALUE")
                {
                    prm.Value = argumentos[indice];
                    indice++;
                }
            }

        }


    }
