using System;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class Pais: Entidad
    {
       bool _EsHabilitado;

       public string CodigoPais { get; set; }
       public string DescripcionPais { get; set; }

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


       public Pais() { }

       public Pais(string pCodigoPais, string pDescripcionPais) 
        {
            if (String.IsNullOrWhiteSpace(pCodigoPais))
                throw new ArgumentNullException(Mensajes.validacion_CodigoPaisDePaisVacioONulo);

            if (String.IsNullOrWhiteSpace(pDescripcionPais))
                throw new ArgumentNullException(Mensajes.validacion_DescripcionPaisDePaisVacioONulo);

            this.CodigoPais  = pCodigoPais;
            this.DescripcionPais = pDescripcionPais;
        }        
    }
}