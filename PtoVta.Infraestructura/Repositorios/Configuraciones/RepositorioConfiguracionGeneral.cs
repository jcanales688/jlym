using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.AmbienteVenta;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

namespace PtoVta.Infraestructura.Repositorios.Configuraciones
{
    public class RepositorioConfiguracionGeneral : Repositorio<ConfiguracionGeneral>, IRepositorioConfiguracionGeneral
    {
        public RepositorioConfiguracionGeneral(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }
 
        public ConfiguracionGeneral Obtener()
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	QTYSALESPERSONTURN	AS CantidadTurnos
                                            ,SALESPERSONTURN	AS TurnoActual
                                            ,QTYSIDE			AS CantidadCaras
                                            ,''					AS SimboloMonedaBase
                                            ,''					AS SimboloMonedaExtranjera
                                            ,FLOATPRICE			AS CantidadDecimalPrecio
                                            ,FLOATCOST			AS CantidadDecimalCosto
                                            ,FLOATQTY			AS CantidadDecimalStock
                                            ,FLOATOUTPUT		AS CantidadDecimalResultado
                                            ,FLOATDESC			AS CantidadDecimalDescuento
                                            ,TAXVALUEIGV		AS PorcentajeImpuesto
                                            ,DATEPROCE			AS FechaProceso
                                            ,TYPECONTROLLERFUEL	AS TipoControlCombustible
                                            ,MAXDIFDIARY		AS DiferenciaDiariaPermitida
                                            ,VENCPASS			AS DiasCambioClave
                                            ,SOURCESITE			AS CodigoAlmacenOrigen
                                            ,CURYORIG			AS CodigoMonedaBase
                                            ,''					AS CodigoMonedaExtranjeraPorDefecto
                                            ,CURYTYPEIDSALES	AS CodigoClaseTipoCambioVentas
                                            ,SOURCECURYTYPEID	AS CodigoClaseTipoCambioOrigen
                                            ,TAXID				AS CodigoImpuesto
                                            ,CUSTID				AS CodigoClienteInterno
                                            ,TYPEPRICEUPDATE	AS CodigoTipoPrecioInventarioActualizable
                                    FROM	" + BaseDatos.PrefijoTabla + @"SETUP (NOLOCK);
                                    
                                    SELECT  CURYID		AS CodigoMoneda
                                            ,DESCR		AS DescripcionMoneda
                                            ,DESCRMONEY AS SimboloMoneda
                                    FROM	" + BaseDatos.PrefijoTabla + @"CURRENCY (NOLOCK)
                                    WHERE	CURYID		= @CURYIDPEN;

                                    SELECT  CURYID		AS CodigoMoneda
                                            ,DESCR		AS DescripcionMoneda
                                            ,DESCRMONEY AS SimboloMoneda
                                    FROM	" + BaseDatos.PrefijoTabla + @"CURRENCY (NOLOCK)
                                    WHERE	CURYID		= @CURYIDUSD";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                                new { 
                                                        CURYIDPEN = EnumMoneda.CodigoMonedaBase, 
                                                        CURYIDUSD = EnumMoneda.CodigoMonedaExtranjera,
                                                    });

                var configuracionGeneral = resultado.Read<ConfiguracionGeneral>().FirstOrDefault();
                var monedaBase = resultado.Read<Moneda>().FirstOrDefault();
                var monedaExtranjera = resultado.Read<Moneda>().FirstOrDefault();                

                if (configuracionGeneral != null)
                {
                    return MapeoConfiguracionGeneral(configuracionGeneral, monedaBase, monedaExtranjera);
                }
                else
                    return null;
            }
        }


        private ConfiguracionGeneral MapeoConfiguracionGeneral(ConfiguracionGeneral pConfiguracionGeneral,
                                                            Moneda pMonedaBase, Moneda pMonedaExtranjera)
        {
            var configuracionGeneral = new ConfiguracionGeneral();
            configuracionGeneral = pConfiguracionGeneral;
            configuracionGeneral.EstablecerMonedaBaseDeConfiguracionGeneral(pMonedaBase);
            configuracionGeneral.EstablecerMonedaExtranjeraDefDeConfiguracionGeneral(pMonedaExtranjera);

            return configuracionGeneral;
        }        
    }
}