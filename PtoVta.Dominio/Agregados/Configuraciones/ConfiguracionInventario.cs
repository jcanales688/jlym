using System;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public class ConfiguracionInventario  : Entidad
    {
        public string PeriodoInventario { get; set; }
        public string RutaReportesInventario { get; set; }
        public int PermitirStockNegativo { get; set; }
        public decimal MaximoRedondeoInventario { get; set; }

        public string CodigoTMAIngresoTransferencia { get; private set; }
        public string CodigoTMASalidaTransferencia { get; private set; }
        public string CodigoTMACompraTienda { get; private set; }
        public string CodigoTMACompraPlaya { get; private set; }
        public string CodigoTMAReversaCompraPlaya { get; private set; }
        public string CodigoTMAReversaCompraTienda { get; private set; }
        public string CodigoTMAVentas { get; private set; }
        public string CodigoProveedorDefault { get; private set; }  
        public string CodigoArticuloRedondeoInventario { get; private set; }


        public virtual TipoMovimientoAlmacen TMAIngresoTransferencia { get; private set; }
        public virtual TipoMovimientoAlmacen TMASalidaTransferencia { get; private set; }
        public virtual TipoMovimientoAlmacen TMACompraTienda { get; private set; }
        public virtual TipoMovimientoAlmacen TMACompraPlaya { get; private set; }
        public virtual TipoMovimientoAlmacen TMAReversaCompraPlaya { get; private set; }
        public virtual TipoMovimientoAlmacen TMAReversaCompraTienda { get; private set; }
        public virtual TipoMovimientoAlmacen TMAVentas { get; private set; }
        public virtual Articulo ArticuloRedondeoInventario { get; private set; }



        public void EstablecerReferenciaTMAIngresoTransferenciaDeConfiguracionInventario(string pCodigoTMAIngresoTransferencia)
        {
            if (!string.IsNullOrEmpty(pCodigoTMAIngresoTransferencia))
            {
                this.CodigoTMAIngresoTransferencia = pCodigoTMAIngresoTransferencia;
                // this.TMAIngresoTransferencia = null;
            }
        }

        public void EstablecerReferenciaTMASalidaTransferenciaDeConfiguracionInventario(string pCodigoTMASalidaTransferencia)
        {
            if (!string.IsNullOrEmpty(pCodigoTMASalidaTransferencia))
            {
                this.CodigoTMASalidaTransferencia = pCodigoTMASalidaTransferencia;
                // this.TMASalidaTransferencia = null;
            }
        }

        public void EstablecerReferenciaTMACompraTiendaDeConfiguracionInventario(string pCodigoTMACompraTienda)
        {
            if (!string.IsNullOrEmpty(pCodigoTMACompraTienda))
            {
                this.CodigoTMACompraTienda = pCodigoTMACompraTienda;
                // this.TMACompraTienda = null;
            }
        }

        public void EstablecerReferenciaTMACompraPlayaDeConfiguracionInventario(string pCodigoTMACompraPlaya)
        {
            if (!string.IsNullOrEmpty(pCodigoTMACompraPlaya))
            {
                this.CodigoTMACompraPlaya = pCodigoTMACompraPlaya;
                // this.TMACompraPlaya = null;
            }
        }

        public void EstablecerReferenciaTMAReversaCompraPlayaDeConfiguracionInventario(string pCodigoTMAReversaCompraPlaya)
        {
            if (!string.IsNullOrEmpty(pCodigoTMAReversaCompraPlaya))
            {
                this.CodigoTMAReversaCompraPlaya = pCodigoTMAReversaCompraPlaya;
                // this.TMAReversaCompraPlaya = null;
            }
        }

        public void EstablecerReferenciaTMAReversaCompraTiendaDeConfiguracionInventario(string pCodigoTMAReversaCompraTienda)
        {
            if (!string.IsNullOrEmpty(pCodigoTMAReversaCompraTienda))
            {
                this.CodigoTMAReversaCompraTienda = pCodigoTMAReversaCompraTienda;
                // this.TMAReversaCompraTienda = null;
            }
        }

        public void EstablecerReferenciaTMAVentasDeConfiguracionInventario(string pCodigoTMAVentas)
        {
            if (!string.IsNullOrEmpty(pCodigoTMAVentas))
            {
                this.CodigoTMAVentas = pCodigoTMAVentas;
                // this.TMAVentas = null;
            }
        }

        public void EstablecerReferenciaArtRedondeoInvDeConfiguracionInventario(string pCodigoArticuloRedondeoInventario)
        {
            if (!string.IsNullOrEmpty(pCodigoArticuloRedondeoInventario))
            {
                this.CodigoArticuloRedondeoInventario = pCodigoArticuloRedondeoInventario;
                // this.ArticuloRedondeoInventario = null;
            }
        }        
    }
}