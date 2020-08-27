using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{
    public class RepositorioConfiguracionVentaTest
    {
        private readonly IRepositorioConfiguracionVenta _IRepositorioConfiguracionVenta;
        public RepositorioConfiguracionVentaTest(){      
                _IRepositorioConfiguracionVenta = new RepositorioConfiguracionVenta(ConfiguracionGlobal.CadenaConexionBd);

        } 


       [Fact]
        public void Obtener_Test()
        {
            var configuracionVenta = _IRepositorioConfiguracionVenta.Obtener();
            
            Assert.False(configuracionVenta == null);
        }                  
    }
}