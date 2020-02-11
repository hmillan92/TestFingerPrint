using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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

        public string CrearOperador(Operador oOperador)
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

                cmd.CommandText = "Insert into Operadores (empID, Huella) " +
                        "Values  ('" + oOperador.empID + "','" + oOperador.Huella + "')";

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

        public List<Operador> ListarOperadores([Optional] int pempID)
        {
            List<Operador> ListaOperadores = new List<Operador>();
            var ConClass = new DaConnectSQL();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = ConClass.DASQLConnection();
            cmd.CommandType = CommandType.Text;

            try
            {
                ConClass.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cmd.Connection;
                if (pempID <= 0)
                {
                    cmd.CommandText = "SELECT * FROM Operadores ";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM Operadores where empID =  '" + pempID + "'";
                }

                SqlDataReader dr = cmd.ExecuteReader();
            
                while (dr.Read())
                {
                    {
                        Operador OperadoresBd = new Operador();
                        OperadoresBd.empID = dr.GetInt32(0);
                        OperadoresBd.Huella = dr.GetString(1);

                        ListaOperadores.Add(OperadoresBd);
                    }
                }
                dr.Close();
            }

            catch (Exception ex)
            {
                string Mensaje = ex.Message;
            }
            return ListaOperadores;
        }
    }
}
