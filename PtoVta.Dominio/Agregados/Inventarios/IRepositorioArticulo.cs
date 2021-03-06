using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public interface IRepositorioArticulo: IRepositorio<Articulo>
    {
        IEnumerable<Articulo> ObtenerPorCategoriaYSubcategoria(string pCodigoCategoria ,string pCodigoSubCategoria, string pCodigoAlmacen);
        Articulo ObtenerPorCodigo(string pCodigoArticulo, string pCodigoAlmacen);
    }

}