using System;
using PtoVta.Dominio.Agregados.Ventas;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public interface IServicioDominioListaPrecios
    {
       Decimal ObtenerPrecioVentaArticulo(Articulo pArticulo, ListaPrecioCliente pListaPrecioCliente, 
                            ListaPrecioInventario pListaPrecioInventario, string pCodigoClienteDeListaPrecio, 
                            string pCodigoClienteInterno, int pCantidadDecimalPrecio);
    }
}