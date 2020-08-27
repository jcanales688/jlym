using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Inventarios;

namespace PtoVta.Aplicacion.GestionInventarios
{
    public interface IServicioAplicacionArticulo
    {
        ResultadoServicio<ArticuloDTO> ObtenerPorCategoriaYSubcategoria(string pCodigoCategoria, string pCodigoSubCategoria, string pCodigoAlmacen);        
        decimal ObtenerPrecioVentaDeArticulo(string pCodigoCliente, string pCodigoArticulo, string pCodigoAlmacen);
    }    
}