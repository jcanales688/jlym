using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioAlmacen : IRepositorio<Almacen>
    {
        Almacen ObtenerPorCodigo(string pCodigoAlmacen);        
    }
}
