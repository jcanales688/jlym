using System;

namespace PtoVta.Aplicacion.DTO.Inventarios
{
    public class ArticuloDetalleDTO
    {

        public Guid Id { get; set; }

        public decimal StockMinimo { get; set; }
        public decimal StockMaximo { get; set; }
        public decimal StockInicial { get; set; }
        public decimal StockActual { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimoInv { get; set; }
        public decimal StockUltimoInv { get; set; }
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
        public string  CodigoTipoPrecioInventario  { get; set; }
        public string CodigoAlmacen  { get; set; }


    }

}