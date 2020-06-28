using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioEstadoDocumento : Repositorio<EstadoDocumento>, IRepositorioEstadoDocumento
    {
        public RepositorioEstadoDocumento(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public EstadoDocumento ObtenerPorCodigo(string CodigoEstadoDocumento)
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

                var estadoDocumento = cn.QueryFirstOrDefault<EstadoDocumento>(cadenaSQL,
                                                new { USERID = CodigoEstadoDocumento });

                if (estadoDocumento != null)
                {
                    return estadoDocumento;
                }
                else
                    return null;
            }
        }
    }
}