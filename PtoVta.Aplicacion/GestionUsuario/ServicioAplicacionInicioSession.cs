using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo.Validaciones;
using PtoVta.Infraestructura.Transversales.Log;

namespace PtoVta.Aplicacion.GestionUsuario
{
    public class ServicioAplicacionInicioSession:IServicioAplicacionInicioSession
    {
        private IRepositorioModuloSistema _IRepositorioModuloSistema;
        private IRepositorioUsuarioSistema _IRepositorioUsuarioSistema;      
        private IRepositorioVendedor _IRepositorioVendedor;

        private IValidadorInicioSesion _IValidadorInicioSesion;
        private IServicioDominioValidarUsuarioSistema _IServicioDominioValidarUsuarioSistema;
        private IServicioDominioValidarUsuarioVendedor _IServicioDominioValidarUsuarioVendedor;        

        public ServicioAplicacionInicioSession(
                                        IRepositorioModuloSistema pIRepositorioModuloSistema,
                                        IRepositorioUsuarioSistema pIRepositorioUsuarioSistema,
                                        IRepositorioVendedor pIRepositorioVendedor,

                                        IValidadorInicioSesion pIValidadorInicioSesion,                                        
                                        IServicioDominioValidarUsuarioSistema pIServicioDominioValidarUsuarioSistema,
                                        IServicioDominioValidarUsuarioVendedor pIServicioDominioValidarUsuarioVendedor

            )
        {

            if (pIRepositorioModuloSistema == null)
                throw new ArgumentNullException("Mensajes.excepcion_IRepositorioModuloSistemaNuloEnServicioAplicacionInicioSession");

            if (pIRepositorioUsuarioSistema == null)
                throw new ArgumentNullException("Mensajes.excepcion_IRepositorioUsuarioSistemaNuloEnServicioAplicacionInicioSession");

            if (pIRepositorioVendedor == null)
                throw new ArgumentNullException("Mensajes.excepcion_IRepositorioVendedorNuloEnServicioAplicacionInicioSession");

            if (pIValidadorInicioSesion == null)
                throw new ArgumentNullException("Mensajes.excepcion_IValidadorInicioSesionNuloEnServicioAplicacionInicioSession");

            if (pIServicioDominioValidarUsuarioSistema == null)
                throw new ArgumentNullException("Mensajes.excepcion_IServicioDominioValidarUsuarioSistemaNuloEnServicioAplicacionInicioSession");

            if (pIServicioDominioValidarUsuarioVendedor == null)
                throw new ArgumentNullException("Mensajes.excepcion_IServicioDominioValidarUsuarioVendedorNuloEnServicioAplicacionInicioSession");


            _IRepositorioModuloSistema = pIRepositorioModuloSistema;
            _IRepositorioUsuarioSistema = pIRepositorioUsuarioSistema;
            _IRepositorioVendedor = pIRepositorioVendedor;

            _IValidadorInicioSesion = pIValidadorInicioSesion;
            _IServicioDominioValidarUsuarioSistema = pIServicioDominioValidarUsuarioSistema;
            _IServicioDominioValidarUsuarioVendedor = pIServicioDominioValidarUsuarioVendedor;
                
        }

        public ModuloSistemaDTO GestionInicioSesion(string pUsuario, string pClave, Guid pModuloSistemaId)
        {
            UsuarioSistema usuarioSistemaAcceso;
            ModuloSistema accesosModuloSistema = null;
            bool esUsuarioSistemaAccesoValido = false;
            bool esUsuarioDelVendedorValido = false;
            bool esInicioSesionValido = false;

            if (string.IsNullOrEmpty(pUsuario) || string.IsNullOrEmpty(pClave))
                throw new ArgumentException("Mensajes.advertencia_NoSePuedeValidarInicioSesionConUsuarioOClaveNula");

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
                    LogFactory.CrearLog().LogError("Mensajes.excepcion_UsuarioDeVendedorInvalido");
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
                                                .ObtenerDerechosAccesosUsuario(usuarioSistemaAcceso.Id, pModuloSistemaId);

                esUsuarioSistemaAccesoValido = _IServicioDominioValidarUsuarioSistema
                                                .ValidarUsuarioSistema(usuarioSistemaAcceso, accesosModuloSistema, pClave);

                if (!(esUsuarioSistemaAccesoValido))
                    LogFactory.CrearLog().LogError("Mensajes.excepcion_UsuarioSistemaDeVendedorInvalido");

            }
            else
                LogFactory.CrearLog().LogError("Mensajes.excepcion_UsuarioSistemaInvalido");


            //Validamos usuario en el dominio (PENDIENTE de implementar de una manera orientada a objetos), ej. Active Directory
            if (esUsuarioSistemaAccesoValido)
                esInicioSesionValido = _IValidadorInicioSesion.ValidarInicioSesion(string.Empty, string.Empty);


            //Devolucion
            if ((esUsuarioSistemaAccesoValido == true) && 
                (accesosModuloSistema != null))
            {
                return accesosModuloSistema.ProyectadoComo<ModuloSistemaDTO>();
            }
            else
            {
                LogFactory.CrearLog().LogWarning("Mensajes.advertencia_UsuarioSistemaInvalidoYSinDerechos");

                return null;
            }

        }


        public void Dispose()
        {
            _IRepositorioModuloSistema.Dispose();
            _IRepositorioUsuarioSistema.Dispose();
            _IRepositorioVendedor.Dispose();

            _IValidadorInicioSesion.Dispose();
            //_IServicioDominioValidarUsuarioSistema.Dispose();
            //_IServicioDominioValidarUsuarioVendedor.Dispose();
        }        
    }
}
