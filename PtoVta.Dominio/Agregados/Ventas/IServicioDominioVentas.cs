using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public interface IServicioDominioVentas
    {
        void ObtenerCondicionYTipoPagoDeVenta(string pCodigoTipoDocumentoDeVenta, CondicionPago pCondicionPagoDeVenta, CondicionPago pCondicionPagoDefault, 
                                        TipoPago pTipoPagoDeVenta, TipoPago pTipoPagoEfectivo, Cliente pCliente,
                                        ConfiguracionPuntoVenta pConfiguracionPuntoVenta, string pCodigoTipoDocumentoNotaCredito, 
                                        decimal pTotalNacional, bool pEsVentaACuentaPorCobrar, decimal pSaldoDisponibleAdelanto);


        void CalcularSaldoVentaAdelantada(decimal saldoIniPagoAdelantado, decimal saldoFinPagoAdelantado,
                                            IEnumerable<Venta> pagoInicial, IEnumerable<Venta> consumos);


        bool ExisteComprobanteDePagoDeVenta(string pNuevoCorrelativoDocumento, string correlEncontrado);

        void CalcularVueltoVentaSegunMoneda(Venta pVenta, ClaseTipoCambio pClaseTipoCambio, bool pFlagCambioMonedaVuelto, 
                                            int pCantidadDecimalPrecio, decimal pEfectivoVueltoExtranjera, decimal pTotalVueltoSegunMoneda,
                                            decimal pTotalFaltanteExtranjera,  decimal pTotalFaltanteNacional,string pCodigoMonedaVuelto, 
                                            string pCodigoMonedaBase, string pCodigoMonedaExtranjera);
    }
}