using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class ClienteLimiteCreditoDTO
    {
        public decimal PorcentajeLimite { get; set; }
        public decimal MontoLimite { get; set; }
        public decimal Deuda { get; set; }
        public decimal PorcentajeExcede { get; set; }
        public decimal MontoExcedente { get; set; }
        
        public string CodigoCliente { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }        
    }
}