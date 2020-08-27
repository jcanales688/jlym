using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public interface IRepositorioListaPrecioInventario: IRepositorio<ListaPrecioInventario>
    {
        ListaPrecioInventario ObtenerListaPrecioInventario(string pCodigoArticulo, string pCodigoAlmacen);
    }
}