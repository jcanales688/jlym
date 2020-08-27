using System;


namespace PtoVta.Dominio.Agregados.Ventas
{
    public static class PedidoEESSFactory
    {
        public static PedidoEESS CrearPedidoEESS(int pCorrelativo, string pNumeroCara, string pNumeroDocumento,
            bool pAfectaInventario, DateTime pFechaDocumento, DateTime pFechaProceso,
            string pPeriodo, decimal pTotalNacional, decimal pTotalExtranjera,
            decimal pSubTotalNacional, decimal pSubTotalExtranjera, decimal pImpuestoIgvNacional,
            decimal pImpuestoIgvExtranjera, decimal pImpuestoIscNacional, decimal pImpuestoIscExtranjera,
            decimal pTotalNoAfectoNacional, decimal pTotalNoAfectoExtranjera, decimal pPorcentajeDescuentoPrimero,
            decimal pPorcentajeDescuentoSegundo, decimal pTotalDescuentoNacional, decimal pTotalDescuentoExtranjera,
            decimal pTotalVueltoNacional, decimal pTotalVueltoExtranjera, decimal pTotalEfectivoNacional,
            decimal pTotalEfectivoExtranjera, string pRucCliente, string pNombreCompletoCliente,
            string pPlaca, decimal pNumeroVale, decimal pTipoCambio,
            bool pProcesadoCierreZ, bool pProcesadoCierreX, int pNumeroPuntos,
            string pNombreTerminal, int pKilometraje, string pDireccionCliente,
            int pTipoCliente, string pDescripcionTipoCliente, string pDescripcionEstado,
            decimal pTipoCambioClienteCredito, int pDiasDeGraciaClienteCredito, decimal pLimiteCreditoClienteCredito,
            decimal pDeudaClienteClienteCredito, decimal pPlusCreditoClienteCredito, bool pAfecto,
            string pNumeroTarjeta, int pPagoTarjeta, string pDescripcionTarjeta,
            string pCodigoTipoDocumento, string pCodigoTipoPago, string pCodigoAlmacen,
            string pCodigoMoneda, string pCodigoEstadoDocumento, string pCodigoCondicionPago,
            string pCodigoVendedor, string pCodigoUsuarioDeSistema, string pCodigoImpuestoIgv,
            string pCodigoImpuestoIsc, string pCodigoCliente, string pCodigoClaseTipoCambio,
            string pCodigoPuntoDeVenta, string pCodigoEstado, string pCodigoMonedaCredito,
            string pCodigoClaseTipoCambioClienteCredito, string pCodigoTarjetaPromocion, string pCodigoTarjeta,
            string pCodigoMonedaTarjeta)
        {
            var nuevoPedidoEESS = new PedidoEESS(pCorrelativo, pNumeroCara, pNumeroDocumento,
                                pAfectaInventario, pFechaDocumento, pFechaProceso,
                                pPeriodo, pTotalNacional, pTotalExtranjera,
                                pSubTotalNacional, pSubTotalExtranjera, pImpuestoIgvNacional,
                                pImpuestoIgvExtranjera, pImpuestoIscNacional, pImpuestoIscExtranjera,
                                pTotalNoAfectoNacional, pTotalNoAfectoExtranjera, pPorcentajeDescuentoPrimero,
                                pPorcentajeDescuentoSegundo, pTotalDescuentoNacional, pTotalDescuentoExtranjera,
                                pTotalVueltoNacional, pTotalVueltoExtranjera, pTotalEfectivoNacional,
                                pTotalEfectivoExtranjera, pRucCliente, pNombreCompletoCliente,
                                pPlaca, pNumeroVale, pTipoCambio,
                                pProcesadoCierreZ, pProcesadoCierreX, pNumeroPuntos,
                                pNombreTerminal, pKilometraje, pDireccionCliente,
                                pTipoCliente, pDescripcionTipoCliente, pDescripcionEstado,
                                pTipoCambioClienteCredito, pDiasDeGraciaClienteCredito, pLimiteCreditoClienteCredito,
                                pDeudaClienteClienteCredito, pPlusCreditoClienteCredito, pAfecto,
                                pNumeroTarjeta, pPagoTarjeta, pDescripcionTarjeta);


            nuevoPedidoEESS.EstablecerReferenciaTipoDocumentoDeVenta(pCodigoTipoDocumento);
            nuevoPedidoEESS.EstablecerReferenciaTipoPagoDeVenta(pCodigoTipoPago);
            nuevoPedidoEESS.EstablecerReferenciaAlmacenDeVenta(pCodigoAlmacen);
            nuevoPedidoEESS.EstablecerReferenciaMonedaDeVenta(pCodigoMoneda);
            nuevoPedidoEESS.EstablecerReferenciaEstadoDocumentoDeVenta(pCodigoEstadoDocumento);
            nuevoPedidoEESS.EstablecerReferenciaCondicionPagoDeVenta(pCodigoCondicionPago);
            nuevoPedidoEESS.EstablecerReferenciaVendedorDeVenta(pCodigoVendedor);
            nuevoPedidoEESS.EstablecerReferenciaUsuarioSistemaDeVenta(pCodigoUsuarioDeSistema);
            nuevoPedidoEESS.EstablecerReferenciaImpuestoIgvDeCliente(pCodigoImpuestoIgv);
            nuevoPedidoEESS.EstablecerReferenciaImpuestoIscDeCliente(pCodigoImpuestoIsc);
            nuevoPedidoEESS.EstablecerReferenciaClienteDeVenta(pCodigoCliente);
            nuevoPedidoEESS.EstablecerReferenciaClaseTipoCambioDeVenta(pCodigoClaseTipoCambio);
            nuevoPedidoEESS.EstablecerReferenciaConfiguracionPuntoVentaDeVenta(pCodigoPuntoDeVenta);
            nuevoPedidoEESS.EstablecerReferenciaEstadoDeVenta(pCodigoEstado);
            nuevoPedidoEESS.EstablecerReferenciaMonedaCreditoDeVenta(pCodigoMonedaCredito);
            nuevoPedidoEESS.EstablecerReferenciaClaseTipoCambioClienteCreditoDeVenta(pCodigoClaseTipoCambioClienteCredito);
            nuevoPedidoEESS.EstablecerReferenciaTarjetaPromocionDeVenta(pCodigoTarjetaPromocion);
            nuevoPedidoEESS.EstablecerReferenciaTarjetaDeVenta(pCodigoTarjeta);
            nuevoPedidoEESS.EstablecerReferenciaMonedaTarjetaDeVenta(pCodigoMonedaTarjeta);

            return nuevoPedidoEESS;
        }

    }
}