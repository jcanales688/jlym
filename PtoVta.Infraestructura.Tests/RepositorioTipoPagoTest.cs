using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioTipoPagoTest
    {
        private readonly IRepositorioTipoPago _IRepositorioTipoPago;
        public RepositorioTipoPagoTest(){      
                _IRepositorioTipoPago = new RepositorioTipoPago(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]        
        public void ObtenerPorCodigo_Test()
        {
            var tipoDePago = _IRepositorioTipoPago.ObtenerPorCodigo("15");
            
            Assert.False(tipoDePago == null);
        }   
    }

}