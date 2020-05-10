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
                throw new InvalidOperationException("Mensajes.validacion_ModuloSistemaNuloEnServicioDominioValidarUsuarioSistema");

            //Modulo sistema no existe
            if (pModuloSistema == null)
                throw new InvalidOperationException("Mensajes.validacion_ModuloSistemaNuloEnServicioDominioValidarUsuarioSistema");                


            var ventanaConDerechos = pModuloSistema.VentanasUsuario.FirstOrDefault(c => c.DerechosAccesoUsuario.Count > 0);

            if (pUsuarioSistema == null)
            {
                //Usuario no existe
                throw new InvalidOperationException("Mensajes.validacion_VendedorSinUsuarioSistemaEnServicioDominioValidarUsuarioSistema");
            }
            else
            {
                if (!(pUsuarioSistema.EsHabilitado))
                {
                    //Usuario Inactivo
                    throw new InvalidOperationException("Mensajes.validacion_UsuarioSistemaDeVendedorInactivoEnServicioDominioValidarUsuarioSistema");
                }

                if (pModuloSistema.VentanasUsuario.Count == 0)
                {
                    //Usuario de  sistema sin privilegios
                    throw new InvalidOperationException("Mensajes.validacion_UsuarioSistemaDeVendedorSinPrivilegiosEnServicioDominioValidarUsuarioSistema");
                }


                if (ventanaConDerechos == null)
                {
                    //Usuario de  sistema sin privilegios
                    throw new InvalidOperationException("Mensajes.validacion_VentanaDeSistemaSinPrivilegiosEnServicioDominioValidarUsuarioSistema");
                }
            }

            esUsuarioSistemaValido = true;

            return esUsuarioSistemaValido;
        }
    }
}
