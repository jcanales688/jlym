using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioTipoNegocio : IRepositorio<TipoNegocio>
    {
        TipoNegocio ObtenerPorCodigo(string pCodigoTipoNegocio);
    }
}