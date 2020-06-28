using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioTipoMovimientoAlmacen: IRepositorio<TipoMovimientoAlmacen>
    {
        TipoMovimientoAlmacen ObtenerPorCodigo(string pCodigoTipoMovimientoAlmacen);
    }
}