using System;

namespace PtoVta.Infraestructura.Transversales.Log
{
    public static class LogFactory
    {
        static ILogFactory _actualLogFactory = null;

        //Establecer la factoría a usar
        //registroFactory: Log fábrica de usar
        public static void EstablecerActual(ILogFactory logFactory)
        {
            _actualLogFactory = logFactory;
        }

        //Crear un nuevo IRegistro
        //retorna: Creacion Ilog
        public static ILog CrearLog()
        {
            return (_actualLogFactory != null) ? _actualLogFactory.Crear() : null;
        }        
    }
}
