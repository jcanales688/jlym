using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class Impuesto : Entidad
    {
        bool _EsHabilitado;


        public string CodigoImpuesto { get; set; }
        public string DescripcionImpuesto { get; set; }
        public decimal Valor { get; set; }

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