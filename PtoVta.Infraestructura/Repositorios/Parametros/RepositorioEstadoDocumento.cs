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

        public EstadoDocumento ObtenerPorCodigo(string pCodigoEstadoDocumento)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	DOCSTATUSID		AS CodigoEstadoDocumento
                                            ,DESCR			AS DescripcionEstadoDocumento
                                            ,DOCSTATUSID	AS AbreviaturaEstadoDocumento
                                    FROM	PC_IN_DOCSTATUS (NOLOCK)
                                    WHERE	DOCSTATUSID		= @DOCSTATUSID";

                var estadoDocumento = cn.QueryFirstOrDefault<EstadoDocumento>(cadenaSQL,
                                                new { DOCSTATUSID = pCodigoEstadoDocumento });

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