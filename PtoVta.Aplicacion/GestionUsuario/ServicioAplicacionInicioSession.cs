using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.Transversales.Autenticacion;
using PtoVta.Infraestructura.Transversales.Log;
using PtoVta.Infraestructura.Transversales.Comun;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.GestionUsuario
{
    public class ServicioAplicacionInicioSession:IServicioAplicacionInicioSession
    {
        private IRepositorioModuloSistema _IRepositorioModuloSistema;
        private IRepositorioUsuarioSistema _IRepositorioUsuarioSistema;      
        private IRepositorioVendedor _IRepositorioVendedor;

        private IAutenticacion _IAutenticacion;
        private IServicioDominioValidarUsuarioSistema _IServicioDominioValidarUsuarioSistema;
        private IServicioDominioValidarUsuarioVendedor _IServicioDominioValidarUsuarioVendedor;        

        public ServicioAplicacionInicioSession(
                                        IRepositorioModuloSistema pIRepositorioModuloSistema,
                                        IRepositorioUsuarioSistema pIRepositorioUsuarioSistema,
                                        IRepositorioVendedor pIRepositorioVendedor,

                                        IAutenticacion pIAutenticacion,                                        
                                        IServicioDominioValidarUsuarioSistema pIServicioDominioValidarUsuarioSistema,
                                        IServicioDominioValidarUsuarioVendedor pIServicioDominioValidarUsuarioVendedor

            )
        {

            if (pIRepositorioModuloSistema == null)
                throw new ArgumentNullException("IRepositorioModuloSistema Nulo En ServicioAplicacionInicioSession");

            if (pIRepositorioUsuarioSistema == null)
                throw new ArgumentNullException("IRepositorioUsuarioSistema Nulo En ServicioAplicacionInicioSession");

            if (pIRepositorioVendedor == null)
                throw new ArgumentNullException("IRepositorioVendedor Nulo En ServicioAplicacionInicioSession");

            if (pIAutenticacion == null)
                throw new ArgumentNullException("IValidadorInicioSesion Nulo En ServicioAplicacionInicioSession");

            if (pIServicioDominioValidarUsuarioSistema == null)
                throw new ArgumentNullException("IServicioDominioValidarUsuarioSistema Nulo En ServicioAplicacionInicioSession");

            if (pIServicioDominioValidarUsuarioVendedor == null)
                throw new ArgumentNullException("IServicioDominioValidarUsuarioVendedor Nulo En ServicioAplicacionInicioSession");


            _IRepositorioModuloSistema = pIRepositorioModuloSistema;
            _IRepositorioUsuarioSistema = pIRepositorioUsuarioSistema;
            _IRepositorioVendedor = pIRepositorioVendedor;

            _IAutenticacion = pIAutenticacion;
            _IServicioDominioValidarUsuarioSistema = pIServicioDominioValidarUsuarioSistema;
            _IServicioDominioValidarUsuarioVendedor = pIServicioDominioValidarUsuarioVendedor;
                
        }

        public ResultadoServicio<ModuloSistemaDTO> GestionInicioSesion(string pUsuario, string pClave, string pCodigoModuloSistema)
        {
            UsuarioSistema usuarioSistemaAcceso;
            ModuloSistema accesosModuloSistema = null;
            bool esUsuarioSistemaAccesoValido = false;
            bool esUsuarioDelVendedorValido = false;
            string mensajeValidacion = string.Empty;

            if (string.IsNullOrEmpty(pUsuario) || string.IsNullOrEmpty(pClave))
                throw new ArgumentException(Mensajes.advertencia_UsuarioOClaveNula);

            //Validamos Usuario Vendedor 
            Vendedor vendedorLogueado = _IRepositorioVendedor.ObtenerVendedorPorUsuario(pUsuario);

            if (vendedorLogueado != null)
            {
                //Desecriptar clave
                var claveDesencriptada = EncriptacionBasica.EncriptarYDesencriptar(pClave.Trim().ToUpper());
                esUsuarioDelVendedorValido = _IServicioDominioValidarUsuarioVendedor
                                                    .ValidarUsuarioVendedor(vendedorLogueado, claveDesencriptada.Trim());

                if (esUsuarioDelVendedorValido)
                {
                    usuarioSistemaAcceso = vendedorLogueado.UsuarioSistemaAcceso;
                }
                else
                {
                    usuarioSistemaAcceso = null;
                    mensajeValidacion = Mensajes.advertencia_UsuarioVendedorInvalido;
                }

            }
            else
            {
                //Para los que inician session como otros usuarios diferentes a Vendedores
                usuarioSistemaAcceso = _IRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario(pUsuario, pClave);
            }


            //Usaurio Sistema Valido
            if (usuarioSistemaAcceso != null)
            {
                //Obtenemos privilegios del usuario
                accesosModuloSistema = _IRepositorioModuloSistema
                    .ObtenerDerechosAccesosUsuario(usuarioSistemaAcceso.CodigoUsuarioDeSistema.Trim(), pCodigoModuloSistema.Trim());

                esUsuarioSistemaAccesoValido = _IServicioDominioValidarUsuarioSistema
                                                    .ValidarUsuarioSistema(usuarioSistemaAcceso, accesosModuloSistema, pClave.Trim());

                if (!(esUsuarioSistemaAccesoValido))
                    mensajeValidacion = Mensajes.advertencia_UsuarioSistemaDeVendedorInvalido;

            }
            else
                mensajeValidacion = Mensajes.advertencia_UsuarioSistemaInvalido;


            //Validamos usuario en el dominio (PENDIENTE de implementar de una manera orientada a objetos), ej. Active Directory
            // if (esUsuarioSistemaAccesoValido)
            //     esInicioSesionValido = _IAutenticacion.ValidarInicioSesion(string.Empty, string.Empty);


            //Devolucion
            if ((esUsuarioSistemaAccesoValido == true) && 
                (accesosModuloSistema != null))
            {
                mensajeValidacion = Mensajes.advertencia_UsuarioValido;
                // LogFactory.CrearLog().LogError(mensajeValidacion);
                
                return new ResultadoServicio<ModuloSistemaDTO>(7,mensajeValidacion,
                        string.Empty, accesosModuloSistema.ProyectadoComo<ModuloSistemaDTO>(), null);
            }
            else
            {
                mensajeValidacion = Mensajes.advertencia_UsuarioSistemaInvalidoYSinDerechos;
                LogFactory.CrearLog().LogError(mensajeValidacion);

                return new ResultadoServicio<ModuloSistemaDTO>(0,mensajeValidacion,
                                                                    string.Empty, null, null);
            }
        }


        // public void Dispose()
        // {
            // _IRepositorioModuloSistema.Dispose();
            // _IRepositorioUsuarioSistema.Dispose();
            // _IRepositorioVendedor.Dispose();

            // _IAutenticacion.Dispose();
            //_IServicioDominioValidarUsuarioSistema.Dispose();
            //_IServicioDominioValidarUsuarioVendedor.Dispose();
        // }        
    }
}
