using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Modulo
{
    public class RepositorioCategoriaArticulo : Repositorio<CategoriaArticulo>, IRepositorioCategoriaArticulo
    {
        public RepositorioCategoriaArticulo(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override IEnumerable<CategoriaArticulo> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public CategoriaArticulo ObtenerPorCodigo(string pCodigoCategoriaArticulo)
        {
            throw new NotImplementedException();
        }
    }

}