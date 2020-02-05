using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncionesLogicas
{
    public class Helper
    {
        #region Constantes FMD
        public int Format = 1769473;
        public string Version = "1.0.0";

        #endregion

        #region Mensajes de Consola
        public string ColocarHuella = "Coloque su huella sobre el lector...";

        public string HuellaCapturada = "Huella capturada.";

        public string FmdCreado = "Fmd creado exitosamente.\r\nPulse siguiente para continuar o coloque huella para volver a capturar...";

        public string FmdError = "El proceso de Enroll no fue exitoso. Inténtalo de nuevo.";

        public string HuellaNoEncontrada = "Huella no encontrada.";

        public string ErrorServidor = "Error al conectar con el servidor, revise su conexion.";

        public string HuellaExiste = "Ya existe un Operador con esta huella";

        public string Error = "Error:  ";
        #endregion

        #region Mensajes del Sistema

        public string NombreSistema = "Pesaje";
        #endregion

    }
}
