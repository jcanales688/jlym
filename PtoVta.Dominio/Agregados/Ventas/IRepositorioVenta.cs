using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public interface IRepositorioVenta: IRepositorio<Venta>
    {
        Decimal ObtenerNumeroDocumentoVenta(string pCodigoTipoDocumento, decimal pCorrelativoDocumento,
                                            string pCodigoAlmacen); 

        //esto es parte de GetFiltered de patro repositorio
        IEnumerable<Venta> ObtenerVentasPorCodigoCliente(string pCodigoCliente);

        IEnumerable<Venta> ObtenerPagoVentaAdelantada(string pCodigoCliente, string pCodigoAlmacen,
                                                string pCodigoTipoDocumento, DateTime pFechaProcesoVentas);

        IEnumerable<Venta> ObtenerConsumoVentaAdelantada(string pCodigoTipoPago, string pCodigoCliente, string pCodigoAlmacen,
                                        string pCodigoTipoDocumento, DateTime pFechaProcesoVentas);
    }
}