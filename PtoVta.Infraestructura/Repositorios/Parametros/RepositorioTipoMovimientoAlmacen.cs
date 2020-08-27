using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

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
                string cadenaSQL = @"SELECT  TYPEDOCID		AS CodigoTipoMovimientoAlmacen
                                            ,DESCR			AS DescripcionTipoMovimientoAlmacen
                                            ,INOUT			AS IngresoOSalida
                                            ,STKVALUE		AS EsValorizado
                                            ,STKCOSPRICE	AS ValorizadoPorPrecioVentaOCostoReposicion         
                                            ,STKAVGCOST		AS ValorizadoPorCostoPromedio
                                            ,STKBUY			AS EsTipoIngresoPorCompra
                                            ,STKVENDOR		AS RequiereProveedor
                                            ,STKAVERAGE		AS EnCalculoCostoPromedio
                                            ,DESCR_ABR		AS DescripcionAbreviada
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_TYPEDOC (NOLOCK)
                                    WHERE	TYPEDOCID = @TYPEDOCID";

                var tipoMovimientoAlmacen = cn.QueryFirstOrDefault<TipoMovimientoAlmacen>(cadenaSQL,
                                                    new { TYPEDOCID = pCodigoTipoMovimientoAlmacen });

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