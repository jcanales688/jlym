using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

namespace PtoVta.Infraestructura.Repositorios.Ventas
{
    public class RepositorioPedidoEESS : Repositorio<PedidoEESS>, IRepositorioPedidoEESS
    {
        public RepositorioPedidoEESS(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override void Agregar(PedidoEESS pPedidoEESS)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                cn.Open();
                using (var transaccion = cn.BeginTransaction())
                {
                    //Cabecera Pedido
                    string sqlAgregaPedidoEESS = @"INSERT INTO " + BaseDatos.PrefijoTabla + @"TMP_OP_SALES
                                                        (CORRNBR
                                                        ,NBRSIDE
                                                        ,NBRDOCUMENT
                                                        ,STKINVENTORY
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
                                                        ,POINTNUMBER    
                                                        ,TERMINALNAME    
                                                        ,KILOMETRAJE    
                                                        ,ADDRESS    
                                                        ,TYPECUSTOMER    
                                                        ,DSCRTYPECUST    
                                                        ,ESTADO    
                                                        ,MONTOTIPOCAMBIO    
                                                        ,DIASDEGRACIA    
                                                        ,LIMITECRED    
                                                        ,DEUDACLIENTE    
                                                        ,MTOSURPLUS    
                                                        ,AFECTO    
                                                        ,NBRCARD    
                                                        ,PAGO    
                                                        ,DESCRCARD        
                                                        ,DOCTYPEID        
                                                        ,TYPEPAYMENTID        
                                                        ,SITEID        
                                                        ,CURYID        
                                                        ,DOCSTATUSID        
                                                        ,TERMID        
                                                        ,SALESPERID        
                                                        ,USERID        
                                                        ,TAXIGVID        
                                                        ,TAXISCID        
                                                        ,CUSTIDSS        
                                                        ,CURYTYPEID        
                                                        ,SALESPOINT        
                                                        ,IDESTADO        
                                                        ,MONEDACREDITO        
                                                        ,TIPODECAMBIO            
                                                        ,PROMOTIONCARDID        
                                                        ,CARDID       
                                                        ,CURYIDCARD)
                                            VALUES		(@CORRNBR
                                                        ,@NBRSIDE
                                                        ,@NBRDOCUMENT
                                                        ,@STKINVENTORY
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
                                                        ,@POINTNUMBER    
                                                        ,@TERMINALNAME    
                                                        ,@KILOMETRAJE    
                                                        ,@ADDRESS    
                                                        ,@TYPECUSTOMER    
                                                        ,@DSCRTYPECUST    
                                                        ,@ESTADO    
                                                        ,@MONTOTIPOCAMBIO    
                                                        ,@DIASDEGRACIA    
                                                        ,@LIMITECRED    
                                                        ,@DEUDACLIENTE    
                                                        ,@MTOSURPLUS    
                                                        ,@AFECTO    
                                                        ,@NBRCARD    
                                                        ,@PAGO    
                                                        ,@DESCRCARD        
                                                        ,@DOCTYPEID        
                                                        ,@TYPEPAYMENTID        
                                                        ,@SITEID        
                                                        ,@CURYID        
                                                        ,@DOCSTATUSID        
                                                        ,@TERMID        
                                                        ,@SALESPERID        
                                                        ,@USERID        
                                                        ,@TAXIGVID        
                                                        ,@TAXISCID        
                                                        ,@CUSTIDSS        
                                                        ,@CURYTYPEID        
                                                        ,@SALESPOINT        
                                                        ,@IDESTADO        
                                                        ,@MONEDACREDITO        
                                                        ,@TIPODECAMBIO            
                                                        ,@PROMOTIONCARDID        
                                                        ,@CARDID       
                                                        ,@CURYIDCARD)";

                    var filasAfectadasAgregaPedidoEESS = cn.Execute(sqlAgregaPedidoEESS, new
                    {
                        CORRNBR = pPedidoEESS.Correlativo,
                        NBRSIDE = pPedidoEESS.NumeroCara,
                        NBRDOCUMENT = pPedidoEESS.NumeroDocumento,
                        STKINVENTORY = pPedidoEESS.AfectaInventario,
                        DATEDOC = pPedidoEESS.FechaDocumento,
                        DATEPROCESALES = pPedidoEESS.FechaProceso,
                        PERPOST = pPedidoEESS.Periodo,
                        TOTALPEN = pPedidoEESS.TotalNacional,
                        TOTALUSD = pPedidoEESS.TotalExtranjera,
                        SUBTOTALPEN = pPedidoEESS.SubTotalNacional,
                        SUBTOTALUSD = pPedidoEESS.SubTotalExtranjera,
                        TAXIGVPEN = pPedidoEESS.ImpuestoIgvNacional,
                        TAXIGVUSD = pPedidoEESS.ImpuestoIgvExtranjera,
                        TAXISCPEN = pPedidoEESS.ImpuestoIscNacional,
                        TAXISCUSD = pPedidoEESS.ImpuestoIscExtranjera,
                        TOTALNOTAFFECTPEN = pPedidoEESS.TotalNoAfectoNacional,
                        TOTALNOTAFFECTUSD = pPedidoEESS.TotalNoAfectoExtranjera,
                        PORCENTDISCOUNT1 = pPedidoEESS.PorcentajeDescuentoPrimero,
                        PORCENTDISCOUNT2 = pPedidoEESS.PorcentajeDescuentoSegundo,
                        TOTDISCOUNTPEN = pPedidoEESS.TotalDescuentoNacional,
                        TOTDISCOUNTUSD = pPedidoEESS.TotalDescuentoExtranjera,
                        TOTRETURNEDPEN = pPedidoEESS.TotalVueltoNacional,
                        TOTRETURNEDUSD = pPedidoEESS.TotalVueltoExtranjera,
                        TOTCASHPEN = pPedidoEESS.TotalEfectivoNacional,
                        TOTCASHUSD = pPedidoEESS.TotalEfectivoExtranjera,
                        TAXREGNBR = pPedidoEESS.RucCliente,
                        CUSTNAME = pPedidoEESS.NombreCompletoCliente,
                        PLACA = pPedidoEESS.Placa,
                        NBRBONUS = pPedidoEESS.NumeroVale,
                        CURYRATE = pPedidoEESS.TipoCambio,
                        STKCLOSEDZ = pPedidoEESS.ProcesadoCierreZ,
                        STKCLOSEDX = pPedidoEESS.ProcesadoCierreX,
                        POINTNUMBER = pPedidoEESS.NumeroPuntos,
                        TERMINALNAME = pPedidoEESS.NombreTerminal,
                        KILOMETRAJE = pPedidoEESS.Kilometraje,
                        ADDRESS = pPedidoEESS.DireccionCliente,
                        TYPECUSTOMER = pPedidoEESS.TipoCliente,
                        DSCRTYPECUST = pPedidoEESS.DescripcionTipoCliente,
                        ESTADO = pPedidoEESS.DescripcionEstado,
                        MONTOTIPOCAMBIO = pPedidoEESS.TipoCambioClienteCredito,
                        DIASDEGRACIA = pPedidoEESS.DiasDeGraciaClienteCredito,
                        LIMITECRED = pPedidoEESS.LimiteCreditoClienteCredito,
                        DEUDACLIENTE = pPedidoEESS.DeudaClienteClienteCredito,
                        MTOSURPLUS = pPedidoEESS.PlusCreditoClienteCredito,
                        AFECTO = pPedidoEESS.Afecto,
                        NBRCARD = pPedidoEESS.NumeroTarjeta,
                        PAGO = pPedidoEESS.PagoTarjeta,
                        DESCRCARD = pPedidoEESS.DescripcionTarjeta,
                        DOCTYPEID = pPedidoEESS.CodigoTipoDocumento,
                        TYPEPAYMENTID = pPedidoEESS.CodigoTipoPago,
                        SITEID = pPedidoEESS.CodigoAlmacen,
                        CURYID = pPedidoEESS.CodigoMoneda,
                        DOCSTATUSID = pPedidoEESS.CodigoEstadoDocumento,
                        TERMID = pPedidoEESS.CodigoCondicionPago,
                        SALESPERID = pPedidoEESS.CodigoVendedor,
                        USERID = pPedidoEESS.CodigoUsuarioDeSistema,
                        TAXIGVID = pPedidoEESS.CodigoImpuestoIgv,
                        TAXISCID = pPedidoEESS.CodigoImpuestoIsc,
                        CUSTIDSS = pPedidoEESS.CodigoCliente,
                        CURYTYPEID = pPedidoEESS.CodigoClaseTipoCambio,
                        SALESPOINT = pPedidoEESS.CodigoPuntoDeVenta,
                        IDESTADO = pPedidoEESS.CodigoEstado,
                        MONEDACREDITO = pPedidoEESS.CodigoMonedaCredito,
                        TIPODECAMBIO = pPedidoEESS.CodigoClaseTipoCambioClienteCredito,
                        PROMOTIONCARDID = pPedidoEESS.CodigoTarjetaPromocion,
                        CARDID = pPedidoEESS.CodigoTarjeta,
                        CURYIDCARD = pPedidoEESS.CodigoMonedaTarjeta
                    }, transaction: transaccion);


                    //Detalle Pedido
                    if (pPedidoEESS.PedidoEESSDetalles != null && pPedidoEESS.PedidoEESSDetalles.Any())
                    {
                        foreach (var detallePedido in pPedidoEESS.PedidoEESSDetalles)
                        {
                            string sqlAgregaDetallePedido = @"INSERT INTO " + BaseDatos.PrefijoTabla + @"TMP_OP_SALESDET
                                                                            (CORRNBR
                                                                            ,SEQUENCE
                                                                            ,NBRDOCUMENT
                                                                            ,DATEDOC
                                                                            ,DATEPROCESALES
                                                                            ,PERPOST
                                                                            ,STKCLOSEDZ
                                                                            ,STKCLOSEDX
                                                                            ,NBRTURN
                                                                            ,NBRSIDE
                                                                            ,NBRTRANSACFUEL
                                                                            ,DISCDSCTO1
                                                                            ,DISCDSCTO2
                                                                            ,DISCMTOPEN
                                                                            ,DISCMTOUSD
                                                                            ,PORCENTTAXIGV
                                                                            ,PORCENTTAXISC
                                                                            ,TOTALPEN
                                                                            ,TOTALUSD
                                                                            ,TAXPEN
                                                                            ,TAXUSD
                                                                            ,STKITEM
                                                                            ,STKFISI
                                                                            ,SLS_PRICE
                                                                            ,SLSPRICESALE
                                                                            ,STDCOSTPEN
                                                                            ,STDCOSTUSD
                                                                            ,DESCRINVENTORY
                                                                            ,QTY
                                                                            ,KIT
                                                                            ,COMBUSTIBLE
                                                                            ,NUM_PEAJE  
                                                                            ,DOCTYPEID
                                                                            ,SITEID
                                                                            ,INVTIDSKU
                                                                            ,CURYID
                                                                            ,DOCSTATUSID
                                                                            ,PTOVTA
                                                                            ,STKUNITID
                                                                            ,USERID   
                                                                            ,INVTIDALTER)
                                                                VALUES		(@CORRNBR
                                                                            ,@SEQUENCE
                                                                            ,@NBRDOCUMENT
                                                                            ,@DATEDOC
                                                                            ,@DATEPROCESALES
                                                                            ,@PERPOST
                                                                            ,@STKCLOSEDZ
                                                                            ,@STKCLOSEDX
                                                                            ,@NBRTURN
                                                                            ,@NBRSIDE
                                                                            ,@NBRTRANSACFUEL
                                                                            ,@DISCDSCTO1
                                                                            ,@DISCDSCTO2
                                                                            ,@DISCMTOPEN
                                                                            ,@DISCMTOUSD
                                                                            ,@PORCENTTAXIGV
                                                                            ,@PORCENTTAXISC
                                                                            ,@TOTALPEN
                                                                            ,@TOTALUSD
                                                                            ,@TAXPEN
                                                                            ,@TAXUSD
                                                                            ,@STKITEM
                                                                            ,@STKFISI
                                                                            ,@SLS_PRICE
                                                                            ,@SLSPRICESALE
                                                                            ,@STDCOSTPEN
                                                                            ,@STDCOSTUSD
                                                                            ,@DESCRINVENTORY
                                                                            ,@QTY
                                                                            ,@KIT
                                                                            ,@COMBUSTIBLE
                                                                            ,@NUM_PEAJE  
                                                                            ,@DOCTYPEID
                                                                            ,@SITEID
                                                                            ,@INVTIDSKU
                                                                            ,@CURYID
                                                                            ,@DOCSTATUSID
                                                                            ,@PTOVTA
                                                                            ,@STKUNITID
                                                                            ,@USERID   
                                                                            ,@INVTIDALTER)";

                            var filasAfectadasAgregaDetallePedido = cn.Execute(sqlAgregaDetallePedido, new
                            {
                                CORRNBR = detallePedido.Correlativo,
                                SEQUENCE = detallePedido.Secuencia,
                                NBRDOCUMENT = detallePedido.NumeroDocumento,
                                DATEDOC = detallePedido.FechaDocumento,
                                DATEPROCESALES = detallePedido.FechaProceso,
                                PERPOST = detallePedido.Periodo,
                                STKCLOSEDZ = detallePedido.ProcesadoCierreZ,
                                STKCLOSEDX = detallePedido.ProcesadoCierreX,
                                NBRTURN = detallePedido.NumeroTurno,
                                NBRSIDE = detallePedido.NumeroCara,
                                NBRTRANSACFUEL = detallePedido.NumeroTransaccionCombustible,
                                DISCDSCTO1 = detallePedido.PorcentajeDescuentoPrimero,
                                DISCDSCTO2 = detallePedido.PorcentajeDescuentoSegundo,
                                DISCMTOPEN = detallePedido.PorcentajeDescuentoNacional,
                                DISCMTOUSD = detallePedido.PorcentajeDescuentoExtranjera,
                                PORCENTTAXIGV = detallePedido.PorcentajeImpuestoIgv,
                                PORCENTTAXISC = detallePedido.PorcentajeImpuestoIsc,
                                TOTALPEN = detallePedido.TotalNacional,
                                TOTALUSD = detallePedido.TotalExtranjera,
                                TAXPEN = detallePedido.ImpuestoNacional,
                                TAXUSD = detallePedido.ImpuestoExtranjera,
                                STKITEM = detallePedido.EsInventariable,
                                STKFISI = detallePedido.EnInventarioFisico,
                                SLS_PRICE = detallePedido.Precio,
                                SLSPRICESALE = detallePedido.PrecioVenta,
                                STDCOSTPEN = detallePedido.CostoEstandarNacional,
                                STDCOSTUSD = detallePedido.CostoEstandarExtranjera,
                                DESCRINVENTORY = detallePedido.DescripcionArticulo,
                                QTY = detallePedido.Cantidad,
                                KIT = detallePedido.EsFormula,
                                COMBUSTIBLE = detallePedido.EsArticuloCombustible,
                                NUM_PEAJE = detallePedido.NumeroPeaje,
                                DOCTYPEID = detallePedido.CodigoTipoDocumento,
                                SITEID = detallePedido.CodigoAlmacen,
                                INVTIDSKU = detallePedido.CodigoArticulo,
                                CURYID = detallePedido.CodigoMoneda,
                                DOCSTATUSID = detallePedido.CodigoEstadoDocumento,
                                PTOVTA = detallePedido.CodigoPuntoDeVenta,
                                STKUNITID = detallePedido.CodigoUnidadDeMedida,
                                USERID = detallePedido.CodigoUsuarioDeSistema,
                                INVTIDALTER = detallePedido.CodigoArticuloAlterno,
                            }, transaction: transaccion);
                        }
                    }


                    //Pedido Vales
                    if (pPedidoEESS.PedidoEESSConVales != null && pPedidoEESS.PedidoEESSConVales.Any())
                    {
                        foreach (var pedidoConVale in pPedidoEESS.PedidoEESSConVales)
                        {
                            string sqlAgregaPedidoConVale = @"INSERT INTO " + BaseDatos.PrefijoTabla + @"TMP_OP_BONUS
                                                                            (CORRNBR
                                                                            ,NBRBONUS
                                                                            ,CUSTIDSS
                                                                            ,SITEID)
                                                                VALUES		(@CORRNBR
                                                                            ,@NBRBONUS
                                                                            ,@CUSTIDSS
                                                                            ,@SITEID)";

                            var filasAfectadasAgregaPedidoConVale = cn.Execute(sqlAgregaPedidoConVale, new
                            {
                                CORRNBR = pedidoConVale.Correlativo,
                                NBRBONUS = pedidoConVale.NumeroVale,
                                CUSTIDSS = pedidoConVale.CodigoCliente,
                                SITEID = pedidoConVale.CodigoAlmacen
                            }, transaction: transaccion);
                        }
                    }

                    transaccion.Commit();
                }
            }
        }

