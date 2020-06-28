using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class DocumentoAnticipadoDTO
    {
        public decimal NumeroDocumento { get; set; }
        public DateTime FechaProceso { get; set; }

        public string CodigoTipoDocumento { get; set; }
        public string CodigoAlmacen { get; set; }        
    }
}