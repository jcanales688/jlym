using System;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Inventarios;
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
        public void Modificar_Test()
        {        
            var articulo = new Articulo();
            articulo.CodigoArticulo = "20101";
            articulo.AgregarArticuloDetalle(0,0,0,777888, DateTime.Now, DateTime.Now,0,0,0,0,0,0,0,0,
                        string.Empty,string.Empty,string.Empty, string.Empty,"24");

            _IRepositorioArticuloTest.Modificar(articulo);

            var articuloModificado =  _IRepositorioArticuloTest.ObtenerPorCodigo("20101", "24");

            Assert.True(articuloModificado.ArticuloDetalle.StockActual == articulo.ArticuloDetalle.StockActual);
        }

        [Fact]
        public void ObtenerPorCategoriaYSubcategoria_Test()
        {        
            var articulo =  _IRepositorioArticuloTest.ObtenerPorCategoriaYSubcategoria("2", "201", "24");

            Assert.False(articulo == null);
        }

        [Fact]
        public void ObtenerPorCodigo_Test()
        {        
            var articulo =  _IRepositorioArticuloTest.ObtenerPorCodigo("20101", "24");

            Assert.False(articulo == null);
        }        
    }
}
