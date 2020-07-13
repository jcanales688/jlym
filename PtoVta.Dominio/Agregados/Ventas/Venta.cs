using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.CuentasPorCobrar;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.EstadosVenta;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class Venta: Entidad
    {
        bool _EsHabilitado;

        HashSet<VentaDetalle> _lineasVentaDetalle;
        HashSet<VentaConTarjeta> _lineasVentaConTarjeta;
        HashSet<VentaConVale> _lineasVentaConVale;

        HashSet<DocumentoAnticipado> _lineasDocumentoAnticipado;
        HashSet<CuentaPorCobrar> _lineasCuentaPorCobrar;


        public string NumeroDocumento { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public string Periodo { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjera { get; set; }
        public decimal SubTotalNacional { get; set; }
        public decimal SubTotalExtranjera { get; set; }
        public decimal ImpuestoIgvNacional { get; set; }
        public decimal ImpuestoIgvExtranjera { get; set; }
        public decimal ImpuestoIscNacional { get; set; }
        public decimal ImpuestoIscExtranjera { get; set; }
        public decimal TotalNoAfectoNacional { get; set; }
        public decimal TotalNoAfectoExtranjera { get; set; }
        public Nullable<decimal> TotalAfectoNacional { get; set; }
        public Nullable<decimal> ValorVenta { get; set; }
        public decimal PorcentajeDescuentoPrimero { get; set; }
        public decimal PorcentajeDescuentoSegundo { get; set; }
        public decimal TotalDescuentoNacional { get; set; }
        public decimal TotalDescuentoExtranjera { get; set; }
        public decimal TotalVueltoNacional { get; set; }
        public decimal TotalVueltoExtranjera { get; set; }
        public decimal TotalEfectivoNacional { get; set; }
        public decimal TotalEfectivoExtranjera { get; set; }
        public string RucCliente { get; set; }
        public string NombreCompletoCliente { get; set; }
        public string Placa { get; set; }
        public Nullable<decimal> NumeroVale { get; set; }
        public decimal TipoCambio { get; set; }
        public bool ProcesadoCierreZ { get; set; }
        public bool ProcesadoCierreX { get; set; }
        public Nullable<int> Kilometraje { get; set; }
        public bool AfectaInventario { get; set; }

        public bool EsHabilitado
        {
            get
            {
                return _EsHabilitado;
            }
            private set
            {
                _EsHabilitado = value;
            }
        }



        public string CodigoMoneda { get; private set; }
        public string CodigoClaseTipoCambio { get; private set; }
        public string CodigoCliente { get; private set; }
        public string CodigoTipoDocumento { get; private set; }
        public string CodigoEstadoDocumento { get; private set; }
        public string CodigoVendedor { get; private set; }
        public string CodigoCondicionPago { get; private set; }
        public string CodigoTipoPago { get; private set; }
        public string CodigoConfiguracionPuntoVenta { get; private set; }
        public string CodigoAlmacen { get; private set; }
        public string CodigoTipoNegocio { get; private set; }
        public string CodigoUsuarioDeSistema { get; private set; }
        public string CodigoImpuestoIgv { get; private set; }
        public string CodigoImpuestoIsc { get; private set; }


        ////////public virtual CuentaPorCobrar CuentaPorCobrar { get; private set; }  ///Ahora se encuentra en Clientes
        public virtual Moneda Moneda  { get; private set; } //
        public virtual ClaseTipoCambio ClaseTipoCambio { get; private set; } //
        public virtual Cliente Cliente { get; private set; } //
        public virtual TipoDocumento TipoDocumento { get; private set; } //
        public virtual EstadoDocumento EstadoDocumento { get; private set; } //
        public virtual Vendedor Vendedor { get; private set; } //
        public virtual CondicionPago CondicionPago { get; private set; } //
        public virtual TipoPago TipoPago { get; private set; } //
        public virtual ConfiguracionPuntoVenta ConfiguracionPuntoVenta { get; private set; } //
        public virtual Almacen Almacen { get; private set; }
        public virtual TipoNegocio TipoNegocio { get; private set; }
        public virtual UsuarioSistema UsuarioSistema { get; private set; }
        public virtual Impuesto ImpuestoIgv { get; private set; }
        public virtual Impuesto ImpuestoIsc { get; private set; }


        public void CalcularTotalVenta()
        {
            //int cantItem = 0;
            decimal sumTotalNacional = 0;
            decimal sumTotalExtranjera = 0;
            decimal sumSubTotalNacional = 0;
            decimal sumSubTotalExtranjera = 0;
            decimal sumTotalImpuestoNacional = 0;
            decimal sumTotalImpuestoExtranjera = 0;

            //Por cada fila de detalle
            foreach (var linea in this.VentaDetalles)
            {
                var cantidad = linea.Cantidad;
                var precio = linea.PrecioVenta;
                var tipoDeCambio = this.TipoCambio; //Se asume que se determino el tipo de cambio de conversion en la capa de aplicacion o presentacion  

                //Total
                var totalNacional = cantidad * precio;
                var totalExtranjera = Math.Round(totalNacional / tipoDeCambio, 4); //4  : es valor por defecto .- determiar el valor de redondeo real

                //SubTotal
                var subTotalNacional = totalNacional - ((totalNacional * this.ImpuestoIgv.Valor) / 100);
                var subTotalExtranjera = totalExtranjera - ((totalExtranjera * this.ImpuestoIgv.Valor) / 100);

                //Total impuesto
                var totalImpuestoNacional = (totalNacional * this.ImpuestoIgv.Valor) / 100;
                var totalImpuestoExtranjera = (totalExtranjera * this.ImpuestoIgv.Valor) / 100;

                //Asignacion valores Detalle
                linea.PrecioVenta = precio;
                linea.PorcentajeImpuestoIgv = this.ImpuestoIgv.Valor;
                linea.TotalNacional = totalNacional;
                linea.TotalExtranjera = totalExtranjera;
                linea.ImpuestoNacional = totalImpuestoNacional;
                linea.ImpuestoExtranjera = totalImpuestoExtranjera;

                //Calculo Totales Cabecera
                sumTotalNacional = sumTotalNacional + totalNacional;
                sumTotalExtranjera = sumTotalExtranjera + totalExtranjera;
                sumSubTotalNacional += subTotalNacional;
                sumSubTotalExtranjera += subTotalExtranjera;
                sumTotalImpuestoNacional = sumTotalImpuestoNacional + totalImpuestoNacional;
                sumTotalImpuestoExtranjera = sumTotalImpuestoExtranjera + totalImpuestoExtranjera;

                //Num fila
                //cantItem++;
            }

            //Asignacion valores Cabecera
            //ventaDTO.TipoCambio = pTipodeDeCambio;
            this.TotalNacional = sumTotalNacional;
            this.TotalExtranjera = sumTotalExtranjera;
            this.SubTotalNacional = sumSubTotalNacional;
            this.SubTotalExtranjera = sumSubTotalExtranjera;
            this.ImpuestoIgvNacional = sumTotalImpuestoNacional;
            this.ImpuestoIgvExtranjera = sumTotalImpuestoExtranjera;

            this.ValorVenta = this.SubTotalNacional; //Invenstigar la formula de obtencion valor de venta
        }



        //este metodo no va: No se utiliza
        public void IdentificarTipoPagoVenta()
        {
            //Determinamos el Tipo de pago
            //Verificar si es solo pago en efectivo en doble moneda
            //y/o existe pago con tarjeta
            //o Ambos

            //Solo si es tipo de pago venta adelantado
            if (this.TipoPago.CodigoTipoPago == VentaTipoPago.VentaContadoAdelantado) { return; }  //antes: 14

            if (TotalEfectivoNacional + TotalEfectivoExtranjera > 0)
            {
                this.TipoPago.CodigoTipoPago = VentaTipoPago.VentaEfectivo;      //Efectivo; antes: 1
            }

            if (this.VentaConTarjetas != null)
            {
                if (this.VentaConTarjetas.Count > 0)
                {
                    if (this.TipoPago.CodigoTipoPago == VentaTipoPago.VentaEfectivo) //antes: 1
                    {
                        this.TipoPago.CodigoTipoPago = VentaTipoPago.VentaOtros;  //Otros; antes: 0
                    }
                    else
                    {
                        this.TipoPago.CodigoTipoPago = VentaTipoPago.VentaTarjeta; //Tarjetas; antes 2
                        
                    }
                }
            }


        }

        
        public void ValidarPagoEnEfectivo(decimal pTotalVueltoNacional, decimal pTotalVueltoExtranjera,
                                                string pCodigoMonedaVuelto, string pCodigoMonedaBase)
        {
            //Validaciones Previas ante de dar vuelto
            if (pCodigoMonedaVuelto == pCodigoMonedaBase)
            {
                if (TotalNacional > TotalEfectivoNacional)
                {
                    throw new ArgumentException(Mensajes.excepcion_MontoNacionalPagadoInsuficiente);
                }

                TotalVueltoNacional = pTotalVueltoNacional;
                TotalVueltoExtranjera = 0;
            }
            else
            {
                if (TotalExtranjera > TotalEfectivoExtranjera)
                {
                    throw new ArgumentException(Mensajes.excepcion_MontoExtranjeraPagadoInsuficiente);
                }

                TotalVueltoExtranjera = pTotalVueltoExtranjera;
                TotalVueltoNacional = 0;
            }



        }



        public void CalcularTotalPagoConTarjeta(decimal pTotalEfectivoPagoNacional,
                                            decimal pTotalEfectivoPagoExtranjera, 
                                            string pCodigoMonedaBase)
        {
            //Obtener totales desde tarjeta
            if (this.TipoPago.CodigoTipoPago == VentaTipoPago.VentaOtros || 
                    this.TipoPago.CodigoTipoPago == VentaTipoPago.VentaTarjeta) //antes: .. 0 || .. 2
            {
                foreach (var pagoTarjeta in this.VentaConTarjetas)
                {
                    if (pagoTarjeta.CodigoMoneda == pCodigoMonedaBase)
                    {
                        pTotalEfectivoPagoNacional = pTotalEfectivoPagoNacional + pagoTarjeta.TotalTarjetaNacional;
                    }
                    else
                    {
                        pTotalEfectivoPagoExtranjera = pTotalEfectivoPagoExtranjera + pagoTarjeta.TotalTarjetaExtranjera;
                    }
                }
            }
        }


        public void Habilitar()
        {
            if (!EsHabilitado)
                this._EsHabilitado = true;

        }

        public void Deshabilitar()
        {

            if (EsHabilitado)
                this._EsHabilitado = false;
        }

        public virtual ICollection<VentaDetalle> VentaDetalles
        {
            get
            {
                if (_lineasVentaDetalle == null)
                    _lineasVentaDetalle = new HashSet<VentaDetalle>();

                return _lineasVentaDetalle;
            }

            set
            {
                _lineasVentaDetalle = new HashSet<VentaDetalle>(value);
            }
        }

        public virtual ICollection<VentaConTarjeta> VentaConTarjetas 
        {
            get
            {
                if(_lineasVentaConTarjeta == null)
                    _lineasVentaConTarjeta = new HashSet<VentaConTarjeta>();
       

                return _lineasVentaConTarjeta;
            }

            set
            {
                _lineasVentaConTarjeta = new HashSet<VentaConTarjeta>(value);
            }
        }


        public virtual ICollection<VentaConVale> VentaConVales 
        {   get
            {
                if (_lineasVentaConVale == null)
                    _lineasVentaConVale = new HashSet<VentaConVale>();

                return _lineasVentaConVale;
            }

            set
            {
                _lineasVentaConVale = new HashSet<VentaConVale>(value);
            }
        }


        public virtual ICollection<DocumentoAnticipado> DocumentosAnticipado
        {
            get
            {
                if (_lineasDocumentoAnticipado == null)
                    _lineasDocumentoAnticipado = new HashSet<DocumentoAnticipado>();


                return _lineasDocumentoAnticipado;
            }

            set
            {
                _lineasDocumentoAnticipado = new HashSet<DocumentoAnticipado>(value);
            }
        }


        public virtual ICollection<CuentaPorCobrar> CuentasPorCobrar
        {
            get
            {
                if (_lineasCuentaPorCobrar == null)
                    _lineasCuentaPorCobrar = new HashSet<CuentaPorCobrar>();

                return _lineasCuentaPorCobrar;
            }

            set
            {
                _lineasCuentaPorCobrar = new HashSet<CuentaPorCobrar>(value);
            }
        }




        public VentaDetalle AgregarNuevaVentaDetalle(short pSecuencia,int pNumeroTurno, string pNumeroCara, 
                            decimal pPorcentajeImpuestoIgv, decimal pPorcentajeImpuestoIsc,decimal pTotalNacional, 
                            decimal pTotalExtranjera, decimal pImpuestoNacional, decimal pImpuestoExtranjera,
                            decimal pPorcentajeDescuentoPrimero, decimal pTotalDescuentoNacional, decimal pTotalDescuentoExtranjera,
                            decimal pPrecio,decimal pPrecioVenta, string pDescripcionArticulo, 
                            decimal pCantidad, int pEsFormula, string pCodigoArticulo, 
                            string pCodigoArticuloAlterno,bool pEsInventariable, bool pEnInventarioFisico)
        {
            //Validaciones


            if (string.IsNullOrEmpty(pCodigoArticulo)
                ||
                pSecuencia <= 0
                ||
                String.IsNullOrEmpty(pNumeroCara)
                ||
                pNumeroTurno < 0
                ||
                pPorcentajeImpuestoIgv <= 0
                ||
                pTotalNacional <= 0
                //||
                //pImpuestoNacional <= 0 //No hay calculo de impuesto a nivel de detalle por ahora
                ||
                pPrecio <= 0
                ||  
                pPrecioVenta <= 0
                ||
                String.IsNullOrWhiteSpace(pDescripcionArticulo)
                ||
                pCantidad <= 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaVentaDetalle);
            
            //Crear nuevo detalles venta
            var nuevaLineaVentaDetalle = new VentaDetalle()
            {
                VentaId = this.Id,
                CodigoArticulo = pCodigoArticulo,
                CodigoArticuloAlterno = pCodigoArticuloAlterno,
                CodigoMoneda = this.CodigoMoneda,
                CodigoEstadoDocumento = this.CodigoEstadoDocumento,
                CodigoAlmacen = this.CodigoAlmacen,
                CodigoTipoDocumento = this.CodigoTipoDocumento,
                CodigoUsuarioDeSistema = this.CodigoUsuarioDeSistema,
                NumeroDocumento = this.NumeroDocumento,
                FechaDocumento = this.FechaDocumento,
                FechaProceso = this.FechaProceso,
                Periodo = this.Periodo,
                Secuencia = pSecuencia,
                NumeroTurno = pNumeroTurno,
                NumeroCara = pNumeroCara,
                PorcentajeImpuestoIgv = pPorcentajeImpuestoIgv,
                PorcentajeImpuestoIsc = pPorcentajeImpuestoIsc,
                TotalNacional = pTotalNacional,
                TotalExtranjera = pTotalExtranjera,
                ImpuestoNacional = pImpuestoNacional,
                ImpuestoExtranjera = pImpuestoExtranjera,
                PorcentajeDescuentoPrimero = pPorcentajeDescuentoPrimero,
                TotalDescuentoNacional = pTotalDescuentoNacional,
                TotalDescuentoExtranjera = pTotalDescuentoExtranjera,
                Precio = pPrecio,
                PrecioVenta = pPrecioVenta,
                DescripcionArticulo = pDescripcionArticulo,
                Cantidad = pCantidad,
                EsFormula = pEsFormula,
                EsInventariable = pEsInventariable,
                EnInventarioFisico = pEnInventarioFisico
            };

            //Establecer la identidad
            nuevaLineaVentaDetalle.GenerarNuevaIdentidad();

            this.VentaDetalles.Add(nuevaLineaVentaDetalle);

            return nuevaLineaVentaDetalle;

        }

        public VentaConTarjeta AgregarNuevaVentaConTarjeta(short pSecuencia, string pNumeroTarjeta, decimal pTotalTarjetaNacional,
                    decimal pTotalTarjetaExtranjera, string pCodigoMoneda, string pCodigoTarjeta)
        {

            if (string.IsNullOrEmpty(pCodigoMoneda)
                ||
                string.IsNullOrEmpty(pCodigoTarjeta))
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaVentaConTarjeta);


            var nuevaLineaVentaConTarjeta = new VentaConTarjeta()
            {
                VentaId = this.Id,
                NumeroDocumento = this.NumeroDocumento,                
                CodigoAlmacen = this.CodigoAlmacen,
                CodigoTipoDocumento = this.CodigoTipoDocumento,
                FechaProceso = this.FechaProceso,
                CodigoMoneda = pCodigoMoneda,
                CodigoTarjeta = pCodigoTarjeta,        
                Secuencia = pSecuencia,
                NumeroTarjeta = pNumeroTarjeta,
                TotalTarjetaNacional = pTotalTarjetaNacional,
                TotalTarjetaExtranjera = pTotalTarjetaExtranjera,             
            };

            nuevaLineaVentaConTarjeta.GenerarNuevaIdentidad();

            this.VentaConTarjetas.Add(nuevaLineaVentaConTarjeta);

            return nuevaLineaVentaConTarjeta;
        }

        public VentaConVale AgregarNuevaVentaConVale(decimal pNumeroVale, decimal pMontoVale)
        {
            if (pNumeroVale == 0
                ||
                pMontoVale == 0)
                    throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaVentaConVale);

            var nuevaLineaVentaConVale = new VentaConVale()
            {
                VentaId =this.Id,                
                NumeroDocumento = this.NumeroDocumento,
                CodigoAlmacen = this.CodigoAlmacen,
                CodigoTipoDocumento = this.CodigoTipoDocumento,
                CodigoCliente = this.CodigoCliente,
                FechaProceso = this.FechaProceso,
                CodigoMoneda = this.CodigoMoneda,
                NumeroVale = pNumeroVale,
                MontoVale = pMontoVale                                
            };

            nuevaLineaVentaConVale.GenerarNuevaIdentidad();

            this.VentaConVales.Add(nuevaLineaVentaConVale);

            return nuevaLineaVentaConVale;
        }



        //nueva innovavion de Agregado - Realacion 1 a 0, 1
        public DocumentoAnticipado AgregarNuevoDocumentoAnticipado()
        {
            var nuevaLineaDocumentoAnticipado = new DocumentoAnticipado()
            {
                NumeroDocumento = this.NumeroDocumento,                
                CodigoTipoDocumento = this.CodigoTipoDocumento,
                CodigoAlmacen = this.CodigoAlmacen,            
                FechaProceso = this.FechaProceso
            };

            nuevaLineaDocumentoAnticipado.GenerarNuevaIdentidad();

            this.DocumentosAnticipado.Add(nuevaLineaDocumentoAnticipado);

            return nuevaLineaDocumentoAnticipado;
        }


        //Cuenta por Cobrar
        public CuentaPorCobrar AgregarNuevaCuentaPorCobrar(double pReferencia, DateTime pFechaVencimiento, decimal pPagoDocumentoNacional, 
                                    decimal pPagoDocumentoExtranjera,decimal pSaldoDocumentoNacional, decimal pSaldoDocumentoExtranjera, 
                                    int pDiasDeGracia, decimal pNumeroVale, string pCodigoEstadoDocumento, 
                                    string pCodigoDiaDePago, string pCodigoTipoDocumentoReferencia)
        {
            if (string.IsNullOrEmpty(pCodigoEstadoDocumento)
                ||
                string.IsNullOrEmpty(pCodigoDiaDePago)
                ||
                pReferencia <= 0)
                    throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaCuentaPorCobrar);


            var nuevaLineaCuentaPorCobrar = new CuentaPorCobrar(this.NumeroDocumento, this.CodigoMoneda, this.CodigoClaseTipoCambio,
                                pCodigoEstadoDocumento, pCodigoDiaDePago, this.CodigoAlmacen,
                                this.CodigoUsuarioDeSistema, this.CodigoTipoDocumento, this.CodigoCliente, 
                                pCodigoTipoDocumentoReferencia, pReferencia,DateTime.Now, 
                                this.FechaProceso, pFechaVencimiento,this.TotalNacional, 
                                this.TotalExtranjera, pPagoDocumentoNacional,pPagoDocumentoExtranjera, 
                                pSaldoDocumentoNacional,pSaldoDocumentoExtranjera,this.RucCliente, 
                                this.TipoCambio, pDiasDeGracia,pNumeroVale);

            nuevaLineaCuentaPorCobrar.GenerarNuevaIdentidad();

            this.CuentasPorCobrar.Add(nuevaLineaCuentaPorCobrar);

            return nuevaLineaCuentaPorCobrar;
        }

        //Moneda
        public void EstablecerMonedaDeVenta(Moneda pMoneda)
        {
            if (pMoneda == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaDeVentaEnEstadoNuloOTransitorio);
            }

            this.CodigoMoneda = pMoneda.CodigoMoneda;
            this.Moneda = pMoneda;
        }

        public void EstablecerReferenciaMonedaDeVenta(string pCodigoMoneda)
        {
            if (!string.IsNullOrEmpty(pCodigoMoneda))
            {
                this.CodigoMoneda = pCodigoMoneda;
                this.Moneda = null;
            }
        }

        //ClaseTipoCambio
        public void EstablecerClaseTipoCambioDeVenta(ClaseTipoCambio pClaseTipoCambio)
        {
            if (pClaseTipoCambio == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ClaseTipoCambioDeVentaEnEstadoNuloOTransitorio);
            }

            this.CodigoClaseTipoCambio = pClaseTipoCambio.CodigoClaseTipoCambio;
            this.ClaseTipoCambio = pClaseTipoCambio;
        }
        public void EstablecerReferenciaClaseTipoCambioDeVenta(string pCodigoClaseTipoCambio)
        {
            if (!string.IsNullOrEmpty(pCodigoClaseTipoCambio))
            {
                this.CodigoClaseTipoCambio = pCodigoClaseTipoCambio;
                this.ClaseTipoCambio = null;
            }
        }

        //Cliente
        public void EstablecerClienteDeVenta(Cliente pCliente)
        {
            if (pCliente == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ClienteDeVentaEnEstadoNuloOTransitorio);

            }

            this.CodigoCliente = pCliente.CodigoCliente;
            this.Cliente = pCliente;

            this.RucCliente = pCliente.Ruc;
            this.NombreCompletoCliente = pCliente.NombresORazonSocial;
        }

        public void EstablecerReferenciaClienteDeVenta(string pCodigoCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoCliente))
            {
                this.CodigoCliente = pCodigoCliente;
                this.Cliente = null;
            }
        }

        //TipoDocumento
        public void EstablecerTipoDocumentoDeVenta(TipoDocumento pTipoDocumento)
        {
            if (pTipoDocumento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoDocumentoDeVentaEnEstadoNuloOTransitorio);
            }

            this.CodigoTipoDocumento = pTipoDocumento.CodigoTipoDocumento;
            this.TipoDocumento = pTipoDocumento;

            //Quitado para no consumir funcionalidades comunes desde el Dominio. Dominio - Libre. 2015-03-29
            //this.NumeroDocumento = FuncionesNegocio.CorrelativoDocumento(tipoDocumento.CorrelativoDocumento.Single().Serie,
            //                            (long)tipoDocumento.CorrelativoDocumento.Single().Correlativo);
        }

        public void EstablecerReferenciaTipoDocumentoDeVenta(string pCodigoTipoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumento))
            {
                this.CodigoTipoDocumento = pCodigoTipoDocumento;
                this.TipoDocumento = null;
            }
        }        

        //ImpuestoIgv
        public void EstablecerImpuestoIgvDeCliente(Impuesto pImpuestoIgv)
        {
            if (pImpuestoIgv == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ImpuestoIgvDeVentaEnEstadoNuloOTransitorio);

            }


            this.CodigoImpuestoIgv = pImpuestoIgv.CodigoImpuesto;
            this.ImpuestoIgv = pImpuestoIgv;
        }

        public void EstablecerReferenciaImpuestoIgvDeCliente(string pCodigoImpuestoIgv)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIgv))
            {
                this.CodigoImpuestoIgv = pCodigoImpuestoIgv;
                this.ImpuestoIgv = null;
            }
        }


        //ImpuestoIsc
        public void EstablecerImpuestoIscDeCliente(Impuesto pImpuestoIsc)
        {
            if (pImpuestoIsc == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ImpuestoIscDeVentaEnEstadoNuloOTransitorio);
            }

            this.CodigoImpuestoIsc = pImpuestoIsc.CodigoImpuesto;
            this.ImpuestoIsc = pImpuestoIsc;
        }

        public void EstablecerReferenciaImpuestoIscDeCliente(string pCodigoImpuestoIsc)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIsc))
            {

                this.CodigoImpuestoIsc = pCodigoImpuestoIsc;
                this.ImpuestoIsc = null;
            }
        }





        //EstadoDocumento
        public void EstablecerEstadoDocumentoDeVenta(EstadoDocumento pEstadoDocumento)
        {
            if (pEstadoDocumento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_EstadoDocumentoDeVentaEnEstadoNuloOTransitorio);
            }

            this.CodigoEstadoDocumento = pEstadoDocumento.CodigoEstadoDocumento;
            this.EstadoDocumento = pEstadoDocumento;

        }

        public void EstablecerReferenciaEstadoDocumentoDeVenta(string pCodigoEstadoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoDocumento))
            {
                this.CodigoEstadoDocumento = pCodigoEstadoDocumento;
                this.EstadoDocumento = null;
            }
        }



        //Vendedor
        public void EstablecerVendedorDeVenta(Vendedor pVendedor)
        {
            if (pVendedor == null)
            {
                throw new ArgumentException(Mensajes.excepcion_VendedorDeVentaEnEstadoNuloOTransitorio);
            }

            this.CodigoVendedor = pVendedor.CodigoVendedor;
            this.Vendedor = pVendedor;
        }

        public void EstablecerReferenciaVendedorDeVenta(string pCodigoVendedor)
        {
            if (!string.IsNullOrEmpty(pCodigoVendedor))
            {
                this.CodigoVendedor = pCodigoVendedor;
                this.Vendedor = null;
            }
        }

        //CondicionPago
        public void EstablecerCondicionPagoDeVenta(CondicionPago pCondicionPago)
        {
            if (pCondicionPago == null)
            {
                throw new ArgumentException(Mensajes.excepcion_CondicionPagoDeVentaEnEstadoNuloOTransitorio);

            }

            this.CodigoCondicionPago = pCondicionPago.CodigoCondicionPago;
            this.CondicionPago = pCondicionPago;
        }

        public void EstablecerReferenciaCondicionPagoDeVenta(string pCodigoCondicionPago)
        {
            if (!string.IsNullOrEmpty(pCodigoCondicionPago))
            {
                this.CodigoCondicionPago = pCodigoCondicionPago;
                this.CondicionPago = null;
            }
        }

        //TipoPago
        public void EstablecerTipoPagoDeVenta(TipoPago pTipoPago)
        {
            if (pTipoPago == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoPagoDeVentaEnEstadoNuloOTransitorio);
            }

            this.CodigoTipoPago = pTipoPago.CodigoTipoPago;
            this.TipoPago = pTipoPago;
        }

        public void EstablecerReferenciaTipoPagoDeVenta(string pCodigoTipoPago)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoPago))
            {
                this.CodigoTipoPago = pCodigoTipoPago;
                this.TipoPago = null;
            }
        }

        //ConfiguracionPuntoVenta
        public void EstablecerConfiguracionPuntoVentaDeVenta(ConfiguracionPuntoVenta pConfiguracionPuntoVenta)
        {
            if (pConfiguracionPuntoVenta == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ConfiguracionPuntoVentaDeVentaEnEstadoNuloOTransitorio);
            }

            this.CodigoConfiguracionPuntoVenta = pConfiguracionPuntoVenta.CodigoConfiguracionPuntoVenta;
            this.ConfiguracionPuntoVenta = pConfiguracionPuntoVenta;
        }

        public void EstablecerReferenciaConfiguracionPuntoVentaDeVenta(string pCodigoConfiguracionPuntoVenta)
        {
            if (!string.IsNullOrEmpty(pCodigoConfiguracionPuntoVenta))
            {

                this.CodigoConfiguracionPuntoVenta = pCodigoConfiguracionPuntoVenta;
                this.ConfiguracionPuntoVenta = null;
            }
        }

        //Almacen
        public void EstablecerAlmacenDeVenta(Almacen pAlmacen)
        {
            if (pAlmacen == null)
            {
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeVentaEnEstadoNuloOTransitorio);

            }

            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeVenta(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {

                this.CodigoAlmacen = pCodigoAlmacen;
                this.Almacen = null;
            }
        }

        //TipoNegocio
        public void EstablecerTipoNegocioDeVenta(TipoNegocio pTipoNegocio)
        {
            if (pTipoNegocio == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoNegocioDeVentaEnEstadoNuloOTransitorio);

            }

            this.CodigoTipoNegocio = pTipoNegocio.CodigoTipoNegocio;
            this.TipoNegocio = pTipoNegocio;
        }

        public void EstablecerReferenciaTipoNegocioDeVenta(string pCodigoTipoNegocio)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoNegocio))
            {
                this.CodigoTipoNegocio = pCodigoTipoNegocio;
                this.TipoNegocio = null;
            }
        }

        //UsuarioSistema
        public void EstablecerUsuarioSistemaDeVenta(UsuarioSistema pUsuarioDeSistema)
        {
            if (pUsuarioDeSistema == null)
            {
                throw new ArgumentException(Mensajes.excepcion_UsuarioDeSistemaDeVentaEnEstadoNuloOTransitorio);

            }

            this.CodigoUsuarioDeSistema = pUsuarioDeSistema.CodigoUsuarioDeSistema;
            this.UsuarioSistema = pUsuarioDeSistema;
        }

        public void EstablecerReferenciaUsuarioSistemaDeVenta(string pCodigoUsuarioDeSistema)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioDeSistema))
            {
                this.CodigoUsuarioDeSistema = pCodigoUsuarioDeSistema;
                this.UsuarioSistema = null;
            }
        }        
    }
}