using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_DataLayer
{
    public class DBTrasaccion
    {
        public void DACliente()
        {
            DaConnectSQL DASQLConnection = new DaConnectSQL();
        }

        public bool ValidaConexionSQL()
        {
            bool Exitosa = false;
            var ConClass = new DaConnectSQL();

            ConClass.Open();

            if (ConClass.Con.State == ConnectionState.Open)
                Exitosa = true;

            return Exitosa;
        }

        public string CrearOperador(OperadorHString ObjOperador)
        {
            string Respuesta;
            var ConClass = new DaConnectSQL();
            
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConClass.DASQLConnection();
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = ConClass.Tran;
                ConClass.Open();
                cmd.Transaction = ConClass.Con.BeginTransaction();

                cmd.CommandText = "Insert into Operadores (Nombre, CodOperador, Huella) " +
                        "Values  ('" + ObjOperador.Nombre + "','" + ObjOperador.CodOperador + "','" + ObjOperador.Huella + "')";

                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
                Respuesta = "Registro creado";
            }

            catch (Exception ex)
            {
                ConClass.RollBackTransaction();
                Respuesta = ex.Message;
            }

            finally
            {
                ConClass.Close();
            }

            return Respuesta;
        }

        public List<OperadorHString> ListarHuellas()
        {
            List<OperadorHString> ListaHuellas = new List<OperadorHString>();
            var ConClass = new DaConnectSQL();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = ConClass.DASQLConnection();
            cmd.CommandType = CommandType.Text;

            ConClass.Open();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cmd.Connection;

            cmd.CommandText = "SELECT * FROM Operadores ";

            SqlDataReader dr = cmd.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    {
                        OperadorHString OperadoresBd = new OperadorHString();
                        OperadoresBd.IdOperador = dr.GetInt32(0);
                        OperadoresBd.CodOperador = dr.GetString(1);
                        OperadoresBd.Nombre = dr.GetString(2);
                        OperadoresBd.Huella = dr.GetString(3);

                        ListaHuellas.Add(OperadoresBd);
                    }
                }
                dr.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error en la transaccion " + ex.Message);
            }
            return ListaHuellas;
        }
    }
}
