using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioTipoPago: IRepositorio<TipoPago>
    {
        TipoPago ObtenerPorCodigo(string pCodigoTipoPago);        
    }
}