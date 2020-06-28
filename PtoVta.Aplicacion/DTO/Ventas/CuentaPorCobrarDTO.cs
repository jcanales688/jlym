using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class CuentaPorCobrarDTO
    {
        public decimal NumeroDocumento { get; set; }
        public double Referencia { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalNacionalCtaCobrar { get; set; }
        public decimal TotalExtranjeraCtaCobrar { get; set; }
        public decimal PagoDocumentoNacional { get; set; }
        public decimal PagoDocumentoExtranjera { get; set; }
        public decimal SaldoDocumentoNacional { get; set; }
        public decimal SaldoDocumentoExtranjera { get; set; }
        public string Ruc { get; set; }
        public decimal TipoCambio { get; set; }
        public int DiasDeGracia { get; set; }
        public decimal NumeroVale { get; set; }


        public string CodigoMoneda { get; set; }
        public string CodigoClaseTipoCambio { get; set; }
        public string CodigoEstadoDocumento { get; set; }
        public string CodigoDiaDePago { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }
        public string CodigoTipoDocumento { get; set; }
    }
}