using System;


namespace PtoVta.Dominio.Agregados.Ventas
{
    public static class PedidoEESSFactory
    {
        public static PedidoEESS CrearPedidoEESS(int pCorrelativo,string pNumeroCara, string pNumeroDocumento,
            bool pAfectaInventario, DateTime pFechaDocumento, DateTime pFechaProceso,
            string pPeriodo, decimal pTotalNacional, decimal pTotalExtranjera,
            decimal pSubTotalNacional, decimal pSubTotalExtranjera,decimal pImpuestoIgvNacional,
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
            string pCodigoConfiguracionPuntoVenta, string pCodigoEstado, string pCodigoMonedaCredito, 
            string pCodigoClaseTipoCambioClienteCredito, string pCodigoTarjetaPromocion, string pCodigoTarjeta,
            string pCodigoMonedaTarjeta)
        {
            var nuevoPedidoEESS = new PedidoEESS();

            nuevoPedidoEESS.Correlativo = pCorrelativo;
            nuevoPedidoEESS.NumeroCara = pNumeroCara;  
            nuevoPedidoEESS.NumeroDocumento = pNumeroDocumento;
            nuevoPedidoEESS.AfectaInventario = pAfectaInventario;
            nuevoPedidoEESS.FechaDocumento = pFechaDocumento;    
            nuevoPedidoEESS.FechaProceso = pFechaProceso;
            nuevoPedidoEESS.Periodo = pPeriodo;
            nuevoPedidoEESS.TotalNacional = pTotalNacional;
            nuevoPedidoEESS.TotalExtranjera = pTotalExtranjera;
            nuevoPedidoEESS.SubTotalNacional = pSubTotalNacional;
            nuevoPedidoEESS.SubTotalExtranjera = pSubTotalExtranjera;
            nuevoPedidoEESS.ImpuestoIgvNacional = pImpuestoIgvNacional;
            nuevoPedidoEESS.ImpuestoIgvExtranjera = pImpuestoIgvExtranjera;
            nuevoPedidoEESS.ImpuestoIscNacional = pImpuestoIscNacional;
            nuevoPedidoEESS.ImpuestoIscExtranjera = pImpuestoIscExtranjera;
            nuevoPedidoEESS.TotalNoAfectoNacional = pTotalNoAfectoNacional;
            nuevoPedidoEESS.TotalNoAfectoExtranjera = pTotalNoAfectoExtranjera;
            nuevoPedidoEESS.PorcentajeDescuentoPrimero = pPorcentajeDescuentoPrimero;
            nuevoPedidoEESS.PorcentajeDescuentoSegundo = pPorcentajeDescuentoSegundo;
            nuevoPedidoEESS.TotalDescuentoNacional = pTotalDescuentoNacional;
            nuevoPedidoEESS.TotalDescuentoExtranjera = pTotalDescuentoExtranjera;
            nuevoPedidoEESS.TotalVueltoNacional = pTotalVueltoNacional;
            nuevoPedidoEESS.TotalVueltoExtranjera= pTotalVueltoExtranjera;
            nuevoPedidoEESS.TotalEfectivoNacional = pTotalEfectivoNacional;
            nuevoPedidoEESS.TotalEfectivoExtranjera = pTotalEfectivoExtranjera;
            nuevoPedidoEESS.RucCliente = pRucCliente;
            nuevoPedidoEESS.NombreCompletoCliente = pNombreCompletoCliente;
            nuevoPedidoEESS.Placa = pPlaca;
            nuevoPedidoEESS.NumeroVale = pNumeroVale;
            nuevoPedidoEESS.TipoCambio = pTipoCambio;
            nuevoPedidoEESS.ProcesadoCierreZ = pProcesadoCierreZ;
            nuevoPedidoEESS.ProcesadoCierreX = pProcesadoCierreX;
            nuevoPedidoEESS.NumeroPuntos = pNumeroPuntos;
            nuevoPedidoEESS.NombreTerminal = pNombreTerminal;
            nuevoPedidoEESS.Kilometraje = pKilometraje;
            nuevoPedidoEESS.DireccionCliente = pDireccionCliente;
            nuevoPedidoEESS.TipoCliente = pTipoCliente;
            nuevoPedidoEESS.DescripcionTipoCliente = pDescripcionTipoCliente;
            nuevoPedidoEESS.DescripcionEstado = pDescripcionEstado;
            nuevoPedidoEESS.TipoCambioClienteCredito = pTipoCambioClienteCredito;
            nuevoPedidoEESS.DiasDeGraciaClienteCredito = pDiasDeGraciaClienteCredito;
            nuevoPedidoEESS.LimiteCreditoClienteCredito = pLimiteCreditoClienteCredito;
            nuevoPedidoEESS.DeudaClienteClienteCredito = pDeudaClienteClienteCredito;
            nuevoPedidoEESS.PlusCreditoClienteCredito = pPlusCreditoClienteCredito;
            nuevoPedidoEESS.Afecto = pAfecto;
            nuevoPedidoEESS.NumeroTarjeta = pNumeroTarjeta;
            nuevoPedidoEESS.PagoTarjeta = pPagoTarjeta;
            nuevoPedidoEESS.DescripcionTarjeta = pDescripcionTarjeta;

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
            nuevoPedidoEESS.EstablecerReferenciaConfiguracionPuntoVentaDeVenta(pCodigoConfiguracionPuntoVenta);
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