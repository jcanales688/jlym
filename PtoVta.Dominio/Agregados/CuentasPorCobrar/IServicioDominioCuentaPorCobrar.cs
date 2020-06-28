using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;

namespace PtoVta.Dominio.Agregados.CuentasPorCobrar 
{
    public interface IServicioDominioCuentaPorCobrar
    {
        DateTime ObtenerFechaVenceDocumentoCuentaPorCobrar(DateTime pFechaDocumentoVenta,
                                                    Cliente pCliente, CondicionPago pCondicionPago);        
    }
}
