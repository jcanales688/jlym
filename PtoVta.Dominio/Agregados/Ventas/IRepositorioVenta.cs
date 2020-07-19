using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public interface IRepositorioVenta: IRepositorio<Venta>
    {
        string ObtenerNumeroDocumentoVenta(string pCodigoTipoDocumento, string pCorrelativoDocumento,
                                            string pCodigoAlmacen); 

        //esto es parte de GetFiltered de patro repositorio
        IEnumerable<Venta> ObtenerVentasPorCodigoCliente(string pCodigoCliente);
        IEnumerable<Venta> ObtenerPagoVentaAdelantada(string pCodigoCliente, string pCodigoAlmacen,
                                                string pCodigoTipoDocumento, DateTime pFechaProcesoVentas);
        IEnumerable<Venta> ObtenerConsumoVentaAdelantada(string pCodigoTipoPago, string pCodigoCliente, string pCodigoAlmacen,
                                        string pCodigoTipoDocumento, DateTime pFechaProcesoVentas);

        IEnumerable<Venta> ObtenerTodos(string pCodigoAlmacen, string pFechaProcesoInicio, string pFechaProcesoFin, 
                                                string pNumeroDocumento, string pCodigoTipoNegocio);                                        
    }
}