        public PedidoEESS ObtenerPorNumeroPedido(int pCorrelativo)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	CORRNBR AS Correlativo,
                                            NBRSIDE AS NumeroCara,
                                            NBRDOCUMENT AS NumeroDocumento,
                                            STKINVENTORY AS AfectaInventario,
                                            DATEDOC AS FechaDocumento,
                                            DATEPROCESALES AS FechaProceso,
                                            PERPOST AS Periodo,
                                            TOTALPEN AS TotalNacional,
                                            TOTALUSD AS TotalExtranjera,
                                            SUBTOTALPEN AS SubTotalNacional,
                                            SUBTOTALUSD AS SubTotalExtranjera,
                                            TAXIGVPEN AS ImpuestoIgvNacional,
                                            TAXIGVUSD AS ImpuestoIgvExtranjera,
                                            TAXISCPEN AS ImpuestoIscNacional,
                                            TAXISCUSD AS ImpuestoIscExtranjera,
                                            TOTALNOTAFFECTPEN AS TotalNoAfectoNacional,
                                            TOTALNOTAFFECTUSD AS TotalNoAfectoExtranjera,
                                            PORCENTDISCOUNT1 AS PorcentajeDescuentoPrimero,
                                            PORCENTDISCOUNT2 AS PorcentajeDescuentoSegundo,
                                            TOTDISCOUNTPEN AS TotalDescuentoNacional,
                                            TOTDISCOUNTUSD AS TotalDescuentoExtranjera,
                                            TOTRETURNEDPEN AS TotalVueltoNacional,
                                            TOTRETURNEDUSD AS TotalVueltoExtranjera,
                                            TOTCASHPEN AS TotalEfectivoNacional,
                                            TOTCASHUSD AS TotalEfectivoExtranjera,
                                            TAXREGNBR AS RucCliente,
                                            CUSTNAME AS NombreCompletoCliente,
                                            PLACA AS Placa,
                                            NBRBONUS AS NumeroVale,
                                            CURYRATE AS TipoCambio,
                                            STKCLOSEDZ AS ProcesadoCierreZ,
                                            STKCLOSEDX AS ProcesadoCierreX,
                                            POINTNUMBER AS NumeroPuntos,
                                            TERMINALNAME AS NombreTerminal,
                                            KILOMETRAJE AS Kilometraje,
                                            ADDRESS AS DireccionCliente,
                                            TYPECUSTOMER AS TipoCliente,
                                            DSCRTYPECUST AS DescripcionTipoCliente,
                                            ESTADO AS DescripcionEstado,
                                            MONTOTIPOCAMBIO AS TipoCambioClienteCredito,
                                            DIASDEGRACIA AS DiasDeGraciaClienteCredito,
                                            LIMITECRED AS LimiteCreditoClienteCredito,
                                            DEUDACLIENTE AS DeudaClienteClienteCredito,
                                            MTOSURPLUS AS PlusCreditoClienteCredito,
                                            AFECTO AS Afecto,
                                            NBRCARD AS NumeroTarjeta,
                                            PAGO AS PagoTarjeta,
                                            DESCRCARD AS DescripcionTarjeta,
                                            DOCTYPEID AS CodigoTipoDocumento,
                                            TYPEPAYMENTID AS CodigoTipoPago,
                                            SITEID AS CodigoAlmacen,
                                            CURYID AS CodigoMoneda,
                                            DOCSTATUSID AS CodigoEstadoDocumento,
                                            TERMID AS CodigoCondicionPago,
                                            SALESPERID AS CodigoVendedor,
                                            USERID AS CodigoUsuarioDeSistema,
                                            TAXIGVID AS CodigoImpuestoIgv,
                                            TAXISCID AS CodigoImpuestoIsc,
                                            CUSTIDSS AS CodigoCliente,
                                            CURYTYPEID AS CodigoClaseTipoCambio,
                                            SALESPOINT AS CodigoPuntoDeVenta,
                                            IDESTADO AS CodigoEstado,
                                            MONEDACREDITO AS CodigoMonedaCredito,
                                            TIPODECAMBIO AS CodigoClaseTipoCambioClienteCredito,
                                            PROMOTIONCARDID AS CodigoTarjetaPromocion,
                                            CARDID AS CodigoTarjeta,
                                            CURYIDCARD AS CodigoMonedaTarjeta
                                    FROM	" + BaseDatos.PrefijoTabla + @"TMP_OP_SALES (NOLOCK)
                                    WHERE	CORRNBR = @CORRNBR;

