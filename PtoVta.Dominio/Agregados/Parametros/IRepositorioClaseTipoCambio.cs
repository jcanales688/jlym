using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioClaseTipoCambio : IRepositorio<ClaseTipoCambio>
    {
        ClaseTipoCambio ObtenerMontoTipoDeCambio(string pCodigoMonedaOrigen, string pCodigoMonedaDestino,
                                                        DateTime pFechaTipoDeCambio, string pCodigoClaseTipoCambio);        

        ClaseTipoCambio ObtenerPorCodigo(string pCodigoClaseTipoCambio);                                                        

    }
}