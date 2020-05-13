using System;
using Xunit;
using PtoVta.Dominio;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Infraestructura.Repositorios.Modulo;

namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioModuloSistemaTest
    {
        private readonly IRepositorioModuloSistema _IRepositorioModuloSistema;
        public RepositorioModuloSistemaTest(){      
                _IRepositorioModuloSistema = new RepositorioModuloSistema(ConfiguracionGlobal.CadenaConexionBd);
        }


        [Fact]
        public void ObtenerDerechosAccesosUsuario_Test()
        {        
            var moduloSistema =  _IRepositorioModuloSistema.ObtenerDerechosAccesosUsuario("ADMINISORO", "OP");


            Assert.False(moduloSistema == null);
        }
    }
}
