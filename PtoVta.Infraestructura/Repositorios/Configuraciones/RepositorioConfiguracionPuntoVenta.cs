using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Configuraciones
{
    public class RepositorioConfiguracionPuntoVenta : Repositorio<ConfiguracionPuntoVenta>, IRepositorioConfiguracionPuntoVenta
    {
        public RepositorioConfiguracionPuntoVenta(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public ConfiguracionPuntoVenta ConsultarTerminalPuntoVenta(string pNombreTerminal, string pNombrePuntoDeVenta)
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

                var configuracionPuntoDeVenta = cn.QueryFirstOrDefault<ConfiguracionPuntoVenta>(cadenaSQL,
                                             new { USERID = pNombreTerminal, USERID2 = pNombrePuntoDeVenta });

                if (configuracionPuntoDeVenta != null)
                {
                    return configuracionPuntoDeVenta;
                }
                else
                    return null;
            }

            // var configPtoVta = (from cptovta in unidadTrabajoActual.ConfiguracionesPuntoVenta
            //     where cptovta.NombreTerminal == pNombreTerminal
            //           && cptovta.NombrePuntoVenta == pNombrePuntoDeVenta
            //     select cptovta).FirstOrDefault();

            // return configPtoVta;
        }

        public ConfiguracionPuntoVenta ObtenerPorCodigo(string pCodigoConfiguracionPuntoVenta)
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

                var configuracionPuntoDeVenta = cn.QueryFirstOrDefault<ConfiguracionPuntoVenta>(cadenaSQL,
                                                    new { USERID = pCodigoConfiguracionPuntoVenta });

                if (configuracionPuntoDeVenta != null)
                {
                    return configuracionPuntoDeVenta;
                }
                else
                    return null;

            }
        }
    }
}