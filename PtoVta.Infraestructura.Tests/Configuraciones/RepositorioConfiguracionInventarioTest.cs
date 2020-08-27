using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{
    public class RepositorioConfiguracionInventarioTest
    {
        private readonly IRepositorioConfiguracionInventario _RepositorioConfiguracionInventario;
        public RepositorioConfiguracionInventarioTest(){      
                _RepositorioConfiguracionInventario = new RepositorioConfiguracionInventario(ConfiguracionGlobal.CadenaConexionBd);

        }             


        [Fact]
        public void Obtener_Test()
        {
            var configuracionInventario = _RepositorioConfiguracionInventario.Obtener();
            
            Assert.False(configuracionInventario == null);
        }                
    }
}