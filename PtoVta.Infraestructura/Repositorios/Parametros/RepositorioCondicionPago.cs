using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioCondicionPago : Repositorio<CondicionPago>, IRepositorioCondicionPago
    {
        public RepositorioCondicionPago(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public CondicionPago ObtenerPorCodigo(string pCodigoCondicionPago)
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

                var condicionDePago = cn.QueryFirstOrDefault<CondicionPago>(cadenaSQL,
                                    new { USERID = pCodigoCondicionPago });

                if (condicionDePago != null)
                {
                    return condicionDePago;
                }
                else
                    return null;
            }
        }
    }
}