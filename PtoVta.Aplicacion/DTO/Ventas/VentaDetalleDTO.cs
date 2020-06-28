using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class VentaDetalleDTO
    {
        public decimal NumeroDocumento { get; set; }
        public short Secuencia { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public string Periodo { get; set; }
        public int NumeroTurno { get; set; }
        public string NumeroCara { get; set; }
        public decimal PorcentajeImpuestoIgv { get; set; }
        public decimal PorcentajeImpuestoIsc { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjera { get; set; }
        public decimal ImpuestoNacional { get; set; }
        public decimal ImpuestoExtranjera { get; set; }
        public decimal PorcentajeDescuentoPrimero { get; set; }
        public decimal TotalDescuentoNacional { get; set; }
        public decimal TotalDescuentoExtranjera { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioVenta { get; set; }
        public string ArticuloDescripcionArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public string UsuarioSistema { get; set; }
        public int EsFormula { get; set; }
        public string ArticuloCodigoArticulo { get; set; }


        public string CodigoArticulo { get; set; }
        public string CodigoArticuloAlterno { get; set; }
        public string CodigoMoneda { get; set; }
        public string CodigoEstadoDocumento { get; set; }        
    }

}