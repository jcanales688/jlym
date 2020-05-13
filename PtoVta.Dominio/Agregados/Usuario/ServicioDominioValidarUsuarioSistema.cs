using System;
using System.Linq;
using PtoVta.Dominio.Agregados.Modulo;

namespace PtoVta.Dominio.Agregados.Usuario
{
    public class ServicioDominioValidarUsuarioSistema: IServicioDominioValidarUsuarioSistema
    {
        public bool ValidarUsuarioSistema(  UsuarioSistema pUsuarioSistema,
                                             ModuloSistema pModuloSistema, 
                                            string pClave)
        {

            bool esUsuarioSistemaValido = false;

            //Clave Invalida
            if (String.IsNullOrEmpty(pClave))
                throw new InvalidOperationException("Clave nula o vacia.");

            //Modulo sistema no existe
            if (pModuloSistema == null)
                throw new InvalidOperationException("No existen modulos de sistema asignados al usuario.");                


            var ventanaConDerechos = pModuloSistema.VentanasUsuario.FirstOrDefault(c => c.DerechosAccesoUsuario.Count > 0);

            if (pUsuarioSistema == null)
            {
                //Usuario no existe
                throw new InvalidOperationException("Vendedor sin Usuario de Sistema asignado.");
            }
            else
            {
                if (!(pUsuarioSistema.EsHabilitado))
                {
                    //Usuario Inactivo
                    throw new InvalidOperationException("Usuario de Sistema de vendedor inactivo.");
                }

                if (pModuloSistema.VentanasUsuario.Count == 0)
                {
                    //Usuario de  sistema sin privilegios
                    throw new InvalidOperationException("Usuario de Sistema de vendedor sin privilegios asignados.");
                }


                if (ventanaConDerechos == null)
                {
                    //Usuario de  sistema sin privilegios
                    throw new InvalidOperationException("Usuario de Sistema de vendedor sin privilegios.");
                }
            }

            esUsuarioSistemaValido = true;

            return esUsuarioSistemaValido;
        }
    }
}
