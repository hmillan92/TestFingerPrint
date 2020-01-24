using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    public class FmdOperador
    {        

        //
        // Resumen:
        //     Image format (i.e., ANSI, ISO, ...)
        public Constants.Formats.Fmd Format { get; }
        //
        // Resumen:
        //     Width.
        
        public int Width { get; }
        //
        // Resumen:
        //     View count.
        
        public int ViewCount { get; }
        //
        // Resumen:
        //     Height.
        
        public int Height { get; }
        //
        // Resumen:
        //     Capture equipment ids.
        
        public int CaptureEquipmentIds { get; }
        //
        // Resumen:
        //     Capture equipment comp.
        
        public int CaptureEquipmentComp { get; }
        //
        // Resumen:
        //     Views contained within the Fmd.
       
        public IList<Fmv> Views { get; }

        //
        // Resumen:
        //     Deserialize string data into an Fmd object.
        //
        // Parámetros:
        //   data:
        //     Serialized Fmd
        //
        // Devuelve:
        //     Fmd deserialized.
        //public static Fmd DeserializeXml(string data);
        ////
        //// Resumen:
        ////     Serialize Fml object to xml string.
        ////
        //// Parámetros:
        ////   obj:
        ////     Fmd
        ////
        //// Devuelve:
        ////     Fmd serialized to string.
        //public static string SerializeXml(Fmd obj);

        //
        // Resumen:
        //     Fingerprint Minutiae View.
        public class Fmv
        {
            //
            // Resumen:
            //     Raw view bytes.
            
            public byte[] Bytes { get; }
            //
            // Resumen:
            //     Finger position.
            
            public int FingerPosition { get; }
            //
            // Resumen:
            //     Minutiae count.
            
            public int MinutiaeCount { get; }
            //
            // Resumen:
            //     Quality score.
            
            public int Quality { get; }
            //
            // Resumen:
            //     View number.
           
            public int ViewNumber { get; }
        }
    }
}
