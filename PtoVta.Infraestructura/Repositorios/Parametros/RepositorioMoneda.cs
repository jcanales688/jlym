using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioMoneda : Repositorio<Moneda>, IRepositorioMoneda
    {
        public RepositorioMoneda(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public Moneda ObtenerPorCodigo(string pCodigoMoneda)
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

                var moneda = cn.QueryFirstOrDefault<Moneda>(cadenaSQL,
                                    new { USERID = pCodigoMoneda });

                if (moneda != null)
                {
                    return moneda;
                }
                else
                    return null;
            }
        }
    }
}