using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.Transversales.Autenticacion;
using PtoVta.Infraestructura.Transversales.Log;

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
                throw new ArgumentException("No Se Puede Validar Inicio Sesion Con Usuario O Clave Nula");

            //Validamos Usuario Vendedor 
            Vendedor vendedorLogueado = _IRepositorioVendedor.ObtenerVendedorPorUsuario(pUsuario);

            if (vendedorLogueado != null)
            {
                esUsuarioDelVendedorValido = _IServicioDominioValidarUsuarioVendedor.ValidarUsuarioVendedor(vendedorLogueado, pClave);

                if (esUsuarioDelVendedorValido)
                {
                    usuarioSistemaAcceso = vendedorLogueado.UsuarioSistemaAcceso;
                }
                else
                {
                    usuarioSistemaAcceso = null;
                    mensajeValidacion = "Usuario De Vendedor Invalido";
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
                    mensajeValidacion = "Usuario Sistema De Vendedor Invalido";

            }
            else
                mensajeValidacion = "Usuario Sistema Invalido";


            //Validamos usuario en el dominio (PENDIENTE de implementar de una manera orientada a objetos), ej. Active Directory
            // if (esUsuarioSistemaAccesoValido)
            //     esInicioSesionValido = _IAutenticacion.ValidarInicioSesion(string.Empty, string.Empty);


            //Devolucion
            if ((esUsuarioSistemaAccesoValido == true) && 
                (accesosModuloSistema != null))
            {
                mensajeValidacion = "Usuario Valido";
                LogFactory.CrearLog().LogError(mensajeValidacion);
                
                return new ResultadoServicio<ModuloSistemaDTO>(7,mensajeValidacion,
                        string.Empty, accesosModuloSistema.ProyectadoComo<ModuloSistemaDTO>());
            }
            else
            {
                mensajeValidacion = "Usuario Sistema Invalido Y Sin Derechos";
                LogFactory.CrearLog().LogError(mensajeValidacion);

                return new ResultadoServicio<ModuloSistemaDTO>(0,mensajeValidacion,
                                                                    string.Empty, null);
            }

        }


        public void Dispose()
        {
            _IRepositorioModuloSistema.Dispose();
            _IRepositorioUsuarioSistema.Dispose();
            _IRepositorioVendedor.Dispose();

            _IAutenticacion.Dispose();
            //_IServicioDominioValidarUsuarioSistema.Dispose();
            //_IServicioDominioValidarUsuarioVendedor.Dispose();
        }        
    }
}
