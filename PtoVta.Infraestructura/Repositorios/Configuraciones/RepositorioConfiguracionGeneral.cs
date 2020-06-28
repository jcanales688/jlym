using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Configuraciones
{
    public class RepositorioConfiguracionGeneral : Repositorio<ConfiguracionGeneral>, IRepositorioConfiguracionGeneral
    {
        public RepositorioConfiguracionGeneral(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public ConfiguracionGeneral ObtenerPorCodigo(string pCodigoConfiguracionGeneral)
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

                var configuracionGeneral = cn.QueryFirstOrDefault<ConfiguracionGeneral>(cadenaSQL,
                                                new { USERID = pCodigoConfiguracionGeneral });

                if (configuracionGeneral != null)
                {
                    return configuracionGeneral;
                }
                else
                    return null;
            }
        }
    }
}