using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioTipoNegocio : Repositorio<TipoNegocio>, IRepositorioTipoNegocio
    {
        public RepositorioTipoNegocio(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public TipoNegocio ObtenerPorCodigo(string pCodigoTipoNegocio)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	BUSINESSTYPE	AS CodigoTipoNegocio
                                            ,DESCRBUSINESS	AS DescripcionTipoNegocio
                                    FROM	PC_OP_BUSINESSTYPE (NOLOCK)
                                    WHERE	BUSINESSTYPE	= @BUSINESSTYPE";

                var tipoDeNegocio = cn.QueryFirstOrDefault<TipoNegocio>(cadenaSQL,
                                                new { BUSINESSTYPE = pCodigoTipoNegocio });

                if (tipoDeNegocio != null)
                {
                    return tipoDeNegocio;
                }
                else
                    return null;

            }
        }
    }
}