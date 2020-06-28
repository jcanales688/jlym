using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class Tarjeta : Entidad
    {
        bool _EsHabilitado;

        public string CodigoTarjeta { get; set; }

        public string Cuenta { get; set; }
        public string DescripcionTarjeta { get; set; }
        public int NumeroOrden { get; set; }
        public string AjusteTarjeta { get; set; }
        //public int EstadoTarjeta { get; set; }

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

    }
}