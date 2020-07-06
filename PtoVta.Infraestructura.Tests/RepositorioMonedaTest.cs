using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioMonedaTest
    {
        private readonly IRepositorioMoneda _IRepositorioMoneda;
        public RepositorioMonedaTest(){      
                _IRepositorioMoneda = new RepositorioMoneda(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]        
        public void ObtenerPorCodigo_Test()
        {
            var moneda = _IRepositorioMoneda.ObtenerPorCodigo("PEN");
            
            Assert.False(moneda == null);
        }   

    }

}