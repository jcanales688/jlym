using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class ResultadoPedidoRetailGrabadoDTO
    {
        public int Correlativo { get; set; }
        public string NumeroDocumento { get; set; }
        public string RucCliente { get; set; }
        public string ClienteNombresORazonSocial { get; set; }
        public string ClienteDireccionPrimera { get; set; }
        public decimal TotalNoAfectoNacional { get; set; }
        public decimal TotalNoAfectoExtranjera { get; set; }
        public decimal ImpuestoIgvNacional { get; set; }
        public decimal ImpuestoIGVExtranjera { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjera { get; set; }
        public string TipoPagoCodigoTipoPago { get; set; }
        public string Placa { get; set; }
        public decimal SubTotalNacional { get; set; }
        public decimal SubTotalExtranjera { get; set; }
        public decimal TotalEfectivoNacional { get; set; }
        public decimal TotalEfectivoExtranjera { get; set; }
        public decimal TotalVueltoNacional { get; set; }
        public decimal TotalVueltoExtranjera { get; set; }
        public string CodigoMoneda { get; private set; }
        public string TipoDocumentoDescripcionTipoDocumento { get; set; }        
    }
}