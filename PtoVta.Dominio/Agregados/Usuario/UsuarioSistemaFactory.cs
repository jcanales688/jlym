using System;

namespace PtoVta.Dominio.Agregados.Usuario
{
    public  static class UsuarioSistemaFactory
    {
        public static UsuarioSistema CrearUsuarioSistema(string pUsuarioDeSistema, DateTime pFechaExpiracion,
                                                        //int pEstadoUsuario, 
                                                        string pDescripcionUsuario, string pContraseña)
        {
            var usuarioSistema = new UsuarioSistema();

            usuarioSistema.GenerarNuevaIdentidad();

            usuarioSistema.CodigoUsuarioDeSistema = pUsuarioDeSistema;
            usuarioSistema.FechaExpiracion = pFechaExpiracion;
            //usuarioSistema.EstadoUsuario = pEstadoUsuario;
            usuarioSistema.DescripcionUsuario = pDescripcionUsuario;
            usuarioSistema.Contraseña = pContraseña;

            usuarioSistema.Habilitar();


            return usuarioSistema;

        }        
    }
}
