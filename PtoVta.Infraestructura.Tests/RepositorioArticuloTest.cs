using System;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioArticuloTest
    {
        private readonly IRepositorioArticulo _IRepositorioArticuloTest;
        public RepositorioArticuloTest(){      
                _IRepositorioArticuloTest = new RepositorioArticulo(ConfiguracionGlobal.CadenaConexionBd);
        }


        [Fact]
        public void ObtenerPorCategoriaYSubcategoria_Test()
        {        
            var articulo =  _IRepositorioArticuloTest.ObtenerPorCategoriaYSubcategoria("2", "201", "24");


            Assert.False(articulo == null);
        }
    }
}
