using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Aplicacion.GestionParametros;
using PtoVta.Aplicacion.GestionUsuario;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Modulo;
using PtoVta.Infraestructura.Repositorios.Usuario;
using PtoVta.Infraestructura.Transversales.Adaptador;
using PtoVta.Infraestructura.Transversales.Autenticacion;
using PtoVta.Infraestructura.Transversales.Log;
using PtoVta.Infraestructura.TransversalesNET.Adaptador;
using PtoVta.Infraestructura.TransversalesNET.Autenticacion;
using PtoVta.Infraestructura.TransversalesNET.Log;
using Xunit;

namespace PtoVta.Aplicacion.Tests
{
    public class ServicioAplicacionParametrosTest
    {
        private IRepositorioCategoriaArticulo _IRepositorioCategoriaArticulo;
        private IServicioAplicacionParametros _IServicioAplicacionParametros;

        public ServicioAplicacionParametrosTest()
        {
            _IRepositorioCategoriaArticulo = new RepositorioCategoriaArticulo(ConfiguracionGlobal.CadenaConexionBd);

            _IServicioAplicacionParametros = new ServicioAplicacionParametros(_IRepositorioCategoriaArticulo);

           LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

           var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
           TipoAdaptadorFactory.EstablecerActual(adaptadorFactory); 
        }



        [Fact]
        public void ObtenerCategorias_Test() 
        {
    
            ResultadoServicio<CategoriaArticuloDTO> categorias = _IServicioAplicacionParametros
                            .ObtenerCategorias("1");

            Assert.False(categorias == null);
        }        
    }
}