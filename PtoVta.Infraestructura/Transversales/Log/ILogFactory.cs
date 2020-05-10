using System;

namespace PtoVta.Infraestructura.Transversales.Log
{
    public interface ILogFactory
    {
        //Crear un nuevo ILOG
        //retorna: El ILOG creado
        ILog Crear();        
    }
}