                                    SELECT	CORRNBR AS Correlativo,
                                            SEQUENCE AS Secuencia,
                                            NBRDOCUMENT AS NumeroDocumento,
                                            DATEDOC AS FechaDocumento,
                                            DATEPROCESALES AS FechaProceso,
                                            PERPOST AS Periodo,
                                            STKCLOSEDZ AS ProcesadoCierreZ,
                                            STKCLOSEDX AS ProcesadoCierreX,
                                            NBRTURN AS NumeroTurno,
                                            NBRSIDE AS NumeroCara,
                                            NBRTRANSACFUEL AS NumeroTransaccionCombustible,
                                            DISCDSCTO1 AS PorcentajeDescuentoPrimero,
                                            DISCDSCTO2 AS PorcentajeDescuentoSegundo,
                                            DISCMTOPEN AS PorcentajeDescuentoNacional,
                                            DISCMTOUSD AS PorcentajeDescuentoExtranjera,
                                            PORCENTTAXIGV AS PorcentajeImpuestoIgv,
                                            PORCENTTAXISC AS PorcentajeImpuestoIsc,
                                            TOTALPEN AS TotalNacional,
                                            TOTALUSD AS TotalExtranjera,
                                            TAXPEN AS ImpuestoNacional,
                                            TAXUSD AS ImpuestoExtranjera,
                                            STKITEM AS EsInventariable,
                                            STKFISI AS EnInventarioFisico,
                                            SLS_PRICE AS Precio,
                                            SLSPRICESALE AS PrecioVenta,
                                            STDCOSTPEN AS CostoEstandarNacional,
                                            STDCOSTUSD AS CostoEstandarExtranjera,
                                            DESCRINVENTORY AS DescripcionArticulo,
                                            QTY AS Cantidad,
                                            KIT AS EsFormula,
                                            COMBUSTIBLE AS EsArticuloCombustible,
                                            NUM_PEAJE AS NumeroPeaje,
                                            DOCTYPEID AS CodigoTipoDocumento,
                                            SITEID AS CodigoAlmacen,
                                            INVTIDSKU AS CodigoArticulo,
                                            CURYID AS CodigoMoneda,
                                            DOCSTATUSID AS CodigoEstadoDocumento,
                                            PTOVTA AS CodigoPuntoDeVenta,
                                            STKUNITID AS CodigoUnidadDeMedida,
                                            USERID AS CodigoUsuarioDeSistema,
                                            INVTIDALTER AS CodigoArticuloAlterno
                                    FROM	" + BaseDatos.PrefijoTabla + @"TMP_OP_SALESDET (NOLOCK)
                                    WHERE	CORRNBR IN (SELECT	CORRNBR
                                                        FROM	" + BaseDatos.PrefijoTabla + @"TMP_OP_SALES (NOLOCK)
                                                        WHERE	CORRNBR = @CORRNBR);

