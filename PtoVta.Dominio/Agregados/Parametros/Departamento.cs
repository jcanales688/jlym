using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class Departamento : Entidad
    {
        HashSet<Distrito> _distritos;

        bool _EsHabilitado;

        public string CodigoDepartamento { get; set; }
        public string DescripcionDepartamento{ get; set; }

        // public string CodigoPais{ get; set; }


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

        public virtual ICollection<Distrito> Distritos
        {
            get
            {
                if (_distritos == null)
                    _distritos = new HashSet<Distrito>();

                return _distritos;
            }
            set
            {
                _distritos = new HashSet<Distrito>(value);
            }
        }

        public Distrito AgregarNuevoDistrito(string pCodigoDistrito, string pDescripcionDistrito)
        {
           if (String.IsNullOrWhiteSpace(pCodigoDistrito)
               ||
               String.IsNullOrWhiteSpace(pDescripcionDistrito))
               throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaDistrito);

           var nuevaLineaDistrito = new Distrito()
           {
               CodigoDepartamento = this.CodigoDepartamento,
               CodigoDistrito = pCodigoDistrito,
               DescripcionDistrito = pDescripcionDistrito
           };

           //Establecer la identidad
           nuevaLineaDistrito.GenerarNuevaIdentidad();

           this.Distritos.Add(nuevaLineaDistrito);

           return nuevaLineaDistrito;
        }

       public Departamento() { }

       public Departamento(string pCodigoDepartamento, string pDescripcionDepartamento) 
       {
           if (String.IsNullOrWhiteSpace(pCodigoDepartamento))
               throw new ArgumentNullException(Mensajes.validacion_CodigoDepartamentoDeDepartamentoONulo);

           if (String.IsNullOrWhiteSpace(pDescripcionDepartamento))
               throw new ArgumentNullException(Mensajes.validacion_DescripcionDepartamentoDeDepartamentoVacioONulo);


           this.CodigoDepartamento = pCodigoDepartamento;
           this.DescripcionDepartamento = pDescripcionDepartamento;


       }
    }
}