using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.MensajesDominio;

namespace PtoVta.Dominio.Agregados.CuentasPorCobrar 
{
    public class CuentaPorCobrar: Entidad
    {
        public decimal NumeroDocumento { get; set; }

        public double Referencia { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalNacionalCtaCobrar { get; set; }
        public decimal TotalExtranjeraCtaCobrar { get; set; }
        public decimal PagoDocumentoNacional { get; set; }
        public decimal PagoDocumentoExtranjera { get; set; }
        public decimal SaldoDocumentoNacional { get; set; }
        public decimal SaldoDocumentoExtranjera { get; set; }
        public string Ruc { get; set; }
        public decimal TipoCambio { get; set; }
        public int DiasDeGracia { get; set; }
        public decimal NumeroVale { get; set; }


        public Guid VentaId { get; set; }

        public string CodigoMoneda { get;  private set; }
        public string CodigoClaseTipoCambio { get;  private set; }
        public string CodigoEstadoDocumento { get; private set; }
        public string CodigoDiaDePago { get; private set; }
        public string CodigoAlmacen { get; private set; }
        public string CodigoUsuarioDeSistema { get; private set; }
        public string CodigoTipoDocumento { get; private set; }



        public Moneda Moneda { get; private set; }
        public ClaseTipoCambio ClaseTipoCambio { get; private set; }
        public EstadoDocumento EstadoDocumento { get; private set; }
        public DiaDePago DiaDePago { get; private set; }
        public Almacen Almacen { get; private set; }
        public UsuarioSistema UsuarioSistema { get; private set; }
        public TipoDocumento TipoDocumento { get; private set; }


        public CuentaPorCobrar(decimal pNumeroDocumento, string pCodigoMoneda,string pCodigoClaseTipoCambio,string pCodigoEstadoDocumento,
                string pCodigoDiaDePago,string pCodigoAlmacen,string pCodigoUsuarioDeSistema,
                string pCodigoTipoDocumento, double pReferencia,DateTime pFechaDocumento,
                DateTime pFechaProceso,DateTime pFechaVencimiento,decimal pTotalNacionalCtaCobrar,
                decimal pTotalExtranjeraCtaCobrar,decimal pPagoDocumentoNacional,decimal pPagoDocumentoExtranjera,
                decimal pSaldoDocumentoNacional,decimal pSaldoDocumentoExtranjera,string pRuc,
                decimal pTipoCambio,int pDiasDeGracia,decimal pNumeroVale)
        {
                NumeroDocumento = pNumeroDocumento;

                CodigoMoneda  = pCodigoMoneda;
                CodigoClaseTipoCambio  = pCodigoClaseTipoCambio;
                CodigoEstadoDocumento = pCodigoEstadoDocumento;
                CodigoDiaDePago = pCodigoDiaDePago; 
                CodigoAlmacen = pCodigoAlmacen;
                CodigoUsuarioDeSistema = pCodigoUsuarioDeSistema;
                CodigoTipoDocumento = pCodigoTipoDocumento;
                Referencia = pReferencia;
                FechaDocumento = pFechaDocumento;
                FechaProceso = pFechaProceso;
                FechaVencimiento = pFechaVencimiento;
                TotalNacionalCtaCobrar = pTotalNacionalCtaCobrar;
                TotalExtranjeraCtaCobrar = pTotalExtranjeraCtaCobrar;
                PagoDocumentoNacional = pPagoDocumentoNacional;
                PagoDocumentoExtranjera = pPagoDocumentoExtranjera;
                SaldoDocumentoNacional = pSaldoDocumentoNacional;
                SaldoDocumentoExtranjera = pSaldoDocumentoExtranjera;
                Ruc = pRuc;
                TipoCambio = pTipoCambio;
                DiasDeGracia = pDiasDeGracia;
                NumeroVale  = pNumeroVale;
        }

        //Moneda
        public void EstablecerMonedaDeCuentaPorCobrar(Moneda pMoneda)
        {
            if (pMoneda == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaDeCuentasPorCobrarEnEstadoNuloOTransitorio);

            }
   
            this.CodigoMoneda = pMoneda.CodigoMoneda;
            this.Moneda = pMoneda;
        }

        public void EstablecerReferenciaMonedaDeCuentaPorCobrar(string pCodigoMoneda)
        {
            if (!string.IsNullOrEmpty(pCodigoMoneda))
            {     
                this.CodigoMoneda = pCodigoMoneda;
                this.Moneda = null;
            }
        }

