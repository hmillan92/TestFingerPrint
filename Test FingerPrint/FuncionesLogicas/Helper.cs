using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuncionesLogicas
{
    public class Helper
    {
        #region Constantes FMD
        public int Format = 1769473;
        public string Version = "1.0.0";

        #endregion

        #region Mensajes de Consola
        public string ColocarHuella = "Coloque una huella sobre el lector...";

        public string HuellaCapturada = "Huella capturada.";

        public string FmdCreado = "Fmd creado exitosamente.\r\nPulse siguiente para continuar o coloque huella para volver a capturar...";

        public string FmdError = "El proceso de Enroll no fue exitoso. Inténtalo de nuevo.";

        public string HuellaNoEncontrada = "Huella no encontrada.";

        public string ErrorServidor = "Error al tratar de conectar con el servidor o base de datos, por favor revise su conexion e intente nuevamente.";

        public string HuellaExiste = "Ya existe un Operador con esta huella";

        public string Error = "Error:  ";
        #endregion

        #region Mensajes del Sistema

        public string NombreSistema = "Pesaje";
        public string LectorOff = "Lector no detectado";
        public string ServerOff = "desconectado";
        public string ServerOn = "conectado";
        #endregion

    }
}
