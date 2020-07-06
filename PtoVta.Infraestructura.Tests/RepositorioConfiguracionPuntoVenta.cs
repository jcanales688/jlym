using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioConfiguracionPuntoVentaTest
    {
        private readonly IRepositorioConfiguracionPuntoVenta _IRepositorioConfiguracionPuntoVenta;
        public RepositorioConfiguracionPuntoVentaTest(){      
                _IRepositorioConfiguracionPuntoVenta = new RepositorioConfiguracionPuntoVenta(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]
        public void ObtenerPorTerminalYPuntoVenta_Test()
        {
            var configuracionPtoVta = _IRepositorioConfiguracionPuntoVenta.ObtenerPorTerminalYPuntoVenta("ISLA01-24", "PTOVTA01");
            
            Assert.False(configuracionPtoVta == null);
        }

        [Fact]
        public void ObtenerPorPuntoDeVenta_Test()
        {
            var configuracionPtoVta = _IRepositorioConfiguracionPuntoVenta.ObtenerPorPuntoDeVenta("PTOVTA04");
            
            Assert.False(configuracionPtoVta == null);
        }        

    }

}