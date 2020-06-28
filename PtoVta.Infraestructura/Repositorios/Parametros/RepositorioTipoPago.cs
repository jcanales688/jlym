using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioTipoPago : Repositorio<TipoPago>, IRepositorioTipoPago
    {
        public RepositorioTipoPago(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public TipoPago ObtenerPorCodigo(string pCodigoTipoPago)
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

                var tipoDePago = cn.QueryFirstOrDefault<TipoPago>(cadenaSQL,
                                    new { USERID = pCodigoTipoPago });

                if (tipoDePago != null)
                {
                    return tipoDePago;
                }
                else
                    return null;

            }
            // var consultaTipoPago = (from tp in unidadTrabajoActual.TiposPago
            //     where tp.CodigoTipoPago == pCodigoTipoPago
            //     select tp).FirstOrDefault();

            // return consultaTipoPago;
        }
    }
}