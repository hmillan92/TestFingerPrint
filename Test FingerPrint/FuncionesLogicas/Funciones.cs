using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPUruNet;
using Entidades;
using HM_DataLayer;

namespace FuncionesLogicas
{

    public class Funciones
    {
        DBTrasaccion transaccion = new DBTrasaccion();
        Helper oHelper = new Helper();
        private const int PROBABILITY_ONE = 0x7fffffff;
              

        public bool ValidaConexionSQL()
        {          
            bool Exitosa = transaccion.ValidaConexionSQL();
            return Exitosa;
        }

        public string CrearOperador(Fmd presult, string pcodOperador, string pnombre,string pstatus)
        {
            Operador oOperador;
            string Mensaje;
            string huellaConvertida;

            oOperador = CompararHuella(presult);

            if (oOperador.IdOperador == 0)
            {
                huellaConvertida = Convert.ToBase64String(presult.Bytes);

                oOperador = new Operador();
                oOperador.CodOperador = pcodOperador;
                oOperador.Nombre = pnombre;
                oOperador.Huella = huellaConvertida;
                oOperador.Status = pstatus;

                Mensaje = transaccion.CrearOperador(oOperador);
            }

            else
            {
                Mensaje = "Ya existe un operador registrado con esta huella";
            }
          
            return Mensaje;
        }


        public List<Operador> ListarHuellas()
        {
            List<Operador> listarOp = transaccion.ListarHuellas();

            return listarOp;
        }


        public Operador CompararHuella(Fmd pFirstFinger)
        {
            ///Añadimos el parametro firstF a firstFinger un objeto de tipo Fmd que sera el objeto de la huella capturada
            Fmd firstFinger = pFirstFinger;

            List<Operador> listaOp = transaccion.ListarHuellas();
            Operador opEncontrado = null;

            ///un foreach para capturar los valores de cada registro en la BD
            foreach (var item in listaOp)
            {
                byte[] huellaConvertida = Convert.FromBase64String(item.Huella);

                Fmd secondFinger = new Fmd(huellaConvertida, oHelper.Format, oHelper.Version);

                ///Una vez listo los 2 objetos a comparar se lo mandamos al metodo Compare de la clase Comparision, nos devolvera un objeto de tipo CompareResult
                ///Este objeto compareResult tendra una propiedad que se llama Score que es el puntaje que necesitaremos para ver si ambas huellas tienen similitud
                CompareResult compareResult = Comparison.Compare(firstFinger, 0, secondFinger, 0);

                ///preguntamos si el Score es menor a la operacion establecida en el if, si el resultado es true entonces ambas huellas tienen similitud
                ///y asigna los valores al obj operador, si no lo retorna nulo
                if (compareResult.Score < (PROBABILITY_ONE / 100000))
                {
                    opEncontrado = new Operador();

                    opEncontrado.IdOperador = item.IdOperador;
                    opEncontrado.CodOperador = item.CodOperador;
                    opEncontrado.Nombre = item.Nombre;
                    opEncontrado.Status = item.Status;
                }
            }

            return opEncontrado;
        }
    }
}
