using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioConfiguracionGeneralTest
    {
        private readonly IRepositorioConfiguracionGeneral _IRepositorioConfiguracionGeneral;
        public RepositorioConfiguracionGeneralTest(){      
                _IRepositorioConfiguracionGeneral = new RepositorioConfiguracionGeneral(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]
        public void ObtenerPorCodigo_Test()
        {
            var configuracionGeneral = _IRepositorioConfiguracionGeneral.Obtener();
            
            Assert.False(configuracionGeneral == null);
        }

    }

}