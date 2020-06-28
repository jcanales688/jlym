using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
  public class CierreZetaResumenCategoria : Entidad
  {
        // public Guid CategoriaId { get; set; }
        public string CodigoCategoria { get; set; }
        public string Descripcion { get; set; }
        public decimal TotalNacional { get; set; }

        public string CodigoCierreZetaPuntoDeVenta { get; set; }      
  }
}