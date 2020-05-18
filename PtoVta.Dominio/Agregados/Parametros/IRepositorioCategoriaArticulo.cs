using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioCategoriaArticulo : IRepositorio<CategoriaArticulo>
    {
        IEnumerable<CategoriaArticulo> ObtenerTodos(string pTipoNegocio);
        CategoriaArticulo ObtenerPorCodigo(string pCodigoCategoriaArticulo);        
    }
}
