using System;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class TipoMovimientoAlmacen : Entidad
    {
        bool _EsHabilitado;

        public string CodigoTipoMovimientoAlmacen { get; set; }
        public string DescripcionTipoMovimientoAlmacen { get; set; }
        public int IngresoOSalida { get; set; }
        public int EsValorizado { get; set; }
        public int ValorizadoPorPrecioVentaOCostoReposicion { get; set; }
        public int ValorizadoPorCostoPromedio { get; set; }
        public int EsTipoIngresoPorCompra { get; set; }
        public int RequiereProveedor { get; set; }
        public int EnCalculoCostoPromedio { get; set; }
        public string DescripcionAbreviada { get; set; }


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

        public TipoMovimientoAlmacen() { }

        public TipoMovimientoAlmacen(string pCodigoTipoMovimientoAlmacen, string pDescripcionTipoMovimientoAlmacen, int pIngresoOSalida, int pEsValorizado,
                                        int pValorizadoPorPrecioVoCostoRep, int pCostoPromedio, int pEsTipoIngresoPorCompra, int pRequiereProveedor,
                                        int pEnCalculoCostoPromedio, string pDescripcionAbreviada)
        {


            if (String.IsNullOrWhiteSpace(pCodigoTipoMovimientoAlmacen))
                throw new ArgumentNullException(Mensajes.validacion_CodigoTipoMovimientoAlmacenDeTipoMovimientoAlmacenVacioONulo);

            if (String.IsNullOrWhiteSpace(pDescripcionTipoMovimientoAlmacen))
                throw new ArgumentNullException(Mensajes.validacion_DescripcionTipoMovimientoAlmacenDeTipoMovimientoAlmacenVacioONulo);

            if (pIngresoOSalida < 0)
                throw new ArgumentNullException(Mensajes.validacion_IngresoOSalidaDeTipoMovimientoAlmacenMenorACero);

            if (pEsValorizado < 0)
                throw new ArgumentNullException(Mensajes.validacion_EsValorizadoDeTipoMovimientoAlmacenMenorACero);


            if (pValorizadoPorPrecioVoCostoRep < 0)
                throw new ArgumentNullException(Mensajes.validacion_ValorizadoPorPrecioVoCostoRepDeTipoMovimientoAlmacenMenorACero);

            if (pCostoPromedio < 0)
                throw new ArgumentNullException(Mensajes.validacion_CostoPromedioDeTipoMovimientoAlmacenMenorACero);

            if (pEsTipoIngresoPorCompra < 0)
                throw new ArgumentNullException(Mensajes.validacion_EsTipoIngresoPorCompraDeTipoMovimientoAlmacenMenorACero);

            if (pRequiereProveedor < 0)
                throw new ArgumentNullException(Mensajes.validacion_RequiereProveedorDeTipoMovimientoAlmacenMenorACero);

            if (pEnCalculoCostoPromedio < 0)
                throw new ArgumentNullException(Mensajes.validacion_EnCalculoCostoPromedioDeTipoMovimientoAlmacenMenorACero);

            if (String.IsNullOrWhiteSpace(pDescripcionAbreviada))
                throw new ArgumentNullException(Mensajes.validacion_DescripcionAbreviadaDeTipoMovimientoAlmacenVacioONulo);

            //if (pEstado < 0)
            //    throw new ArgumentNullException("Estado");

            this.CodigoTipoMovimientoAlmacen = pCodigoTipoMovimientoAlmacen;
            this.DescripcionTipoMovimientoAlmacen = pDescripcionTipoMovimientoAlmacen;
            this.IngresoOSalida = pIngresoOSalida;
            this.EsValorizado = pEsValorizado;
            this.ValorizadoPorPrecioVentaOCostoReposicion = pValorizadoPorPrecioVoCostoRep;
            this.ValorizadoPorCostoPromedio = pCostoPromedio;
            this.EsTipoIngresoPorCompra = pEsTipoIngresoPorCompra;
            this.RequiereProveedor = pRequiereProveedor;
            this.EnCalculoCostoPromedio = pEnCalculoCostoPromedio;
            this.DescripcionAbreviada = pDescripcionAbreviada;

        }
    }
}