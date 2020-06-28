using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class TipoImpresora : Entidad
    {
        bool _EsHabilitado;

        public string CodigoTipoImpresora { get; set; }
        public string DescripcionImpresora { get; set; }
        public Nullable<int> SaltarLineas { get; set; }
        public string CodigoCortarPapel { get; set; }
        public Nullable<int> LineasDeAncho { get; set; }
        public string CodigoAbrirGaveta { get; set; }
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
    }
}