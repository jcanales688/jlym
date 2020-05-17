using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class TipoNegocio : Entidad
    {
        bool _EsHabilitado;



        public string CodigoTipoNegocio { get; set; }
        public string DescripcionTipoNegocio { get; set; }
  

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

        public TipoNegocio() { }

        public TipoNegocio(string pCodigoTipoNegocio, string pDescripcionTipoNegocio)
        {
            if (String.IsNullOrWhiteSpace(pCodigoTipoNegocio))
                throw new ArgumentNullException("Mensajes.validacion_CodigoTipoNegocioDeTipoNegocioVacioONulo");

            if (String.IsNullOrWhiteSpace(pDescripcionTipoNegocio))
                throw new ArgumentNullException("Mensajes.validacion_DescripcionTipoNegocioDeTipoNegocioVacioONulo");

            this.CodigoTipoNegocio = pCodigoTipoNegocio;
            this.DescripcionTipoNegocio = pDescripcionTipoNegocio;
        }
    }
}
    