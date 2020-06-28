using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
  public class CierreZetaResumenVendedor : Entidad
  {

        // public Guid VendedorId { get; set; }
        public string CodigoVendedor { get; set; }
        public string Nombres { get; set; }
        public decimal TotalNacional { get; set; }


        public string CodigoCierreZetaPuntoDeVenta { get; set; }      
  }
}