using System;

namespace PtoVta.Dominio.BaseTrabajo
{
    internal static class GeneradorIdentidad
    {
        //Este algoritmo genera GUID secuencia definida a través de los límites del sistema,
        //ideal para bases de datos

        public static Guid  NuevaGuidSecuencial()
        {
            byte[] uid = Guid.NewGuid().ToByteArray();
            byte[] binDate = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            byte[] secuentialGuid = new byte[uid.Length];

            secuentialGuid[0] = uid[0];
            secuentialGuid[1] = uid[1];
            secuentialGuid[2] = uid[2];
            secuentialGuid[3] = uid[3];
            secuentialGuid[4] = uid[4];
            secuentialGuid[5] = uid[5];
            secuentialGuid[6] = uid[6];
            //establecer la primera parte del octavo byte de '1100 'más 
            //tarde seremos capaces de validar que se generó por nosotros  

            secuentialGuid[7] = (byte)(0xc0 | (0xf & uid[7]));

            //los últimos 8 bytes son secuenciales,
            //que minimiza la fragmentación del índice
            //a un grado, siempre y cuando no hay un gran
            //número de Secuencial-GUID generados por milisegundo 

            secuentialGuid[9] = binDate[0];
            secuentialGuid[8] = binDate[1];
            secuentialGuid[15] = binDate[2];
            secuentialGuid[14] = binDate[3];
            secuentialGuid[13] = binDate[4];
            secuentialGuid[12] = binDate[5];
            secuentialGuid[11] = binDate[6];
            secuentialGuid[10] = binDate[7];

            return new Guid(secuentialGuid);
        }        
    }
}
