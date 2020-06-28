using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class InventarioFisico : Entidad
    {
        public decimal StockFisico { get; set; }

        public string CodigoArticulo { get; set; }

        public string CodigoAlmacen { get; set; }
        public string CodigoCategoriaArticulo { get; set; }
        public string CodigoSubCategoriaArticulo { get; set; }

        public Almacen Almacen { get; private set; }
        public CategoriaArticulo CategoriaArticulo { get; private set; }
        public SubCategoriaArticulo SubCategoriaArticulo { get; private set; }        
    }
}