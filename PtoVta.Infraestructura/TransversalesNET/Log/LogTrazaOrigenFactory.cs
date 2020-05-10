using System;
using PtoVta.Infraestructura.Transversales.Log;

namespace PtoVta.Infraestructura.TransversalesNET.Log
{
    public class LogTrazaOrigenFactory
        : ILogFactory   
    {
        //Crear el registro de origen de seguimiento
        //Retorna: nuevo Ilog base de infraestructura de Origen Registro
        public ILog Crear()
        {
            //Crear el registro de origen de seguimiento
            //Nueva ILOG basa en las infraestructuras origen de seguimiento
            return new LogTrazaOrigen();
        }        
    }
}
