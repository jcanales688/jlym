using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class PedidoRetailDetalleDTO
    {
        public int Correlativo { get; set; }
        public string NumeroDocumento { get; set; }
        public short Secuencia { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public string Periodo { get; set; }
        public int NumeroTurno { get; set; }
        public decimal PorcentajeImpuestoIgv { get; set; }
        public decimal PorcentajeImpuestoIsc { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjera { get; set; }
        public decimal ImpuestoNacional { get; set; }
        public decimal ImpuestoExtranjera { get; set; }
        public bool EsInventariable { get; set; }    
        public bool EnInventarioFisico { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal CostoEstandarNacional { get; set; }
        public decimal CostoEstandarExtranjera { get; set; }
        public string CodigoArticuloAlterno { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public int EsFormula { get; set; }
        public string NumeroPeaje { get; set; }     


        public string CodigoTipoDocumento  { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoMoneda { get; set; }        
        public string CodigoArticulo { get; set; }
        public string CodigoUnidadDeMedida { get; set; }        
    }
}