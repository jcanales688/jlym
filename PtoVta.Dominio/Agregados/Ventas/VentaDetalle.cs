using System;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class VentaDetalle : Entidad
    {
        public string NumeroDocumento { get; set; }
        public short Secuencia { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public string Periodo { get; set; }
        public int NumeroTurno { get; set; }
        public string NumeroCara { get; set; }
        public decimal PorcentajeImpuestoIgv { get; set; }
        public decimal PorcentajeImpuestoIsc { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjera { get; set; }
        public decimal ImpuestoNacional { get; set; }
        public decimal ImpuestoExtranjera { get; set; }
        public Nullable<decimal> PorcentajeDescuentoPrimero { get; set; }
        public Nullable<decimal> TotalDescuentoNacional { get; set; }
        public Nullable<decimal> TotalDescuentoExtranjera { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioVenta { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }
        public int EsFormula { get; set; }
        public bool EsInventariable { get; set; }
        public bool EnInventarioFisico { get; set; }

        public Guid VentaId { get; set; }
        public string CodigoArticulo { get; set; }
        public string CodigoArticuloAlterno { get; set; }
        public string CodigoMoneda { get; set; }
        public string CodigoEstadoDocumento { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoTipoDocumento  { get; set; }

        public Articulo Articulo { get;private set; }
        public Moneda Moneda{get; private set;}
        public EstadoDocumento EstadoDocumento { get; private set; }


        public void CalcularTotales(decimal pTipoDeCambio, decimal pImpuestoIgvValor)
        {
            this.PorcentajeImpuestoIgv = pImpuestoIgvValor;

            this.TotalNacional = Math.Round(this.PrecioVenta * this.Cantidad, 4);
            this.TotalExtranjera = Math.Round(this.TotalNacional / pTipoDeCambio, 4);

            var montoBaseNacional = this.TotalNacional / (1 + (this.PorcentajeImpuestoIgv / 100));
            var montoBaseExtranjera = montoBaseNacional / pTipoDeCambio;

            this.ImpuestoNacional = Math.Round(this.TotalNacional - montoBaseNacional, 4);
            this.ImpuestoExtranjera = Math.Round(this.TotalExtranjera - montoBaseExtranjera, 4);
        }

        //Articulo
        public void EstablecerArticuloDeVentaDetalle(Articulo pArticulo)
        {
            if (pArticulo == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ArticuloDeVentaDetalleEnEstadoNuloOTransitorio);
            }

            this.CodigoArticulo = pArticulo.CodigoArticulo;
            this.Articulo = pArticulo;
        }

        public void EstablecerReferenciaArticuloDeVenta(string pCodigoArticulo)
        {
            if (!string.IsNullOrEmpty(pCodigoArticulo))
            {

                this.CodigoArticulo = pCodigoArticulo.Trim();
                this.Articulo = null;
            }
        }



        //Moneda
        public void EstablecerMonedaDeVentaDetalle(Moneda pMoneda)
        {
            if (pMoneda == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaDeVentaDetalleEnEstadoNuloOtransitorio);
            }

            this.CodigoMoneda = pMoneda.CodigoMoneda;
            this.Moneda = pMoneda;
        }

        public void EstablecerReferenciaMonedaDeVenta(string pCodigoMoneda)
        {
            if (!string.IsNullOrEmpty(pCodigoMoneda))
            {

                this.CodigoMoneda = pCodigoMoneda.Trim();
                this.Moneda = null;
            }
        }

        //EstadoDocumento
        public void EstablecerEstadoDocumentoDeVentaDetalle(EstadoDocumento pEstadoDocumento)
        {
            if (pEstadoDocumento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_EstadoDocumentoDeVentaDetalleEnEstadoNuloOTransitorio);
            }

            this.CodigoEstadoDocumento = pEstadoDocumento.CodigoEstadoDocumento;
            this.EstadoDocumento = pEstadoDocumento;
        }

        public void EstablecerReferenciaEstadoDocumentoDeVenta(string pCodigoEstadoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoDocumento))
            {

                this.CodigoEstadoDocumento = pCodigoEstadoDocumento.Trim();
                this.EstadoDocumento = null;
            }
        }        
    }
}