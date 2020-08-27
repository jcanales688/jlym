using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{
    public class RepositorioConfiguracionFormatoTicketTest
    {
        private readonly IRepositorioConfiguracionFormatoTicket _IRepositorioConfiguracionFormatoTicket;
        public RepositorioConfiguracionFormatoTicketTest(){      
                _IRepositorioConfiguracionFormatoTicket = new RepositorioConfiguracionFormatoTicket(ConfiguracionGlobal.CadenaConexionBd);

        }        

        [Fact]
        public void Obtener_Test()
        {
            var configuracionFormatoTicket = _IRepositorioConfiguracionFormatoTicket.Obtener();
            
            Assert.False(configuracionFormatoTicket == null);
        }        
    }
}