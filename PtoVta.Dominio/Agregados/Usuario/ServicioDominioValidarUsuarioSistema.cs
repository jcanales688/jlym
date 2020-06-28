using System;
using System.Linq;
using PtoVta.Dominio.Agregados.Modulo;
using static PtoVta.Dominio.BaseTrabajo.Globales.MensajesDominio;

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
                throw new InvalidOperationException(Mensajes.validacion_ClaveNulaOVacia);

            //Modulo sistema no existe
            if (pModuloSistema == null)
                throw new InvalidOperationException(Mensajes.validacion_NoExistenModulosDeSistemaAsignadosAlUsuario);                


            var ventanaConDerechos = pModuloSistema.VentanasUsuario.FirstOrDefault(c => c.DerechosAccesoUsuario.Count > 0);

            if (pUsuarioSistema == null)
            {
                //Usuario no existe
                throw new InvalidOperationException(Mensajes.excepcion_VendedorsinUsuarioDeSistemaAsignado);
            }
            else
            {
                if (!(pUsuarioSistema.EsHabilitado))
                {
                    //Usuario Inactivo
                    throw new InvalidOperationException(Mensajes.excepcion_UsuarioDeSistemaDeVendedorInactivo);
                }

                if (pModuloSistema.VentanasUsuario.Count == 0)
                {
                    //Usuario de  sistema sin privilegios
                    throw new InvalidOperationException(Mensajes.excepcion_UsuarioDeSistemaDeVendedorSinPrivilegiosAsignados);
                }


                if (ventanaConDerechos == null)
                {
                    //Usuario de  sistema sin privilegios
                    throw new InvalidOperationException(Mensajes.excepcion_UsuarioDeSistemaDeVendedorSinPrivilegios);
                }
            }

            esUsuarioSistemaValido = true;

            return esUsuarioSistemaValido;
        }
    }
}
