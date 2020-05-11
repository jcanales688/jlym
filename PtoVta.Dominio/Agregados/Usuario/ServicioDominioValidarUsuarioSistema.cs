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
                throw new InvalidOperationException("ModuloSistema Nulo En ServicioDominioValidarUsuarioSistema");

            //Modulo sistema no existe
            if (pModuloSistema == null)
                throw new InvalidOperationException("ModuloSistemaNulo En ServicioDominioValidarUsuarioSistema");                


            var ventanaConDerechos = pModuloSistema.VentanasUsuario.FirstOrDefault(c => c.DerechosAccesoUsuario.Count > 0);

            if (pUsuarioSistema == null)
            {
                //Usuario no existe
                throw new InvalidOperationException("Vendedor Sin UsuarioSistema En ServicioDominioValidarUsuarioSistema");
            }
            else
            {
                if (!(pUsuarioSistema.EsHabilitado))
                {
                    //Usuario Inactivo
                    throw new InvalidOperationException("UsuarioSistema De Vendedor Inactivo En ServicioDominioValidarUsuarioSistema");
                }

                if (pModuloSistema.VentanasUsuario.Count == 0)
                {
                    //Usuario de  sistema sin privilegios
                    throw new InvalidOperationException("UsuarioSistema De Vendedor Sin Privilegios En ServicioDominioValidarUsuarioSistema");
                }


                if (ventanaConDerechos == null)
                {
                    //Usuario de  sistema sin privilegios
                    throw new InvalidOperationException("Ventana De Sistema Sin Privilegios En ServicioDominioValidarUsuarioSistema");
                }
            }

            esUsuarioSistemaValido = true;

            return esUsuarioSistemaValido;
        }
    }
}
