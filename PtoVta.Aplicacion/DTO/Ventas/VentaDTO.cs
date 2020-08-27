using System;
using System.Collections.Generic;
using PtoVta.Aplicacion.DTO.Parametros;

namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class VentaDTO
    {
        public string NumeroDocumento { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public string Periodo { get; set; } 
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjera { get; set; }
        public decimal SubTotalNacional { get; set; }
        public decimal SubTotalExtranjera { get; set; }
        public decimal ImpuestoIgvNacional { get; set; }
        public decimal ImpuestoIgvExtranjera { get; set; }
        public decimal ImpuestoIscNacional { get; set; }
        public decimal ImpuestoIscExtranjera { get; set; }
        public decimal TotalNoAfectoNacional { get; set; }
        public decimal TotalNoAfectoExtranjera { get; set; }
        public decimal TotalAfectoNacional { get; set; }
        public decimal ValorVenta { get; set; }
        public decimal PorcentajeDescuentoPrimero { get; set; }
        public decimal PorcentajeDescuentoSegundo { get; set; }
        public decimal TotalDescuentoNacional { get; set; }
        public decimal TotalDescuentoExtranjera { get; set; }
        public decimal TotalVueltoNacional { get; set; }
        public decimal TotalVueltoExtranjera { get; set; }
        public decimal TotalEfectivoNacional { get; set; }
        public decimal TotalEfectivoExtranjera { get; set; }
        public string RucCliente { get; set; }
        public string NombreCompletoCliente { get; set; }
        public string Placa { get; set; }
        public decimal NumeroVale { get; set; }
        public decimal TipoCambio { get; set; }
        public bool ProcesadoCierreZ { get; set; }
        public bool ProcesadoCierreX { get; set; }
        public int Kilometraje { get; set; }
        public bool AfectaInventario { get; set; }
        // public string TipoPagoCodigoTipoPago { get; set; }


        //Variables Adicionales Calculo de Vuelto, Correlativos y Documentos Antcipados
        public bool EsVentaPagoAdelantado  { get; set; }           //Determina si se creara documento anticipado
        public string TipoDeVenta  { get; set; }                   //determina que tipo de serie y correlativo se obtendra segun el tipo de venta
        public bool FlagCambioDeMonedaEnVuelto  { get; set; }          //participa en el calculo del vuelto cuendo es en moneda extrajera o bimoneda
        // public decimal TotalVueltoExtranjera  { get; set; }     //participa en el calculo del vuelto cuendo es en moneda extrajera o bimoneda
        // public decimal TotalVueltoNacional  { get; set; }       //participa en el calculo del vuelto cuendo es en moneda extrajera o bimoneda
        public decimal TotalFaltanteExtranjera  { get; set; }      //participa en el calculo del vuelto cuendo es en moneda extrajera o bimoneda
        public decimal TotalFaltanteNacional  { get; set; }        //participa en el calculo del vuelto cuendo es en moneda extrajera o bimoneda
        public string CodigoMonedaVuelto  { get; set; }            //participa en el calculo del vuelto cuendo es en moneda extrajera o bimoneda        


        public string CodigoMoneda { get; set; }
        public string CodigoClaseTipoCambio { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string CodigoEstadoDocumento { get; set; }
        public string CodigoVendedor { get; set; }
        public string CodigoCondicionPago { get; set; }
        public string CodigoTipoPago { get; set; }
        public string CodigoPuntoDeVenta { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoTipoNegocio { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }
        public string CodigoImpuestoIgv { get; set; }
        public string CodigoImpuestoIsc { get; set; }

        public ClienteDTO Cliente { get; set; } 
        

        public VentaDTO(){
            this.VentaDetalles = new List<VentaDetalleDTO>();
            this.VentaConTarjetas = new List<VentaConTarjetaDTO>();
            this.VentaConVales = new List<VentaConValeDTO>();
            // this.DocumentosAnticipado = new List<DocumentoAnticipadoDTO>();
            // this.CuentasPorCobrar = new List<CuentaPorCobrarDTO>();
        }


        // public VentaDTO(): this Inicializa(){}

        public TipoPagoDTO TipoPago { get; set; }
        public List<VentaDetalleDTO> VentaDetalles { get;  set; }
        public List<VentaConTarjetaDTO> VentaConTarjetas { get;  set; }
        public List<VentaConValeDTO> VentaConVales { get;  set; }
        // public List<DocumentoAnticipadoDTO> DocumentosAnticipado { get; set; }
        // public List<CuentaPorCobrarDTO> CuentasPorCobrar { get; set; }   
    }
}