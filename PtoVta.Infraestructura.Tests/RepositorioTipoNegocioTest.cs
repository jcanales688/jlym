using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioTipoNegocioTest
    {
        private readonly IRepositorioTipoNegocio _IRepositorioTipoNegocio;
        public RepositorioTipoNegocioTest(){      
                _IRepositorioTipoNegocio = new RepositorioTipoNegocio(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]        
        public void ObtenerPorCodigo_Test()
        {
            var tipoNegocio = _IRepositorioTipoNegocio.ObtenerPorCodigo("2");
            
            Assert.False(tipoNegocio == null);
        }   
    }

}