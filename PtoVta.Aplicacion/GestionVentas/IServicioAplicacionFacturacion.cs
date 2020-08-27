using System;
using System.Collections.Generic;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;

namespace PtoVta.Aplicacion.GestionVentas
{
    public interface IServicioAplicacionFacturacion
    {
        ResultadoServicio<ResultadoVentaGrabadaDTO> AgregarNuevaVenta(VentaDTO pVentaDTO);
        ResultadoServicio<ResultadoVentaGrabadaDTO> AgregarNuevaVentaDesdePedidoRetail(int pCorrelativoPedido);
        ResultadoServicio<ResultadoVentaGrabadaDTO> AgregarNuevaVentaDesdePedidoEESS(int pCorrelativoPedido);   
        ResultadoServicio<VentaListadoDTO> BuscarVentas(string pCodigoAlmacen, string pFechaProcesoInicio, string pFechaProcesoFin, 
                                                            string pNumeroDocumento, string pCodigoTipoNegocio);

        ResultadoServicio<VentaListadoDTO> BuscarVentasPorCliente(string pCodigoCliente);                                                               
    }

}