        //ClaseTipoCambio
        public void EstablecerClaseTipoCambioDeCuentaPorCobrar(ClaseTipoCambio pClaseTipoCambio)
        {
            if (pClaseTipoCambio == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ClaseTipoDeCambioDeCuentaPorCobrarEnEstadoNuloOTransitorio);

            }

            this.CodigoClaseTipoCambio = pClaseTipoCambio.CodigoClaseTipoCambio;
            this.ClaseTipoCambio = pClaseTipoCambio;
        }

        public void EstablecerReferenciaClaseTipoCambioDeCuentaPorCobrar(string pCodigoClaseTipoCambio)
        {
            if (!string.IsNullOrEmpty(pCodigoClaseTipoCambio))
            {
                this.CodigoClaseTipoCambio = pCodigoClaseTipoCambio;
                this.ClaseTipoCambio = null;
            }
        }

        //EstadoDocumento
        public void EstablecerEstadoDocumentoDeCuentaPorCobrar(EstadoDocumento pEstadoDocumento)
        {
            if (pEstadoDocumento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_EstadoDeDocumentoDeCuentaPorCobrarEnEstadoNuloOTransitorio);
            }

            this.CodigoEstadoDocumento = pEstadoDocumento.CodigoEstadoDocumento;
            this.EstadoDocumento = pEstadoDocumento;
        }

        public void EstablecerReferenciaEstadoDocumentoDeCuentaPorCobrar(string pCodigoEstadoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoDocumento))
            {
                this.CodigoEstadoDocumento = pCodigoEstadoDocumento;
                this.EstadoDocumento = null;
            }
        }


        //DiaDePago
        public void EstablecerDiaDePagoDeCuentaPorCobrar(DiaDePago pDiaDePago)
        {
            if (pDiaDePago == null)
            {
                throw new ArgumentException(Mensajes.excepcion_DiaDePagoDeCuentaPorCobrarEnEstadoNuloOTransitorio);
            }

            this.CodigoDiaDePago = pDiaDePago.CodigoDiaDePago;
            this.DiaDePago = pDiaDePago;
        }

        public void EstablecerReferenciaDiaDePagoDeCuentaPorCobrar(string pCodigoDiaDePago)
        {
            if (!string.IsNullOrEmpty(pCodigoDiaDePago))
            {
                this.CodigoDiaDePago = pCodigoDiaDePago;
                this.DiaDePago = null;
            }
        }


        //Almacen
        public void EstablecerAlmacenDeCuentaPorCobrar(Almacen pAlmacen)
        {
            if (pAlmacen == null)
            {
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeCuentaPorCobrarEnEstadoNuloOTransitorio);
            }

            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeCuentaPorCobrar(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {
                this.CodigoAlmacen = pCodigoAlmacen;
                this.Almacen = null;
            }
        }

        //UsuarioSistema
        public void EstablecerUsuarioSistemaDeCuentaPorCobrar(UsuarioSistema pUsuarioSistema)
        {
            if (pUsuarioSistema == null)
            {
                throw new ArgumentException(Mensajes.excepcion_UsuarioDeSistemaDeCuentaPorCobrarEnEstadoNuloOTransitorio);
            }

            this.CodigoUsuarioDeSistema = pUsuarioSistema.CodigoUsuarioDeSistema;
            this.UsuarioSistema = pUsuarioSistema;
        }

        public void EstablecerReferenciaUsuarioSistemaDeCuentaPorCobrar(string pCodigoUsuarioDeSistema)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioDeSistema))
            {
                this.CodigoUsuarioDeSistema = pCodigoUsuarioDeSistema;
                this.UsuarioSistema = null;
            }
        }

        //TipoDocumento
        public void EstablecerTipoDocumentoDeCuentaPorCobrar(TipoDocumento pTipoDocumento)
        {
            if (pTipoDocumento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoDeDocumentoDeCuentaPorCobrarEnEstadoNuloOTransitorio);
            }

            this.CodigoTipoDocumento = pTipoDocumento.CodigoTipoDocumento;
            this.TipoDocumento = pTipoDocumento;
        }

        public void EstablecerReferenciaTipoDocumentoDeCuentaPorCobrar(string pCodigoTipoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumento))
            {

                this.CodigoTipoDocumento = pCodigoTipoDocumento;
                this.TipoDocumento = null;
            }
        }
    }
}