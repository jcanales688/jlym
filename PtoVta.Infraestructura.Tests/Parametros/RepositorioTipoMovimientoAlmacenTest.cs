using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioTipoMovimientoAlmacenTest
    {
        private readonly IRepositorioTipoMovimientoAlmacen _IRepositorioTipoMovimientoAlmacen;
        public RepositorioTipoMovimientoAlmacenTest(){      
                _IRepositorioTipoMovimientoAlmacen = new RepositorioTipoMovimientoAlmacen(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]        
        public void ObtenerPorCodigo_Test()
        {
            var tipoMovimientoAlmacen = _IRepositorioTipoMovimientoAlmacen.ObtenerPorCodigo("301");
            
            Assert.False(tipoMovimientoAlmacen == null);
        }   
    }

}