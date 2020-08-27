using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class VentaConTarjeta : Entidad
    {
        public string NumeroDocumento { get; set; }
        public short Secuencia { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal TotalTarjetaNacional { get; set; }
        public decimal TotalTarjetaExtranjera { get; set; }
        public DateTime FechaProceso { get; set; }


        public Guid VentaId { get; set; }

        public string CodigoMoneda { get; set; }
        public string CodigoTarjeta { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string CodigoAlmacen { get; set; }


        public Moneda Moneda { get; private set; }
        public Tarjeta Tarjeta { get; private set; }
        public TipoDocumento TipoDocumento { get; private set; } 
        public Almacen Almacen { get; private set; }

        //Moneda
        public void EstablecerMonedaDeVentaConTarjeta(Moneda pMoneda)
        {
            if (pMoneda == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaDeVentaConTarjetaEnEstadoNuloOTransitorio);
            }

            this.CodigoMoneda = pMoneda.CodigoMoneda;
            this.Moneda = pMoneda;
        }

        public void EstablecerReferenciaMonedaDeVentaConTarjeta(string pCodigoMoneda)
        {
            if (!string.IsNullOrEmpty(pCodigoMoneda))
            {
                this.CodigoMoneda = pCodigoMoneda.Trim();
                this.Moneda = null;
            }
        }

        //Tarjeta
        public void EstablecerTarjetaDeVentaConTarjeta(Tarjeta pTarjeta)
        {
            if (pTarjeta == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TarjetaDeVentaConTarjetaEnEstadoNuloOTransitorio);
            }

            this.CodigoTarjeta = pTarjeta.CodigoTarjeta;
            this.Tarjeta = pTarjeta;
        }

        public void EstablecerReferenciaTarjetaDeVentaConTarjeta(string pCodigoTarjeta)
        {
            if (!string.IsNullOrEmpty(pCodigoTarjeta))
            {
                this.CodigoTarjeta = pCodigoTarjeta.Trim();
                this.Tarjeta = null;
            }
        }

        //TipoDocumento
        public void EstablecerTipoDocumentoDeVentaConTarjeta(TipoDocumento pTipoDocumento)
        {
            if (pTipoDocumento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoDocumentoDeVentaconTarjetaEnEstadoNuloOTransitorio);
            }

            this.CodigoTipoDocumento = pTipoDocumento.CodigoTipoDocumento;
            this.TipoDocumento = pTipoDocumento;
        }

        public void EstablecerReferenciaTipoDocumentoDeVentaConTarjeta(string pCodigoTipoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumento))
            {
                this.CodigoTipoDocumento = pCodigoTipoDocumento.Trim();
                this.TipoDocumento = null;
            }
        }

        //Almacen
        public void EstablecerAlmacenDeVentaConTarjeta(Almacen pAlmacen)
        {
            if (pAlmacen == null)
            {
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeVentaConTarjetaEnEstadoNuloOTransitorio);
            }

            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeVentaConTarjeta(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {
                this.CodigoAlmacen = pCodigoAlmacen.Trim();
                this.Almacen = null;
            }
        }
    }
}