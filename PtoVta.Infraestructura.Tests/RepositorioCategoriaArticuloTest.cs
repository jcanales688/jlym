using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Modulo;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioCategoriaArticuloTest
    {
        private readonly IRepositorioCategoriaArticulo _IRepositorioCategoriaArticulo;
        public RepositorioCategoriaArticuloTest(){      
                _IRepositorioCategoriaArticulo = new RepositorioCategoriaArticulo(ConfiguracionGlobal.CadenaConexionBd);
        }


        [Fact]
        public void ObtenerTodos_Test()
        {        
            var categorias =  _IRepositorioCategoriaArticulo.ObtenerTodos("1");

            Assert.False(categorias == null);
        }
    }
}
