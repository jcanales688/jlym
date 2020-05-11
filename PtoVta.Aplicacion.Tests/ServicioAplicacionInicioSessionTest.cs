using System;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.GestionUsuario;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Modulo;
using PtoVta.Infraestructura.Repositorios.Usuario;
using PtoVta.Infraestructura.Transversales.Autenticacion;
using PtoVta.Infraestructura.TransversalesNET.Autenticacion;
using Xunit;

namespace PtoVta.Aplicacion.Tests
{
    public class ServicioAplicacionInicioSessionTest
    {
        private IRepositorioModuloSistema _IRepositorioModuloSistema;
        private IRepositorioUsuarioSistema _IRepositorioUsuarioSistema;      
        private IRepositorioVendedor _IRepositorioVendedor;

        private IAutenticacion _IAutenticacion;
        private IServicioDominioValidarUsuarioSistema _IServicioDominioValidarUsuarioSistema;
        private IServicioDominioValidarUsuarioVendedor _IServicioDominioValidarUsuarioVendedor; 
        private IServicioAplicacionInicioSession _IServicioAplicacionInicioSession; 

        public ServicioAplicacionInicioSessionTest(){
            _IRepositorioModuloSistema = new RepositorioModuloSistema();
            _IRepositorioUsuarioSistema = new RepositorioUsuarioSistema();      
            _IRepositorioVendedor = new RepositorioVendedor();

            _IAutenticacion = new AutenticacionWindows();
            _IServicioDominioValidarUsuarioSistema = new ServicioDominioValidarUsuarioSistema();
            _IServicioDominioValidarUsuarioVendedor = new ServicioDominioValidarUsuarioVendedor();  

            _IServicioAplicacionInicioSession = new ServicioAplicacionInicioSession(
                                        _IRepositorioModuloSistema,
                                        _IRepositorioUsuarioSistema,
                                        _IRepositorioVendedor,

                                        _IAutenticacion,                                        
                                        _IServicioDominioValidarUsuarioSistema,
                                        _IServicioDominioValidarUsuarioVendedor
            );
        }

        [Fact]
        public void GestionInicioSesion_Test() 
        {
            ModuloSistemaDTO moduloSistema = _IServicioAplicacionInicioSession
                            .GestionInicioSesion("71242616", "7('HI,,-", "OP");

            Assert.False(moduloSistema == null);
        }
    }
}