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
    public class RepositorioConfiguracionPuntoVenta : Repositorio<ConfiguracionPuntoVenta>, IRepositorioConfiguracionPuntoVenta
    {
        public RepositorioConfiguracionPuntoVenta(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public void ActualizarCorrelativos(ConfiguracionPuntoVenta pConfiguracionPuntoVenta)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string sqlActualizaCorrelativos = @"UPDATE	" + BaseDatos.PrefijoTabla + @"OP_SALESPOINTSETTING
                                                    SET		NBRTICKFAC		= @NBRTICKFAC
                                                            ,NBRTICKBOL		= @NBRTICKBOL
                                                            ,BATNBRSALES	= @BATNBRSALES
                                                    WHERE	SALESPOINT		= @SALESPOINT";

                var filasAfectadas = cn.Execute(sqlActualizaCorrelativos, new
                {
                    NBRTICKFAC = pConfiguracionPuntoVenta.SerieCorrelativoTickFactura,
                    NBRTICKBOL = pConfiguracionPuntoVenta.SerieCorrelativoTickBoleta,
                    BATNBRSALES = pConfiguracionPuntoVenta.CorrelativoMovimientoAlmacenPorVenta,    
                    SALESPOINT = pConfiguracionPuntoVenta.CodigoPuntoDeVenta
                });
            }
        }        

        public ConfiguracionPuntoVenta ObtenerPorTerminalYPuntoVenta(string pNombreTerminal, string pCodigoPuntoDeVenta)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	SALESPOINT			AS CodigoPuntoDeVenta 
                                            ,TERMINALNAME		AS NombreTerminal 
                                            ,SERIALNUMBER		AS NumeroSerieMaquinaRegistradora
                                            ,STKTICKFAC			AS PermiteTicketFactura 
                                            ,STKTICKBOL			AS PermiteTicketBoleta 
                                            ,''					AS SimboloMonedaCaja 
                                            ,STKLOADSALE		AS PermiteColaTransaccionesManual         
                                            ,DISPTICKFAC		AS DispositivoTicketFactura
                                            ,TYPEDISPTICKFAC	AS TipoDispositivoSalidaTicketFactura
                                            ,DISPTICKBOL		AS DispositivoTicketBoleta
                                            ,TYPEDISPTICKBOL	AS TipoDispositivoSalidaTicketBoleta 
                                            ,NBRTICKFAC			AS SerieCorrelativoTickFactura
                                            ,NBRTICKBOL			AS SerieCorrelativoTickBoleta
                                            ,STKZETAOK			AS RealizoCierreZeta
                                            ,STKTURNOK			AS RealizoCierreTurno
                                            ,STKJUMPAUTOMAT		AS PermiteSaltoAutomatico
                                            ,QTYJUMPAUTOMAT		AS CantidadSaltoAutomatico
                                            ,STKDELETEDOC		AS PedirClaveAnulacionDocumento
                                            ,BATNBRSALES		AS CorrelativoMovimientoAlmacenPorVenta
                                            ,CURYID				AS CodigoMonedaCaja
                                            ,BUSINESSTYPE		AS CodigoTipoNegocio
                                            ,SITEID				AS CodigoAlmacenPuntoVenta
                                            ,PRINTERTYPEID		AS CodigoTipoImpresora
                                            ,''					AS CodigoEstadoDocumentoDefault
                                            ,''					AS CodigoTipoPagoDefault
                                            ,''					AS CodigoEstadoDocumentoAnulado
                                    FROM	" + BaseDatos.PrefijoTabla + @"OP_SALESPOINTSETTING 
                                    WHERE	SALESPOINT			= @SALESPOINT
                                            AND TERMINALNAME	= @TERMINALNAME;
                                            
                                    SELECT	DOCSTATUSID		AS CodigoEstadoDocumento
                                            ,DESCR			AS DescripcionEstadoDocumento
                                            ,DOCSTATUSID	AS AbreviaturaEstadoDocumento
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_DOCSTATUS (NOLOCK)
                                    WHERE	DOCSTATUSID			= @DOCSTATUSIDDEFAULT;
                                    
                                    SELECT  TYPEPAYMENTID	AS CodigoTipoPago
                                            ,DESCR			AS DescripcionTipoPago
                                            ,SHOW			AS Mostrar
                                    FROM	" + BaseDatos.PrefijoTabla + @"TYPEPAYMENT (NOLOCK)
                                    WHERE	TYPEPAYMENTID		= @TYPEPAYMENTIDDEFAULT;
                                    
                                    SELECT	DOCSTATUSID		AS CodigoEstadoDocumento
                                            ,DESCR			AS DescripcionEstadoDocumento
                                            ,DOCSTATUSID	AS AbreviaturaEstadoDocumento
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_DOCSTATUS (NOLOCK)
                                    WHERE	DOCSTATUSID			= @DOCSTATUSIDANULACION";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                            new { 
                                                    SALESPOINT = pCodigoPuntoDeVenta, 
                                                    TERMINALNAME = pNombreTerminal, 
                                                    DOCSTATUSIDDEFAULT = EnumEstadoDocumento.CodigoEstadoDocumentoPorDefecto,
                                                    TYPEPAYMENTIDDEFAULT = EnumTipoPago.CodigoTipoPagoPorDefecto,
                                                    DOCSTATUSIDANULACION = EnumEstadoDocumento.CodigoEstadoDocumentoAnulado
                                                });

                var configuracionPuntoDeVenta = resultado.Read<ConfiguracionPuntoVenta>().FirstOrDefault();
                var estadoDocumentoPorDefecto = resultado.Read<EstadoDocumento>().FirstOrDefault();
                var tipoPagoPorDefecto = resultado.Read<TipoPago>().FirstOrDefault();
                var estadoDocumentoAnulado = resultado.Read<EstadoDocumento>().FirstOrDefault();

                if (configuracionPuntoDeVenta != null)
                {
                    return MapeoConfiguracionPuntoVenta(configuracionPuntoDeVenta, estadoDocumentoPorDefecto, tipoPagoPorDefecto, estadoDocumentoAnulado);
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

        public ConfiguracionPuntoVenta ObtenerPorPuntoDeVenta(string pCodigoPuntoDeVenta)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	SALESPOINT			AS CodigoPuntoDeVenta 
                                            ,TERMINALNAME		AS NombreTerminal 
                                            ,SERIALNUMBER		AS NumeroSerieMaquinaRegistradora
                                            ,STKTICKFAC			AS PermiteTicketFactura 
                                            ,STKTICKBOL			AS PermiteTicketBoleta 
                                            ,''					AS SimboloMonedaCaja 
                                            ,STKLOADSALE		AS PermiteColaTransaccionesManual         
                                            ,DISPTICKFAC		AS DispositivoTicketFactura
                                            ,TYPEDISPTICKFAC	AS TipoDispositivoSalidaTicketFactura
                                            ,DISPTICKBOL		AS DispositivoTicketBoleta
                                            ,TYPEDISPTICKBOL	AS TipoDispositivoSalidaTicketBoleta 
                                            ,NBRTICKFAC			AS SerieCorrelativoTickFactura
                                            ,NBRTICKBOL			AS SerieCorrelativoTickBoleta   
                                            ,STKZETAOK			AS RealizoCierreZeta
                                            ,STKTURNOK			AS RealizoCierreTurno
                                            ,STKJUMPAUTOMAT		AS PermiteSaltoAutomatico
                                            ,QTYJUMPAUTOMAT		AS CantidadSaltoAutomatico
                                            ,STKDELETEDOC		AS PedirClaveAnulacionDocumento
                                            ,BATNBRSALES		AS CorrelativoMovimientoAlmacenPorVenta
                                            ,CURYID				AS CodigoMonedaCaja
                                            ,BUSINESSTYPE		AS CodigoTipoNegocio
                                            ,SITEID				AS CodigoAlmacenPuntoVenta
                                            ,PRINTERTYPEID		AS CodigoTipoImpresora
                                            ,''					AS CodigoEstadoDocumentoDefault
                                            ,''					AS CodigoTipoPagoDefault
                                            ,''					AS CodigoEstadoDocumentoAnulado
                                    FROM	" + BaseDatos.PrefijoTabla + @"OP_SALESPOINTSETTING 
                                    WHERE	SALESPOINT			= @SALESPOINT";

                var configuracionPuntoDeVenta = cn.QueryFirstOrDefault<ConfiguracionPuntoVenta>(cadenaSQL,
                                                    new { SALESPOINT = pCodigoPuntoDeVenta });

                if (configuracionPuntoDeVenta != null)
                {
                    return configuracionPuntoDeVenta;
                }
                else
                    return null;

            }
        }

        private ConfiguracionPuntoVenta MapeoConfiguracionPuntoVenta(ConfiguracionPuntoVenta pConfiguracionPuntoVenta, EstadoDocumento pEstadoDocumentoPorDefecto, 
                                        TipoPago pTipoPagoPorDefecto, EstadoDocumento pEstadoDocumentoAnulado)
        {
            var configuracionPuntoDeVenta = new ConfiguracionPuntoVenta();
            configuracionPuntoDeVenta = pConfiguracionPuntoVenta;
            configuracionPuntoDeVenta.EstablecerEstadoDocumentoDefaultDeConfiguracionPuntoVenta(pEstadoDocumentoPorDefecto);
            configuracionPuntoDeVenta.EstablecerTipoPagoDefaultDeConfiguracionPuntoVenta(pTipoPagoPorDefecto);
            configuracionPuntoDeVenta.EstablecerEstadoDocumentoAnuladoDeConfiguracionPuntoVenta(pEstadoDocumentoAnulado);

            return configuracionPuntoDeVenta;
        }        
    }
}