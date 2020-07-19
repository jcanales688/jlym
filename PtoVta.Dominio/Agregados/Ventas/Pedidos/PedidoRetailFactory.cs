using System;


namespace PtoVta.Dominio.Agregados.Ventas
{
    public static class PedidoRetailFactory
    {
        public static PedidoRetail CrearPedidoRetail(
            int pCorrelativo, string pNumeroDocumento, bool pAfectaInventario,    
            DateTime pFechaDocumento, DateTime pFechaProceso, string pPeriodo,
            decimal pTotalNacional, decimal pTotalExtranjera, decimal pSubTotalNacional,
            decimal pSubTotalExtranjera, decimal pImpuestoIgvNacional, decimal pImpuestoIgvExtranjera,
            decimal pImpuestoIscNacional, decimal pImpuestoIscExtranjera, decimal pTotalNoAfectoNacional,
            decimal pTotalNoAfectoExtranjera, decimal pPorcentajeDescuentoPrimero, decimal pPorcentajeDescuentoSegundo,
            decimal pTotalDescuentoNacional, decimal pTotalDescuentoExtranjera, decimal pTotalVueltoNacional,
            decimal pTotalVueltoExtranjera, decimal pTotalEfectivoNacional, decimal pTotalEfectivoExtranjera,
            string pRucCliente, string pNombreCompletoCliente, string pDireccionCliente,
            string pPlaca, decimal pNumeroVale, decimal pTipoCambio,
            int pNumeroPuntos, int pKilometraje, bool pTransaccionPendiente,
            string  pTipoVenta, bool pTransaccionProcesada, bool pAplicaDescuentoCupon,
            string pCentroDeCosto, string pCodigoTipoDocumento, string pCodigoTipoPago,
            string pCodigoAlmacen, string pCodigoMoneda, string pCodigoCondicionPago,
            string pCodigoVendedor, string pCodigoUsuarioDeSistema, string pCodigoImpuestoIgv,
            string pCodigoImpuestoIsc, string pCodigoCliente, string pCodigoClaseTipoCambio,
            string pCodigoTarjetaPromocion, string pCodigoPuntoDeVenta, string pCodigoTipoNegocio)
        {
            var nuevoPedidoRetail = new PedidoRetail();

            nuevoPedidoRetail.Correlativo = pCorrelativo;
            nuevoPedidoRetail.NumeroDocumento = pNumeroDocumento;
            nuevoPedidoRetail.AfectaInventario = pAfectaInventario;    
            nuevoPedidoRetail.FechaDocumento = pFechaDocumento;
            nuevoPedidoRetail.FechaProceso = pFechaProceso;
            nuevoPedidoRetail.Periodo = pPeriodo;
            nuevoPedidoRetail.TotalNacional = pTotalNacional;
            nuevoPedidoRetail.TotalExtranjera = pTotalExtranjera;
            nuevoPedidoRetail.SubTotalNacional = pSubTotalNacional;
            nuevoPedidoRetail.SubTotalExtranjera = pSubTotalExtranjera;
            nuevoPedidoRetail.ImpuestoIgvNacional = pImpuestoIgvNacional;
            nuevoPedidoRetail.ImpuestoIgvExtranjera = pImpuestoIgvExtranjera;
            nuevoPedidoRetail.ImpuestoIscNacional = pImpuestoIscNacional;
            nuevoPedidoRetail.ImpuestoIscExtranjera = pImpuestoIscExtranjera;
            nuevoPedidoRetail.TotalNoAfectoNacional = pTotalNoAfectoNacional;
            nuevoPedidoRetail.TotalNoAfectoExtranjera = pTotalNoAfectoExtranjera;
            nuevoPedidoRetail.PorcentajeDescuentoPrimero = pPorcentajeDescuentoPrimero;
            nuevoPedidoRetail.PorcentajeDescuentoSegundo = pPorcentajeDescuentoSegundo;
            nuevoPedidoRetail.TotalDescuentoNacional = pTotalDescuentoNacional;
            nuevoPedidoRetail.TotalDescuentoExtranjera = pTotalDescuentoExtranjera;
            nuevoPedidoRetail.TotalVueltoNacional = pTotalVueltoNacional;
            nuevoPedidoRetail.TotalVueltoExtranjera = pTotalVueltoExtranjera;
            nuevoPedidoRetail.TotalEfectivoNacional = pTotalEfectivoNacional;
            nuevoPedidoRetail.TotalEfectivoExtranjera = pTotalEfectivoExtranjera;
            nuevoPedidoRetail.RucCliente = pRucCliente;
            nuevoPedidoRetail.NombreCompletoCliente = pNombreCompletoCliente;
            nuevoPedidoRetail.DireccionCliente = pDireccionCliente;
            nuevoPedidoRetail.Placa = pPlaca;
            nuevoPedidoRetail.NumeroVale = pNumeroVale;
            nuevoPedidoRetail.TipoCambio = pTipoCambio;
            nuevoPedidoRetail.NumeroPuntos = pNumeroPuntos;
            nuevoPedidoRetail.Kilometraje = pKilometraje;
            nuevoPedidoRetail.TransaccionPendiente = pTransaccionPendiente;
            nuevoPedidoRetail.TipoVenta = pTipoVenta;
            nuevoPedidoRetail.TransaccionProcesada = pTransaccionProcesada;
            nuevoPedidoRetail.AplicaDescuentoCupon = pAplicaDescuentoCupon;
            nuevoPedidoRetail.CentroDeCosto = pCentroDeCosto; 

            nuevoPedidoRetail.EstablecerReferenciaTipoDocumentoDeVenta(pCodigoTipoDocumento);
            nuevoPedidoRetail.EstablecerReferenciaTipoPagoDeVenta(pCodigoTipoPago);
            nuevoPedidoRetail.EstablecerReferenciaAlmacenDeVenta(pCodigoAlmacen);
            nuevoPedidoRetail.EstablecerReferenciaMonedaDeVenta(pCodigoMoneda);
            nuevoPedidoRetail.EstablecerReferenciaCondicionPagoDeVenta(pCodigoCondicionPago);
            nuevoPedidoRetail.EstablecerReferenciaVendedorDeVenta(pCodigoVendedor);
            nuevoPedidoRetail.EstablecerReferenciaUsuarioSistemaDeVenta(pCodigoUsuarioDeSistema);
            nuevoPedidoRetail.EstablecerReferenciaImpuestoIgvDeCliente(pCodigoImpuestoIgv);
            nuevoPedidoRetail.EstablecerReferenciaImpuestoIscDeCliente(pCodigoImpuestoIsc);
            nuevoPedidoRetail.EstablecerReferenciaClienteDeVenta(pCodigoCliente);
            nuevoPedidoRetail.EstablecerReferenciaClaseTipoCambioDeVenta(pCodigoClaseTipoCambio);
            nuevoPedidoRetail.EstablecerReferenciaTarjetaPromocionDeVenta(pCodigoTarjetaPromocion);
            nuevoPedidoRetail.EstablecerReferenciaConfiguracionPuntoVentaDeVenta(pCodigoPuntoDeVenta);
            nuevoPedidoRetail.EstablecerReferenciaTipoNegocioDeVenta(pCodigoTipoNegocio);

            return nuevoPedidoRetail;
        }
    }
}