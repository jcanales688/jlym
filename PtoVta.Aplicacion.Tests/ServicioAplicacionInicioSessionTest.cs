using System;
using Xunit;

namespace PtoVta.Aplicacion.Tests
{
    public class ServicioAplicacionInicioSessionTest
    {
        private IRepositorioModuloSistema _IRepositorioModuloSistema;
        private IRepositorioUsuarioSistema _IRepositorioUsuarioSistema;      
        private IRepositorioVendedor _IRepositorioVendedor;

        private IValidadorInicioSesion _IValidadorInicioSesion;
        private IServicioDominioValidarUsuarioSistema _IServicioDominioValidarUsuarioSistema;
        private IServicioDominioValidarUsuarioVendedor _IServicioDominioValidarUsuarioVendedor; 
        private IServicioAplicacionInicioSession _IServicioAplicacionInicioSession; 

        public ServicioAplicacionInicioSessionTest(){
            _IRepositorioModuloSistema = new RepositorioModuloSistema();
            _IRepositorioUsuarioSistema = new RepositorioUsuarioSistema();      
            _IRepositorioVendedor = new RepositorioVendedor();

            _IValidadorInicioSesion = new ValidadorInicioSesion();
            _IServicioDominioValidarUsuarioSistema = new ServicioDominioValidarUsuarioSistema();
            _IServicioDominioValidarUsuarioVendedor = new ServicioDominioValidarUsuarioVendedor();  

            _IServicioAplicacionInicioSession = new ServicioAplicacionInicioSession(
                                        _IRepositorioModuloSistema,
                                        _IRepositorioUsuarioSistema,
                                        _IRepositorioVendedor,

                                        _IValidadorInicioSesion,                                        
                                        _IServicioDominioValidarUsuarioSistema,
                                        _IServicioDominioValidarUsuarioVendedor
            );
        }

        [Fact]
        public void GestionInicioSesion_Test() 
        {

        }
    }
}
