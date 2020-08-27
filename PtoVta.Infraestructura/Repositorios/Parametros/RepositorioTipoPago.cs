using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

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
                string cadenaSQL = @"SELECT	TYPEPAYMENTID	AS CodigoTipoPago
                                            ,DESCR			AS DescripcionTipoPago
                                            ,SHOW			AS Mostrar
                                    FROM	" + BaseDatos.PrefijoTabla + @"TYPEPAYMENT (NOLOCK)
                                    WHERE	TYPEPAYMENTID	= @TYPEPAYMENTID";

                var tipoDePago = cn.QueryFirstOrDefault<TipoPago>(cadenaSQL,
                                    new { TYPEPAYMENTID = pCodigoTipoPago });

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