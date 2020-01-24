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
        private const int PROBABILITY_ONE = 0x7fffffff;
        private Fmd firstFinger;
            
        public bool ValidaConexionSQL()
        {
            
            bool Exitosa = transaccion.ValidaConexionSQL();

            return Exitosa;
        }

        public string CrearOperador(Fmd presult, string pnombre)
        {
            string respuesta;
            string Mensaje;
          
            respuesta = CompararHuella(presult, presult);

            if (respuesta == "Huella no encontrada")
            {
                Operadores operadores = new Operadores();
                operadores.Huella = presult.Bytes;
                operadores.Nombre = pnombre;

                Mensaje = transaccion.CrearOperador(operadores);
            }

            else
            {
                Mensaje = "No se puede agregar, "+respuesta;
            }
          
            return Mensaje;
        }

        public string CompararHuella(Fmd firstF, Fmd secondF)
        {
            ///Añadimos el parametro firstF a firstFinger un objeto de tipo Fmd que sera el objeto de la huella capturada
            firstFinger = firstF;

            ///creamos el segundo objeto de tipo Fmd y le pasamos los parametros que pide el constructor de esa clase, los parametros son tomados del objeto 1
            ///se hace esto porque la propiedad bytes de un mismo objeto al ser cambiada en una variable, se cambia en todas las demas como si fuese una variable estatica
            ///y necesitamos resetear solo los bytes para poder asignarle los bytes que se encuentran en la BD al objeto 2 para compararlas 
            ///solo necesitamos cambiar los bytes porque los otros parametros que pide el constructor y que el toma del objeto 1 no los vamos a necesitar para hacer la comparacion.
            Fmd secondFinger = new Fmd(firstFinger.Bytes,1769473, "1.0.0");

            ///declaramos 4 variables
            ///un tipo string para el mensaje a retornar
            ///uno de tipo int para contar, funcionara como un switche
            ///otro string que almacenaremos y pegaremos en el mensaje el nombre del operador que esta registrado en la BD 
            ///una variable de tipo lista para almacenar los registros que nos traera el metodo ListarHuellas de la clase transaccion 

            string mensaje = "Huella no encontrada";
            int count = 0;
            string nombreOp = "";
            List<HuellaBd> ListaHuellas = transaccion.ListarHuellas();

            ///un foreach para capturar los valores de cada registro
            foreach (var item in ListaHuellas)
            {

                ///los bytes que estan registrados en la BD son de tipo Base64 que es un varchar(max), le pasamos ese string al metodo convert
                ///para que nos convierta de nuevo a bytes y se la asignamos a la variable huellaConvertida
                byte[] huellaConvertida = Convert.FromBase64String(item.Huella);

                //le asignamos los bytes de huellaConvertida que representan los bytes que estan en la BD a la propiedad Bytes del objeto secondFinger para asi poder hacer la comparacion con el obj 1 que es la huella capturada
                secondFinger.Bytes = huellaConvertida;

                ///Una vez listo los 2 objetos a comparar se lo mandamos al metodo Compare de la clase Comparision, nos devolvera un objeto de tipo CompareResult
                ///Este objeto compareResult tendra una propiedad que se llama Score que es el puntaje que necesitaremos para ver si ambas huellas tienen similitud
                CompareResult compareResult = Comparison.Compare(firstFinger, 0, secondFinger, 0);
                
                //preguntamos si el Score es menor a la operacion establecida en el if, si el resultado es true entonces ambas huellas tienen similitud
                if (compareResult.Score < (PROBABILITY_ONE / 100000))
                {

                    ///count es el switche que usaremos para saber si entro a este if asignandole un valor de 1 
                    count++;

                    ///y el nombre para guardaren la variable nombreOp y asi saber con quien operador tuvo similitud la huella capturada
                    nombreOp = item.Nombre;
                }
            }

            ///este if determinara si count es mayor que 1 es porque paso al menos una vez dentro del if que esta en el foreach y crearemos el mensaje con el nombre que nos trajo
            if (count > 0)
            {
                mensaje = "Huella coincide con " + nombreOp;
            }
           
            ///devolvemos el mensaje a verification
            return mensaje;
        }

        public List<HuellaBd> ListarHuellas()
        {
            List<HuellaBd> listarOp = transaccion.ListarHuellas();

            return listarOp;
        }
    }
}
