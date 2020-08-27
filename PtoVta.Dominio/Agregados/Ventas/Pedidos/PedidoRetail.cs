using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

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

        public PedidoRetail(){}


        public PedidoRetail(int pCorrelativo, string pNumeroDocumento, bool pAfectaInventario,    
            DateTime pFechaDocumento, DateTime pFechaProceso, string pPeriodo,
            decimal pTotalNacional, decimal pTotalExtranjera, decimal pSubTotalNacional,
            decimal pSubTotalExtranjera, decimal pImpuestoIgvNacional, decimal pImpuestoIgvExtranjera,
            decimal pImpuestoIscNacional, decimal pImpuestoIscExtranjera, decimal pTotalNoAfectoNacional,
            decimal pTotalNoAfectoExtranjera, decimal pPorcentajeDescuentoPrimero, decimal pPorcentajeDescuentoSegundo,
            decimal pTotalDescuentoNacional, decimal pTotalDescuentoExtranjera, decimal pTotalVueltoNacional,
            decimal pTotalVueltoExtranjera, decimal pTotalEfectivoNacional, decimal pTotalEfectivoExtranjera,
            string pRucCliente, string pNombreCompletoCliente, string pDireccionCliente,
            string pPlaca, decimal pNumeroVale, decimal pTipoCambio,
            int pNumeroPuntos, int pKilometraje, bool pTransaccionPendiente,
            string  pTipoVenta, bool pTransaccionProcesada, bool pAplicaDescuentoCupon,
            string pCentroDeCosto)
        {
            this.Correlativo = pCorrelativo;
            this.NumeroDocumento = pNumeroDocumento;
            this.AfectaInventario = pAfectaInventario;    
            this.FechaDocumento = pFechaDocumento;
            this.FechaProceso = pFechaProceso;
            this.Periodo =  !string.IsNullOrEmpty(pPeriodo) ? pPeriodo.Trim()
                                        : throw new ArgumentException(Mensajes.advertencia_DebeAsignarseUnPeriodoALaTransaccion); 
            this.TotalNacional = pTotalNacional > 0? pTotalNacional
                                        : throw new ArgumentException(Mensajes.advertencia_TotalTransaccionInvalida); 
            this.TotalExtranjera = pTotalExtranjera;
            this.SubTotalNacional = pSubTotalNacional > 0 ? pSubTotalNacional
                                        : throw new ArgumentException(Mensajes.advertencia_SubTotalTransaccionInvalida); 
            this.SubTotalExtranjera = pSubTotalExtranjera;
            this.ImpuestoIgvNacional = pImpuestoIgvNacional > 0 ? pImpuestoIgvNacional
                                        : throw new ArgumentException(Mensajes.advertencia_ImpuestoTransaccionInvalida);             
            this.ImpuestoIgvExtranjera = pImpuestoIgvExtranjera;
            this.ImpuestoIscNacional = pImpuestoIscNacional;
            this.ImpuestoIscExtranjera = pImpuestoIscExtranjera;
            this.TotalNoAfectoNacional = pTotalNoAfectoNacional;
            this.TotalNoAfectoExtranjera = pTotalNoAfectoExtranjera;
            this.PorcentajeDescuentoPrimero = pPorcentajeDescuentoPrimero;
            this.PorcentajeDescuentoSegundo = pPorcentajeDescuentoSegundo;
            this.TotalDescuentoNacional = pTotalDescuentoNacional;
            this.TotalDescuentoExtranjera = pTotalDescuentoExtranjera;
            this.TotalVueltoNacional = pTotalVueltoNacional;
            this.TotalVueltoExtranjera = pTotalVueltoExtranjera;
            this.TotalEfectivoNacional = pTotalEfectivoNacional;
            this.TotalEfectivoExtranjera = pTotalEfectivoExtranjera;
            this.RucCliente = pRucCliente;
            this.NombreCompletoCliente = pNombreCompletoCliente;
            this.DireccionCliente = pDireccionCliente;
            this.Placa = pPlaca;
            this.NumeroVale = pNumeroVale;
            this.TipoCambio = pTipoCambio;
            this.NumeroPuntos = pNumeroPuntos;
            this.Kilometraje = pKilometraje;
            this.TransaccionPendiente = pTransaccionPendiente;
            this.TipoVenta = pTipoVenta;
            this.TransaccionProcesada = pTransaccionProcesada;
            this.AplicaDescuentoCupon = pAplicaDescuentoCupon;
            this.CentroDeCosto = pCentroDeCosto;             
        }


        public PedidoRetailDetalle AgregarNuevoPedidoRetailDetalle(short pSecuencia, int pNumeroTurno,decimal pPorcentajeImpuestoIgv, 
                   decimal pPorcentajeImpuestoIsc, decimal pTotalNacional,decimal pTotalExtranjera, 
                   decimal pImpuestoNacional, decimal pImpuestoExtranjera,bool pEsInventariable, 
                   bool pEnInventarioFisico, decimal pPrecio,decimal pPrecioVenta, 
                   decimal pCostoEstandarNacional, decimal pCostoEstandarExtranjera,string pCodigoArticuloAlterno, 
                   string pDescripcionArticulo, decimal pCantidad,int  pEsFormula, 
                   string pNumeroPeaje, string pCodigoArticulo, string pCodigoUnidadDeMedida)
        {
            if(pSecuencia <= 0)
                throw new ArgumentException(Mensajes.advertencia_SecuenciaDeLineaPedidoRetailDetalleInvalido);

            if(pNumeroTurno <= 0)             
                throw new ArgumentException(Mensajes.advertencia_NumeroTurnoDeLineaPedidoRetailDetallenvalido);   

            if(pTotalNacional <= 0)
                throw new ArgumentException(Mensajes.advertencia_TotalNacionalDeLineaPedidoRetailDetalleInvalido);   

            if(pPrecioVenta <= 0)
                throw new ArgumentException(Mensajes.advertencia_PrecioVentaDeLineaPedidoRetailDetalleInvalido);   

            if(pCantidad <= 0)
                throw new ArgumentException(Mensajes.advertencia_CantidadDeLineaPedidoRetailDetalleInvalido);   

            if(string.IsNullOrEmpty(pCodigoUnidadDeMedida.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoUnidadDeMedidaDeLineaPedidoRetailDetalleInvalido);   


            var nuevoPedidoRetailDetalle = new PedidoRetailDetalle
            {
                Correlativo = this.Correlativo,
                NumeroDocumento = this.NumeroDocumento,
                Secuencia = pSecuencia,
                FechaDocumento = this.FechaDocumento,
                FechaProceso = this.FechaProceso,
                Periodo = this.Periodo,
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
                CodigoUnidadDeMedida = pCodigoUnidadDeMedida.Trim()
            };

            this.PedidoRetailDetalles.Add(nuevoPedidoRetailDetalle);

            return nuevoPedidoRetailDetalle;
        }


        public PedidoRetailConVale AgregarNuevoPedidoRetailConVale(decimal pNumeroVale )
        {
            if(string.IsNullOrEmpty(this.CodigoCliente.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoClienteDeLineaPedidoRetailConValeInvalido);

            if(pNumeroVale <= 0)
                throw new ArgumentException(Mensajes.advertencia_NumeroValeDeLineaPedidoRetailConValeInvalido);

            var nuevoPedidoRetailConVale = new PedidoRetailConVale
            {
                Correlativo = this.Correlativo,
                NumeroVale = pNumeroVale,

                CodigoCliente = this.CodigoCliente.Trim(),
                CodigoAlmacen = this.CodigoAlmacen                    
            };

            this.PedidoRetailConVales.Add(nuevoPedidoRetailConVale);

            return nuevoPedidoRetailConVale;
        }


        public PedidoRetailConTarjeta AgregarNuevoPedidoRetailConTarjeta(short pSecuencia, string pNumeroTarjeta, decimal pTotalTarjetaNacional,
                    decimal pTotalTarjetaExtranjera, int pEsTransaccionPinPad, string pTipoTarjeta,
                    string pDNIAsociadoATarjeta, string pDescripcionTarjeta,  string  pCodigoTarjeta)
        {
            if(pSecuencia <= 0)
                throw new ArgumentException(Mensajes.advertencia_SecuenciaDeLineaPedidoRetailConTarjetaInvalido);

            if(string.IsNullOrEmpty(pNumeroTarjeta.Trim()))
                throw new ArgumentException(Mensajes.advertencia_NumeroTarjetaDeLineaPedidoRetailConTarjetaInvalido);   

            if(pTotalTarjetaNacional <= 0)
                throw new ArgumentException(Mensajes.advertencia_TotalTarjetaNacionalDeLineaPedidoRetailConTarjetaInvalido);                               

            if(string.IsNullOrEmpty(pCodigoTarjeta.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoTarjetaDeLineaPedidoRetailConTarjetaInvalido);   


            var nuevoPedidoRetailConTarjeta = new PedidoRetailConTarjeta
            {
                Correlativo = this.Correlativo,
                Secuencia = pSecuencia,
                NumeroTarjeta = pNumeroTarjeta.Trim(),
                TotalTarjetaNacional = pTotalTarjetaNacional,
                TotalTarjetaExtranjera = pTotalTarjetaExtranjera,
                EsTransaccionPinPad = pEsTransaccionPinPad,
                TipoTarjeta = pTipoTarjeta,
                DNIAsociadoATarjeta = pDNIAsociadoATarjeta,
                DescripcionTarjeta = pDescripcionTarjeta,

                CodigoAlmacen = this.CodigoAlmacen,
                CodigoMoneda = this.CodigoMoneda,
                CodigoTarjeta = pCodigoTarjeta.Trim()
            };

            this.PedidoRetailConTarjetas.Add(nuevoPedidoRetailConTarjeta);

            return nuevoPedidoRetailConTarjeta;
        }


        //TipoDocumento
       public void EstablecerReferenciaTipoDocumentoDeVenta(string pCodigoTipoDocumento)
        {
            if (string.IsNullOrEmpty(pCodigoTipoDocumento.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoTipoDocumentoDePedidoRetailInvalido);

            this.CodigoTipoDocumento = pCodigoTipoDocumento.Trim();
            // this.TipoDocumento = null;
        }

        //TipoPago
        public void EstablecerReferenciaTipoPagoDeVenta(string pCodigoTipoPago)
        {
            if (string.IsNullOrEmpty(pCodigoTipoPago.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoTipoPagoDePedidoRetailInvalido);

            this.CodigoTipoPago = pCodigoTipoPago.Trim();
            // this.TipoPago = null;            
        } 

        //Almacen
        public void EstablecerReferenciaAlmacenDeVenta(string pCodigoAlmacen)
        {
            if (string.IsNullOrEmpty(pCodigoAlmacen.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoAlmacenDePedidoRetailInvalido);            

            this.CodigoAlmacen = pCodigoAlmacen.Trim();
            // this.Almacen = null;            
        }   

        //Moneda
        public void EstablecerReferenciaMonedaDeVenta(string pCodigoMoneda)
        {
            if (string.IsNullOrEmpty(pCodigoMoneda.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoMonedaDePedidoRetailInvalido);

            this.CodigoMoneda = pCodigoMoneda.Trim();
            // this.Moneda = null;            
        } 

       //CondicionPago
        public void EstablecerReferenciaCondicionPagoDeVenta(string pCodigoCondicionPago)
        {
            if (string.IsNullOrEmpty(pCodigoCondicionPago.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoCondicionPagoDePedidoRetailInvalido);

            this.CodigoCondicionPago = pCodigoCondicionPago.Trim();
            // this.CondicionPago = null;            
        }

       //Vendedor
        public void EstablecerReferenciaVendedorDeVenta(string pCodigoVendedor)
        {
            if (string.IsNullOrEmpty(pCodigoVendedor.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoVendedorDePedidoRetailInvalido);

            this.CodigoVendedor = pCodigoVendedor.Trim();
            // this.Vendedor = null;            
        }

       //UsuarioSistema
        public void EstablecerReferenciaUsuarioSistemaDeVenta(string pCodigoUsuarioDeSistema)
        {
            if (string.IsNullOrEmpty(pCodigoUsuarioDeSistema.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoUsuarioDeSistemaDePedidoRetailInvalido);            

            this.CodigoUsuarioDeSistema = pCodigoUsuarioDeSistema.Trim();
            // this.UsuarioSistema = null;            
        }  

      //ImpuestoIgv
        public void EstablecerReferenciaImpuestoIgvDeCliente(string pCodigoImpuestoIgv)
        {
            if (string.IsNullOrEmpty(pCodigoImpuestoIgv))
                throw new ArgumentException(Mensajes.advertencia_CodigoImpuestoIgvDePedidoRetailInvalido);

            this.CodigoImpuestoIgv = pCodigoImpuestoIgv.Trim();
            // this.ImpuestoIgv = null;            
        }

      //ImpuestoIsc
        public void EstablecerReferenciaImpuestoIscDeCliente(string pCodigoImpuestoIsc)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIsc))
            {
                this.CodigoImpuestoIsc = pCodigoImpuestoIsc.Trim();
                // this.ImpuestoIsc = null;            
            }
        }

        //Cliente
        public void EstablecerReferenciaClienteDeVenta(string pCodigoCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoCliente))
            {
                this.CodigoCliente = pCodigoCliente.Trim();
                // this.Cliente = null;     
            }       
        }

        //ClaseTipoCambio
        public void EstablecerReferenciaClaseTipoCambioDeVenta(string pCodigoClaseTipoCambio)
        {
            if (string.IsNullOrEmpty(pCodigoClaseTipoCambio))
                throw new ArgumentException(Mensajes.advertencia_CodigoClaseTipoCambioDePedidoRetailInvalido);

            this.CodigoClaseTipoCambio = pCodigoClaseTipoCambio.Trim();
            // this.ClaseTipoCambio = null;            
        }

        //Tarjeta Promocion
        public void EstablecerReferenciaTarjetaPromocionDeVenta(string pCodigoTarjetaPromocion)
        {
            if (!string.IsNullOrEmpty(pCodigoTarjetaPromocion))
            {
                this.CodigoTarjetaPromocion = pCodigoTarjetaPromocion.Trim();
                // this.TipoNegocio = null;
            }            
        }  


        //Configuracion PuntoVenta
        public void EstablecerReferenciaConfiguracionPuntoVentaDeVenta(string pCodigoPuntoDeVenta)
        {
            if (string.IsNullOrEmpty(pCodigoPuntoDeVenta))
                throw new ArgumentException(Mensajes.advertencia_CodigoPuntoDeVentaDePedidoRetailInvalido);            

            this.CodigoPuntoDeVenta = pCodigoPuntoDeVenta.Trim();
            // this.ConfiguracionPuntoVenta = null;
            
        }

        //Tipo Negocio
        public void EstablecerReferenciaTipoNegocioDeVenta(string pCodigoTipoNegocio)
        {
            if (string.IsNullOrEmpty(pCodigoTipoNegocio.Trim()))
                throw new ArgumentException(Mensajes.advertencia_CodigoTipoNegocioDePedidoRetailInvalido);                        

            this.CodigoTipoNegocio = pCodigoTipoNegocio.Trim();
            // this.ConfiguracionPuntoVenta = null;            
        }                   
    }
}