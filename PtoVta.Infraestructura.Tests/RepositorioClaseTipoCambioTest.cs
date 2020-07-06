using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioClaseTipoCambioTest
    {
        private readonly IRepositorioClaseTipoCambio _IRepositorioClaseTipoCambio;
        public RepositorioClaseTipoCambioTest(){      
                _IRepositorioClaseTipoCambio = new RepositorioClaseTipoCambio(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]
        public void ObtenerMontoTipoDeCambio_Test()
        {
            var claseTipoDeCambio = _IRepositorioClaseTipoCambio.ObtenerMontoTipoDeCambio("PEN", "USD",DateTime.Now,"TCONV");
            
            Assert.False(claseTipoDeCambio == null);
        }
        
        [Fact]
        public void ObtenerPorCodigo_Test()
        {
            var claseTipoDeCambio = _IRepositorioClaseTipoCambio.ObtenerPorCodigo("TCONV");
            
            Assert.False(claseTipoDeCambio == null);
        }        

    }

}