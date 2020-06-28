using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.MensajesDominio;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class TipoDocumento : Entidad
    {
        bool _EsHabilitado;

        HashSet<CorrelativoDocumento> _lineasCorrelativoDocumento;

        public string CodigoTipoDocumento { get; set; }

        public string DescripcionTipoDocumento { get; set; }
        public string Abreviatura { get; set; }

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

        public virtual ICollection<CorrelativoDocumento> CorrelativosDocumento 
        {
            get
            {
                if (_lineasCorrelativoDocumento == null)
                    _lineasCorrelativoDocumento = new HashSet<CorrelativoDocumento>();

                return _lineasCorrelativoDocumento;
            }

            set
            {
                _lineasCorrelativoDocumento = new HashSet<CorrelativoDocumento>(value);
            }
        }

        public CorrelativoDocumento AgregarNuevoCorrelativoDocumento(int pSerie, long pCorrelativo, 
                                        string pTipoDeVenta, int pEstado, string pCodigoAlmacen, string pCodigoConfiguracionPuntoVenta)
        {

            if (string.IsNullOrEmpty(pCodigoAlmacen)
                ||
                string.IsNullOrEmpty(pCodigoConfiguracionPuntoVenta)
                ||
                pSerie <= 0
                ||
                pCorrelativo <= 0
                ||
                pEstado < 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaCorrelativoDocumento);


            var nuevaLineaCorrelativoDocumento = new CorrelativoDocumento(this.CodigoTipoDocumento, pCodigoAlmacen, pCodigoConfiguracionPuntoVenta,
                                                                        pSerie, pCorrelativo, pTipoDeVenta, pEstado);


            nuevaLineaCorrelativoDocumento.GenerarNuevaIdentidad();

            this.CorrelativosDocumento.Add(nuevaLineaCorrelativoDocumento);

            return nuevaLineaCorrelativoDocumento;
        }
    


        public void ActualizaCorrelativoDocumento()
        {
            this.CorrelativosDocumento.FirstOrDefault().Correlativo++; 
        }

    }
}