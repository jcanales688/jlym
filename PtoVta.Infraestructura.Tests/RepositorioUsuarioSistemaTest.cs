using System;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.Repositorios.Usuario;
using Xunit;

namespace PtoVta.Infraestructura.Tests
{
    public class RepositorioUsuarioSistemaTest
    {
        private readonly IRepositorioUsuarioSistema _IRepositorioUsuarioSistema;
        public RepositorioUsuarioSistemaTest()
        {
            _IRepositorioUsuarioSistema = new RepositorioUsuarioSistema(ConfiguracionGlobal.CadenaConexionBd);
        }

        [Fact]
        public void ObtenerUsuarioSistemaPorUsuario_Test()
        {
            var usuarioSistema = _IRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario("43668598",":),+.*,./-");

            Assert.False(usuarioSistema == null);
        }

       [Fact]
        public void ObtenerUsuarioSistemaPorSOLOUsuarioS_Test()
        {
            var usuarioSistema = _IRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario("43668598");

            Assert.False(usuarioSistema == null);
        }        
    }
}
