using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;

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
                    string sqlAgregaPedidoEESS = @"INSERT INTO PC_TMP_OP_SALES
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
                        SALESPOINT = pPedidoEESS.CodigoConfiguracionPuntoVenta,
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
                            string sqlAgregaDetallePedido = @"INSERT INTO PC_TMP_OP_SALESDET
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
                                PTOVTA = detallePedido.CodigoConfiguracionPuntoVenta,
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
                         string sqlAgregaPedidoConVale = @"INSERT INTO PC_TMP_OP_BONUS
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
                string cadenaSQL = @"SELECT	M.MODULEID	AS CodigoModuloSistema
                                            ,M.MODULENAME	AS NombreModulo
                                    FROM	SE_MODULE (NOLOCK)	M
                                    WHERE	MODULEID	= @MODULEID;

                                    SELECT	V.SCREENID		AS CodigoVentanaUsuario
                                            ,V.SCREENNAME	AS NombreVentana
                                            ,V.SCREENTYPE	AS TipoVentana
                                            ,V.MODULEID		AS CodigoModuloSistema
                                    FROM	SE_SCREEN (NOLOCK)	V
                                            INNER JOIN SE_ACCESSDETRIGHTS (NOLOCK) D	ON V.SCREENID = D.SCREENID
                                    WHERE	V.MODULEID		= @MODULEID
                                                AND D.USERID	        = @USERID;
                                                                                        
                                    SELECT	D.VIEWRIGHTS		AS DerechoConsultar
                                            ,D.INSERTRIGHTS		AS DerechoInsertar
                                            ,D.UPDATERIGHTS		AS DerechoActualizar
                                            ,D.DELETERIGHTS		AS DerechoEliminar
                                            ,D.PRINTRIGHTS		AS DerechoImprimir	
                                            ,D.NULLRIGHTS		AS DerechoAnular
                                            ,D.CLOSERIGHTS		AS DerechoEmitir	
                                            ,D.SCREENID			AS CodigoVentanaUsuario
                                            ,D.USERID			AS CodigoUsuarioSistema
                                    FROM	SE_ACCESSDETRIGHTS (NOLOCK) D
                                    WHERE	LTRIM(RTRIM(D.SCREENID)) + LTRIM(RTRIM(D.USERID)) IN(SELECT	LTRIM(RTRIM(V.SCREENID)) + LTRIM(RTRIM(D.USERID))
                                                                                                FROM	SE_SCREEN (NOLOCK)	V
                                                                                                        INNER JOIN SE_ACCESSDETRIGHTS (NOLOCK) D	ON V.SCREENID = D.SCREENID
                                                                                                WHERE	V.MODULEID		= @MODULEID
                                                                                                        AND D.USERID            = @USERID)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { CORRNBR = pCorrelativo});

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

        public IEnumerable<PedidoEESS> ObtenerTodos(string pCodigoConfiguracionPuntoVenta)
        {
            throw new NotImplementedException();
        }


        private PedidoEESS MapeoPedidoEESS(PedidoEESS pPedidoEESS, List<PedidoEESSDetalle> pPedidoEESSDetalles, List<PedidoEESSConVale> pPedidoEESSConVale )
        {
            return new PedidoEESS();
        }
    }
}