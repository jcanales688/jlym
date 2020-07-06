using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioEstadoDocumentoTest
    {
        private readonly IRepositorioEstadoDocumento _IRepositorioEstadoDocumento;
        public RepositorioEstadoDocumentoTest(){      
                _IRepositorioEstadoDocumento = new RepositorioEstadoDocumento(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]

        public void ObtenerPorCodigo_Test()
        {
            var tipoDeDocumento = _IRepositorioEstadoDocumento.ObtenerPorCodigo("OK");
            
            Assert.False(tipoDeDocumento == null);
        }   

    }

}