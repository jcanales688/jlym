using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioTarjetaTest
    {
        private readonly IRepositorioTarjeta _IRepositorioTarjeta;
        public RepositorioTarjetaTest(){      
                _IRepositorioTarjeta = new RepositorioTarjeta(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]        
        public void ObtenerPorCodigo_Test()
        {
            var tarjeta = _IRepositorioTarjeta.ObtenerPorCodigo("02");
            
            Assert.False(tarjeta == null);
        }   
    }

}