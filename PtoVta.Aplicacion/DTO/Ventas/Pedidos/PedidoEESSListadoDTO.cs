using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class PedidoEESSListadoDTO
    {
        public int Correlativo { get; set; }
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
        public string RucCliente { get; set; }
        public string ClienteNombresORazonSocial { get; set; }
        public string Placa { get; set; }
        public decimal NumeroVale { get; set; }
        public decimal TipoCambio { get; set; }
        public bool ProcesadoCierreZ { get; set; }
        public bool ProcesadoCierreX { get; set; }
        public int Kilometraje { get; set; }


        public string CodigoMoneda { get; private set; }
        public string CodigoClaseTipoCambio { get; private set; }
        public string CodigoCliente { get; private set; }
        public string CodigoTipoDocumento { get; private set; }
        public string CodigoEstadoDocumento { get; private set; }
        public string CodigoVendedor { get; private set; }
        public string CodigoCondicionPago { get; private set; }
        public string CodigoTipoPago { get; private set; }
        public string CodigoPuntoDeVenta { get; private set; }
        public string CodigoAlmacen { get; private set; }
        public string CodigoTipoNegocio { get; private set; }
        public string CodigoUsuarioDeSistema { get; private set; }
        public string CodigoImpuestoIgv { get; private set; }
        public string CodigoImpuestoIsc { get; private set; }        
    }
}