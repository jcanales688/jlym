using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class EstadoDocumento : Entidad
    {
        bool _EsHabilitado;

        public string CodigoEstadoDocumento { get; set; }
        public string DescripcionEstadoDocumento { get; set; }
        public string AbreviaturaEstadoDocumento { get; set; }

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
