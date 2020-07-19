using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class PedidoRetail:Entidad
    {
        HashSet<PedidoRetailDetalle> _lineasPedidoRetailDetalle;
        HashSet<PedidoRetailConTarjeta> _lineasPedidoRetailConTarjeta;        
        HashSet<PedidoRetailConVale> _lineasPedidoRetailConVale;
                
                
        // CORRNBR	int
        public int Correlativo { get; set; }

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

        // taxregnbr	UD_CODCLIENT
        public string RucCliente { get; set; }

        // CUSTNAME	varchar
        public string NombreCompletoCliente { get; set; }

        // ADDR1	varchar
        public string DireccionCliente { get; set; }

        // PLACA	UD_PLACA
        public string Placa { get; set; }

        // NBRBONUS	numeric
        public decimal NumeroVale { get; set; }

        // CURYRATE	UD_CAMBIO
        public decimal TipoCambio { get; set; }

        // POINTNUMBER	smallint
        public int NumeroPuntos { get; set; }

        // KILOMETRAJE	int
        public int Kilometraje { get; set; }

        // STKSAVE	bit
        public bool TransaccionPendiente { get; set; }

        // SALESTYPE	char
        public string  TipoVenta { get; set; }

        // PROCESSED	bit
        public bool TransaccionProcesada { get; set; }
        
        // DSC_CUPON	bit
        public bool AplicaDescuentoCupon { get; set; }

        // CENTRO_COSTO	varchar   
        public string CentroDeCosto { get; set; }



        // DOCTYPEID	UD_DOCTYPEID         
        public string CodigoTipoDocumento { get; private set; } 

        // TYPEPAYMENTID	char      
        public string CodigoTipoPago { get; private set; }

        // SITEID	UD_IDSITE
        public string CodigoAlmacen { get; private set; }

        // CURYID	UD_CURYID
        public string CodigoMoneda { get; private set; }

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

        // custidss	UD_CODCLIENT        
        public string CodigoCliente { get; private set; }

        // CURYTYPEID	UD_CURYTYPEID   
        public string CodigoClaseTipoCambio { get; private set; }

        // PROMOTIONCARDID	char 
        public string CodigoTarjetaPromocion { get; private set; } 

        // SALESPOINT	UD_PTOVTA    
        public string CodigoPuntoDeVenta { get; private set; }           

        // businesstype	char
        public string CodigoTipoNegocio { get; private set; } 



        public virtual ICollection<PedidoRetailDetalle> PedidoRetailDetalles
        {
            get
            {
                if (_lineasPedidoRetailDetalle == null)
                    _lineasPedidoRetailDetalle = new HashSet<PedidoRetailDetalle>();

                return _lineasPedidoRetailDetalle;
            }

            set
            {
                _lineasPedidoRetailDetalle = new HashSet<PedidoRetailDetalle>(value);
            }
        }


        public virtual ICollection<PedidoRetailConVale> PedidoRetailConVales 
        {   get
            {
                if (_lineasPedidoRetailConVale == null)
                    _lineasPedidoRetailConVale = new HashSet<PedidoRetailConVale>();

                return _lineasPedidoRetailConVale;
            }

            set
            {
                _lineasPedidoRetailConVale = new HashSet<PedidoRetailConVale>(value);
            }
        }

        public virtual ICollection<PedidoRetailConTarjeta> PedidoRetailConTarjetas 
        {   get
            {
                if (_lineasPedidoRetailConTarjeta == null)
                    _lineasPedidoRetailConTarjeta = new HashSet<PedidoRetailConTarjeta>();

                return _lineasPedidoRetailConTarjeta;
            }

            set
            {
                _lineasPedidoRetailConTarjeta = new HashSet<PedidoRetailConTarjeta>(value);
            }
        }


        public PedidoRetailDetalle AgregarNuevoPedidoRetailDetalle(
                    string pNumeroDocumento, short pSecuencia, DateTime pFechaDocumento,
                    DateTime pFechaProceso, string pPeriodo, int pNumeroTurno,
                    decimal pPorcentajeImpuestoIgv, decimal pPorcentajeImpuestoIsc, decimal pTotalNacional,
                    decimal pTotalExtranjera, decimal pImpuestoNacional, decimal pImpuestoExtranjera,
                    bool pEsInventariable, bool pEnInventarioFisico, decimal pPrecio,
                    decimal pPrecioVenta, decimal pCostoEstandarNacional, decimal pCostoEstandarExtranjera,
                    string pCodigoArticuloAlterno, string pDescripcionArticulo, decimal pCantidad,
                    int  pEsFormula, string pNumeroPeaje, string pCodigoArticulo,
                    string pCodigoUnidadDeMedida)
        {
            var nuevoPedidoRetailDetalle = new PedidoRetailDetalle
            {
                Correlativo = this.Correlativo,
                NumeroDocumento = pNumeroDocumento,
                Secuencia = pSecuencia,
                FechaDocumento = pFechaDocumento,
                FechaProceso = pFechaProceso,
                Periodo = pPeriodo,
                NumeroTurno = pNumeroTurno,
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
                CodigoArticuloAlterno = pCodigoArticuloAlterno,
                DescripcionArticulo = pDescripcionArticulo,
                Cantidad = pCantidad,
                EsFormula = pEsFormula,
                NumeroPeaje = pNumeroPeaje,

                CodigoTipoDocumento = this.CodigoTipoDocumento,
                CodigoAlmacen = this.CodigoAlmacen,
                CodigoMoneda = this.CodigoMoneda,
                CodigoArticulo = pCodigoArticulo,
                CodigoUnidadDeMedida = pCodigoUnidadDeMedida
            };

            this.PedidoRetailDetalles.Add(nuevoPedidoRetailDetalle);

            return nuevoPedidoRetailDetalle;
        }


        public PedidoRetailConVale AgregarNuevoPedidoRetailConVale(decimal pNumeroVale )
        {
            var nuevoPedidoRetailConVale = new PedidoRetailConVale
            {
                Correlativo = this.Correlativo,
                NumeroVale = pNumeroVale,

                CodigoCliente = this.CodigoCliente,
                CodigoAlmacen = this.CodigoAlmacen                    
            };

            this.PedidoRetailConVales.Add(nuevoPedidoRetailConVale);

            return nuevoPedidoRetailConVale;
        }


        public PedidoRetailConTarjeta AgregarNuevoPedidoRetailConTarjeta(short pSecuencia, string pNumeroTarjeta, decimal pTotalTarjetaNacional,
                    decimal pTotalTarjetaExtranjera, int pEsTransaccionPinPad, string pTipoTarjeta,
                    string pDNIAsociadoATarjeta, string pDescripcionTarjeta,  string  pCodigoTarjeta)
        {
            var nuevoPedidoRetailConTarjeta = new PedidoRetailConTarjeta
            {
                Correlativo = this.Correlativo,
                Secuencia = pSecuencia,
                NumeroTarjeta = pNumeroTarjeta,
                TotalTarjetaNacional = pTotalTarjetaNacional,
                TotalTarjetaExtranjera = pTotalTarjetaExtranjera,
                EsTransaccionPinPad = pEsTransaccionPinPad,
                TipoTarjeta = pTipoTarjeta,
                DNIAsociadoATarjeta = pDNIAsociadoATarjeta,
                DescripcionTarjeta = pDescripcionTarjeta,

                CodigoAlmacen = this.CodigoAlmacen,
                CodigoMoneda = this.CodigoMoneda,
                CodigoTarjeta = pCodigoTarjeta
            };

            this.PedidoRetailConTarjetas.Add(nuevoPedidoRetailConTarjeta);

            return nuevoPedidoRetailConTarjeta;
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

        //Tarjeta Promocion
        public void EstablecerReferenciaTarjetaPromocionDeVenta(string pCodigoTarjetaPromocion)
        {
            if (!string.IsNullOrEmpty(pCodigoTarjetaPromocion))
            {
                this.CodigoTarjetaPromocion = pCodigoTarjetaPromocion;
                // this.TipoNegocio = null;
            }
        }  


        //Configuracion PuntoVenta
        public void EstablecerReferenciaConfiguracionPuntoVentaDeVenta(string pCodigoPuntoDeVenta)
        {
            if (!string.IsNullOrEmpty(pCodigoPuntoDeVenta))
            {

                this.CodigoPuntoDeVenta = pCodigoPuntoDeVenta;
                // this.ConfiguracionPuntoVenta = null;
            }
        }

        //Tipo Negocio
        public void EstablecerReferenciaTipoNegocioDeVenta(string pCodigoTipoNegocio)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoNegocio))
            {

                this.CodigoTipoNegocio = pCodigoTipoNegocio;
                // this.ConfiguracionPuntoVenta = null;
            }
        }                   
    }
}