using System;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioEstadoVendedorTest
    {
        private readonly IRepositorioEstadoVendedor _IRepositorioEstadoVendedor;
        public RepositorioEstadoVendedorTest(){      
                _IRepositorioEstadoVendedor = new RepositorioEstadoVendedor(ConfiguracionGlobal.CadenaConexionBd);
        }


        [Fact]
        public void ObtenerPorCodigo_Test()
        {        
            var estadoVendedor =  _IRepositorioEstadoVendedor.ObtenerPorCodigo("03");


            Assert.False(estadoVendedor == null);
        }
    }
}