                                    SELECT	CORRNBR AS Correlativo,
                                            NBRBONUS AS NumeroVale,
                                            CUSTIDSS AS CodigoCliente,
                                            SITEID AS CodigoAlmacen
                                    FROM	" + BaseDatos.PrefijoTabla + @"TMP_OP_BONUS (NOLOCK)
                                    WHERE	CORRNBR IN (SELECT	CORRNBR
                                                        FROM	" + BaseDatos.PrefijoTabla + @"TMP_OP_SALES (NOLOCK)
                                                        WHERE	CORRNBR = @CORRNBR)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { CORRNBR = pCorrelativo });

                var pedidoEESS = resultado.Read<PedidoEESS>().FirstOrDefault();
                var pedidoEESSDetalles = resultado.Read<PedidoEESSDetalle>().ToList();
                var pedidoEESSConVales = resultado.Read<PedidoEESSConVale>().ToList();
                if (pedidoEESS != null)
                {
                    return MapeoPedidoEESS(pedidoEESS, pedidoEESSDetalles, pedidoEESSConVales);
                }
                else
                    return null;

            }
        }

        public IEnumerable<PedidoEESS> ObtenerTodos(string pCodigoPuntoDeVenta)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	CORRNBR AS Correlativo,
                                            NBRSIDE AS NumeroCara,
                                            NBRDOCUMENT AS NumeroDocumento,
                                            STKINVENTORY AS AfectaInventario,
                                            DATEDOC AS FechaDocumento,
                                            DATEPROCESALES AS FechaProceso,
                                            PERPOST AS Periodo,
                                            TOTALPEN AS TotalNacional,
                                            TOTALUSD AS TotalExtranjera,
                                            SUBTOTALPEN AS SubTotalNacional,
                                            SUBTOTALUSD AS SubTotalExtranjera,
                                            TAXIGVPEN AS ImpuestoIgvNacional,
                                            TAXIGVUSD AS ImpuestoIgvExtranjera,
                                            TAXISCPEN AS ImpuestoIscNacional,
                                            TAXISCUSD AS ImpuestoIscExtranjera,
                                            TOTALNOTAFFECTPEN AS TotalNoAfectoNacional,
                                            TOTALNOTAFFECTUSD AS TotalNoAfectoExtranjera,
                                            PORCENTDISCOUNT1 AS PorcentajeDescuentoPrimero,
                                            PORCENTDISCOUNT2 AS PorcentajeDescuentoSegundo,
                                            TOTDISCOUNTPEN AS TotalDescuentoNacional,
                                            TOTDISCOUNTUSD AS TotalDescuentoExtranjera,
                                            TOTRETURNEDPEN AS TotalVueltoNacional,
                                            TOTRETURNEDUSD AS TotalVueltoExtranjera,
                                            TOTCASHPEN AS TotalEfectivoNacional,
                                            TOTCASHUSD AS TotalEfectivoExtranjera,
                                            TAXREGNBR AS RucCliente,
                                            CUSTNAME AS NombreCompletoCliente,
                                            PLACA AS Placa,
                                            NBRBONUS AS NumeroVale,
                                            CURYRATE AS TipoCambio,
                                            STKCLOSEDZ AS ProcesadoCierreZ,
                                            STKCLOSEDX AS ProcesadoCierreX,
                                            POINTNUMBER AS NumeroPuntos,
                                            TERMINALNAME AS NombreTerminal,
                                            KILOMETRAJE AS Kilometraje,
                                            ADDRESS AS DireccionCliente,
                                            TYPECUSTOMER AS TipoCliente,
                                            DSCRTYPECUST AS DescripcionTipoCliente,
                                            ESTADO AS DescripcionEstado,
                                            MONTOTIPOCAMBIO AS TipoCambioClienteCredito,
                                            DIASDEGRACIA AS DiasDeGraciaClienteCredito,
                                            LIMITECRED AS LimiteCreditoClienteCredito,
                                            DEUDACLIENTE AS DeudaClienteClienteCredito,
                                            MTOSURPLUS AS PlusCreditoClienteCredito,
                                            AFECTO AS Afecto,
                                            NBRCARD AS NumeroTarjeta,
                                            PAGO AS PagoTarjeta,
                                            DESCRCARD AS DescripcionTarjeta,
                                            DOCTYPEID AS CodigoTipoDocumento,
                                            TYPEPAYMENTID AS CodigoTipoPago,
                                            SITEID AS CodigoAlmacen,
                                            CURYID AS CodigoMoneda,
                                            DOCSTATUSID AS CodigoEstadoDocumento,
                                            TERMID AS CodigoCondicionPago,
                                            SALESPERID AS CodigoVendedor,
                                            USERID AS CodigoUsuarioDeSistema,
                                            TAXIGVID AS CodigoImpuestoIgv,
                                            TAXISCID AS CodigoImpuestoIsc,
                                            CUSTIDSS AS CodigoCliente,
                                            CURYTYPEID AS CodigoClaseTipoCambio,
                                            SALESPOINT AS CodigoPuntoDeVenta,
                                            IDESTADO AS CodigoEstado,
                                            MONEDACREDITO AS CodigoMonedaCredito,
                                            TIPODECAMBIO AS CodigoClaseTipoCambioClienteCredito,
                                            PROMOTIONCARDID AS CodigoTarjetaPromocion,
                                            CARDID AS CodigoTarjeta,
                                            CURYIDCARD AS CodigoMonedaTarjeta
                                    FROM	" + BaseDatos.PrefijoTabla + @"TMP_OP_SALES (NOLOCK)
                                    WHERE	SALESPOINT = @SALESPOINT
                                    ORDER BY 1";

                var resultado = cn.Query<PedidoEESS>(cadenaSQL,
                                    new
                                    {
                                        SALESPOINT = pCodigoPuntoDeVenta                                        
                                    });

                var ventas = resultado.AsList();

                if (ventas != null)
                {
                    return ventas;
                }
                else
                    return null;
            }
        }


        private PedidoEESS MapeoPedidoEESS(PedidoEESS pPedidoEESS, List<PedidoEESSDetalle> pPedidoEESSDetalles, 
                                                List<PedidoEESSConVale> pPedidoEESSConVales)
        {
            var nuevoPedidoEESS = new PedidoEESS();
            nuevoPedidoEESS = pPedidoEESS;
            
            if(pPedidoEESSDetalles != null && pPedidoEESSDetalles.Any())
            {
                foreach (var detallePedido in pPedidoEESSDetalles)
                {                            
                    nuevoPedidoEESS.AgregarNuevoPedidoEESSDetalle(detallePedido.Secuencia,detallePedido.NumeroTurno, detallePedido.NumeroTransaccionCombustible, 
                                    detallePedido.PorcentajeDescuentoPrimero, detallePedido.PorcentajeDescuentoSegundo, detallePedido.PorcentajeDescuentoNacional, 
                                    detallePedido.PorcentajeDescuentoExtranjera, detallePedido.PorcentajeImpuestoIgv, detallePedido.PorcentajeImpuestoIsc, 
                                    detallePedido.TotalNacional, detallePedido.TotalExtranjera,  detallePedido.ImpuestoNacional, 
                                    detallePedido.ImpuestoExtranjera, detallePedido.EsInventariable, detallePedido.EnInventarioFisico, 
                                    detallePedido.Precio, detallePedido.PrecioVenta, detallePedido.CostoEstandarNacional, 
                                    detallePedido.CostoEstandarExtranjera, detallePedido.DescripcionArticulo, detallePedido.Cantidad, 
                                    detallePedido.EsFormula, detallePedido.EsArticuloCombustible, detallePedido.NumeroPeaje, 
                                    detallePedido.CodigoArticulo, detallePedido.CodigoUnidadDeMedida, detallePedido.CodigoArticuloAlterno);
                }
            }

            if(pPedidoEESSConVales != null && pPedidoEESSConVales.Any())
            {
                foreach (var pedidoConVale in pPedidoEESSConVales)
                {
                    nuevoPedidoEESS.AgregarNuevoPedidoEESSConVale(pedidoConVale.NumeroVale);
                }
            }

            return nuevoPedidoEESS;
        }
    }
}