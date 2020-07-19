using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.CuentasPorCobrar;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Infraestructura.Repositorios.Ventas
{
    public class RepositorioVenta : Repositorio<Venta>, IRepositorioVenta
    {
        public RepositorioVenta(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override void Agregar(Venta pVenta)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                cn.Open();
                using (var transaccion = cn.BeginTransaction())
                {
                    //Cabecera Venta
                    string sqlAgregaVenta = @"INSERT INTO PC_VENTAS
                                                    (NBRDOCUMENT
                                                    ,DATEDOC
                                                    ,DATEPROCESALES
                                                    ,PERPOST		
                                                    ,TOTALPEN		
                                                    ,TOTALUSD		
                                                    ,SUBTOTALPEN	
                                                    ,SUBTOTALUSD	
                                                    ,TAXIGVPEN	
                                                    ,TAXIGVUSD	
                                                    ,TAXISCPEN	
                                                    ,TAXISCUSD	
                                                    ,TOTALNOTAFFECTPEN	
                                                    ,TOTALNOTAFFECTUSD	
                                                    ,PORCENTDISCOUNT1		
                                                    ,PORCENTDISCOUNT2		
                                                    ,TOTDISCOUNTPEN		
                                                    ,TOTDISCOUNTUSD		
                                                    ,TOTRETURNEDPEN		
                                                    ,TOTRETURNEDUSD		
                                                    ,TOTCASHPEN			
                                                    ,TOTCASHUSD			
                                                    ,TAXREGNBR			
                                                    ,CUSTNAME				
                                                    ,PLACA				
                                                    ,NBRBONUS				
                                                    ,CURYRATE				
                                                    ,STKCLOSEDZ			
                                                    ,STKCLOSEDX			
                                                    ,KILOMETRAJE	
                                                    ,STKINVENTORY		
                                                    ,CURYID				
                                                    ,CURYTYPEID			
                                                    ,CUSTIDSS				
                                                    ,DOCTYPEID			
                                                    ,DOCSTATUSID			
                                                    ,SALESPERID			
                                                    ,TERMID				
                                                    ,TYPEPAYMENTID		
                                                    ,SALESPOINT			
                                                    ,SITEID				
                                                    ,BUSINESSTYPE			
                                                    ,USERID				
                                                    ,TAXIGVID				
                                                    ,TAXISCID)
                                            VALUES(@NBRDOCUMENT
                                                    ,@DATEDOC
                                                    ,@DATEPROCESALES
                                                    ,@PERPOST		
                                                    ,@TOTALPEN		
                                                    ,@TOTALUSD		
                                                    ,@SUBTOTALPEN	
                                                    ,@SUBTOTALUSD	
                                                    ,@TAXIGVPEN	
                                                    ,@TAXIGVUSD	
                                                    ,@TAXISCPEN	
                                                    ,@TAXISCUSD	
                                                    ,@TOTALNOTAFFECTPEN	
                                                    ,@TOTALNOTAFFECTUSD	
                                                    ,@PORCENTDISCOUNT1		
                                                    ,@PORCENTDISCOUNT2		
                                                    ,@TOTDISCOUNTPEN		
                                                    ,@TOTDISCOUNTUSD		
                                                    ,@TOTRETURNEDPEN		
                                                    ,@TOTRETURNEDUSD		
                                                    ,@TOTCASHPEN			
                                                    ,@TOTCASHUSD			
                                                    ,@TAXREGNBR			
                                                    ,@CUSTNAME				
                                                    ,@PLACA				
                                                    ,@NBRBONUS				
                                                    ,@CURYRATE				
                                                    ,@STKCLOSEDZ			
                                                    ,@STKCLOSEDX			
                                                    ,@KILOMETRAJE		
                                                    ,@STKINVENTORY	
                                                    ,@CURYID				
                                                    ,@CURYTYPEID			
                                                    ,@CUSTIDSS				
                                                    ,@DOCTYPEID			
                                                    ,@DOCSTATUSID			
                                                    ,@SALESPERID			
                                                    ,@TERMID				
                                                    ,@TYPEPAYMENTID		
                                                    ,@SALESPOINT			
                                                    ,@SITEID				
                                                    ,@BUSINESSTYPE			
                                                    ,@USERID				
                                                    ,@TAXIGVID				
                                                    ,@TAXISCID)	";

                    var filasAfectadasAgregaVenta = cn.Execute(sqlAgregaVenta, new
                    {
                        NBRDOCUMENT = pVenta.NumeroDocumento,
                        DATEDOC = pVenta.FechaDocumento,
                        DATEPROCESALES = pVenta.FechaProceso,
                        PERPOST = pVenta.Periodo,
                        TOTALPEN = pVenta.TotalNacional,
                        TOTALUSD = pVenta.TotalExtranjera,
                        SUBTOTALPEN = pVenta.SubTotalNacional,
                        SUBTOTALUSD = pVenta.SubTotalExtranjera,
                        TAXIGVPEN = pVenta.ImpuestoIgvNacional,
                        TAXIGVUSD = pVenta.ImpuestoIgvExtranjera,
                        TAXISCPEN = pVenta.ImpuestoIscNacional,
                        TAXISCUSD = pVenta.ImpuestoIscExtranjera,
                        TOTALNOTAFFECTPEN = pVenta.TotalNoAfectoNacional,
                        TOTALNOTAFFECTUSD = pVenta.TotalNoAfectoExtranjera,
                        PORCENTDISCOUNT1 = pVenta.PorcentajeDescuentoPrimero,
                        PORCENTDISCOUNT2 = pVenta.PorcentajeDescuentoSegundo,
                        TOTDISCOUNTPEN = pVenta.TotalDescuentoNacional,
                        TOTDISCOUNTUSD = pVenta.TotalDescuentoExtranjera,
                        TOTRETURNEDPEN = pVenta.TotalVueltoNacional,
                        TOTRETURNEDUSD = pVenta.TotalVueltoExtranjera,
                        TOTCASHPEN = pVenta.TotalEfectivoNacional,
                        TOTCASHUSD = pVenta.TotalEfectivoExtranjera,
                        TAXREGNBR = pVenta.RucCliente,
                        CUSTNAME = pVenta.NombreCompletoCliente,
                        PLACA = pVenta.Placa,
                        NBRBONUS = pVenta.NumeroVale,
                        CURYRATE = pVenta.TipoCambio,
                        STKCLOSEDZ = pVenta.ProcesadoCierreZ,
                        STKCLOSEDX = pVenta.ProcesadoCierreX,
                        KILOMETRAJE = pVenta.Kilometraje,
                        STKINVENTORY = pVenta.AfectaInventario,
                        CURYID = pVenta.CodigoMoneda,
                        CURYTYPEID = pVenta.CodigoClaseTipoCambio,
                        CUSTIDSS = pVenta.CodigoCliente,
                        DOCTYPEID = pVenta.CodigoTipoDocumento,
                        DOCSTATUSID = pVenta.CodigoEstadoDocumento,
                        SALESPERID = pVenta.CodigoVendedor,
                        TERMID = pVenta.CodigoCondicionPago,
                        TYPEPAYMENTID = pVenta.CodigoTipoPago,
                        SALESPOINT = pVenta.CodigoPuntoDeVenta,
                        SITEID = pVenta.CodigoAlmacen,
                        BUSINESSTYPE = pVenta.CodigoTipoNegocio,
                        USERID = pVenta.CodigoUsuarioDeSistema,
                        TAXIGVID = pVenta.CodigoImpuestoIgv,
                        TAXISCID = pVenta.CodigoImpuestoIsc
                    }, transaction: transaccion);

                    //Detalle Venta
                    if (pVenta.VentaDetalles != null && pVenta.VentaDetalles.Any())
                    {
                        foreach (var detalleVenta in pVenta.VentaDetalles)
                        {
                            string sqlAgregaDetalleVenta = @"INSERT INTO PC_VENTAS_DET
                                                            (NBRDOCUMENT
                                                            ,SEQUENCE
                                                            ,DATEDOC
                                                            ,DATEPROCESALES
                                                            ,PERPOST
                                                            ,NBRTURN
                                                            ,NBRSIDE
                                                            ,PORCENTTAXIGV
                                                            ,PORCENTTAXISC
                                                            ,TOTALPEN
                                                            ,TOTALUSD
                                                            ,TAXPEN
                                                            ,TAXUSD
                                                            ,SLS_PRICE
                                                            ,SLSPRICESALE
                                                            ,DESCRINVENTORY
                                                            ,QTY
                                                            ,USERID
                                                            ,KIT
                                                            ,INVTIDSKU
                                                            ,INVTIDALTER
                                                            ,CURYID
                                                            ,DOCSTATUSID
                                                            ,SITEID
                                                            ,DOCTYPEID
                                                            ,STKITEM
                                                            ,STKFISI)
                                                    VALUES(@NBRDOCUMENT
                                                            ,@SEQUENCE
                                                            ,@DATEDOC
                                                            ,@DATEPROCESALES
                                                            ,@PERPOST
                                                            ,@NBRTURN
                                                            ,@NBRSIDE
                                                            ,@PORCENTTAXIGV
                                                            ,@PORCENTTAXISC
                                                            ,@TOTALPEN
                                                            ,@TOTALUSD
                                                            ,@TAXPEN
                                                            ,@TAXUSD
                                                            ,@SLS_PRICE
                                                            ,@SLSPRICESALE
                                                            ,@DESCRINVENTORY
                                                            ,@QTY
                                                            ,@USERID
                                                            ,@KIT
                                                            ,@INVTIDSKU
                                                            ,@INVTIDALTER
                                                            ,@CURYID
                                                            ,@DOCSTATUSID
                                                            ,@SITEID
                                                            ,@DOCTYPEID
                                                            ,@STKITEM
                                                            ,@STKFISI)";

                            var filasAfectadasAgregaDetalleVenta = cn.Execute(sqlAgregaDetalleVenta, new
                            {
                                NBRDOCUMENT = detalleVenta.NumeroDocumento,
                                SEQUENCE = detalleVenta.Secuencia,
                                DATEDOC = detalleVenta.FechaDocumento,
                                DATEPROCESALES = detalleVenta.FechaProceso,
                                PERPOST = detalleVenta.Periodo,
                                NBRTURN = detalleVenta.NumeroTurno,
                                NBRSIDE = detalleVenta.NumeroCara,
                                PORCENTTAXIGV = detalleVenta.PorcentajeImpuestoIgv,
                                PORCENTTAXISC = detalleVenta.PorcentajeImpuestoIsc,
                                TOTALPEN = detalleVenta.TotalNacional,
                                TOTALUSD = detalleVenta.TotalExtranjera,
                                TAXPEN = detalleVenta.ImpuestoNacional,
                                TAXUSD = detalleVenta.ImpuestoExtranjera,
                                SLS_PRICE = detalleVenta.Precio,
                                SLSPRICESALE = detalleVenta.PrecioVenta,
                                DESCRINVENTORY = detalleVenta.DescripcionArticulo,
                                QTY = detalleVenta.Cantidad,
                                USERID = detalleVenta.CodigoUsuarioDeSistema,
                                KIT = detalleVenta.EsFormula,
                                INVTIDSKU = detalleVenta.CodigoArticulo,
                                INVTIDALTER = detalleVenta.CodigoArticuloAlterno,
                                CURYID = detalleVenta.CodigoMoneda,
                                DOCSTATUSID = detalleVenta.CodigoEstadoDocumento,
                                SITEID = detalleVenta.CodigoAlmacen,
                                DOCTYPEID = detalleVenta.CodigoTipoDocumento,
                                STKITEM = detalleVenta.EsInventariable,
                                STKFISI = detalleVenta.EnInventarioFisico
                            }, transaction: transaccion);
                        }
                    }

                    //Venta con Tarjeta
                    if (pVenta.VentaConTarjetas != null && pVenta.VentaConTarjetas.Any())
                    {
                        foreach (var ventaConTarjeta in pVenta.VentaConTarjetas)
                        {
                            string sqlAgregaVentaConTarjeta = @"INSERT INTO PC_OP_SALESCARD
                                                                (NBRDOCUMENT
                                                                ,SEQUENCE
                                                                ,NBRCARD
                                                                ,TOTCARDPEN
                                                                ,TOTCARDUSD
                                                                ,DATEPROCESALES
                                                                ,CURYID
                                                                ,CARDID
                                                                ,DOCTYPEID
                                                                ,SITEID)
                                                        VALUES (@NBRDOCUMENT
                                                                ,@SEQUENCE
                                                                ,@NBRCARD
                                                                ,@TOTCARDPEN
                                                                ,@TOTCARDUSD
                                                                ,@DATEPROCESALES
                                                                ,@CURYID
                                                                ,@CARDID
                                                                ,@DOCTYPEID
                                                                ,@SITEID)";

                            var filasAfectadasAgregaVentaConTarjeta = cn.Execute(sqlAgregaVentaConTarjeta, new
                            {
                                NBRDOCUMENT = ventaConTarjeta.NumeroDocumento,
                                SEQUENCE = ventaConTarjeta.Secuencia,
                                NBRCARD = ventaConTarjeta.NumeroTarjeta,
                                TOTCARDPEN = ventaConTarjeta.TotalTarjetaNacional,
                                TOTCARDUSD = ventaConTarjeta.TotalTarjetaExtranjera,
                                DATEPROCESALES = ventaConTarjeta.FechaProceso,
                                CURYID = ventaConTarjeta.CodigoMoneda,
                                CARDID = ventaConTarjeta.CodigoTarjeta,
                                DOCTYPEID = ventaConTarjeta.CodigoTipoDocumento,
                                SITEID = ventaConTarjeta.CodigoAlmacen
                            }, transaction: transaccion);
                        }
                    }

                    //Venta con Vales
                    if (pVenta.VentaConVales != null & pVenta.VentaConVales.Any())
                    {
                        foreach (var ventaConVale in pVenta.VentaConVales)
                        {
                            string sqlAgregaVentaConVale = @"INSERT INTO PC_OP_BONUS
                                                            (NBRDOCUMENT
                                                            ,NBRBONUS
                                                            ,DATEPROCESALES
                                                            ,CUSTIDSS
                                                            ,SITEID
                                                            ,DOCTYPEID)
                                                    VALUES	(@NBRDOCUMENT
                                                            ,@NBRBONUS
                                                            ,@DATEPROCESALES
                                                            ,@CUSTIDSS
                                                            ,@SITEID
                                                            ,@DOCTYPEID)";

                            var filasAfectadasAgregaVentaConVale = cn.Execute(sqlAgregaVentaConVale, new
                            {
                                NBRDOCUMENT = ventaConVale.NumeroDocumento,
                                NBRBONUS = ventaConVale.NumeroVale,
                                DATEPROCESALES = ventaConVale.FechaProceso,
                                CUSTIDSS = ventaConVale.CodigoCliente,
                                SITEID = ventaConVale.CodigoAlmacen,
                                DOCTYPEID = ventaConVale.CodigoTipoDocumento
                            }, transaction: transaccion);
                        }
                    }

                    //Documento Anticipado
                    if(pVenta.DocumentosAnticipado != null &&  pVenta.DocumentosAnticipado.Any())
                    {
                        foreach (var documentoAnticipado in pVenta.DocumentosAnticipado)
                        {
                            string sqlAgregaDocumentoAnticipado = @"INSERT INTO PC_OP_DOCANTI
                                                                                (DOCTYPEID
                                                                                ,NBRDOCUMENT
                                                                                ,SITEID
                                                                                ,DATEPROCE)
                                                                            VALUES(@DOCTYPEID
                                                                                ,@NBRDOCUMENT
                                                                                ,@SITEID
                                                                                ,@DATEPROCE)";

                            var filasAfectadasAgregaDocumentoAnticipado = cn.Execute(sqlAgregaDocumentoAnticipado, new
                            {
                                DOCTYPEID = documentoAnticipado.CodigoTipoDocumento,                                
                                NBRDOCUMENT = documentoAnticipado.NumeroDocumento,
                                SITEID = documentoAnticipado.CodigoAlmacen,
                                DATEPROCE = documentoAnticipado.FechaProceso                        
                            }, transaction: transaccion);
                        }
                    }

                    //A Cuenta por Cobrar
                    if(pVenta.CuentasPorCobrar != null && pVenta.CuentasPorCobrar.Any())
                    {
                        foreach (var cuentaPorCobrar in pVenta.CuentasPorCobrar)
                        {
                            string sqlAgregaCuentaPorCobrar = @"INSERT INTO PC_DOCUMENTOS_CC
                                                                            (DOCTYPEID
                                                                            ,NBRDOCUMENT
                                                                            ,SITEID
                                                                            ,DOCSUMMARYTYPEID
                                                                            ,REFERENCE
                                                                            ,CURYTYPEID
                                                                            ,DOCDATE
                                                                            ,DATEPROCESALES
                                                                            ,DUEDATE
                                                                            ,TOTALPEN
                                                                            ,TOTALUSD
                                                                            ,PAIDMENTPEN
                                                                            ,PAIDMENTUSD
                                                                            ,DOCVALPEN
                                                                            ,DOCVALUSD
                                                                            ,CUSTIDSS
                                                                            ,TAXREGNBR
                                                                            ,DOCSTATUSID
                                                                            ,CURYRATE
                                                                            ,CURYID
                                                                            ,DIAPAGID
                                                                            ,DAYGRACE
                                                                            ,NBRBONUS
                                                                            ,USERID)
                                                                        VALUES(@DOCTYPEID
                                                                            ,@NBRDOCUMENT
                                                                            ,@SITEID
                                                                            ,@DOCSUMMARYTYPEID
                                                                            ,@REFERENCE
                                                                            ,@CURYTYPEID
                                                                            ,@DOCDATE
                                                                            ,@DATEPROCESALES
                                                                            ,@DUEDATE
                                                                            ,@TOTALPEN
                                                                            ,@TOTALUSD
                                                                            ,@PAIDMENTPEN
                                                                            ,@PAIDMENTUSD
                                                                            ,@DOCVALPEN
                                                                            ,@DOCVALUSD
                                                                            ,@CUSTIDSS
                                                                            ,@TAXREGNBR
                                                                            ,@DOCSTATUSID
                                                                            ,@CURYRATE
                                                                            ,@CURYID
                                                                            ,@DIAPAGID
                                                                            ,@DAYGRACE
                                                                            ,@NBRBONUS
                                                                            ,@USERID)";

                            var filasAfectadasAgregaCuentaPorCobrar = cn.Execute(sqlAgregaCuentaPorCobrar, new
                            {
                                DOCTYPEID = cuentaPorCobrar.CodigoTipoDocumento,
                                NBRDOCUMENT = cuentaPorCobrar.NumeroDocumento,
                                SITEID = cuentaPorCobrar.CodigoAlmacen,
                                DOCSUMMARYTYPEID = cuentaPorCobrar.CodigoTipoDocumentoReferencia,
                                REFERENCE = cuentaPorCobrar.Referencia,
                                CURYTYPEID = cuentaPorCobrar.CodigoClaseTipoCambio,
                                DOCDATE = cuentaPorCobrar.FechaDocumento,
                                DATEPROCESALES = cuentaPorCobrar.FechaProceso,
                                DUEDATE = cuentaPorCobrar.FechaVencimiento,
                                TOTALPEN = cuentaPorCobrar.TotalNacionalCtaCobrar,
                                TOTALUSD = cuentaPorCobrar.TotalExtranjeraCtaCobrar,
                                PAIDMENTPEN = cuentaPorCobrar.PagoDocumentoNacional,
                                PAIDMENTUSD = cuentaPorCobrar.PagoDocumentoExtranjera,
                                DOCVALPEN = cuentaPorCobrar.SaldoDocumentoNacional,
                                DOCVALUSD = cuentaPorCobrar.SaldoDocumentoExtranjera,
                                CUSTIDSS = cuentaPorCobrar.CodigoCliente,
                                TAXREGNBR = cuentaPorCobrar.Ruc,
                                DOCSTATUSID = cuentaPorCobrar.CodigoEstadoDocumento,
                                CURYRATE = cuentaPorCobrar.TipoCambio,
                                CURYID = cuentaPorCobrar.CodigoMoneda,
                                DIAPAGID = cuentaPorCobrar.CodigoDiaDePago,
                                DAYGRACE = cuentaPorCobrar.DiasDeGracia, 
                                NBRBONUS = cuentaPorCobrar.NumeroVale,
                                USERID = cuentaPorCobrar.CodigoUsuarioDeSistema
                            }, transaction: transaccion);
                        }
                    }

                    transaccion.Commit();
                }
            }
        }

        public string ObtenerNumeroDocumentoVenta(string pCodigoTipoDocumento, string pCorrelativoDocumento,
                                                    string pCodigoAlmacen)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	NBRDOCUMENT 
                                    FROM	PC_VENTAS (NOLOCK)
                                    WHERE	SITEID			= @SITEID
                                            AND DOCTYPEID	= @DOCTYPEID
                                            AND NBRDOCUMENT	= @NBRDOCUMENT";

                var numeroDocumento = cn.ExecuteScalar<string>(cadenaSQL,
                                    new
                                    {
                                        SITEID = pCodigoAlmacen,
                                        DOCTYPEID = pCodigoTipoDocumento,
                                        NBRDOCUMENT = pCorrelativoDocumento
                                    });

                if (!string.IsNullOrEmpty(numeroDocumento))
                {
                    return numeroDocumento;
                }
                else
                    return string.Empty;
            }

            // var documentoVenta = unidadTrabajoActual.Ventas
            //                     .Where(v => v.TipoDocumentoId == pCodigoTipoDocumento
            //                             && v.NumeroDocumento == pCorrelativoDocumento
            //                             && v.AlmacenId == pCodigoAlmacen
            //                         )
            //                     .Select(s=> s.NumeroDocumento)
            //                     .FirstOrDefault();


            // return Convert.ToDecimal(documentoVenta);
        }

        public IEnumerable<Venta> ObtenerVentasPorCodigoCliente(string pCodigoCliente)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT  NBRDOCUMENT			AS NumeroDocumento
                                            ,DATEDOC			AS FechaDocumento
                                            ,DATEPROCESALES		AS FechaProceso
                                            ,PERPOST			AS Periodo
                                            ,TOTALPEN			AS TotalNacional
                                            ,TOTALUSD			AS TotalExtranjera
                                            ,SUBTOTALPEN		AS SubTotalNacional
                                            ,SUBTOTALUSD		AS SubTotalExtranjera
                                            ,TAXIGVPEN			AS ImpuestoIgvNacional
                                            ,TAXIGVUSD			AS ImpuestoIGVExtranjera
                                            ,TAXISCPEN			AS ImpuestoIscNacional
                                            ,TAXISCUSD			AS ImpuestoIscExtranjera
                                            ,TOTALNOTAFFECTPEN	AS TotalNoAfectoNacional
                                            ,TOTALNOTAFFECTUSD	AS TotalNoAfectoExtranjera
                                            ,0					AS TotalAfectoNacional
                                            ,0					AS ValorVenta
                                            ,PORCENTDISCOUNT1	AS PorcentajeDescuentoPrimero
                                            ,PORCENTDISCOUNT2	AS PorcentajeDescuentoSegundo
                                            ,TOTDISCOUNTPEN		AS TotalDescuentoNacional
                                            ,TOTDISCOUNTUSD		AS TotalDescuentoExtranjera
                                            ,TOTRETURNEDPEN		AS TotalVueltoNacional
                                            ,TOTRETURNEDUSD		AS TotalVueltoExtranjera
                                            ,TOTCASHPEN			AS TotalEfectivoNacional
                                            ,TOTCASHUSD			AS TotalEfectivoExtranjera
                                            ,TAXREGNBR			AS RucCliente
                                            ,CUSTNAME			AS NombreCompletoCliente
                                            ,PLACA				AS Placa
                                            ,NBRBONUS			AS NumeroVale
                                            ,CURYRATE			AS TipoCambio
                                            ,STKCLOSEDZ			AS ProcesadoCierreZ
                                            ,STKCLOSEDX			AS ProcesadoCierreX
                                            ,KILOMETRAJE		AS Kilometraje
                                            ,CURYID				AS CodigoMoneda
                                            ,CURYTYPEID			AS CodigoClaseTipoCambio
                                            ,CUSTIDSS			AS CodigoCliente
                                            ,DOCTYPEID			AS CodigoTipoDocumento
                                            ,DOCSTATUSID		AS CodigoEstadoDocumento
                                            ,SALESPERID			AS CodigoVendedor
                                            ,TERMID				AS CodigoCondicionPago
                                            ,TYPEPAYMENTID		AS CodigoTipoPago
                                            ,SALESPOINT			AS CodigoPuntoDeVenta
                                            ,SITEID				AS CodigoAlmacen
                                            ,BUSINESSTYPE		AS CodigoTipoNegocio
                                            ,USERID				AS CodigoUsuarioDeSistema
                                            ,TAXIGVID			AS CodigoImpuestoIgv
                                            ,TAXISCID			AS CodigoImpuestoIsc
                                    FROM	PC_VENTAS (NOLOCK)
                                    WHERE	CUSTIDSS		= @CUSTIDSS";

                var ventasPorCliente = cn.Query<Venta>(cadenaSQL,
                                                new { CUSTIDSS = pCodigoCliente }).ToList();

                if (ventasPorCliente != null && ventasPorCliente.Any())
                {
                    return ventasPorCliente;
                }
                else
                    return null;
            }
            // return unidadTrabajoActual.Ventas
            //         .Where(v => v.ClienteId == pCodigoCliente
            //              );
        }

        public IEnumerable<Venta> ObtenerTodos(string pCodigoAlmacen, string pFechaProcesoInicio, string pFechaProcesoFin, 
                                                string pNumeroDocumento, string pCodigoTipoNegocio)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                var numeroBuscado = "%" + (string.IsNullOrEmpty(pNumeroDocumento) ? string.Empty : pNumeroDocumento.Trim()) + "%";                
                
                string cadenaSQL = @"SELECT  NBRDOCUMENT			AS NumeroDocumento
                                            ,DATEDOC			AS FechaDocumento
                                            ,DATEPROCESALES		AS FechaProceso
                                            ,PERPOST			AS Periodo
                                            ,TOTALPEN			AS TotalNacional
                                            ,TOTALUSD			AS TotalExtranjera
                                            ,SUBTOTALPEN		AS SubTotalNacional
                                            ,SUBTOTALUSD		AS SubTotalExtranjera
                                            ,TAXIGVPEN			AS ImpuestoIgvNacional
                                            ,TAXIGVUSD			AS ImpuestoIGVExtranjera
                                            ,TAXISCPEN			AS ImpuestoIscNacional
                                            ,TAXISCUSD			AS ImpuestoIscExtranjera
                                            ,TOTALNOTAFFECTPEN	AS TotalNoAfectoNacional
                                            ,TOTALNOTAFFECTUSD	AS TotalNoAfectoExtranjera
                                            ,0					AS TotalAfectoNacional
                                            ,0					AS ValorVenta
                                            ,PORCENTDISCOUNT1	AS PorcentajeDescuentoPrimero
                                            ,PORCENTDISCOUNT2	AS PorcentajeDescuentoSegundo
                                            ,TOTDISCOUNTPEN		AS TotalDescuentoNacional
                                            ,TOTDISCOUNTUSD		AS TotalDescuentoExtranjera
                                            ,TOTRETURNEDPEN		AS TotalVueltoNacional
                                            ,TOTRETURNEDUSD		AS TotalVueltoExtranjera
                                            ,TOTCASHPEN			AS TotalEfectivoNacional
                                            ,TOTCASHUSD			AS TotalEfectivoExtranjera
                                            ,TAXREGNBR			AS RucCliente
                                            ,CUSTNAME			AS NombreCompletoCliente
                                            ,PLACA				AS Placa
                                            ,NBRBONUS			AS NumeroVale
                                            ,CURYRATE			AS TipoCambio
                                            ,STKCLOSEDZ			AS ProcesadoCierreZ
                                            ,STKCLOSEDX			AS ProcesadoCierreX
                                            ,KILOMETRAJE		AS Kilometraje
                                            ,CURYID				AS CodigoMoneda
                                            ,CURYTYPEID			AS CodigoClaseTipoCambio
                                            ,CUSTIDSS			AS CodigoCliente
                                            ,DOCTYPEID			AS CodigoTipoDocumento
                                            ,DOCSTATUSID		AS CodigoEstadoDocumento
                                            ,SALESPERID			AS CodigoVendedor
                                            ,TERMID				AS CodigoCondicionPago
                                            ,TYPEPAYMENTID		AS CodigoTipoPago
                                            ,SALESPOINT			AS CodigoPuntoDeVenta
                                            ,SITEID				AS CodigoAlmacen
                                            ,BUSINESSTYPE		AS CodigoTipoNegocio
                                            ,USERID				AS CodigoUsuarioDeSistema
                                            ,TAXIGVID			AS CodigoImpuestoIgv
                                            ,TAXISCID			AS CodigoImpuestoIsc
                                    FROM	PC_VENTAS (NOLOCK)
                                    WHERE	SITEID				= @SITEID
                                            AND DATEPROCESALES	BETWEEN @DATEPROCESALESINI AND @DATEPROCESALESFIN
                                            AND NBRDOCUMENT		LIKE @NBRDOCUMENT
                                            AND BUSINESSTYPE	= @BUSINESSTYPE
                                    ORDER BY 1, 3, 2";

                var ventasConsultados = cn.Query<Venta>(cadenaSQL, new 
                                            { 
                                                SITEID = pCodigoAlmacen,
                                                DATEPROCESALESINI = pFechaProcesoInicio,
                                                DATEPROCESALESFIN = pFechaProcesoFin,
                                                NBRDOCUMENT = numeroBuscado,
                                                BUSINESSTYPE = pCodigoTipoNegocio
                                            }).ToList();

                if (ventasConsultados != null && ventasConsultados.Any())
                {
                    return ventasConsultados;
                }
                else
                    return null;
            }
        }        

        public IEnumerable<Venta> ObtenerPagoVentaAdelantada(string pCodigoCliente, string pCodigoAlmacen,
                                                            string pCodigoTipoDocumento, DateTime pFechaProcesoVentas)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	V.NBRDOCUMENT			AS NumeroDocumento
                                            ,V.DATEDOC				AS FechaDocumento
                                            ,V.DATEPROCESALES		AS FechaProceso
                                            ,V.PERPOST				AS Periodo
                                            ,V.TOTALPEN				AS TotalNacional
                                            ,V.TOTALUSD				AS TotalExtranjera
                                            ,V.SUBTOTALPEN			AS SubTotalNacional
                                            ,V.SUBTOTALUSD			AS SubTotalExtranjera
                                            ,V.TAXIGVPEN			AS ImpuestoIgvNacional
                                            ,V.TAXIGVUSD			AS ImpuestoIGVExtranjera
                                            ,V.TAXISCPEN			AS ImpuestoIscNacional
                                            ,V.TAXISCUSD			AS ImpuestoIscExtranjera
                                            ,V.TOTALNOTAFFECTPEN	AS TotalNoAfectoNacional
                                            ,V.TOTALNOTAFFECTUSD	AS TotalNoAfectoExtranjera
                                            ,0						AS TotalAfectoNacional
                                            ,0						AS ValorVenta
                                            ,V.PORCENTDISCOUNT1		AS PorcentajeDescuentoPrimero
                                            ,V.PORCENTDISCOUNT2		AS PorcentajeDescuentoSegundo
                                            ,V.TOTDISCOUNTPEN		AS TotalDescuentoNacional
                                            ,V.TOTDISCOUNTUSD		AS TotalDescuentoExtranjera
                                            ,V.TOTRETURNEDPEN		AS TotalVueltoNacional
                                            ,V.TOTRETURNEDUSD		AS TotalVueltoExtranjera
                                            ,V.TOTCASHPEN			AS TotalEfectivoNacional
                                            ,V.TOTCASHUSD			AS TotalEfectivoExtranjera
                                            ,V.TAXREGNBR			AS RucCliente
                                            ,V.CUSTNAME				AS NombreCompletoCliente
                                            ,V.PLACA				AS Placa
                                            ,V.NBRBONUS				AS NumeroVale			  
                                            ,V.CURYRATE				AS TipoCambio
                                            ,V.STKCLOSEDZ			AS ProcesadoCierreZ
                                            ,V.STKCLOSEDX			AS ProcesadoCierreX
                                            ,V.KILOMETRAJE			AS Kilometraje
                                            ,V.CURYID				AS CodigoMoneda
                                            ,V.CURYTYPEID			AS CodigoClaseTipoCambio
                                            ,V.CUSTIDSS				AS CodigoCliente
                                            ,V.DOCTYPEID			AS CodigoTipoDocumento
                                            ,V.DOCSTATUSID			AS CodigoEstadoDocumento
                                            ,V.SALESPERID			AS CodigoVendedor
                                            ,V.TERMID				AS CodigoCondicionPago
                                            ,V.TYPEPAYMENTID		AS CodigoTipoPago
                                            ,V.SALESPOINT			AS CodigoPuntoDeVenta
                                            ,V.SITEID				AS CodigoAlmacen
                                            ,V.BUSINESSTYPE			AS CodigoTipoNegocio
                                            ,V.USERID				AS CodigoUsuarioDeSistema
                                            ,V.TAXIGVID				AS CodigoImpuestoIgv
                                            ,V.TAXISCID				AS CodigoImpuestoIsc 

                                            ,TD.DOCTYPEID			AS TipoDocumentoCodigoTipoDocumento
                                            ,TD.DESCR				AS TipoDocumentoDescripcionTipoDocumento
                                            ,TD.MINDES				AS TipoDocumentoAbreviatura

                                            ,ED.DOCSTATUSID			AS EstadoDocumentoCodigoEstadoDocumento
                                            ,ED.DESCR				AS EstadoDocumentoDescripcionEstadoDocumento
                                            ,ED.DOCSTATUSID			AS EstadoDocumentoAbreviaturaEstadoDocumento                                            

                                            ,DA.NBRDOCUMENT			AS DocumentoAnticipadoNumeroDocumento
                                            ,DA.DATEPROCE			AS DocumentoAnticipadoFechaProceso
                                            ,DA.DOCTYPEID			AS DocumentoAnticipadoCodigoTipoDocumento
                                            ,DA.SITEID				AS DocumentoAnticipadoCodigoAlmacen
                                    FROM	PC_VENTAS				 (NOLOCK) V
                                            INNER JOIN PC_OP_DOCANTI (NOLOCK) DA ON V.SITEID			= DA.SITEID
                                                                                    AND V.DOCTYPEID		= DA.DOCTYPEID
                                                                                    AND V.NBRDOCUMENT	= DA.NBRDOCUMENT
                                            INNER JOIN PC_DOCTYPE	 (NOLOCK) TD ON V.DOCTYPEID			= TD.DOCTYPEID
                                            RIGHT JOIN PC_IN_DOCSTATUS (NOLOCK) ED ON V.DOCSTATUSID		= ED.DOCSTATUSID
                                    WHERE	V.SITEID				= @SITEID
                                            AND V.DOCTYPEID			= @DOCTYPEID
                                            AND V.CUSTIDSS			= @CUSTIDSS
                                            AND V.DOCSTATUSID		!= 'AN'
                                            AND V.DATEPROCESALES	<= @DATEPROCESALES";

                var resultado = cn.Query<dynamic>(cadenaSQL,
                                    new
                                    {
                                        SITEID = pCodigoAlmacen,
                                        DOCTYPEID = pCodigoTipoDocumento,
                                        CUSTIDSS = pCodigoCliente,
                                        DATEPROCESALES = pFechaProcesoVentas
                                    });

                var ventas = resultado.AsList();

                if (ventas != null)
                {
                    return MapeoPagoVentaAdelantada(ventas);
                }
                else
                    return null;
            }

            // var conjuntoVenta = unidadTrabajoActual.CrearConjunto<Venta>();

            // var consVtaPorPagoAdelantado = (from cvta in conjuntoVenta.Include(e => e.EstadoDocumento)
            //     .Include(d => d.DocumentosAnticipado)
            //     from da in cvta.DocumentosAnticipado
            //     where da.TipoDocumentoId == cvta.TipoDocumentoId
            //           && da.NumeroDocumento == cvta.NumeroDocumento
            //           && da.AlmacenId == cvta.AlmacenId

            //           && cvta.EstadoDocumento.CodigoEstadoDocumento != VentaEstadoDocumento.Anulado
            //           && cvta.ClienteId == pCodigoCliente
            //           && cvta.AlmacenId == pCodigoAlmacen
            //           && cvta.TipoDocumentoId == pCodigoTipoDocumento
            //           && cvta.FechaProceso <= pFechaProcesoVentas
            //     select cvta).ToList();

            // return consVtaPorPagoAdelantado;
        }

        public IEnumerable<Venta> ObtenerConsumoVentaAdelantada(string pCodigoTipoPago, string pCodigoCliente, string pCodigoAlmacen,
                                        string pCodigoTipoDocumento, DateTime pFechaProcesoVentas)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	V.NBRDOCUMENT			AS NumeroDocumento
                                            ,V.DATEDOC				AS FechaDocumento
                                            ,V.DATEPROCESALES		AS FechaProceso
                                            ,V.PERPOST				AS Periodo
                                            ,V.TOTALPEN				AS TotalNacional
                                            ,V.TOTALUSD				AS TotalExtranjera
                                            ,V.SUBTOTALPEN			AS SubTotalNacional
                                            ,V.SUBTOTALUSD			AS SubTotalExtranjera
                                            ,V.TAXIGVPEN			AS ImpuestoIgvNacional
                                            ,V.TAXIGVUSD			AS ImpuestoIGVExtranjera
                                            ,V.TAXISCPEN			AS ImpuestoIscNacional
                                            ,V.TAXISCUSD			AS ImpuestoIscExtranjera
                                            ,V.TOTALNOTAFFECTPEN	AS TotalNoAfectoNacional
                                            ,V.TOTALNOTAFFECTUSD	AS TotalNoAfectoExtranjera
                                            ,0						AS TotalAfectoNacional
                                            ,0						AS ValorVenta
                                            ,V.PORCENTDISCOUNT1		AS PorcentajeDescuentoPrimero
                                            ,V.PORCENTDISCOUNT2		AS PorcentajeDescuentoSegundo
                                            ,V.TOTDISCOUNTPEN		AS TotalDescuentoNacional
                                            ,V.TOTDISCOUNTUSD		AS TotalDescuentoExtranjera
                                            ,V.TOTRETURNEDPEN		AS TotalVueltoNacional
                                            ,V.TOTRETURNEDUSD		AS TotalVueltoExtranjera
                                            ,V.TOTCASHPEN			AS TotalEfectivoNacional
                                            ,V.TOTCASHUSD			AS TotalEfectivoExtranjera
                                            ,V.TAXREGNBR			AS RucCliente
                                            ,V.CUSTNAME				AS NombreCompletoCliente
                                            ,V.PLACA				AS Placa
                                            ,V.NBRBONUS				AS NumeroVale			  
                                            ,V.CURYRATE				AS TipoCambio
                                            ,V.STKCLOSEDZ			AS ProcesadoCierreZ
                                            ,V.STKCLOSEDX			AS ProcesadoCierreX
                                            ,V.KILOMETRAJE			AS Kilometraje
                                            ,V.CURYID				AS CodigoMoneda
                                            ,V.CURYTYPEID			AS CodigoClaseTipoCambio
                                            ,V.CUSTIDSS				AS CodigoCliente
                                            ,V.DOCTYPEID			AS CodigoTipoDocumento
                                            ,V.DOCSTATUSID			AS CodigoEstadoDocumento
                                            ,V.SALESPERID			AS CodigoVendedor
                                            ,V.TERMID				AS CodigoCondicionPago
                                            ,V.TYPEPAYMENTID		AS CodigoTipoPago
                                            ,V.SALESPOINT			AS CodigoPuntoDeVenta
                                            ,V.SITEID				AS CodigoAlmacen
                                            ,V.BUSINESSTYPE			AS CodigoTipoNegocio
                                            ,V.USERID				AS CodigoUsuarioDeSistema
                                            ,V.TAXIGVID				AS CodigoImpuestoIgv
                                            ,V.TAXISCID				AS CodigoImpuestoIsc 

                                            ,TD.DOCTYPEID			AS TipoDocumentoCodigoTipoDocumento
                                            ,TD.DESCR				AS TipoDocumentoDescripcionTipoDocumento
                                            ,TD.MINDES				AS TipoDocumentoAbreviatura

                                            ,ED.DOCSTATUSID			AS EstadoDocumentoCodigoEstadoDocumento
                                            ,ED.DESCR				AS EstadoDocumentoDescripcionEstadoDocumento
                                            ,ED.DOCSTATUSID			AS EstadoDocumentoAbreviaturaEstadoDocumento
                                    FROM	PC_VENTAS				 (NOLOCK) V
                                            INNER JOIN PC_DOCTYPE	 (NOLOCK) TD ON V.DOCTYPEID			= TD.DOCTYPEID
                                            RIGHT JOIN PC_IN_DOCSTATUS (NOLOCK) ED ON V.DOCSTATUSID		= ED.DOCSTATUSID
                                    WHERE	V.SITEID				= @SITEID
                                            AND V.DOCTYPEID			= @DOCTYPEID
                                            AND V.CUSTIDSS			= @CUSTIDSS
                                            AND V.TYPEPAYMENTID		= @TYPEPAYMENTID
                                            AND V.DOCSTATUSID		!= 'AN'
                                            AND V.DATEPROCESALES	<= @DATEPROCESALES";

                var resultado = cn.Query<dynamic>(cadenaSQL,
                                    new
                                    {
                                        SITEID = pCodigoAlmacen,
                                        DOCTYPEID = pCodigoTipoDocumento,
                                        CUSTIDSS = pCodigoCliente,
                                        TYPEPAYMENTID = pCodigoTipoPago,
                                        DATEPROCESALES = pFechaProcesoVentas
                                    });

                var ventas = resultado.AsList();

                if (ventas != null)
                {
                    return MapeoConsumoVentaAdelantada(ventas);
                }
                else
                    return null;
            }

            // var conjuntoVenta = unidadTrabajoActual.CrearConjunto<Venta>();

            // var consConsumoVtaAdelanto = (from cvta in conjuntoVenta.Include(e => e.EstadoDocumento)
            //     where cvta.EstadoDocumento.CodigoEstadoDocumento != VentaEstadoDocumento.Anulado
            //           && cvta.TipoPagoId == pCodigoTipoPago
            //           && cvta.ClienteId == pCodigoCliente
            //           && cvta.AlmacenId == pCodigoAlmacen
            //           && cvta.TipoDocumentoId == pCodigoTipoDocumento
            //           && cvta.FechaProceso <= pFechaProcesoVentas
            //     select cvta).ToList();

            // return consConsumoVtaAdelanto;
        }

        // private List<Venta> MapeoVenta(List<Venta> pVentas, EstadoDocumento pEstadoDocumento,
        //                                                 DocumentoAnticipado pDocumentoAnticipado)
        // {
        //     return new List<Venta>();
        // }

        private List<Venta> MapeoPagoVentaAdelantada(dynamic pPagosVentaAdelantada)
        {
            var pagosVentaAdelantada = new List<Venta>();

            foreach (dynamic pagoVentaAdelantada in pPagosVentaAdelantada)
            {
                var ventaPagoAdelantado = new Venta()
                {
                    NumeroDocumento = pagoVentaAdelantada.NumeroDocumento,
                    FechaDocumento = pagoVentaAdelantada.FechaDocumento,
                    FechaProceso = pagoVentaAdelantada.FechaProceso,
                    Periodo = pagoVentaAdelantada.Periodo,
                    TotalNacional = pagoVentaAdelantada.TotalNacional,
                    TotalExtranjera = pagoVentaAdelantada.TotalExtranjera,
                    SubTotalNacional = pagoVentaAdelantada.SubTotalNacional,
                    SubTotalExtranjera = pagoVentaAdelantada.SubTotalExtranjera,
                    ImpuestoIgvNacional = pagoVentaAdelantada.ImpuestoIgvNacional,
                    ImpuestoIgvExtranjera = pagoVentaAdelantada.ImpuestoIGVExtranjera,
                    ImpuestoIscNacional = pagoVentaAdelantada.ImpuestoIscNacional,
                    ImpuestoIscExtranjera = pagoVentaAdelantada.ImpuestoIscExtranjera,
                    TotalNoAfectoNacional = pagoVentaAdelantada.TotalNoAfectoNacional,
                    TotalNoAfectoExtranjera = pagoVentaAdelantada.TotalNoAfectoExtranjera,
                    TotalAfectoNacional = pagoVentaAdelantada.TotalAfectoNacional,
                    ValorVenta = pagoVentaAdelantada.ValorVenta,
                    PorcentajeDescuentoPrimero = pagoVentaAdelantada.PorcentajeDescuentoPrimero,
                    PorcentajeDescuentoSegundo = pagoVentaAdelantada.PorcentajeDescuentoSegundo,
                    TotalDescuentoNacional = pagoVentaAdelantada.TotalDescuentoNacional,
                    TotalDescuentoExtranjera = pagoVentaAdelantada.TotalDescuentoExtranjera,
                    TotalVueltoNacional = pagoVentaAdelantada.TotalVueltoNacional,
                    TotalVueltoExtranjera = pagoVentaAdelantada.TotalVueltoExtranjera,
                    TotalEfectivoNacional = pagoVentaAdelantada.TotalEfectivoNacional,
                    TotalEfectivoExtranjera = pagoVentaAdelantada.TotalEfectivoExtranjera,
                    RucCliente = pagoVentaAdelantada.RucCliente,
                    NombreCompletoCliente = pagoVentaAdelantada.NombreCompletoCliente,
                    Placa = pagoVentaAdelantada.Placa,
                    NumeroVale = pagoVentaAdelantada.NumeroVale,
                    TipoCambio = pagoVentaAdelantada.TipoCambio,
                    ProcesadoCierreZ = pagoVentaAdelantada.ProcesadoCierreZ,
                    ProcesadoCierreX = pagoVentaAdelantada.ProcesadoCierreX,
                    Kilometraje = pagoVentaAdelantada.Kilometraje
                };

                ventaPagoAdelantado.EstablecerReferenciaMonedaDeVenta(pagoVentaAdelantada.CodigoMoneda);
                ventaPagoAdelantado.EstablecerReferenciaClaseTipoCambioDeVenta(pagoVentaAdelantada.CodigoClaseTipoCambio);
                ventaPagoAdelantado.EstablecerReferenciaClienteDeVenta(pagoVentaAdelantada.CodigoCliente);
                ventaPagoAdelantado.EstablecerReferenciaTipoDocumentoDeVenta(pagoVentaAdelantada.CodigoTipoDocumento);
                // ventaPagoAdelantado.EstablecerReferenciaEstadoDocumentoDeVenta(pagoVentaAdelantada.CodigoEstadoDocumento);
                ventaPagoAdelantado.EstablecerReferenciaVendedorDeVenta(pagoVentaAdelantada.CodigoVendedor);
                ventaPagoAdelantado.EstablecerReferenciaCondicionPagoDeVenta(pagoVentaAdelantada.CodigoCondicionPago);
                ventaPagoAdelantado.EstablecerReferenciaTipoPagoDeVenta(pagoVentaAdelantada.CodigoTipoPago);
                ventaPagoAdelantado.EstablecerReferenciaConfiguracionPuntoVentaDeVenta(pagoVentaAdelantada.CodigoPuntoDeVenta);
                ventaPagoAdelantado.EstablecerReferenciaAlmacenDeVenta(pagoVentaAdelantada.CodigoAlmacen);
                ventaPagoAdelantado.EstablecerReferenciaTipoNegocioDeVenta(pagoVentaAdelantada.CodigoTipoNegocio);
                ventaPagoAdelantado.EstablecerReferenciaUsuarioSistemaDeVenta(pagoVentaAdelantada.CodigoUsuarioDeSistema);
                ventaPagoAdelantado.EstablecerReferenciaImpuestoIgvDeCliente(pagoVentaAdelantada.CodigoImpuestoIgv);
                ventaPagoAdelantado.EstablecerReferenciaImpuestoIscDeCliente(pagoVentaAdelantada.CodigoImpuestoIsc);

                ventaPagoAdelantado.EstablecerTipoDocumentoDeVenta(new TipoDocumento()
                {
                    CodigoTipoDocumento = pagoVentaAdelantada.TipoDocumentoCodigoTipoDocumento,
                    DescripcionTipoDocumento = pagoVentaAdelantada.TipoDocumentoDescripcionTipoDocumento,
                    Abreviatura = pagoVentaAdelantada.TipoDocumentoAbreviatura
                });

                ventaPagoAdelantado.EstablecerEstadoDocumentoDeVenta(new EstadoDocumento
                {
                    CodigoEstadoDocumento = pagoVentaAdelantada.EstadoDocumentoCodigoEstadoDocumento,
                    DescripcionEstadoDocumento = pagoVentaAdelantada.EstadoDocumentoDescripcionEstadoDocumento,
                    AbreviaturaEstadoDocumento = pagoVentaAdelantada.EstadoDocumentoAbreviaturaEstadoDocumento
                });

                ventaPagoAdelantado.AgregarNuevoDocumentoAnticipado();

                pagosVentaAdelantada.Add(ventaPagoAdelantado);
            }

            return pagosVentaAdelantada;
        }

        private List<Venta> MapeoConsumoVentaAdelantada(dynamic pConsumosVentaAdelantada)
        {
            var consumosVentaAdelantada = new List<Venta>();

            foreach (dynamic consumoVentaAdelantada in pConsumosVentaAdelantada)
            {
                var ventaPagoAdelantado = new Venta()
                {
                    NumeroDocumento = consumoVentaAdelantada.NumeroDocumento,
                    FechaDocumento = consumoVentaAdelantada.FechaDocumento,
                    FechaProceso = consumoVentaAdelantada.FechaProceso,
                    Periodo = consumoVentaAdelantada.Periodo,
                    TotalNacional = consumoVentaAdelantada.TotalNacional,
                    TotalExtranjera = consumoVentaAdelantada.TotalExtranjera,
                    SubTotalNacional = consumoVentaAdelantada.SubTotalNacional,
                    SubTotalExtranjera = consumoVentaAdelantada.SubTotalExtranjera,
                    ImpuestoIgvNacional = consumoVentaAdelantada.ImpuestoIgvNacional,
                    ImpuestoIgvExtranjera = consumoVentaAdelantada.ImpuestoIGVExtranjera,
                    ImpuestoIscNacional = consumoVentaAdelantada.ImpuestoIscNacional,
                    ImpuestoIscExtranjera = consumoVentaAdelantada.ImpuestoIscExtranjera,
                    TotalNoAfectoNacional = consumoVentaAdelantada.TotalNoAfectoNacional,
                    TotalNoAfectoExtranjera = consumoVentaAdelantada.TotalNoAfectoExtranjera,
                    TotalAfectoNacional = consumoVentaAdelantada.TotalAfectoNacional,
                    ValorVenta = consumoVentaAdelantada.ValorVenta,
                    PorcentajeDescuentoPrimero = consumoVentaAdelantada.PorcentajeDescuentoPrimero,
                    PorcentajeDescuentoSegundo = consumoVentaAdelantada.PorcentajeDescuentoSegundo,
                    TotalDescuentoNacional = consumoVentaAdelantada.TotalDescuentoNacional,
                    TotalDescuentoExtranjera = consumoVentaAdelantada.TotalDescuentoExtranjera,
                    TotalVueltoNacional = consumoVentaAdelantada.TotalVueltoNacional,
                    TotalVueltoExtranjera = consumoVentaAdelantada.TotalVueltoExtranjera,
                    TotalEfectivoNacional = consumoVentaAdelantada.TotalEfectivoNacional,
                    TotalEfectivoExtranjera = consumoVentaAdelantada.TotalEfectivoExtranjera,
                    RucCliente = consumoVentaAdelantada.RucCliente,
                    NombreCompletoCliente = consumoVentaAdelantada.NombreCompletoCliente,
                    Placa = consumoVentaAdelantada.Placa,
                    NumeroVale = consumoVentaAdelantada.NumeroVale,
                    TipoCambio = consumoVentaAdelantada.TipoCambio,
                    ProcesadoCierreZ = consumoVentaAdelantada.ProcesadoCierreZ,
                    ProcesadoCierreX = consumoVentaAdelantada.ProcesadoCierreX,
                    Kilometraje = consumoVentaAdelantada.Kilometraje
                };

                ventaPagoAdelantado.EstablecerReferenciaMonedaDeVenta(consumoVentaAdelantada.CodigoMoneda);
                ventaPagoAdelantado.EstablecerReferenciaClaseTipoCambioDeVenta(consumoVentaAdelantada.CodigoClaseTipoCambio);
                ventaPagoAdelantado.EstablecerReferenciaClienteDeVenta(consumoVentaAdelantada.CodigoCliente);
                ventaPagoAdelantado.EstablecerReferenciaTipoDocumentoDeVenta(consumoVentaAdelantada.CodigoTipoDocumento);
                // ventaPagoAdelantado.EstablecerReferenciaEstadoDocumentoDeVenta(pagoVentaAdelantada.CodigoEstadoDocumento);
                ventaPagoAdelantado.EstablecerReferenciaVendedorDeVenta(consumoVentaAdelantada.CodigoVendedor);
                ventaPagoAdelantado.EstablecerReferenciaCondicionPagoDeVenta(consumoVentaAdelantada.CodigoCondicionPago);
                ventaPagoAdelantado.EstablecerReferenciaTipoPagoDeVenta(consumoVentaAdelantada.CodigoTipoPago);
                ventaPagoAdelantado.EstablecerReferenciaConfiguracionPuntoVentaDeVenta(consumoVentaAdelantada.CodigoPuntoDeVenta);
                ventaPagoAdelantado.EstablecerReferenciaAlmacenDeVenta(consumoVentaAdelantada.CodigoAlmacen);
                ventaPagoAdelantado.EstablecerReferenciaTipoNegocioDeVenta(consumoVentaAdelantada.CodigoTipoNegocio);
                ventaPagoAdelantado.EstablecerReferenciaUsuarioSistemaDeVenta(consumoVentaAdelantada.CodigoUsuarioDeSistema);
                ventaPagoAdelantado.EstablecerReferenciaImpuestoIgvDeCliente(consumoVentaAdelantada.CodigoImpuestoIgv);
                ventaPagoAdelantado.EstablecerReferenciaImpuestoIscDeCliente(consumoVentaAdelantada.CodigoImpuestoIsc);

                ventaPagoAdelantado.EstablecerTipoDocumentoDeVenta(new TipoDocumento()
                {
                    CodigoTipoDocumento = consumoVentaAdelantada.TipoDocumentoCodigoTipoDocumento,
                    DescripcionTipoDocumento = consumoVentaAdelantada.TipoDocumentoDescripcionTipoDocumento,
                    Abreviatura = consumoVentaAdelantada.TipoDocumentoAbreviatura
                });

                ventaPagoAdelantado.EstablecerEstadoDocumentoDeVenta(new EstadoDocumento
                {
                    CodigoEstadoDocumento = consumoVentaAdelantada.EstadoDocumentoCodigoEstadoDocumento,
                    DescripcionEstadoDocumento = consumoVentaAdelantada.EstadoDocumentoDescripcionEstadoDocumento,
                    AbreviaturaEstadoDocumento = consumoVentaAdelantada.EstadoDocumentoAbreviaturaEstadoDocumento
                });

                consumosVentaAdelantada.Add(ventaPagoAdelantado);
            }

            return consumosVentaAdelantada;
        }
    }
}