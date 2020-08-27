using System;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Inventarios;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioListaPrecioInventarioTest
    {
        private readonly IRepositorioListaPrecioInventario _IRepositorioListaPrecioInventario;
        public RepositorioListaPrecioInventarioTest(){      
                _IRepositorioListaPrecioInventario = new RepositorioListaPrecioInventario(ConfiguracionGlobal.CadenaConexionBd);
        }


        [Fact]
        public void ObtenerListaPrecioInventario_Test()
        {     
            var codigoAlmacen = "24";
            var codigoArticulo = "40118";
            var codigoListaPrecio = 372;

            var listaPrecioInventario =  _IRepositorioListaPrecioInventario.ObtenerListaPrecioInventario(codigoArticulo, codigoAlmacen);

            Assert.True(listaPrecioInventario.CodigoListaPrecioInventario == codigoListaPrecio);
        }           

    }

}