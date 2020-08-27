using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioCondicionPagoTest
    {
        private readonly IRepositorioCondicionPago _IRepositorioCondicionPago;
        public RepositorioCondicionPagoTest(){      
                _IRepositorioCondicionPago = new RepositorioCondicionPago(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]
        public void ObtenerPorCodigo_Test()
        {
            var condicionDePago = _IRepositorioCondicionPago.ObtenerPorCodigo("02");
            
            Assert.False(condicionDePago == null);
        }

    }

}