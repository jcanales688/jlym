using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.AmbienteVenta;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class ServicioDominioVentas : IServicioDominioVentas
    {
        public void ObtenerCondicionYTipoPagoDeVenta(string pCodigoTipoDocumentoDeVenta, CondicionPago pCondicionPagoDeVenta, CondicionPago pCondicionPagoDefault,
                                                TipoPago pTipoPagoDeVenta, TipoPago pTipoPagoDefault, Cliente pCliente,
                                                ConfiguracionPuntoVenta pConfiguracionPuntoVenta, string pCodigoTipoDocumentoNotaCredito, 
                                                decimal pTotalNacional, bool pEsVentaACuentaPorCobrar, decimal pSaldoDisponibleAdelanto)
        {
            switch (pTipoPagoDeVenta.CodigoTipoPago) 
            {
                //Credito
                case EnumTipoPago.CodigoTipoPagoValesCredito:
                    //Obtener Condicion de Pago a partir de entidad Cliente
                    if (pCondicionPagoDeVenta == null)
                    {
                        pCondicionPagoDeVenta = pCondicionPagoDefault; //Creado Parametro en Setup CondicionPagoDefault

                        if (pCondicionPagoDeVenta == null)                    
                            throw new Exception(Mensajes.advertencia_CondicionPagoPorVentasAsociadoAVentaNoExiste);
                    }

                    //*** Obtener tipo de documento Nota de Credito
                    if (pCodigoTipoDocumentoDeVenta.Trim() != pCodigoTipoDocumentoNotaCredito)
                    {
                        // *** este tipo de moneda en verdad se trae desde Configuracion General 
                        if (pCliente.CodigoMoneda == pConfiguracionPuntoVenta.CodigoMonedaCaja)
                        {
                            //Verificar limite de credito : Excede limite de credito
                            if (pCliente.ValidarLimiteCredito(pTotalNacional) == true)
                            {

                                if (pCliente.DocumentosLibre != null)
                                {
                                    //Parte en que se Inicia el Grabado de Consumo de FP6  
                                    //Tiene una Opción más de que se le facture al Crédito - .....
                                    //// 'DOCUMENTOS LIBRES' que se consideran como venta consumo pago adelantado

                                    if (pCliente.DocumentosLibre.FirstOrDefault().TotalLibre >= pTotalNacional)
                                    {
                                        //Inserta registro en tabla : OP_DOCUMENTFREEDET
                                        //Actualizar Deuda del Cliente
                                        pCliente.ActualizarDeuda(pTotalNacional);

                                        //Adjuntar Cuentas por Cobrar si pago es al credito
                                        //Obtener Estado de Documento
                                        pEsVentaACuentaPorCobrar = true;
                                    }
                                    else
                                    {
                                        //Setear venta como Efectivo
                                        pTipoPagoDeVenta = pTipoPagoDefault;

                                        if (pTipoPagoDeVenta == null)
                                        {
                                            throw new Exception(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);
                                        }

                                        throw new ArgumentException(Mensajes.advertencia_ClienteExcedeLimiteCredito); //Comentado
                                    }
                                }
                                else
                                {
                                    //Setear venta como Efectivo
                                    pTipoPagoDeVenta = pTipoPagoDefault;

                                    if (pTipoPagoDeVenta == null)
                                    {
                                        throw new Exception(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);
                                    }

                                    throw new ArgumentException(Mensajes.advertencia_ClienteExcedeLimiteCredito); //Comentado
                                }

                            }
                            else
                            {
                                //Actualizar Deuda del Cliente
                                pCliente.ActualizarDeuda(pTotalNacional);

                                //Adjuntar Cuentas por Cobrar si pago es al credito
                                pEsVentaACuentaPorCobrar = true;
                            }
                        }
                    }

                    break;
                case EnumTipoPago.CodigoTipoPagoContadoAdelantado:  //Adelantado

                    if (pCliente.ControlarSaldoDisponible == 1)
                    {
                        if (pTotalNacional > pSaldoDisponibleAdelanto)
                        {
                            //Excede el Saldo
                            //nuevaVenta
                            pTipoPagoDeVenta = pTipoPagoDefault;

                            if (pTipoPagoDeVenta == null)
                                throw new Exception(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);                            
                        }

                    }

                    break;

                case EnumTipoPago.CodigoTipoPagoOtros:
                case EnumTipoPago.CodigoTipoPagoTarjeta:

                    break;

            }
        }

        public void CalcularSaldoVentaAdelantada(decimal saldoIniPagoAdelantado, decimal saldoFinPagoAdelantado,
                                                IEnumerable<Venta> pagoInicial, IEnumerable<Venta> consumos)
        {
            saldoIniPagoAdelantado = pagoInicial.Single().TotalNacional;

            if (consumos.Count() != 0)
            {
                foreach (var consumosDePagoAdelantado in consumos)
                {
                    saldoFinPagoAdelantado = saldoIniPagoAdelantado - consumosDePagoAdelantado.TotalNacional;
                }
            }
            else            
                saldoFinPagoAdelantado = saldoIniPagoAdelantado;            
        }

        public bool ExisteComprobanteDePagoDeVenta(string pNuevoCorrelativoDocumento, string pActualCorrelativoDocumento)
        {
            bool existeDoc = false;

            if (string.IsNullOrEmpty(pNuevoCorrelativoDocumento.Trim()))            
                throw new Exception(Mensajes.advertencia_NuevoCorrelativoDocumentoGeneradoIncorreactamente);        

            if (pNuevoCorrelativoDocumento.Trim() == pActualCorrelativoDocumento.Trim())
            {
                existeDoc = true;
            }

            return existeDoc;
        }


        //este metodo no va
        public void CalcularVueltoVentaSegunMoneda(Venta pVenta, ClaseTipoCambio pClaseTipoCambio, bool pFlagCambioMonedaVuelto, 
                                int pCantidadDecimalPrecio, decimal pEfectivoVueltoExtranjera, decimal pTotalVueltoSegunMoneda,
                                decimal pTotalFaltanteExtranjera,  decimal pTotalFaltanteNacional,string pCodigoMonedaVuelto, 
                                string pCodigoMonedaBase, string pCodigoMonedaExtranjera)
        {

            decimal totalEfectivoPagoNacional = pVenta.TotalEfectivoNacional;
            decimal totalEfectivoPagoExtranjera = pVenta.TotalEfectivoExtranjera;

            decimal montoPagadoNacional = 0;
            decimal montoPagadoExtranjera = 0;

            decimal totalVueltoNacional = 0;
            decimal totalVueltoExtranjera = 0;

            decimal totalFaltanteNacional = 0;
            decimal totalFaltanteExtranjera = 0;

            //Puede se moneda extranjera en cambio de moneda vuelto
            pCodigoMonedaVuelto = pVenta.CodigoMoneda;

            //Obtener totales desde tarjeta
            pVenta.CalcularTotalPagoConTarjeta(totalEfectivoPagoNacional, totalEfectivoPagoExtranjera, pCodigoMonedaBase);
                
            //obtener tipo de cambio de venta: VALIDARLO
            var tipoDeCambioANacionalSeleccionado = (from tipoCambioNacional in pClaseTipoCambio.TiposDeCambio
                                         where tipoCambioNacional.CodigoMonedaDestino == pCodigoMonedaBase
                                         select tipoCambioNacional).FirstOrDefault();

            var tipoDeCambioAExtranjeraSeleccionado = (from tipoCambioExtranjera in pClaseTipoCambio.TiposDeCambio
                                        where tipoCambioExtranjera.CodigoMonedaDestino == pCodigoMonedaExtranjera
                                        select tipoCambioExtranjera).FirstOrDefault();

            //Calcular total Pagos para calcular el vuelto segun moneda
            if (pVenta.CodigoMoneda == pCodigoMonedaBase)
            {
                montoPagadoNacional =
                    totalEfectivoPagoNacional + ObtenerConversionSegunTipoDeCambio(totalEfectivoPagoExtranjera,
                                                                        tipoDeCambioANacionalSeleccionado.MontoTipoDeCambio,
                                                                        tipoDeCambioANacionalSeleccionado.Operador,
                                                                        pCantidadDecimalPrecio);

                montoPagadoExtranjera = ObtenerConversionSegunTipoDeCambio(montoPagadoNacional,
                                                                        tipoDeCambioAExtranjeraSeleccionado.MontoTipoDeCambio,
                                                                        tipoDeCambioAExtranjeraSeleccionado.Operador,
                                                                        pCantidadDecimalPrecio);
            }
            else
            {
                montoPagadoExtranjera =
                    totalEfectivoPagoExtranjera + ObtenerConversionSegunTipoDeCambio(totalEfectivoPagoNacional,
                                                                        tipoDeCambioAExtranjeraSeleccionado.MontoTipoDeCambio,
                                                                        tipoDeCambioAExtranjeraSeleccionado.Operador,
                                                                        pCantidadDecimalPrecio);

                montoPagadoNacional = ObtenerConversionSegunTipoDeCambio(montoPagadoExtranjera,
                                                                        tipoDeCambioANacionalSeleccionado.MontoTipoDeCambio,
                                                                        tipoDeCambioANacionalSeleccionado.Operador,
                                                                        pCantidadDecimalPrecio);
            }


            //Calcular vuelto segun moneda
            ////Verificar Faltantes  en doble moneda
            if (pCodigoMonedaVuelto == pCodigoMonedaBase)
            {
                if (montoPagadoNacional >= pVenta.TotalNacional) //Entonces hay vuelto
                {
                    totalVueltoNacional = Math.Round(montoPagadoNacional - pVenta.TotalNacional,
                                            pCantidadDecimalPrecio);
                    totalVueltoExtranjera = ObtenerConversionSegunTipoDeCambio(totalVueltoNacional,
                                                                    tipoDeCambioAExtranjeraSeleccionado.MontoTipoDeCambio,
                                                                    tipoDeCambioAExtranjeraSeleccionado.Operador,
                                                                    pCantidadDecimalPrecio);

                    totalFaltanteNacional = 0;
                    totalFaltanteExtranjera = 0;

                }
                else
                {
                    totalVueltoNacional = 0;
                    totalVueltoExtranjera = 0;
                    totalFaltanteNacional = pVenta.TotalNacional - montoPagadoNacional;
                    totalFaltanteExtranjera = ObtenerConversionSegunTipoDeCambio(pTotalFaltanteNacional,
                                                                    tipoDeCambioAExtranjeraSeleccionado.MontoTipoDeCambio,
                                                                    tipoDeCambioAExtranjeraSeleccionado.Operador,
                                                                    pCantidadDecimalPrecio);
                }
            }
            else
            {
                if (montoPagadoExtranjera >= pVenta.TotalExtranjera)
                {
                    totalVueltoExtranjera = Math.Round(montoPagadoExtranjera - pVenta.TotalExtranjera,
                                                pCantidadDecimalPrecio);
                    totalVueltoNacional = ObtenerConversionSegunTipoDeCambio(totalVueltoExtranjera,
                                                        tipoDeCambioANacionalSeleccionado.MontoTipoDeCambio,
                                                        tipoDeCambioANacionalSeleccionado.Operador,
                                                        pCantidadDecimalPrecio);
                    totalFaltanteNacional = 0;
                    totalFaltanteExtranjera = 0;
                }
                else
                {
                    totalVueltoExtranjera = 0;
                    totalVueltoNacional = 0;
                    totalFaltanteExtranjera = pVenta.TotalExtranjera - montoPagadoExtranjera;
                    totalFaltanteNacional = ObtenerConversionSegunTipoDeCambio(pTotalFaltanteExtranjera,
                                                        tipoDeCambioANacionalSeleccionado.MontoTipoDeCambio,
                                                        tipoDeCambioANacionalSeleccionado.Operador,
                                                        pCantidadDecimalPrecio);
                }
            }

            //Resultado en Pantalla
            pTotalFaltanteNacional = totalFaltanteNacional;
            pTotalFaltanteExtranjera = totalFaltanteExtranjera;

            //si se queiere saber sobre el simbolo de moenda tomar propiedad: IdMonedaVenta; idMonedaVuelto es temporal
            pTotalVueltoSegunMoneda = pCodigoMonedaVuelto == pCodigoMonedaBase ? totalVueltoNacional : totalVueltoExtranjera;

            //Obteniendo resultados de pago, vuelto, faltante
            pVenta.ValidarYActualizarPagoEnEfectivo(totalVueltoNacional, totalVueltoExtranjera, pCodigoMonedaVuelto, pCodigoMonedaBase);
            
            //solo cuando hay vuelto extranjero o bimoneda
            if (pFlagCambioMonedaVuelto == true)
                CalcularVueltoVentaBimoneda(totalVueltoExtranjera, totalVueltoNacional,
                                            pTotalVueltoSegunMoneda, pEfectivoVueltoExtranjera,
                                            pCodigoMonedaVuelto,pCodigoMonedaBase,
                                            tipoDeCambioANacionalSeleccionado,pCantidadDecimalPrecio);


            // //Actualizamos Venta Actual, con datos del pago
            // pVenta.TotalVueltoNacional = totalVueltoNacional;
            // pVenta.TotalVueltoExtranjera = totalVueltoExtranjera;

        }


        //este metodo no va
        private void CalcularVueltoVentaBimoneda(decimal pTotalVueltoExtranjera,decimal pTotalVueltoNacional, 
                                                decimal pTotalVueltoSegunMoneda,decimal pEfectivoVueltoExtranjera,
                                                string pCodigoMonedaVuelto, string pCodigoMonedaBase,
                                                TipoDeCambio tipoDeCambioANacionalSeleccionado,int pCantidadDecimalPrecio)
        {

            decimal totalVueltoCambioMoneda = 0;

            //contiene el total de vuelto en soles o dolares
            if (pTotalVueltoSegunMoneda == 0)            
                return;
            
            if (pCodigoMonedaVuelto == pCodigoMonedaBase)
            {
                totalVueltoCambioMoneda = pTotalVueltoExtranjera;
            }
            else        
                totalVueltoCambioMoneda = pTotalVueltoNacional;            

            //Validacion de vuelto y conversion del vuelto
            if (totalVueltoCambioMoneda < pEfectivoVueltoExtranjera)            
                throw new ArgumentException(Mensajes.advertencia_MontoExcedeElVueltoOriginal);            

            //Conversion del vuelto soles
            var vueltoSegunMoneda = totalVueltoCambioMoneda - pEfectivoVueltoExtranjera;

            if (vueltoSegunMoneda > 0)
            {
                pTotalVueltoNacional = ObtenerConversionSegunTipoDeCambio(vueltoSegunMoneda,
                                                tipoDeCambioANacionalSeleccionado.MontoTipoDeCambio,
                                                tipoDeCambioANacionalSeleccionado.Operador,
                                                pCantidadDecimalPrecio);
            }
            else            
                pTotalVueltoNacional = 0;
            
            pTotalVueltoExtranjera = pEfectivoVueltoExtranjera;
        }


        private decimal ObtenerConversionSegunTipoDeCambio(decimal pValor, decimal pTipoDecambio,
                                                           string pOperador, int pRedondeoConversionTipoDecambio)
        {
            decimal valorConvertido = 0;

            if (pOperador == "/")
            {
                valorConvertido = Math.Round(pValor / pTipoDecambio, pRedondeoConversionTipoDecambio);
            }
            else            
                valorConvertido = Math.Round(pValor * pTipoDecambio, pRedondeoConversionTipoDecambio);            

            return valorConvertido;
        }   
    } 
}