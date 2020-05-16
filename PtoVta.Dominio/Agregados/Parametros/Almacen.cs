using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{

    public class Almacen : Entidad
        //, IValidatableObject
    {
        bool _EsHabilitado;



        public string CodigoAlmacen { get; set; }

        public string DescripcionAlmacen { get; set; }
        public string DireccionPrincipal { get; set; }
        public string DireccionAlterno { get; set; }
        public string TelefonoPrincipal { get; set; }
        public string TelefonoAlterno { get; set; }
        public string Fax { get; set; }
        public string Responsable { get; set; }
        public string UsuarioSistemaCrea { get; set; }

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

        public Almacen() { }

 

    }
}