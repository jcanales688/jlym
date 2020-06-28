using System;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public static class VentaFactory
    {
        public static Venta CrearVenta(decimal pNumeroDocumento,DateTime pFechaDocumento,DateTime pFechaProceso,
                    string pPeriodo,decimal pTotalNacional,decimal pTotalExtranjera,
                    decimal pSubTotalNacional,decimal pSubTotalExtranjera,decimal pImpuestoIgvNacional,
                    decimal pImpuestoIGVExtranjera,decimal pImpuestoIscNacional,decimal pImpuestoIscExtranjera,
                    decimal pTotalNoAfectoNacional,decimal pTotalNoAfectoExtranjera,decimal pTotalAfectoNacional,
                    decimal pValorVenta,decimal pPorcentajeDescuentoPrimero,decimal pPorcentajeDescuentoSegundo,
                    decimal pTotalDescuentoNacional,decimal pTotalDescuentoExtranjera,decimal pTotalVueltoNacional,
                    decimal pTotalVueltoExtranjera,decimal pTotalEfectivoNacional,decimal pTotalEfectivoExtranjera,
                    string pPlaca,decimal pNumeroVale,decimal pTipoCambio,
                    int pProcesadoCierreZ,int pProcesadoCierreX,int pKilometraje,
                    Moneda pMoneda, ClaseTipoCambio pClaseTipoCambio, Cliente pCliente, 
                    TipoDocumento pTipoDocumento, EstadoDocumento pEstadoDocumento, Vendedor pVendedor, 
                    CondicionPago pCondicionPago,TipoPago pTipoPago, ConfiguracionPuntoVenta pConfiguracionPuntoVenta,
                    Almacen pAlmacen, TipoNegocio pTipoNegocio, UsuarioSistema pUsuarioSistema)
        {
            var venta = new Venta();

            venta.GenerarNuevaIdentidad();

            venta.NumeroDocumento = pNumeroDocumento;
            venta.FechaDocumento = pFechaDocumento;
            venta.FechaProceso = pFechaProceso;
            venta.Periodo = pPeriodo;
            venta.TotalNacional = pTotalNacional;
            venta.TotalExtranjera = pTotalExtranjera;
            venta.SubTotalNacional = pSubTotalNacional;
            venta.SubTotalExtranjera = pSubTotalExtranjera;
            venta.ImpuestoIgvNacional = pImpuestoIgvNacional;
            venta.ImpuestoIGVExtranjera = pImpuestoIGVExtranjera;
            venta.ImpuestoIscNacional = pImpuestoIscNacional;
            venta.ImpuestoIscExtranjera = pImpuestoIscExtranjera;
            venta.TotalNoAfectoNacional = pTotalNoAfectoNacional;
            venta.TotalNoAfectoExtranjera = pTotalNoAfectoExtranjera;
            venta.TotalAfectoNacional = pTotalAfectoNacional;
            venta.ValorVenta = pValorVenta; //Investigar de donde sale el valor venta ? venta.Subtotalventa
            venta.PorcentajeDescuentoPrimero = pPorcentajeDescuentoPrimero;
            venta.PorcentajeDescuentoSegundo = pPorcentajeDescuentoSegundo;
            venta.TotalDescuentoNacional = pTotalDescuentoNacional;
            venta.TotalDescuentoExtranjera = pTotalDescuentoExtranjera;
            venta.TotalVueltoNacional = pTotalVueltoNacional;
            venta.TotalVueltoExtranjera = pTotalVueltoExtranjera;
            venta.TotalEfectivoNacional = pTotalEfectivoNacional;
            venta.TotalEfectivoExtranjera = pTotalEfectivoExtranjera;
            venta.RucCliente = pCliente.Ruc;                                //pRucCliente;                      
            venta.NombreCompletoCliente = pCliente.NombresORazonSocial;     //pNombreCompletoCliente;           
            venta.Placa = pPlaca;
            venta.NumeroVale = pNumeroVale;                                 
            venta.TipoCambio = pTipoCambio;                                 
            venta.ProcesadoCierreZ = pProcesadoCierreZ;
            venta.ProcesadoCierreX = pProcesadoCierreX;
            venta.Kilometraje = pKilometraje;

            venta.Habilitar();

            venta.EstablecerMonedaDeVenta(pMoneda);
            venta.EstablecerClaseTipoCambioDeVenta(pClaseTipoCambio);
            venta.EstablecerClienteDeVenta(pCliente);
            venta.EstablecerTipoDocumentoDeVenta(pTipoDocumento);
            venta.EstablecerEstadoDocumentoDeVenta(pEstadoDocumento);
            venta.EstablecerVendedorDeVenta(pVendedor);
            venta.EstablecerCondicionPagoDeVenta(pCondicionPago);
            venta.EstablecerTipoPagoDeVenta(pTipoPago);
            venta.EstablecerConfiguracionPuntoVentaDeVenta(pConfiguracionPuntoVenta);
            venta.EstablecerAlmacenDeVenta(pAlmacen);
            venta.EstablecerTipoNegocioDeVenta(pTipoNegocio);
            venta.EstablecerUsuarioSistemaDeVenta(pUsuarioSistema);

            venta.EstablecerImpuestoIgvDeCliente(pCliente.ImpuestoIgv);

            if (pCliente.ImpuestoIsc != null)
                venta.EstablecerImpuestoIscDeCliente(pCliente.ImpuestoIsc);


            return venta;
        }
    }
}