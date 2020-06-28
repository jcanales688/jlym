using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class CondicionPago : Entidad
    {
        bool _EsHabilitado;

        public string CodigoCondicionPago { get; set; }
        public int DiasPago { get; set; }
        public string DescripcionCondicionPago { get; set; }
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