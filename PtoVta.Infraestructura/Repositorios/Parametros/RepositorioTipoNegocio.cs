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
                string cadenaSQL = @"SELECT	USERID		AS CodigoUsuarioDeSistema
                                            ,EXPIRED	AS FechaExpiracion
                                            ,USERNAME	AS DescripcionUsuario
                                            ,PASSWORD	AS Contraseña
                                            ,STATUS AS EsHabilitado
                                    FROM    SE_USERREC (NOLOCK)
                                    WHERE	USERID			= @USERID";

                var tipoDeNegocio = cn.QueryFirstOrDefault<TipoNegocio>(cadenaSQL,
                                                new { USERID = pCodigoTipoNegocio });

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