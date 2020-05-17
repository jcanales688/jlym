
using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Colaborador
{
    public class RepositorioArticulo : Repositorio<Articulo>, IRepositorioArticulo
    {
        public RepositorioArticulo(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public IEnumerable<Articulo> ObtenerPorCategoriaYSubcategoria(string pCodigoCategoria, string pCodigoSubCategoria)
        {
            throw new NotImplementedException();
        }
    }

}