using System;
using System.Linq;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioTipoDocumentoTest
    {
        private readonly IRepositorioTipoDocumento _IRepositorioTipoDocumento;
        public RepositorioTipoDocumentoTest(){      
                _IRepositorioTipoDocumento = new RepositorioTipoDocumento(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]        
        public void ObtenerCorrelativoDocumento_Test()
        {
            var tipoDocumentoYCorrelativo = _IRepositorioTipoDocumento.ObtenerCorrelativoDocumento("24", "", "03", "", 0);
            
            Assert.False(tipoDocumentoYCorrelativo == null);
        }   

        [Fact]        
        public void ObtenerPorCodigo_Test()
        {
            var tipoDocumento = _IRepositorioTipoDocumento.ObtenerPorCodigo("03");
            
            Assert.False(tipoDocumento == null);
        }   

        
        [Fact]        
        public void ActualizarCorrelativoDocumento_Test()
        {
            var numeroDocumento = 1270027413;            
            var serieDocumento = numeroDocumento.ToString().Substring(0,3);
            var nuevoCorrelativo = 27415;
            var codigoAlmacen = "24";

            var tipoDocumento = new TipoDocumento{CodigoTipoDocumento = "01"};
            tipoDocumento.AgregarNuevoCorrelativoDocumento("120", 1183, "", 0, "24", "");
            tipoDocumento.AgregarNuevoCorrelativoDocumento("127", nuevoCorrelativo, "", 0, "24", "");


            _IRepositorioTipoDocumento.ActualizarCorrelativoDocumento(tipoDocumento, "24",  serieDocumento);
            

            var tipoDocumentoYCorrelativo = _IRepositorioTipoDocumento.ObtenerCorrelativoDocumento("24", "", "01", "", 0);

            var correlaltivoDelTipoDocumento = tipoDocumentoYCorrelativo.CorrelativosDocumento.FirstOrDefault(w => w.Serie == serieDocumento
                                    && w.CodigoAlmacen.Trim() == codigoAlmacen && w.CodigoTipoDocumento.Trim() == tipoDocumento.CodigoTipoDocumento);

            Assert.True(correlaltivoDelTipoDocumento.Correlativo == nuevoCorrelativo);
        }   

    }

}