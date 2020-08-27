using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class MovimientoAlmacen : Entidad
    {
          bool _EsHabilitado;

        public string CorrelativoMovimiento { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public decimal MontoTipoDeCambio { get; set; }
        public DateTime FechaTipoDeCambio { get; set; }
        public string Periodo { get; set; }
        public int FlagEntradaSalida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoReposicionExtranjera { get; set; }
        public decimal CostoReposicionNacional { get; set; }
        public bool EsArticuloFormula { get; set; }
        public decimal Precio { get; set; }
        public string DocumentoReferencia { get; set; }
        public int EnInventarioFisico { get; set; }


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




        public string CodigoAlmacen { get; set; }
        public string CodigoArticulo { get; set; }
        public string CodigoTipoMovimientoAlmacen { get; set; }
        public string CodigoTipoDocumentoReferencia { get; set; }

        public virtual Almacen Almacen { get; private set; }
        public virtual Articulo Articulo { get; private set; }
        public virtual TipoMovimientoAlmacen TipoMovimientoAlmacen { get; private set; }
        public virtual TipoDocumento TipoDocumento { get; private set; }

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


        public void EstablecerAlmacenDeMovimientoAlmacen(Almacen pAlmacen)
        {
            if (pAlmacen == null)
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeMovimientoAlmacenNuloOTransitorio);

            //relacion
            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeMovimientoAlmacen(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {
                //relacion
                this.CodigoAlmacen = pCodigoAlmacen.Trim();
                this.Almacen = null;
            }
        }

        public void EstablecerArticuloDeMovimientoAlmacen(Articulo pArticulo)
        {
            if (pArticulo == null)
                throw new ArgumentException(Mensajes.excepcion_ArticuloDeMovimientoAlmacenNuloOTransitorio);

            //relacion
            this.CodigoArticulo = pArticulo.CodigoArticulo;
            this.Articulo = pArticulo;
        }

        public void EstablecerReferenciaArticuloDeMovimientoAlmacen(string pCodigoArticulo)
        {
            if (!string.IsNullOrEmpty(pCodigoArticulo))
            {
                //relacion
                this.CodigoArticulo = pCodigoArticulo.Trim();
                this.Articulo = null;
            }
        }

        public void EstablecerTipoMovimientoAlmacenDeMovimientoAlmacen(TipoMovimientoAlmacen pTipoMovimientoAlmacen)
        {
            if (pTipoMovimientoAlmacen == null)
                throw new ArgumentException(Mensajes.excepcion_TipoMovimientoAlmacenDeMovimientoAlmacenNuloOTransitorio);

            //relacion
            this.CodigoTipoMovimientoAlmacen = pTipoMovimientoAlmacen.CodigoTipoMovimientoAlmacen;
            this.TipoMovimientoAlmacen = pTipoMovimientoAlmacen;
        }

        public void EstablecerReferenciaTipoMovimientoAlmacenDeMovimientoAlmacen(string pCodigoTipoMovimientoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoMovimientoAlmacen))
            {
                //relacion
                this.CodigoTipoMovimientoAlmacen = pCodigoTipoMovimientoAlmacen.Trim();
                this.TipoMovimientoAlmacen = null;
            }
        }

        public void EstablecerTipoDocumentoDeMovimientoAlmacen(TipoDocumento pTipoDocumento)
        {
            if (pTipoDocumento == null)
                throw new ArgumentException(Mensajes.excepcion_TipoDocumentoDeMovimientoAlmacenNuloOTransitorio);


            //relacion
            this.CodigoTipoDocumentoReferencia = pTipoDocumento.CodigoTipoDocumento;
            this.TipoDocumento = pTipoDocumento;

            //Quitado para no consumir funcionalidades comunes desde el Dominio. Dominio - Libre. 2015-03-29
            //this.NumeroDocumento = FuncionesNegocio.CorrelativoDocumento(tipoDocumento.CorrelativoDocumento.Single().Serie,
            //                            (long)tipoDocumento.CorrelativoDocumento.Single().Correlativo);
        }

        public void EstablecerReferenciaTipoDocumentoDeMovimientoAlmacen(string pCodigoTipoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumento))
            {
                //relacion
                this.CodigoTipoDocumentoReferencia = pCodigoTipoDocumento.Trim();
                this.TipoDocumento = null;
            }
        }      
    }
}