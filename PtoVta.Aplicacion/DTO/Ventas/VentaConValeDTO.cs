using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class VentaConValeDTO
    {
        public decimal NumeroDocumento { get; set; }
        public decimal NumeroVale { get; set; }
        public DateTime FechaProceso { get; set; }
        public decimal MontoVale { get; set; }

        public string CodigoCliente { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string CodigoMoneda { get; set; }
    }

}