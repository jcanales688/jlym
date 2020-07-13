using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class PedidoEESS:Entidad
    {
        HashSet<PedidoEESSDetalle> _lineasPedidoEESSDetalle;
        HashSet<PedidoEESSConVale> _lineasPedidoEESSConVale;


        // CORRNBR	int
        public int Correlativo { get; set; }

        // NBRSIDE	UD_CARA
        public string NumeroCara { get; set; }  

        // NBRDOCUMENT	varchar
        public string NumeroDocumento { get; set; }

        // STKINVENTORY	bit
        public bool AfectaInventario { get; set; }

        // DATEDOC	smalldatetime
        public DateTime FechaDocumento { get; set; }
        
        // DATEPROCESALES	smalldatetime
        public DateTime FechaProceso { get; set; }

        // PERPOST	UD_PERPOST
        public string Periodo { get; set; }

        // TOTALPEN	numeric
        public decimal TotalNacional { get; set; }

        // TOTALUSD	numeric
        public decimal TotalExtranjera { get; set; }

        // SUBTOTALPEN	numeric
        public decimal SubTotalNacional { get; set; }

        // SUBTOTALUSD	numeric
        public decimal SubTotalExtranjera { get; set; }

        // TAXIGVPEN	numeric
        public decimal ImpuestoIgvNacional { get; set; }

        // TAXIGVUSD	numeric
        public decimal ImpuestoIgvExtranjera { get; set; }

        // TAXISCPEN	numeric
        public decimal ImpuestoIscNacional { get; set; }

        // TAXISCUSD	numeric
        public decimal ImpuestoIscExtranjera { get; set; }

        // TOTALNOTAFFECTPEN	numeric
        public decimal TotalNoAfectoNacional { get; set; }

        // TOTALNOTAFFECTUSD	numeric
        public decimal TotalNoAfectoExtranjera { get; set; }

        // PORCENTDISCOUNT1	numeric
        public decimal PorcentajeDescuentoPrimero { get; set; }

        // PORCENTDISCOUNT2	numeric
        public decimal PorcentajeDescuentoSegundo { get; set; }

        // TOTDISCOUNTPEN	numeric
        public decimal TotalDescuentoNacional { get; set; }

        // TOTDISCOUNTUSD	numeric
        public decimal TotalDescuentoExtranjera { get; set; }

        // TOTRETURNEDPEN	numeric
        public decimal TotalVueltoNacional { get; set; }

        // TOTRETURNEDUSD	numeric
        public decimal TotalVueltoExtranjera { get; set; }

        // TOTCASHPEN	numeric
        public decimal TotalEfectivoNacional { get; set; }

        // TOTCASHUSD	numeric
        public decimal TotalEfectivoExtranjera { get; set; }

        // TAXREGNBR	UD_CODCLIENT
        public string RucCliente { get; set; }
        
        // CUSTNAME	varchar
        public string NombreCompletoCliente { get; set; }

        // PLACA	UD_PLACA
        public string Placa { get; set; }

        // NBRBONUS	numeric
        public decimal NumeroVale { get; set; }

        // CURYRATE	UD_CAMBIO
        public decimal TipoCambio { get; set; }

        // STKCLOSEDZ	bit
        public bool ProcesadoCierreZ { get; set; }

        // STKCLOSEDX	bit
        public bool ProcesadoCierreX { get; set; }

        // POINTNUMBER	smallint
        public int NumeroPuntos { get; set; }

        // TERMINALNAME	varchar
        public string NombreTerminal { get; set; }

        // KILOMETRAJE	numeric
        public int Kilometraje { get; set; }

        // ADDRESS	varchar
        public string DireccionCliente { get; set; }

        // TYPECUSTOMER	int
        public int TipoCliente { get; set; }

        // DSCRTYPECUST	varchar
        public string DescripcionTipoCliente { get; set; }

        // ESTADO	varchar
        public string DescripcionEstado { get; set; }

        // MONTOTIPOCAMBIO	UD_CURYRATE
        public decimal TipoCambioClienteCredito { get; set; }

        // DIASDEGRACIA	int
        public int DiasDeGraciaClienteCredito { get; set; }

        // LIMITECRED	numeric
        public decimal LimiteCreditoClienteCredito { get; set; }

        // DEUDACLIENTE	numeric
        public decimal DeudaClienteClienteCredito { get; set; }

        // MTOSURPLUS	numeric
        public decimal PlusCreditoClienteCredito { get; set; }

        // AFECTO	bit
        public bool Afecto { get; set; }

        // NBRCARD	char
        public string NumeroTarjeta { get; set; }

        // PAGO	int
        public int PagoTarjeta { get; set; }

        // DESCRCARD	UD_NOMBRE
        public string DescripcionTarjeta { get; set; }

        // USER1	UD_USER1
        // USER2	UD_USER2
        // USER3	UD_USER3
        // USER4	UD_USER5
        // USER5	UD_USER5
        // USER6	UD_USER6
        // USER7	UD_USER7
        // USER8	UD_USER8
        // USER9	UD_USER9
        // CENTRO_COSTO	varchar
        // FLAG_TICKET_IMP	char
        // NUM_TJFLOTAS	varchar
        // NUM_TJ_CALIBRACION	varchar
        // TIPO_FACT	char
        // VENTADNI	varchar
        // TOTALDNI	numeric
        // FLAG_TIPO_CONSUMO	char
        // FLAG_SIGV	tinyint
        // TipoTarjeta	varchar
        // DNITarjeta	varchar
        // NombreTarjeta	varchar
        // FLAG_CANJE	bit
        // PORCANJEAR	numeric
        // NUMANTICIPO	varchar
        // MONANTICIPO	varchar
        // VENTASINTARJETA	varchar  



        // DOCTYPEID	UD_DOCTYPEID
        public string CodigoTipoDocumento { get; private set; }            

        // TYPEPAYMENTID	char
        public string CodigoTipoPago { get; private set; }

        // SITEID	UD_IDSITE
        public string CodigoAlmacen { get; private set; }

        // CURYID	UD_CURYID
        public string CodigoMoneda { get; private set; }

        // DOCSTATUSID	char
        public string CodigoEstadoDocumento { get; private set; }

        // TERMID	char
        public string CodigoCondicionPago { get; private set; }

        // SALESPERID	UD_SALESPERSONID
        public string CodigoVendedor { get; private set; }

        // USERID	UD_USERID
        public string CodigoUsuarioDeSistema { get; private set; }

        // TAXIGVID	char
        public string CodigoImpuestoIgv { get; private set; }

        // TAXISCID	char
        public string CodigoImpuestoIsc { get; private set; }

        // CUSTIDSS	UD_CODCLIENT
        public string CodigoCliente { get; private set; }

        // CURYTYPEID	UD_CURYTYPEID
        public string CodigoClaseTipoCambio { get; private set; }

        // SALESPOINT	UD_PTOVTA
        public string CodigoConfiguracionPuntoVenta { get; private set; }                  

        // IDESTADO	char
        public string CodigoEstado { get; private set; }        

        // MONEDACREDITO	UD_CURYID
        public string CodigoMonedaCredito { get; private set; }

        // TIPODECAMBIO	UD_CURYTYPEID
        public string CodigoClaseTipoCambioClienteCredito { get; private set; }
    
        // PROMOTIONCARDID	char
        public string CodigoTarjetaPromocion { get; private set; }   

        // CARDID	char
        public string CodigoTarjeta { get;  private set; }        

        // CURYIDCARD	UD_CURYID
        public string CodigoMonedaTarjeta { get; private set; }


        
        public virtual ICollection<PedidoEESSDetalle> PedidoEESSDetalles
        {
            get
            {
                if (_lineasPedidoEESSDetalle == null)
                    _lineasPedidoEESSDetalle = new HashSet<PedidoEESSDetalle>();

                return _lineasPedidoEESSDetalle;
            }

            set
            {
                _lineasPedidoEESSDetalle = new HashSet<PedidoEESSDetalle>(value);
            }
        }


        public virtual ICollection<PedidoEESSConVale> PedidoEESSConVales 
        {   get
            {
                if (_lineasPedidoEESSConVale == null)
                    _lineasPedidoEESSConVale = new HashSet<PedidoEESSConVale>();

                return _lineasPedidoEESSConVale;
            }

            set
            {
                _lineasPedidoEESSConVale = new HashSet<PedidoEESSConVale>(value);
            }
        }


        public PedidoEESSDetalle AgregarNuevoPedidoEESSDetalle(short pSecuencia, string pNumeroDocumento, DateTime pFechaDocumento,    
                DateTime pFechaProceso, string pPeriodo, bool pProcesadoCierreZ,        
                bool pProcesadoCierreX, int pNumeroTurno, string pNumeroCara,          
                string pNumeroTransaccionCombustible, decimal pPorcentajeDescuentoPrimero, decimal pPorcentajeDescuentoSegundo,        
                decimal pPorcentajeDescuentoNacional, decimal pPorcentajeDescuentoExtranjera, decimal pPorcentajeImpuestoIgv,        
                decimal pPorcentajeImpuestoIsc, decimal pTotalNacional, decimal pTotalExtranjera,        
                decimal pImpuestoNacional, decimal pImpuestoExtranjera, bool pEsInventariable,        
                bool pEnInventarioFisico, decimal pPrecio, decimal pPrecioVenta,        
                decimal pCostoEstandarNacional, decimal pCostoEstandarExtranjera, string pDescripcionArticulo,        
                decimal pCantidad, int pEsFormula, bool pEsArticuloCombustible,        
                string pNumeroPeaje, string pCodigoArticulo, string pCodigoUnidadDeMedida, 
                string pCodigoArticuloAlterno)
        {
            var nuevaLineaPedidoEESSDetalle = new PedidoEESSDetalle
            {
                Correlativo = this.Correlativo,                    
                Secuencia = pSecuencia,
                NumeroDocumento = pNumeroDocumento,        
                FechaDocumento = pFechaDocumento,    
                FechaProceso = pFechaProceso,        
                Periodo = pPeriodo,         
                ProcesadoCierreZ = pProcesadoCierreZ,        
                ProcesadoCierreX = pProcesadoCierreX,        
                NumeroTurno = pNumeroTurno,        
                NumeroCara = pNumeroCara,          
                NumeroTransaccionCombustible = pNumeroTransaccionCombustible,            
                PorcentajeDescuentoPrimero = pPorcentajeDescuentoPrimero,        
                PorcentajeDescuentoSegundo = pPorcentajeDescuentoSegundo,        
                PorcentajeDescuentoNacional = pPorcentajeDescuentoNacional,        
                PorcentajeDescuentoExtranjera = pPorcentajeDescuentoExtranjera,        
                PorcentajeImpuestoIgv = pPorcentajeImpuestoIgv,        
                PorcentajeImpuestoIsc = pPorcentajeImpuestoIsc,        
                TotalNacional = pTotalNacional,        
                TotalExtranjera = pTotalExtranjera,        
                ImpuestoNacional = pImpuestoNacional,        
                ImpuestoExtranjera = pImpuestoExtranjera,        
                EsInventariable = pEsInventariable,        
                EnInventarioFisico = pEnInventarioFisico,        
                Precio = pPrecio,        
                PrecioVenta = pPrecioVenta,        
                CostoEstandarNacional = pCostoEstandarNacional,            
                CostoEstandarExtranjera = pCostoEstandarExtranjera,        
                DescripcionArticulo = pDescripcionArticulo,        
                Cantidad = pCantidad,        
                EsFormula = pEsFormula,        
                EsArticuloCombustible = pEsArticuloCombustible,        
                NumeroPeaje = pNumeroPeaje,
                CodigoArticulo = pCodigoArticulo,                
                CodigoUnidadDeMedida = pCodigoUnidadDeMedida,
                CodigoArticuloAlterno = pCodigoArticuloAlterno,  
                CodigoTipoDocumento = this.CodigoTipoDocumento,
                CodigoAlmacen = this.CodigoAlmacen,
                CodigoMoneda = this.CodigoMoneda,
                CodigoEstadoDocumento = this.CodigoEstadoDocumento,    
                CodigoConfiguracionPuntoVenta = this.CodigoConfiguracionPuntoVenta,
                CodigoUsuarioDeSistema = this.CodigoUsuarioDeSistema                                          
            };

            this.PedidoEESSDetalles.Add(nuevaLineaPedidoEESSDetalle);
 
            return nuevaLineaPedidoEESSDetalle;
        }                     


        public PedidoEESSConVale AgregarNuevoPedidoEESSConVale(decimal pNumeroVale)
        {
            var nuevaLineaPedidoEESSConVale = new PedidoEESSConVale
            {
                Correlativo = this.Correlativo,
                NumeroVale = pNumeroVale,

                CodigoAlmacen = this.CodigoAlmacen,
                CodigoCliente = this.CodigoCliente
            };

            this.PedidoEESSConVales.Add(nuevaLineaPedidoEESSConVale);

            return nuevaLineaPedidoEESSConVale;
        }


        //TipoDocumento
       public void EstablecerReferenciaTipoDocumentoDeVenta(string pCodigoTipoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumento))
            {
                this.CodigoTipoDocumento = pCodigoTipoDocumento;
                // this.TipoDocumento = null;
            }
        }

        //TipoPago
        public void EstablecerReferenciaTipoPagoDeVenta(string pCodigoTipoPago)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoPago))
            {
                this.CodigoTipoPago = pCodigoTipoPago;
                // this.TipoPago = null;
            }
        } 

        //Almacen
        public void EstablecerReferenciaAlmacenDeVenta(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {

                this.CodigoAlmacen = pCodigoAlmacen;
                // this.Almacen = null;
            }
        }               

        //Moneda
        public void EstablecerReferenciaMonedaDeVenta(string pCodigoMoneda)
        {
            if (!string.IsNullOrEmpty(pCodigoMoneda))
            {
                this.CodigoMoneda = pCodigoMoneda;
                // this.Moneda = null;
            }
        }  

       //EstadoDocumento
       public void EstablecerReferenciaEstadoDocumentoDeVenta(string pCodigoEstadoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoDocumento))
            {
                this.CodigoEstadoDocumento = pCodigoEstadoDocumento;
                // this.EstadoDocumento = null;
            }
        }             

        //CondicionPago
        public void EstablecerReferenciaCondicionPagoDeVenta(string pCodigoCondicionPago)
        {
            if (!string.IsNullOrEmpty(pCodigoCondicionPago))
            {
                this.CodigoCondicionPago = pCodigoCondicionPago;
                // this.CondicionPago = null;
            }
        }

       //Vendedor
        public void EstablecerReferenciaVendedorDeVenta(string pCodigoVendedor)
        {
            if (!string.IsNullOrEmpty(pCodigoVendedor))
            {
                this.CodigoVendedor = pCodigoVendedor;
                // this.Vendedor = null;
            }
        }

       //UsuarioSistema
        public void EstablecerReferenciaUsuarioSistemaDeVenta(string pCodigoUsuarioDeSistema)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioDeSistema))
            {
                this.CodigoUsuarioDeSistema = pCodigoUsuarioDeSistema;
                // this.UsuarioSistema = null;
            }
        }  

      //ImpuestoIgv
        public void EstablecerReferenciaImpuestoIgvDeCliente(string pCodigoImpuestoIgv)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIgv))
            {
                this.CodigoImpuestoIgv = pCodigoImpuestoIgv;
                // this.ImpuestoIgv = null;
            }
        }


        //ImpuestoIsc
        public void EstablecerReferenciaImpuestoIscDeCliente(string pCodigoImpuestoIsc)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIsc))
            {

                this.CodigoImpuestoIsc = pCodigoImpuestoIsc;
                // this.ImpuestoIsc = null;
            }
        }

        //Cliente
        public void EstablecerReferenciaClienteDeVenta(string pCodigoCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoCliente))
            {
                this.CodigoCliente = pCodigoCliente;
                // this.Cliente = null;
            }
        }

        //ClaseTipoCambio
        public void EstablecerReferenciaClaseTipoCambioDeVenta(string pCodigoClaseTipoCambio)
        {
            if (!string.IsNullOrEmpty(pCodigoClaseTipoCambio))
            {
                this.CodigoClaseTipoCambio = pCodigoClaseTipoCambio;
                // this.ClaseTipoCambio = null;
            }
        }
                                        
        //ConfiguracionPuntoVenta
        public void EstablecerReferenciaConfiguracionPuntoVentaDeVenta(string pCodigoConfiguracionPuntoVenta)
        {
            if (!string.IsNullOrEmpty(pCodigoConfiguracionPuntoVenta))
            {

                this.CodigoConfiguracionPuntoVenta = pCodigoConfiguracionPuntoVenta;
                // this.ConfiguracionPuntoVenta = null;
            }
        }  

        //Estado
        public void EstablecerReferenciaEstadoDeVenta(string pCodigoEstado)
        {
            if (!string.IsNullOrEmpty(pCodigoEstado))
            {
                this.CodigoEstado = pCodigoEstado;
                // this.TipoNegocio = null;
            }
        }         

        //Moneda Credito
        public void EstablecerReferenciaMonedaCreditoDeVenta(string pCodigoMonedaCredito)
        {
            if (!string.IsNullOrEmpty(pCodigoMonedaCredito))
            {
                this.CodigoMonedaCredito = pCodigoMonedaCredito;
                // this.TipoNegocio = null;
            }
        } 

        //Clase Tipo Cambio Cliente Credito
        public void EstablecerReferenciaClaseTipoCambioClienteCreditoDeVenta(string pCodigoClaseTipoCambioClienteCredito)
        {
            if (!string.IsNullOrEmpty(pCodigoClaseTipoCambioClienteCredito))
            {
                this.CodigoClaseTipoCambioClienteCredito = pCodigoClaseTipoCambioClienteCredito;
                // this.TipoNegocio = null;
            }
        }         

        //Tarjeta Promocion
        public void EstablecerReferenciaTarjetaPromocionDeVenta(string pCodigoTarjetaPromocion)
        {
            if (!string.IsNullOrEmpty(pCodigoTarjetaPromocion))
            {
                this.CodigoTarjetaPromocion = pCodigoTarjetaPromocion;
                // this.TipoNegocio = null;
            }
        }         
          
        //Tarjeta Credito
        public void EstablecerReferenciaTarjetaDeVenta(string pCodigoTarjeta)
        {
            if (!string.IsNullOrEmpty(pCodigoTarjeta))
            {
                this.CodigoTarjeta = pCodigoTarjeta;
                // this.TipoNegocio = null;
            }
        } 


        //Moneda Tarjeta Credito
        public void EstablecerReferenciaMonedaTarjetaDeVenta(string pCodigoMonedaTarjeta)
        {
            if (!string.IsNullOrEmpty(pCodigoMonedaTarjeta))
            {
                this.CodigoMonedaTarjeta = pCodigoMonedaTarjeta;
                // this.TipoNegocio = null;
            }
        }         

    }
}