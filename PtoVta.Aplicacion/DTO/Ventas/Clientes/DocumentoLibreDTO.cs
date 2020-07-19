using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class DocumentoLibreDTO
    {
        public decimal NumeroDocumentoLibre { get; set; }
        public DateTime FechaProcesoInicial { get; set; }
        public DateTime FechaProcesoFinal { get; set; }
        public decimal TotalLibre { get; set; }

        public string CodigoCliente { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }        
    }
}