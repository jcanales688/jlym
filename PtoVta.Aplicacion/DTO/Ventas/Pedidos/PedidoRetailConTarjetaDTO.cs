using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class PedidoRetailConTarjetaDTO
    {
        public int Correlativo { get; set; }
        public short Secuencia { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal TotalTarjetaNacional { get; set; }
        public decimal TotalTarjetaExtranjera { get; set; }
        public int EsTransaccionPinPad { get; set; }
        public string TipoTarjeta { get; set; }
        public string DNIAsociadoATarjeta { get; set; }
        public string DescripcionTarjeta { get; set; }

        
        public string CodigoAlmacen { get; set; }
        public string CodigoTarjeta { get; set; }   
        public string CodigoMoneda { get; set; }           
    }
}