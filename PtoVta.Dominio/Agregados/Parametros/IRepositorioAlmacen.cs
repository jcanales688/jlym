using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioAlmacen : IRepositorio<Almacen>
    {
        Almacen ObtenerPorCodigo(string pCodigoAlmacen);   

        IEnumerable<Almacen> ObtenerHabilitados();     
    }
}
