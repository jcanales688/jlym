using System;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using Xunit;

namespace PtoVta.Infraestructura.Tests
{
    public class RepositorioVendedorTest
    {
        private readonly IRepositorioVendedor _IRepositorioVendedor;
        public RepositorioVendedorTest()
        {
            _IRepositorioVendedor = new RepositorioVendedor(ConfiguracionGlobal.CadenaConexionBd);
        }

        [Fact]
        public void ObtenerVendedorPorUsuario_Test()
        {
            var vendedor = _IRepositorioVendedor.ObtenerVendedorPorUsuario("42928283");
            
            Assert.False(vendedor == null);
        }
    }
}
