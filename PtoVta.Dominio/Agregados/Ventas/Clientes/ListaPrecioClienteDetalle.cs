using System;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class ListaPrecioClienteDetalle: Entidad
    {
        public decimal MontoDescuento { get; set; }
        public decimal PrecioAntesLista { get; set; }
        public decimal NuevoPrecioCliente { get; set; }
        public string DescripcionArticulo { get; set; }


        public string CodigoListaPrecioCliente { get; set; }

        public string CodigoAlmacen { get; set; }
        public string CodigoArticulo { get; set; }

        public Almacen Almacen { get; private set; }
        public Articulo Articulo { get; private set; }        
    }
}