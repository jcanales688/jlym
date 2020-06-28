using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioTipoMovimientoAlmacen : Repositorio<TipoMovimientoAlmacen>, IRepositorioTipoMovimientoAlmacen
    {
        public RepositorioTipoMovimientoAlmacen(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public TipoMovimientoAlmacen ObtenerPorCodigo(string pCodigoTipoMovimientoAlmacen)
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

                var tipoMovimientoAlmacen = cn.QueryFirstOrDefault<TipoMovimientoAlmacen>(cadenaSQL,
                                                    new { USERID = pCodigoTipoMovimientoAlmacen });

                if (tipoMovimientoAlmacen != null)
                {
                    return tipoMovimientoAlmacen;
                }
                else
                    return null;

            }
        }
    }
}