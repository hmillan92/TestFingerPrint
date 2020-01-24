using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class OperadorHBytes
    {
        public int IdOperador { get; set; }

        public string CodOperador { get; set; }

        public string Nombre { get; set; }

        public byte[] Huella { get; set; }
    }
}
