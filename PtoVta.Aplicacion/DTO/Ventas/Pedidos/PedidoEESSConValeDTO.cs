using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class PedidoEESSConValeDTO
    {
        public int Correlativo { get; set; }
        public decimal NumeroVale { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoAlmacen { get; set; }                
    }
}