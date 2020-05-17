using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class CategoriaArticulo : Entidad
    {
        HashSet<SubCategoriaArticulo> _lineasSubCategoriaArticulo;

        bool _EsHabilitado;

        public string CodigoCategoriaArticulo { get; set; }
        public string DescripcionCategoriaArticulo { get; set; }
        public string CodigoContable { get; set; }
        public string Comentario { get; set; }


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

        public string CodigoTipoNegocio { get; private set; }
        public virtual TipoNegocio TipoNegocio { get; private set; }

        public virtual ICollection<SubCategoriaArticulo> SubCategoriasArticulo 
        {
            get
            {
                if (_lineasSubCategoriaArticulo == null)
                    _lineasSubCategoriaArticulo = new HashSet<SubCategoriaArticulo>();

                return _lineasSubCategoriaArticulo;
            }
            set
            {
                _lineasSubCategoriaArticulo = new HashSet<SubCategoriaArticulo>(value);
            }
        }

        public SubCategoriaArticulo AgregarNuevaSubCategoriaArticulo(
                    string pDescripcionSubCategoriaArticulo, decimal pPorcentajeDiferencia, 
                    string pCodigoTipoMovInvFisIngreso, string pCodigoTipoMovInvFisSalida)
        {
            if (string.IsNullOrEmpty(pCodigoTipoMovInvFisIngreso)
                ||
                string.IsNullOrEmpty(pCodigoTipoMovInvFisSalida)
                ||
                String.IsNullOrWhiteSpace(pDescripcionSubCategoriaArticulo)
                )
                throw new ArgumentException("Mensajes.excepcion_DatosNoValidosParaLineaSubCategoriaArticulo");



            var nuevaLineaSubCategoriaArticulo = new SubCategoriaArticulo()
            {
                CodigoCategoriaArticulo = this.CodigoCategoriaArticulo,
                CodigoTipoMovInvFisIngreso = pCodigoTipoMovInvFisIngreso,
                CodigoTipoMovInvFisSalida = pCodigoTipoMovInvFisSalida,
                //IdSubCategoriaArticulo = pIdSubCategoriaArticulo,
                DescripcionSubCategoriaArticulo = pDescripcionSubCategoriaArticulo,
                PorcentajeDiferencia = pPorcentajeDiferencia
            };

            //Establecer la identidad
            nuevaLineaSubCategoriaArticulo.GenerarNuevaIdentidad();

            this.SubCategoriasArticulo.Add(nuevaLineaSubCategoriaArticulo);

            return nuevaLineaSubCategoriaArticulo;
        }

        public void EstablecerTipoNegocioDeCategoriaArticulo(TipoNegocio pTipoNegocio)
        {
            if (pTipoNegocio == null)
            {
                throw new ArgumentException("Mensajes.excepcion_TipoNegocioDeCategoriaArticuloEnEstadoNuloOTransitorio");
            }

            //relacion
            this.CodigoTipoNegocio = pTipoNegocio.CodigoTipoNegocio;
            this.TipoNegocio = pTipoNegocio;
        }

        public void EstablecerReferenciaTipoNegocioDeCategoriaArticulo(string pCodigoTipoNegocio)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoNegocio))
            {
                //relacion
                this.CodigoTipoNegocio = pCodigoTipoNegocio;
                this.TipoNegocio = null;
            }
        }
    }
}    