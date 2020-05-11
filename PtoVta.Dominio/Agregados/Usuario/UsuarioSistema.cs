using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Usuario
{
    public class UsuarioSistema: Entidad
    {
        bool _EsHabilitado;

        public string CodigoUsuarioDeSistema { get; set; }

        // public string UsuarioDeSistema { get; set; }
        public DateTime FechaExpiracion { get; set; }
     
        public string DescripcionUsuario { get; set; }
        public string Contraseña { get; set; }

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
