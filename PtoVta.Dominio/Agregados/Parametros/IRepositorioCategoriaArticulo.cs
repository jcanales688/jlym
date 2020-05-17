using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioCategoriaArticulo : IRepositorio<CategoriaArticulo>
    {
        CategoriaArticulo ObtenerPorCodigo(string pCodigoCategoriaArticulo);        
    }
}
