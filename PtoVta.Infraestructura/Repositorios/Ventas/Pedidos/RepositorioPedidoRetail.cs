using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Infraestructura.Repositorios.Ventas
{
    public class RepositorioPedidoRetail : Repositorio<PedidoRetail>, IRepositorioPedidoRetail
    {
        public RepositorioPedidoRetail(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override void Agregar(PedidoRetail pPedidoRetail)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                cn.Open();
                using (var transaccion = cn.BeginTransaction())
                {
                    //Cabecera Venta
                    string sqlAgregaPedido = @"INSERT INTO PC_TMP_OP_SALESCSTORE
                                                            (CORRNBR	
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
                                                            ,taxregnbr	
                                                            ,CUSTNAME	
                                                            ,ADDR1	
                                                            ,PLACA	
                                                            ,NBRBONUS	
                                                            ,CURYRATE	
                                                            ,POINTNUMBER	
                                                            ,KILOMETRAJE	
                                                            ,STKSAVE	
                                                            ,SALESTYPE	
                                                            ,PROCESSED	
                                                            ,DSC_CUPON	
                                                            ,CENTRO_COSTO	
                                                            ,DOCTYPEID	
                                                            ,TYPEPAYMENTID	
                                                            ,SITEID	
                                                            ,CURYID	
                                                            ,TERMID	
                                                            ,SALESPERID	
                                                            ,USERID	
                                                            ,TAXIGVID	
                                                            ,TAXISCID	
                                                            ,custidss	
                                                            ,CURYTYPEID	
                                                            ,PROMOTIONCARDID	
                                                            ,SALESPOINT	
                                                            ,businesstype)
                                                    VALUES	(@CORRNBR	
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
                                                            ,@ADDR1	
                                                            ,@PLACA	
                                                            ,@NBRBONUS	
                                                            ,@CURYRATE	
                                                            ,@POINTNUMBER	
                                                            ,@KILOMETRAJE	
                                                            ,@STKSAVE	
                                                            ,@SALESTYPE	
                                                            ,@PROCESSED	
                                                            ,@DSC_CUPON	
                                                            ,@CENTRO_COSTO	
                                                            ,@DOCTYPEID	
                                                            ,@TYPEPAYMENTID	
                                                            ,@SITEID	
                                                            ,@CURYID	
                                                            ,@TERMID	
                                                            ,@SALESPERID	
                                                            ,@USERID	
                                                            ,@TAXIGVID	
                                                            ,@TAXISCID	
                                                            ,@CUSTIDSS	
                                                            ,@CURYTYPEID	
                                                            ,@PROMOTIONCARDID	
                                                            ,@SALESPOINT	
                                                            ,@BUSINESSTYPE)";

                    var filasAfectadasAgregaPedido = cn.Execute(sqlAgregaPedido, new
                    {
                        CORRNBR = pPedidoRetail.Correlativo,
                        NBRDOCUMENT = pPedidoRetail.NumeroDocumento,
                        STKINVENTORY = pPedidoRetail.AfectaInventario,
                        DATEDOC = pPedidoRetail.FechaDocumento,
                        DATEPROCESALES = pPedidoRetail.FechaProceso,
                        PERPOST = pPedidoRetail.Periodo,
                        TOTALPEN = pPedidoRetail.TotalNacional,
                        TOTALUSD = pPedidoRetail.TotalExtranjera,
                        SUBTOTALPEN = pPedidoRetail.SubTotalNacional,
                        SUBTOTALUSD = pPedidoRetail.SubTotalExtranjera,
                        TAXIGVPEN = pPedidoRetail.ImpuestoIgvNacional,
                        TAXIGVUSD = pPedidoRetail.ImpuestoIgvExtranjera,
                        TAXISCPEN = pPedidoRetail.ImpuestoIscNacional,
                        TAXISCUSD = pPedidoRetail.ImpuestoIscExtranjera,
                        TOTALNOTAFFECTPEN = pPedidoRetail.TotalNoAfectoNacional,
                        TOTALNOTAFFECTUSD = pPedidoRetail.TotalNoAfectoExtranjera,
                        PORCENTDISCOUNT1 = pPedidoRetail.PorcentajeDescuentoPrimero,
                        PORCENTDISCOUNT2 = pPedidoRetail.PorcentajeDescuentoSegundo,
                        TOTDISCOUNTPEN = pPedidoRetail.TotalDescuentoNacional,
                        TOTDISCOUNTUSD = pPedidoRetail.TotalDescuentoExtranjera,
                        TOTRETURNEDPEN = pPedidoRetail.TotalVueltoNacional,
                        TOTRETURNEDUSD = pPedidoRetail.TotalVueltoExtranjera,
                        TOTCASHPEN = pPedidoRetail.TotalEfectivoNacional,
                        TOTCASHUSD = pPedidoRetail.TotalEfectivoExtranjera,
                        TAXREGNBR = pPedidoRetail.RucCliente,
                        CUSTNAME = pPedidoRetail.NombreCompletoCliente,
                        ADDR1 = pPedidoRetail.DireccionCliente,
                        PLACA = pPedidoRetail.Placa,
                        NBRBONUS = pPedidoRetail.NumeroVale,
                        CURYRATE = pPedidoRetail.TipoCambio,
                        POINTNUMBER = pPedidoRetail.NumeroPuntos,
                        KILOMETRAJE = pPedidoRetail.Kilometraje,
                        STKSAVE = pPedidoRetail.TransaccionPendiente,
                        SALESTYPE = pPedidoRetail.TipoVenta,
                        PROCESSED = pPedidoRetail.TransaccionProcesada,
                        DSC_CUPON = pPedidoRetail.AplicaDescuentoCupon,
                        CENTRO_COSTO = pPedidoRetail.CentroDeCosto,
                        DOCTYPEID = pPedidoRetail.CodigoTipoDocumento,
                        TYPEPAYMENTID = pPedidoRetail.CodigoTipoPago,
                        SITEID = pPedidoRetail.CodigoAlmacen,
                        CURYID = pPedidoRetail.CodigoMoneda,
                        TERMID = pPedidoRetail.CodigoCondicionPago,
                        SALESPERID = pPedidoRetail.CodigoVendedor,
                        USERID = pPedidoRetail.CodigoUsuarioDeSistema,
                        TAXIGVID = pPedidoRetail.CodigoImpuestoIgv,
                        TAXISCID = pPedidoRetail.CodigoImpuestoIsc,
                        CUSTIDSS = pPedidoRetail.CodigoCliente,
                        CURYTYPEID = pPedidoRetail.CodigoClaseTipoCambio,
                        PROMOTIONCARDID = pPedidoRetail.CodigoTarjetaPromocion,
                        SALESPOINT = pPedidoRetail.CodigoPuntoDeVenta,
                        BUSINESSTYPE = pPedidoRetail.CodigoTipoNegocio
                    }, transaction: transaccion);

                    //Detalle Venta
                    if (pPedidoRetail.PedidoRetailDetalles != null && pPedidoRetail.PedidoRetailDetalles.Any())
                    {
                        foreach (var detallePedido in pPedidoRetail.PedidoRetailDetalles)
                        {
                            string sqlAgregaDetallePedido = @"INSERT INTO PC_TMP_OP_SALESDETCSTORE
                                                                        (CORRNBR
                                                                        ,NBRDOCUMENT
                                                                        ,SEQUENCE	
                                                                        ,DATEDOC	
                                                                        ,DATEPROCESALES	
                                                                        ,PERPOST	
                                                                        ,NBRTURN	
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
                                                                        ,INVTIDALTER	
                                                                        ,DESCRINVENTORY	
                                                                        ,QTY	
                                                                        ,KIT	
                                                                        ,NUM_PEAJE	
                                                                        ,DOCTYPEID	
                                                                        ,SITEID	
                                                                        ,CURYID	
                                                                        ,INVTIDSKU	
                                                                        ,STKUNITID)
                                                                VALUES	(@CORRNBR
                                                                        ,@NBRDOCUMENT
                                                                        ,@SEQUENCE	
                                                                        ,@DATEDOC	
                                                                        ,@DATEPROCESALES	
                                                                        ,@PERPOST	
                                                                        ,@NBRTURN	
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
                                                                        ,@INVTIDALTER	
                                                                        ,@DESCRINVENTORY	
                                                                        ,@QTY	
                                                                        ,@KIT	
                                                                        ,@NUM_PEAJE	
                                                                        ,@DOCTYPEID	
                                                                        ,@SITEID	
                                                                        ,@CURYID	
                                                                        ,@INVTIDSKU	
                                                                        ,@STKUNITID)";

                            var filasAfectadasAgregaDetallePedido = cn.Execute(sqlAgregaDetallePedido, new
                            {
                                CORRNBR = detallePedido.Correlativo,
                                NBRDOCUMENT = detallePedido.NumeroDocumento,
                                SEQUENCE = detallePedido.Secuencia,
                                DATEDOC = detallePedido.FechaDocumento,
                                DATEPROCESALES = detallePedido.FechaProceso,
                                PERPOST = detallePedido.Periodo,
                                NBRTURN = detallePedido.NumeroTurno,
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
                                INVTIDALTER = detallePedido.CodigoArticuloAlterno,
                                DESCRINVENTORY = detallePedido.DescripcionArticulo,
                                QTY = detallePedido.Cantidad,
                                KIT = detallePedido.EsFormula,
                                NUM_PEAJE = detallePedido.NumeroPeaje,
                                DOCTYPEID = detallePedido.CodigoTipoDocumento,
                                SITEID = detallePedido.CodigoAlmacen,
                                CURYID = detallePedido.CodigoMoneda,
                                INVTIDSKU = detallePedido.CodigoArticulo,
                                STKUNITID = detallePedido.CodigoUnidadDeMedida
                            }, transaction: transaccion);
                        }
                    }

                    //Venta con Tarjeta
                    if (pPedidoRetail.PedidoRetailConTarjetas != null && pPedidoRetail.PedidoRetailConTarjetas.Any())
                    {
                        foreach (var pedidoConTarjeta in pPedidoRetail.PedidoRetailConTarjetas)
                        {
                            string sqlAgregaPedidoConTarjeta = @"INSERT INTO PC_TMP_OP_SALESCARD
                                                                                (CORRNBR
                                                                                ,SEQUENCE
                                                                                ,NBRCARD
                                                                                ,TOTCARDPEN
                                                                                ,TOTCARDUSD
                                                                                ,TRANSACPINPAD
                                                                                ,TIPOTARJETA
                                                                                ,DNITARJETA
                                                                                ,NOMBRETARJETA	
                                                                                ,SITEID
                                                                                ,CARDID
                                                                                ,CURYID)
                                                                    VALUES		(@CORRNBR
                                                                                ,@SEQUENCE
                                                                                ,@NBRCARD
                                                                                ,@TOTCARDPEN
                                                                                ,@TOTCARDUSD
                                                                                ,@TRANSACPINPAD
                                                                                ,@TIPOTARJETA
                                                                                ,@DNITARJETA
                                                                                ,@NOMBRETARJETA	
                                                                                ,@SITEID
                                                                                ,@CARDID
                                                                                ,@CURYID)";

                            var filasAfectadasAgregaPedidoConTarjeta = cn.Execute(sqlAgregaPedidoConTarjeta, new
                            {
                                CORRNBR = pedidoConTarjeta.Correlativo,
                                SEQUENCE = pedidoConTarjeta.Secuencia,
                                NBRCARD = pedidoConTarjeta.NumeroTarjeta,
                                TOTCARDPEN = pedidoConTarjeta.TotalTarjetaNacional,
                                TOTCARDUSD = pedidoConTarjeta.TotalTarjetaExtranjera,
                                TRANSACPINPAD = pedidoConTarjeta.EsTransaccionPinPad,
                                TipoTarjeta = pedidoConTarjeta.TipoTarjeta,
                                DNITarjeta = pedidoConTarjeta.DNIAsociadoATarjeta,
                                NombreTarjeta = pedidoConTarjeta.DescripcionTarjeta,
                                SITEID = pedidoConTarjeta.CodigoAlmacen,
                                CARDID = pedidoConTarjeta.CodigoTarjeta,
                                curyid = pedidoConTarjeta.CodigoMoneda
                            }, transaction: transaccion);
                        }
                    }

                    //Venta con Vales
                    if (pPedidoRetail.PedidoRetailConVales != null & pPedidoRetail.PedidoRetailConVales.Any())
                    {
                        foreach (var pedidoConVale in pPedidoRetail.PedidoRetailConVales)
                        {
                            string sqlAgregaPedidoConVale = @"INSERT INTO PC_TMP_OP_BONUS
                                                                        (CORRNBR	
                                                                        ,NBRBONUS
                                                                        ,CUSTIDSS
                                                                        ,SITEID)
                                                                VALUE	(@CORRNBR	
                                                                        ,@NBRBONUS
                                                                        ,@CUSTIDSS
                                                                        ,@SITEID)";

                            var filasAfectadasAgregaPedidoConVale = cn.Execute(sqlAgregaPedidoConVale, new
                            {
                                CORRNBR = pedidoConVale.Correlativo,
                                NBRBONUS = pedidoConVale.NumeroVale,
                                custidss = pedidoConVale.CodigoCliente,
                                SITEID = pedidoConVale.CodigoAlmacen
                            }, transaction: transaccion);
                        }
                    }

                    transaccion.Commit();
                }
            }
        }

        public PedidoRetail ObtenerPorNumeroPedido(int pCorrelativo)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT  CORRNBR AS Correlativo,
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
                                            ADDR1 AS DireccionCliente,
                                            PLACA AS Placa,
                                            NBRBONUS AS NumeroVale,
                                            CURYRATE AS TipoCambio,
                                            POINTNUMBER AS NumeroPuntos,
                                            KILOMETRAJE AS Kilometraje,
                                            STKSAVE AS TransaccionPendiente,
                                            SALESTYPE AS TipoVenta,
                                            PROCESSED AS TransaccionProcesada,
                                            DSC_CUPON AS AplicaDescuentoCupon,
                                            CENTRO_COSTO AS CentroDeCosto,
                                            DOCTYPEID AS CodigoTipoDocumento,
                                            TYPEPAYMENTID AS CodigoTipoPago,
                                            SITEID AS CodigoAlmacen,
                                            CURYID AS CodigoMoneda,
                                            TERMID AS CodigoCondicionPago,
                                            SALESPERID AS CodigoVendedor,
                                            USERID AS CodigoUsuarioDeSistema,
                                            TAXIGVID AS CodigoImpuestoIgv,
                                            TAXISCID AS CodigoImpuestoIsc,
                                            CUSTIDSS AS CodigoCliente,
                                            CURYTYPEID AS CodigoClaseTipoCambio,
                                            PROMOTIONCARDID AS CodigoTarjetaPromocion,
                                            SALESPOINT AS CodigoPuntoDeVenta,
                                            BUSINESSTYPE AS CodigoTipoNegocio
                                    FROM	PC_TMP_OP_SALESCSTORE (NOLOCK)
                                    WHERE	CORRNBR	= @CORRNBR;

                                    SELECT	CORRNBR  AS Correlativo,
                                            NBRDOCUMENT  AS NumeroDocumento,
                                            SEQUENCE  AS Secuencia,
                                            DATEDOC  AS FechaDocumento,
                                            DATEPROCESALES  AS FechaProceso,
                                            PERPOST  AS Periodo,
                                            NBRTURN  AS NumeroTurno,
                                            PORCENTTAXIGV  AS PorcentajeImpuestoIgv,
                                            PORCENTTAXISC  AS PorcentajeImpuestoIsc,
                                            TOTALPEN  AS TotalNacional,
                                            TOTALUSD  AS TotalExtranjera,
                                            TAXPEN  AS ImpuestoNacional,
                                            TAXUSD  AS ImpuestoExtranjera,
                                            STKITEM  AS EsInventariable,
                                            STKFISI  AS EnInventarioFisico,
                                            SLS_PRICE  AS Precio,
                                            SLSPRICESALE  AS PrecioVenta,
                                            STDCOSTPEN  AS CostoEstandarNacional,
                                            STDCOSTUSD  AS CostoEstandarExtranjera,
                                            INVTIDALTER  AS CodigoArticuloAlterno,
                                            DESCRINVENTORY  AS DescripcionArticulo,
                                            QTY  AS Cantidad,
                                            KIT  AS EsFormula,
                                            NUM_PEAJE  AS NumeroPeaje,
                                            DOCTYPEID  AS CodigoTipoDocumento,
                                            SITEID  AS CodigoAlmacen,
                                            CURYID  AS CodigoMoneda,
                                            INVTIDSKU  AS CodigoArticulo,
                                            STKUNITID  AS CodigoUnidadDeMedida
                                    FROM	PC_TMP_OP_SALESDETCSTORE (NOLOCK)
                                    WHERE	CORRNBR	IN (SELECT	CORRNBR
                                                        FROM	PC_TMP_OP_SALESCSTORE (NOLOCK)
                                                        WHERE	CORRNBR	= @CORRNBR);

                                    SELECT	CORRNBR  AS Correlativo,
                                            SEQUENCE  AS Secuencia,
                                            NBRCARD  AS NumeroTarjeta,
                                            TOTCARDPEN  AS TotalTarjetaNacional,
                                            TOTCARDUSD  AS TotalTarjetaExtranjera,
                                            TRANSACPINPAD  AS EsTransaccionPinPad,
                                            TipoTarjeta  AS TipoTarjeta,
                                            DNITarjeta  AS DNIAsociadoATarjeta,
                                            NombreTarjeta  AS DescripcionTarjeta,
                                            SITEID  AS CodigoAlmacen,
                                            CARDID  AS CodigoTarjeta,
                                            curyid  AS CodigoMoneda	
                                    FROM	PC_TMP_OP_SALESCARD (NOLOCK)
                                    WHERE	CORRNBR	IN (SELECT	CORRNBR
                                                        FROM	PC_TMP_OP_SALESCSTORE (NOLOCK)
                                                        WHERE	CORRNBR	= @CORRNBR);

                                    SELECT	CORRNBR  AS Correlativo,
                                            NBRBONUS  AS NumeroVale,
                                            custidss  AS CodigoCliente,
                                            SITEID  AS CodigoAlmacen
                                    FROM	PC_TMP_OP_BONUS (NOLOCK)
                                    WHERE	CORRNBR	IN (SELECT	CORRNBR
                                                        FROM	PC_TMP_OP_SALESCSTORE (NOLOCK)
                                                        WHERE	CORRNBR	= @CORRNBR)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { CORRNBR = pCorrelativo });

                var pedidoRetail = resultado.Read<PedidoRetail>().FirstOrDefault();
                var pedidoRetailDetalles = resultado.Read<PedidoRetailDetalle>().ToList();
                var pedidoRetailConTarjetas = resultado.Read<PedidoRetailConTarjeta>().ToList();
                var pedidoRetailConVales = resultado.Read<PedidoRetailConVale>().ToList();
                if (pedidoRetail != null)
                {
                    return MapeoPedidoRetail(pedidoRetail, pedidoRetailDetalles, pedidoRetailConTarjetas, pedidoRetailConVales);
                }
                else
                    return null;

            }
        }

        public IEnumerable<PedidoRetail> ObtenerTodos(string pCodigoPuntoDeVenta)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT  CORRNBR AS Correlativo,
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
                                            ADDR1 AS DireccionCliente,
                                            PLACA AS Placa,
                                            NBRBONUS AS NumeroVale,
                                            CURYRATE AS TipoCambio,
                                            POINTNUMBER AS NumeroPuntos,
                                            KILOMETRAJE AS Kilometraje,
                                            STKSAVE AS TransaccionPendiente,
                                            SALESTYPE AS TipoVenta,
                                            PROCESSED AS TransaccionProcesada,
                                            DSC_CUPON AS AplicaDescuentoCupon,
                                            CENTRO_COSTO AS CentroDeCosto,
                                            DOCTYPEID AS CodigoTipoDocumento,
                                            TYPEPAYMENTID AS CodigoTipoPago,
                                            SITEID AS CodigoAlmacen,
                                            CURYID AS CodigoMoneda,
                                            TERMID AS CodigoCondicionPago,
                                            SALESPERID AS CodigoVendedor,
                                            USERID AS CodigoUsuarioDeSistema,
                                            TAXIGVID AS CodigoImpuestoIgv,
                                            TAXISCID AS CodigoImpuestoIsc,
                                            CUSTIDSS AS CodigoCliente,
                                            CURYTYPEID AS CodigoClaseTipoCambio,
                                            PROMOTIONCARDID AS CodigoTarjetaPromocion,
                                            SALESPOINT AS CodigoPuntoDeVenta,
                                            BUSINESSTYPE AS CodigoTipoNegocio
                                    FROM	PC_TMP_OP_SALESCSTORE (NOLOCK)
                                    WHERE	SALESPOINT	    = @SALESPOINT
                                            AND SALESTYPE   = @SALESTYPE                                     
                                            STKSAVE         = 1 
                                            AND PROCESSED   = 0 
                                            AND TOTALPEN    > 0
                                    ORDER BY 1";

                var resultado = cn.Query<PedidoRetail>(cadenaSQL,
                                    new
                                    {
                                        SALESPOINT = pCodigoPuntoDeVenta,
                                        SALESTYPE  = EnumModoTipoVenta.ModoTipoVentaAutomatico                                        
                                    });

                var pedidos = resultado.AsList();

                if (pedidos != null)
                {
                    return pedidos;
                }
                else
                    return null;
            }
        }

        private PedidoRetail MapeoPedidoRetail(PedidoRetail pPedidoRetail, List<PedidoRetailDetalle> pPedidoRetailDetalles, 
                        List<PedidoRetailConTarjeta> pPedidoRetailConTarjetas,  List<PedidoRetailConVale> pPedidoRetailConVales)
        {
            var nuevoPedidoRetail = new PedidoRetail();
            nuevoPedidoRetail = pPedidoRetail;

            if(pPedidoRetailDetalles != null && pPedidoRetailDetalles.Any())
            {
                foreach (var pedidoDetalles in pPedidoRetailDetalles)
                {
                    nuevoPedidoRetail.AgregarNuevoPedidoRetailDetalle(pedidoDetalles.NumeroDocumento, pedidoDetalles.Secuencia, pedidoDetalles.FechaDocumento,
                                        pedidoDetalles.FechaProceso, pedidoDetalles.Periodo, pedidoDetalles.NumeroTurno,
                                        pedidoDetalles.PorcentajeImpuestoIgv, pedidoDetalles.PorcentajeImpuestoIsc, pedidoDetalles.TotalNacional,
                                        pedidoDetalles.TotalExtranjera, pedidoDetalles.ImpuestoNacional, pedidoDetalles.ImpuestoExtranjera,
                                        pedidoDetalles.EsInventariable, pedidoDetalles.EnInventarioFisico, pedidoDetalles.Precio,
                                        pedidoDetalles.PrecioVenta, pedidoDetalles.CostoEstandarNacional, pedidoDetalles.CostoEstandarExtranjera,
                                        pedidoDetalles.CodigoArticuloAlterno, pedidoDetalles.DescripcionArticulo, pedidoDetalles.Cantidad,
                                        pedidoDetalles.EsFormula, pedidoDetalles.NumeroPeaje, pedidoDetalles.CodigoArticulo,
                                        pedidoDetalles.CodigoUnidadDeMedida);
                }
            }

            if(pPedidoRetailConTarjetas != null && pPedidoRetailConTarjetas.Any())
            {
                foreach (var pedidoConTarjeta in pPedidoRetailConTarjetas)
                {
                    nuevoPedidoRetail.AgregarNuevoPedidoRetailConTarjeta(pedidoConTarjeta.Secuencia, pedidoConTarjeta.NumeroTarjeta, pedidoConTarjeta.TotalTarjetaNacional,
                                        pedidoConTarjeta.TotalTarjetaExtranjera, pedidoConTarjeta.EsTransaccionPinPad, pedidoConTarjeta.TipoTarjeta,
                                        pedidoConTarjeta.DNIAsociadoATarjeta, pedidoConTarjeta.DescripcionTarjeta,  pedidoConTarjeta.CodigoTarjeta);                   
                }
            }

            if(pPedidoRetailConVales != null && pPedidoRetailConVales.Any())
            {
                foreach (var pedidoConVale in pPedidoRetailConVales)
                {
                    nuevoPedidoRetail.AgregarNuevoPedidoRetailConVale(pedidoConVale.NumeroVale);
                }                    
            }

            return new PedidoRetail();                
        }        
    }
}