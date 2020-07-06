using System;
using System.Collections.Generic;
using PtoVta.Aplicacion.DTO.Parametros;

namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class VentaDTO
    {
        public Guid Id { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public string Periodo { get; set; } 
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjera { get; set; }
        public decimal SubTotalNacional { get; set; }
        public decimal SubTotalExtranjera { get; set; }
        public decimal ImpuestoIgvNacional { get; set; }
        public decimal ImpuestoIGVExtranjera { get; set; }
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
        public string ClienteRuc { get; set; }
        public string ClienteNombresORazonSocial { get; set; }
        public string Placa { get; set; }
        public decimal NumeroVale { get; set; }
        public decimal TipoCambio { get; set; }
        public bool ProcesadoCierreZ { get; set; }
        public bool ProcesadoCierreX { get; set; }
        public int Kilometraje { get; set; }
        public bool AfectaInventario { get; set; }
        public string TipoPagoCodigoTipoPago { get; set; }


        public string CodigoMoneda { get; set; }
        public string CodigoClaseTipoCambio { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string CodigoEstadoDocumento { get; set; }
        public string CodigoVendedor { get; set; }
        public string CodigoCondicionPago { get; set; }
        public string CodigoTipoPago { get; set; }
        public string CodigoConfiguracionPuntoVenta { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoTipoNegocio { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }
        public string CodigoImpuestoIgv { get; set; }
        public string CodigoImpuestoIsc { get; set; }



        public TipoPagoDTO TipoPago { get; set; }
        public List<VentaDetalleDTO> VentaDetalles { get;  set; }
        public List<VentaConTarjetaDTO> VentaConTarjetas { get;  set; }
        public List<VentaConValeDTO> VentaConVales { get;  set; }
        public List<DocumentoAnticipadoDTO> DocumentosAnticipado { get; set; }
        public List<CuentaPorCobrarDTO> CuentasPorCobrar { get; set; }   
    }
}