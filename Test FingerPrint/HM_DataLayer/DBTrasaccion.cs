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

        public string CrearOperador(Operadores ObjOperador)
        {
            string Respuesta;
            var ConClass = new DaConnectSQL();
            string huellaConvertida = Convert.ToBase64String(ObjOperador.Huella);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConClass.DASQLConnection();
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = ConClass.Tran;
                ConClass.Open();
                cmd.Transaction = ConClass.Con.BeginTransaction();

                cmd.CommandText = "Insert into Operadores2 (Nombre, Huella) " +
                        "Values  ('" + ObjOperador.Nombre + "','" + huellaConvertida + "')";

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

        public List<HuellaBd> ListarHuellas()
        {
            List<HuellaBd> ListaHuellas = new List<HuellaBd>();
            var ConClass = new DaConnectSQL();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = ConClass.DASQLConnection();
            cmd.CommandType = CommandType.Text;

            ConClass.Open();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cmd.Connection;

            cmd.CommandText = "SELECT * FROM Operadores2 ";



            SqlDataReader dr = cmd.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    {
                        HuellaBd OperadoresBd = new HuellaBd();
                        OperadoresBd.IdOperador = dr.GetInt32(0);
                        OperadoresBd.Nombre = dr.GetString(1);
                        OperadoresBd.Huella = dr.GetString(2);

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
