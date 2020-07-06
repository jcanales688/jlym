using System;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class Distrito : Entidad
    {
      bool _EsHabilitado;

        public string CodigoDistrito { get; set; }
        public string DescripcionDistrito { get; set; }

        public string CodigoDepartamento { get; set; }


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


       public Distrito() { }

       public Distrito(string pCodigoDistrito, string pDescripcionDistrito) 
       {
           if (String.IsNullOrWhiteSpace(pCodigoDistrito))
               throw new ArgumentNullException(Mensajes.validacion_CodigoDistritoDeDistritoVacioONulo);

           if (String.IsNullOrWhiteSpace(pDescripcionDistrito))
               throw new ArgumentNullException(Mensajes.validacion_DescripcionDistritoDeDistritoVacioONulo);


           this.CodigoDistrito = pCodigoDistrito;
           this.DescripcionDistrito = pDescripcionDistrito;
       }
    }
}