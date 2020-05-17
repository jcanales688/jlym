using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Inventarios;

namespace PtoVta.Aplicacion.GestionInventarios
{
    public class ServicioAplicacionArticulo : IServicioAplicacionArticulo
    {
        public ResultadoServicio<ArticuloDTO> ObtenerPorCategoriaYSubcategoria(string pCodigoCategoria, string pCodigoSubCategoria)
        {
            throw new NotImplementedException();
        }
    }
}