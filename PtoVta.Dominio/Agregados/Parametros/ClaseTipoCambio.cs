using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class ClaseTipoCambio:Entidad
    {
        bool _EsHabilitado;

        HashSet<TipoDeCambio> _lineasTipoDeCambio;
 
        public string CodigoClaseTipoCambio { get; set; }
        public string DescripcionClaseTipoCambio { get; set; }

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

        public virtual ICollection<TipoDeCambio> TiposDeCambio
        {
            get
            {
                if (_lineasTipoDeCambio == null)
                    _lineasTipoDeCambio = new HashSet<TipoDeCambio>();

                return _lineasTipoDeCambio;
            }

            set
            {
                _lineasTipoDeCambio = new HashSet<TipoDeCambio>(value);
            }
        }



        public TipoDeCambio AgregarNuevoTipoDeCambio(
                DateTime pFechaTipoDeCambio, decimal pMontoTipoDeCambio, string pOperador, string pUsuarioSistema,
                string pCodigoMonedaDesde, string pCodigoMonedaHasta)
        {

            if (string.IsNullOrEmpty(pCodigoMonedaDesde.Trim())
                ||
                string.IsNullOrEmpty(pCodigoMonedaHasta.Trim())
                ||
                pFechaTipoDeCambio == null
                ||
                pMontoTipoDeCambio <= 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaTipoDeCambio);


            var nuevaLineaTipoDeCambio = new TipoDeCambio(pFechaTipoDeCambio, pMontoTipoDeCambio, pOperador, pUsuarioSistema, 
                                                        this.CodigoClaseTipoCambio, pCodigoMonedaDesde.Trim(), pCodigoMonedaHasta.Trim());

            nuevaLineaTipoDeCambio.GenerarNuevaIdentidad();

            this.TiposDeCambio.Add(nuevaLineaTipoDeCambio);

            return nuevaLineaTipoDeCambio;

        }
    }
}