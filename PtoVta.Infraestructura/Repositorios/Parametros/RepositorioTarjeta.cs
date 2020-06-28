using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioTarjeta : Repositorio<Tarjeta>, IRepositorioTarjeta
    {
        public RepositorioTarjeta(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public Tarjeta ObtenerPorCodigo(string pCodigoTarjeta)
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

                var tarjeta = cn.QueryFirstOrDefault<Tarjeta>(cadenaSQL,
                                            new { USERID = pCodigoTarjeta });

                if (tarjeta != null)
                {
                    return tarjeta;
                }
                else
                    return null;
            }
        }
    }
}