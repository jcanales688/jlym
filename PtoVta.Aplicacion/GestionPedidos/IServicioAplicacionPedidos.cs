using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;

namespace PtoVta.Aplicacion.GestionPedidos
{
    public interface IServicioAplicacionPedidos
    {
        //EESS
        ResultadoServicio<ResultadoPedidoEESSGrabadoDTO> AgregarNuevoPedidoEESS(PedidoEESSDTO pPedidoEESSDTO);
        ResultadoServicio<ResultadoPedidoRetailGrabadoDTO> AgregarNuevoPedidoRetail(PedidoRetailDTO pPedidoEESSDTO);


        //Retail
        ResultadoServicio<PedidoEESSListadoDTO> BuscarPedidoEESSPorPuntoDeVenta(string pCodigoPuntoDeVenta);
        ResultadoServicio<PedidoEESSDTO> BuscarPedidoEESSPorNumero(int pCorrelativo);
        ResultadoServicio<PedidoRetailListadoDTO> BuscarPedidoRetailPorPuntoDeVenta(string pCodigoPuntoDeVenta);
        ResultadoServicio<PedidoRetailDTO> BuscarPedidoRetailPorNumero(int pCorrelativo);
    }
}