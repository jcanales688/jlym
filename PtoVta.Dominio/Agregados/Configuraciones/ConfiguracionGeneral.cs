using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public class ConfiguracionGeneral : Entidad  
    {
        bool _EsHabilitado;


        public int CantidadTurnos { get; set; }
        public int TurnoActual { get; set; }
        public int CantidadCaras { get; set; }
        public string SimboloMonedaBase{ get; set; }
        public string SimboloMonedaExtranjera{ get; set; }
        public int CantDecimalPrecio { get; set; }
        public int CantDecimalCosto { get; set; }
        public int CantDecimalStock { get; set; }
        public int CantDecimalResultado { get; set; }
        public Nullable<int> CantDecimalDescuento { get; set; }
        public decimal PorcentajeImpuesto { get; set; }
        public DateTime FechaProceso { get; set; }
        public Nullable<int> TipoControlCombustible { get; set; }
        public decimal DiferenciaDiariaPermitida { get; set; }
        public Nullable<int> DiasCambioClave { get; set; }
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


        public string CodigoAlmacenOrigen { get; private set; }
        public string CodigoMonedaBase { get; private set; }
        public string CodigoMonedaExtranjeraPorDefecto { get; private set; }
        public string CodigoClaseTipoCambioVentas { get; private set; }
        public string CodigoClaseTipoCambioOrigen { get; private set; }
        public string CodigoImpuesto { get; private set; }
        public string CodigoClienteInterno { get; private set; }
        public string CodigoTipoPrecioInventarioActualizable { get; private set; }

        public virtual Almacen AlmacenOrigen { get; private set; }
        public virtual Moneda MonedaBase { get; private set; }
        public virtual Moneda MonedaExtranjera { get; private set; }
        public virtual ClaseTipoCambio ClaseTipoCambioVentas { get; private set; }
        public virtual ClaseTipoCambio ClaseTipoCambioOrigen { get; private set; }
        public virtual Impuesto Impuesto { get; private set; }
        public virtual Cliente ClienteInterno { get; private set; }
        public virtual TipoPrecioInventario TipoPrecioInventarioActualizable { get; private set; }

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

        public void EstablecerAlmacenDeConfiguracionGeneral(Almacen pAlmacenOrigen)
        {
            if (pAlmacenOrigen == null)
            {
                throw new ArgumentException(Mensajes.excepcion_AlmacenOrigenDeConfiguracionGeneralEnEstadoNuloOTransitorio);

            }

            //relacion
            this.CodigoAlmacenOrigen = pAlmacenOrigen.CodigoAlmacen;
            this.AlmacenOrigen = pAlmacenOrigen;
        }

        public void EstablecerMonedaBaseDeConfiguracionGeneral(Moneda pMonedaBase)
        {
            if (pMonedaBase == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaBaseDeConfiguracionGeneralEnEstadoNuloOTransitorio);

            }

            //relacion
            this.CodigoMonedaBase = pMonedaBase.CodigoMoneda;
            this.MonedaBase = pMonedaBase;
        }

        public void EstablecerMonedaExtranjeraDefDeConfiguracionGeneral(Moneda pMonedaExtranjeraDef)
        {
            if (pMonedaExtranjeraDef == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaExtranjeraDeConfiguracionGeneralEnEstadoNuloOTransitorio);

            }

            //relacion
            this.CodigoMonedaExtranjeraPorDefecto = pMonedaExtranjeraDef.CodigoMoneda;
            this.MonedaExtranjera = pMonedaExtranjeraDef;
        }

        public void EstablecerClaseTipoCambioVentasDeConfiguracionGeneral(ClaseTipoCambio pClaseTipoCambioVentas)
        {
            if (pClaseTipoCambioVentas == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ClaseTipoCambioVentasDeConfiguracionGeneralEnEstadoNuloOTransitorio);

            }

            //relacion
            this.CodigoClaseTipoCambioVentas = pClaseTipoCambioVentas.CodigoClaseTipoCambio;
            this.ClaseTipoCambioVentas = pClaseTipoCambioVentas;
        }

        public void EstablecerClaseTipoCambioOrigenDeConfiguracionGeneral(ClaseTipoCambio pClaseTipoCambioOrigen)
        {
            if (pClaseTipoCambioOrigen == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ClaseTipoCambioOrigenDeConfiguracionGeneralEnEstadoNuloOTransitorio);

            }

            //relacion
            this.CodigoClaseTipoCambioOrigen = pClaseTipoCambioOrigen.CodigoClaseTipoCambio;
            this.ClaseTipoCambioOrigen = pClaseTipoCambioOrigen;
        }

        public void EstablecerImpuestoDeConfiguracionGeneral(Impuesto pImpuesto)
        {
            if (pImpuesto == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ImpuestoDeConfiguracionGeneralEnEstadoNuloOTransitorio);

            }

            //relacion
            this.CodigoImpuesto = pImpuesto.CodigoImpuesto;
            this.Impuesto = pImpuesto;
        }


        //Cliente Interno
        public void EstablecerClienteInternoDeConfiguracionGeneral(Cliente pClienteInterno)
        {
            if (pClienteInterno == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ClienteInternoDeConfiguracionGeneralEnEstadoNuloOTransitorio);

            }

            //relacion
            this.CodigoClienteInterno = pClienteInterno.CodigoCliente;
            this.ClienteInterno = pClienteInterno;
        }

        //Cliente Interno
        public void EstablecerTipoPrecioInventarioActualizableDeConfiguracionGeneral(TipoPrecioInventario pTipoPrecioInventarioActualizable)
        {
            if (pTipoPrecioInventarioActualizable == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoPrecioInventarioActualizableDeConfiguracionGeneralEnEstadoNuloOTransitorio);

            }

            //relacion
            this.CodigoTipoPrecioInventarioActualizable = pTipoPrecioInventarioActualizable.CodigoTipoPrecioInventario;
            this.TipoPrecioInventarioActualizable = pTipoPrecioInventarioActualizable;
        }        
    }
}