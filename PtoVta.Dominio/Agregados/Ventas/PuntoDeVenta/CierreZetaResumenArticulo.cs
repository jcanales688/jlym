using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
  public class CierreZetaResumenArticulo:Entidad
  {

        // public Guid ArticuloId { get; set; }
        public string CodigoArticulo { get; set; }
        public string Descripcion { get; set; }
        public decimal TotalNacional { get; set; }

        public string CodigoCierreZetaPuntoDeVenta { get; set; }      
  }
}