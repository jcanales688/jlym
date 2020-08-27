using System;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;
// using PtoVta.Dominio.Agregados.

namespace PtoVta.Dominio.Agregados.Modulo
{
    public class DerechoAccesoUsuario: Entidad
    {
        public int DerechoConsultar { get; set; }   
        public int DerechoInsertar { get; set; }
        public int DerechoActualizar { get; set; }
        public int DerechoEliminar { get; set; }
        public int DerechoImprimir { get; set; }
        public int DerechoAnular { get; set; }
        public int DerechoEmitir { get; set; }

        public string CodigoVentanaUsuario { get;  set; }
        // public Guid VentanaUsuarioId { get; set; }

        public string CodigoUsuarioSistema { get; set; }
        // public Guid UsuarioSistemaId { get; set; }


        public UsuarioSistema UsuarioSistema { get; private set; }



        //Usuario
        public void EstablecerUsuarioSistemaDeDerechoAccesoUsuario(UsuarioSistema pUsuarioSistema)
        {
            if (pUsuarioSistema == null)
            {
                throw new ArgumentException(Mensajes.excepcion_NoSePuedeAsociarUsuarioSistemaTransitorioONulo);

            }

            //relacion
            this.CodigoUsuarioSistema = pUsuarioSistema.CodigoUsuarioDeSistema;
            this.UsuarioSistema = pUsuarioSistema;
        }

        public void EstablecerReferenciaUsuarioSistemaDeDerechoAccesoUsuario(string pCodigoUsuarioSistema)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioSistema))
            {
                //relacion 
                this.CodigoUsuarioSistema = pCodigoUsuarioSistema.Trim();
                this.UsuarioSistema = null;
            }
        }
    }
}
