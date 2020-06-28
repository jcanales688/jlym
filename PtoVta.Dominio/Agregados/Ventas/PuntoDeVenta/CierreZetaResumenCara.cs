using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
  public class CierreZetaResumenCara:Entidad
  {
        // public Guid CaraId { get; set; }
        public string CodigoCara { get; set; }
        public string DescripcionCara { get; set; }
        // public Guid ArticuloId { get; set; }
        public string CodigoArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal TotalNacional { get; set; }

        public string CodigoCierreZetaPuntoDeVenta { get; set; }      
  }
}