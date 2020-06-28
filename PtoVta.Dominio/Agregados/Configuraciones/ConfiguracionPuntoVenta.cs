using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.MensajesDominio;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public class ConfiguracionPuntoVenta : Entidad
    {
        bool _EsHabilitado;

        HashSet<CierreZetaPuntoDeVenta> _lineasCierreZetaPuntoDeVenta;

        public string CodigoConfiguracionPuntoVenta { get; set; }    
        public string NombrePuntoVenta { get; set; }
        public string NombreTerminal { get; set; }
        public string NumeroSerieMaquinaRegistradora { get; set; }
        public bool PermiteTicketFactura { get; set; }
        public bool PermiteTicketBoleta { get; set; }
        public string SimboloMonedaCaja { get; set; }
        public bool PermiteColaTransaccionesManual { get; set; }         //stkloadsale; Salvar Transacciones
        public bool DispositivoTicketFactura { get; set; }
        public string TipoDispositivoSalidaTicketFactura { get; set; }
        public bool DispositivoTicketBoleta { get; set; }
        public string TipoDispositivoSalidaTicketBoleta { get; set; }
        public long SerieCorrelativoTickFac { get; set; }
        public long SerieCorrelativoTickBol { get; set; }
        public bool RealizoCierreZeta { get; set; }
        public bool RealizoCierreTurno { get; set; }
        public bool PermiteSaltoAutomatico { get; set; }
        public int CantidadSaltoAutomatico { get; set; }
        public bool PedirClaveAnulacionDocumento { get; set; }
        //public int PermiteRegistrarCantidadVentaAutomatico { get; set; } //igual a PermiteSaltoAutomatico
        public decimal CorrelativoMovimientoAlmacenPorVenta { get; set; }


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


        public string CodigoMonedaCaja { get; private set; }
        public string CodigoTipoNegocio { get; private set; }
        public string CodigoAlmacenPuntoVenta { get; private set; }
        public string CodigoTipoImpresora { get; private set; }
        public string CodigoEstadoDocumentoDefault { get; set; }
        public string CodigoTipoPagoDefault { get; set; }
        public string CodigoEstadoDocumentoAnulado { get; set; }


        public virtual Moneda MonedaCaja { get; private set; }
        public virtual TipoNegocio TipoNegocio { get; private set; }
        public virtual Almacen AlmacenPuntoVenta { get; private set; }
        public virtual TipoImpresora TipoImpresora { get; private set; }
        public virtual EstadoDocumento EstadoDocumentoDefault { get; private set; }
        public virtual TipoPago TipoPagoDefault { get; private set; }
        public virtual EstadoDocumento EstadoDocumentoAnulado { get; private set; }



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

        public virtual ICollection<CierreZetaPuntoDeVenta> CierresZetaPuntoDeVenta
        {
            get
            {
                if (_lineasCierreZetaPuntoDeVenta == null)
                    _lineasCierreZetaPuntoDeVenta = new HashSet<CierreZetaPuntoDeVenta>();

                return _lineasCierreZetaPuntoDeVenta;
            }
            set
            {
                _lineasCierreZetaPuntoDeVenta = new HashSet<CierreZetaPuntoDeVenta>(value);
            } 
        }

        public CierreZetaPuntoDeVenta AgregarNuevoCierreZetaPuntoDeVenta(
                                            DateTime pFechaProcesoVentas, DateTime pFechaCierreZeta,
                                            decimal pTotalCierreZeta, short pNumeroZetaPtoVta)
        {
            //if (pConfiguracionPuntoVentaId == Guid.Empty)
            //    throw new ArgumentNullException("Messages.exception_InvalidDataForOrderLine");

            if (pFechaProcesoVentas == null
                ||
                pFechaCierreZeta == null
                ||
                pTotalCierreZeta <= 0
                ||
                pNumeroZetaPtoVta <= 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaCierreZetaPuntoDeVenta);



            var nuevaLineaCierreZetaPuntoDeVenta = new CierreZetaPuntoDeVenta()
            {
                CodigoConfiguracionPuntoVenta = this.CodigoConfiguracionPuntoVenta,
                FechaProcesoVentas = pFechaProcesoVentas,
                FechaCierreZeta = pFechaCierreZeta,
                TotalCierreZeta = pTotalCierreZeta,
                NumeroZetaPtoVta = pNumeroZetaPtoVta
            };

            //Establecer la identidad
            nuevaLineaCierreZetaPuntoDeVenta.GenerarNuevaIdentidad();

            this.CierresZetaPuntoDeVenta.Add(nuevaLineaCierreZetaPuntoDeVenta);

            return nuevaLineaCierreZetaPuntoDeVenta;


        }


        public void EstablecerMonedaDeConfiguracionPuntoVenta(Moneda pMonedaCaja)
        {
            if (pMonedaCaja == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaCajaDeConfiguracionPuntoDeVentaNuloOTransitorio);
            }

            //relacion
            this.CodigoMonedaCaja = pMonedaCaja.CodigoMoneda;
            this.MonedaCaja = pMonedaCaja;
        }

        public void EstablecerReferenciaMonedaDeConfiguracionPuntoVenta(string pCodigoMonedaCaja)
        {
            if (!string.IsNullOrEmpty(pCodigoMonedaCaja))
            {
                //relacion
                this.CodigoMonedaCaja = pCodigoMonedaCaja;
                this.MonedaCaja = null;
            }
        }

        public void EstablecerTipoNegocioDeConfiguracionPuntoVenta(TipoNegocio pTipoNegocio)
        {
            if (pTipoNegocio == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoNegocioDeConfiguracionPuntoDeVentaNuloOTransitorio);
            }

            //relacion
            this.CodigoTipoNegocio = pTipoNegocio.CodigoTipoNegocio;
            this.TipoNegocio = pTipoNegocio;
        }

        public void EstablecerReferenciaTipoNegocioDeConfiguracionPuntoVenta(string pCodigoTipoNegocio)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoNegocio))
            {
                //relacion
                this.CodigoTipoNegocio = pCodigoTipoNegocio;
                this.TipoNegocio = null;
            }
        }

        public void EstablecerAlmacenDeConfiguracionPuntoVenta(Almacen pAlmacenPuntoVenta)
        {
            if (pAlmacenPuntoVenta == null)
            {
                throw new ArgumentException(Mensajes.excepcion_AlmacenPuntoVentaDeConfiguracionPuntoDeVentaNuloOTransitorio);
            }

            //relacion
            this.CodigoAlmacenPuntoVenta = pAlmacenPuntoVenta.CodigoAlmacen;
            this.AlmacenPuntoVenta = pAlmacenPuntoVenta;
        }

        public void EstablecerReferenciaAlmacenDeConfiguracionPuntoVenta(string pCodigoAlmacenPuntoVenta)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacenPuntoVenta))
            {
                //relacion
                this.CodigoAlmacenPuntoVenta = pCodigoAlmacenPuntoVenta;
                this.AlmacenPuntoVenta = null;
            }
        }

        public void EstablecerTipoImpresoraDeConfiguracionPuntoVenta(TipoImpresora pTipoImpresora)
        {
            if (pTipoImpresora == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoImpresoraDeConfiguracionPuntoDeVentaNuloOTransitorio);
            }

            //relacion
            this.CodigoTipoImpresora = pTipoImpresora.CodigoTipoImpresora;
            this.TipoImpresora = pTipoImpresora;
        }

        public void EstablecerReferenciaTipoImpresoraDeConfiguracionPuntoVenta(string pCodigoTipoImpresora)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoImpresora))
            {
                //relacion
                this.CodigoTipoImpresora = pCodigoTipoImpresora;
                this.TipoImpresora = null;
            }
        }


        //..
        public void EstablecerEstadoDocumentoDefaultDeConfiguracionPuntoVenta(EstadoDocumento pEstadoDocumentoDefault)
        {
            if (pEstadoDocumentoDefault == null || pEstadoDocumentoDefault.EsTransitorio())
            {
                throw new ArgumentException(Mensajes.excepcion_EstadoDeDocumentoDefaultDeConfiguracionPuntoDeVentaNuloOTransitorio);
            }

            //relacion
            this.CodigoEstadoDocumentoDefault = pEstadoDocumentoDefault.CodigoEstadoDocumento;
            this.EstadoDocumentoDefault = pEstadoDocumentoDefault;
        }

        public void EstablecerReferenciaEstadoDocumentoDefaultDeConfiguracionPuntoVenta(string pCodigoEstadoDocumentoDefault)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoDocumentoDefault))
            {
                //relacion
                this.CodigoEstadoDocumentoDefault = pCodigoEstadoDocumentoDefault;
                this.EstadoDocumentoDefault = null;
            }
        }


        public void EstablecerTipoPagoDefaultDeConfiguracionPuntoVenta(TipoPago pTipoPagoDefault)
        {
            if (pTipoPagoDefault == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoPagoDefaultDeConfiguracionPuntoVentaNuloOTransitorio);
            }

            //relacion
            this.CodigoTipoPagoDefault = pTipoPagoDefault.CodigoTipoPago;
            this.TipoPagoDefault = pTipoPagoDefault;
        }

        public void EstablecerReferenciaTipoPagoDefaultDeConfiguracionPuntoVenta(string pCodigoTipoPagoDefault)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoPagoDefault))
            {
                //relacion
                this.CodigoTipoPagoDefault = pCodigoTipoPagoDefault;
                this.TipoPagoDefault = null;
            }
        }


        public void EstablecerEstadoDocumentoAnuladoDeConfiguracionPuntoVenta(EstadoDocumento pEstadoDocumentoAnulado)
        {
            if (pEstadoDocumentoAnulado == null)
            {
                throw new ArgumentException(Mensajes.excepcion_EstadoDocumentoAnuladoDeConfiguracionPuntoVentaNuloOTransitorio);

            }

            //relacion
            this.CodigoEstadoDocumentoAnulado = pEstadoDocumentoAnulado.CodigoEstadoDocumento;
            this.EstadoDocumentoAnulado = pEstadoDocumentoAnulado;
        }

        public void EstablecerReferenciaEstadoDocumentoAnuladoDeConfiguracionPuntoVenta(string pCodigoEstadoDocumentoAnulado)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoDocumentoAnulado))
            {
                //relacion
                this.CodigoEstadoDocumentoAnulado = pCodigoEstadoDocumentoAnulado;
                this.EstadoDocumentoAnulado = null;
            }
        }
    }
}