using System;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.Repositorios.Ventas;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioListaPrecioClienteTest
    {
        private readonly IRepositorioListaPrecioCliente _IRepositorioListaPrecioCliente;
        public RepositorioListaPrecioClienteTest(){      
                _IRepositorioListaPrecioCliente = new RepositorioListaPrecioCliente(ConfiguracionGlobal.CadenaConexionBd);

        }
        

        [Fact]
        public void ObtenerListaPrecioCliente_Test()
        {
            var codigoCliente  = "20600380207";
            var codigoAlmacen = "24";
            var fechaProceso = "20100602";  
            var codigoArticulo = "10005";
            var codigoListaPrecio = "PEC9505-09";

            var ultimaListaPrecioCliente = _IRepositorioListaPrecioCliente.ObtenerListaPrecioCliente(codigoCliente, codigoArticulo,
                                                                codigoAlmacen, fechaProceso);
            
            Assert.True(ultimaListaPrecioCliente.CodigoListaPrecioCliente == codigoListaPrecio);
        }

    }

}