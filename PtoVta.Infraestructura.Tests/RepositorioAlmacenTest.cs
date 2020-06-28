using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Inventarios;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioAlmacenTest
    {
        private readonly IRepositorioAlmacen _IRepositorioAlmacen;
        public RepositorioAlmacenTest(){      
                _IRepositorioAlmacen = new RepositorioAlmacen(ConfiguracionGlobal.CadenaConexionBd);
        }


        [Fact]
        public void ObtenerPorCodigo_Test()
        {        
            var almacen =  _IRepositorioAlmacen.ObtenerPorCodigo("24");


            Assert.False(almacen == null);
        }
    }
}
