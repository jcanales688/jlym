using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class VentaConTarjetaDTO
    {
        public decimal NumeroDocumento { get; set; }
        public short Secuencia { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal TotalTarjetaNacional { get; set; }
        public decimal TotalTarjetaExtranjera { get; set; }
        public DateTime FechaProceso { get; set; }
        public string TarjetaDescripcionTarjeta { get; set; }


        public string CodigoMoneda { get; set; }
        public string CodigoTarjeta { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string CodigoAlmacen { get; set; }
    }
}