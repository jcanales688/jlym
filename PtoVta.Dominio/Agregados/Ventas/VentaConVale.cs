using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.MensajesDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{   
    public class VentaConVale : Entidad
    {
        public decimal NumeroDocumento { get; set; }
        public decimal NumeroVale { get; set; }
        public DateTime FechaProceso { get; set; }
        public Nullable<decimal> MontoVale { get; set; }

        public Guid VentaId { get; set; }


        public string CodigoCliente { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string CodigoMoneda { get; set; }


        public Cliente Cliente { get; private set; }
        public Almacen Almacen { get; private set; }
        public TipoDocumento TipoDocumento { get; private set; }
        public Moneda Moneda { get; private set; }

        //Cliente
        public void EstablecerClienteDeVentaConVale(Cliente pCliente)
        {
            if (pCliente == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ClienteDeVentaConValeEnEstadoNuloOTransitorio);
            }

            this.CodigoCliente = pCliente.CodigoCliente;
            this.Cliente = pCliente;
        }
        public void EstablecerReferenciaClienteDeVentaConVale(string pCodigoCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoCliente))
            {

                this.CodigoCliente = pCodigoCliente;
                this.Cliente = null;
            }
        }

        //Almacen
        public void EstablecerAlmacenDeVentaConVale(Almacen pAlmacen)
        {
            if (pAlmacen == null)
            {
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeVentaConValeEnEstadoNuloOTransitorio);
            }

            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeVentaConVale(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {

                this.CodigoAlmacen = pCodigoAlmacen;
                this.Almacen = null;
            }
        }

        //TipoDocumento
        public void EstablecerTipoDocumentoDeVentaConVale(TipoDocumento pTipoDocumento)
        {
            if (pTipoDocumento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoDocumentoDeVentaConValeEnEstadoNuloOTransitorio);
            }

            this.CodigoTipoDocumento = pTipoDocumento.CodigoTipoDocumento;
            this.TipoDocumento = pTipoDocumento;
        }

        public void EstablecerReferenciaTipoDocumentoDeVentaConVale(string pCodigoTipoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumento))
            {

                this.CodigoTipoDocumento = pCodigoTipoDocumento;
                this.TipoDocumento = null;
            }
        }

        //Moneda

        public void EstablecerMonedaDeVentaConTarjeta(Moneda pMoneda)
        {
            if (pMoneda == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaDeVentaConValeEnEstadoNuloOTransitorio);
            }

            this.CodigoMoneda = pMoneda.CodigoMoneda;
            this.Moneda = pMoneda;
        }

        public void EstablecerReferenciaMonedaDeVentaConTarjeta(string pCodigoMoneda)
        {
            if (!string.IsNullOrEmpty(pCodigoMoneda))
            {
                this.CodigoMoneda = pCodigoMoneda;
                this.Moneda = null;
            }
        }        
    }

}