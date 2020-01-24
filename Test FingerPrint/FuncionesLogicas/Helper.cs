using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncionesLogicas
{
    public class Helper
    {

        public static string ConvertirHuellaAString(byte[] pHuellaAConvertir)
        {
            byte[] HuellaAConvertir = pHuellaAConvertir;
            string huellaConvertida;
            huellaConvertida = Convert.ToBase64String(HuellaAConvertir);

            return huellaConvertida;
        }
        
    }
}
