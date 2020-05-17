using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class SubCategoriaArticulo : Entidad
    {
        public string CodigoSubCategoriaArticulo { get; set; }
        public string DescripcionSubCategoriaArticulo { get; set; }
        public decimal PorcentajeDiferencia { get; set; }


        public string CodigoCategoriaArticulo { get; set; }

        public string CodigoTipoMovInvFisIngreso { get; set; }
        public string CodigoTipoMovInvFisSalida { get; set; }

        public TipoMovimientoAlmacen TipoMovInvFisIngreso { get; private set; }
        public TipoMovimientoAlmacen TipoMovInvFisSalida { get; private  set; }



        public void EstablecerTipoMovInvFisIngresoDeSubCategoriaArticulo(TipoMovimientoAlmacen pTipoMovInvFisIngreso)
        {
            if (pTipoMovInvFisIngreso == null || pTipoMovInvFisIngreso.EsTransitorio())
            {
                throw new ArgumentException("Mensajes.excepcion_TipoMovAlmacenInvFisIngresoEnEstadoNuloOTransitorio");

            }

            //relacion
            this.CodigoTipoMovInvFisIngreso = pTipoMovInvFisIngreso.CodigoTipoMovimientoAlmacen;
            this.TipoMovInvFisIngreso = pTipoMovInvFisIngreso;
        }

        public void EstablecerReferenciaTipoMovInvFisIngresoDeSubCategoriaArticulo(string pCodigoTipoMovInvFisIngreso)
        {
            if (! string.IsNullOrEmpty(pCodigoTipoMovInvFisIngreso))
            {
                //relacion
                this.CodigoTipoMovInvFisIngreso = pCodigoTipoMovInvFisIngreso;
                this.TipoMovInvFisIngreso = null;
            }
        }

        public void EstablecerTipoMovInvFisSalidaDeSubCategoriaArticulo(TipoMovimientoAlmacen pTipoMovInvFisSalida)
        {
            if (pTipoMovInvFisSalida == null)
            {
                throw new ArgumentException("Mensajes.excepcion_TipoMovAlmacenInvFisSalidaEnEstadoNuloOTransitorio");

            }

            //relacion
            this.CodigoTipoMovInvFisSalida = pTipoMovInvFisSalida.CodigoTipoMovimientoAlmacen;
            this.TipoMovInvFisSalida = pTipoMovInvFisSalida;
        }

        public void EstablecerReferenciaTipoMovInvFisSalidaDeSubCategoriaArticulo(string pCodigoTipoMovInvFisSalida)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoMovInvFisSalida))
            {
                //relacion
                this.CodigoTipoMovInvFisSalida = pCodigoTipoMovInvFisSalida;
                this.TipoMovInvFisSalida = null;
            }
        }
    }
}    