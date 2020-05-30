using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class ArticuloDetalle: Entidad
    {
        public decimal StockMinimo { get; set; }
        public decimal StockMaximo { get; set; }
        public decimal StockInicial { get; set; }   
        public decimal StockActual { get; set; }
        public Nullable<DateTime> FechaCreacion { get; set; }
        public Nullable<DateTime> FechaUltimoInv { get; set; }
        public Nullable<decimal> StockUltimoInv { get; set; }
        public decimal CostoPromedioNacional { get; set; }
        public decimal CostoPromedioExtranjera { get; set; }
        public decimal CostoReposicionNacional { get; set; }
        public decimal CostoReposicionExtranjera { get; set; }
        public decimal CostoRepoNacionalUltimoInv { get; set; }
        public decimal CostoRepoExtranjeraUltimoInv { get; set; }   
        public decimal Precio { get; set; }
        public string CodContableInventariable { get; set; }
        public string CodContableNoInventariable { get; set; }

        public string CodigoArticulo { get; set; }
        // public Guid ArticuloId { get; set; }
        public string  CodigoTipoPrecioInventario  { get; set; }
        // public Guid TipoPrecioInventarioId { get; set; }
        public string CodigoAlmacen  { get; set; }
        // public Guid AlmacenId { get; set; }



    }
}