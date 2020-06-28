using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Aplicacion.DTO.Inventarios;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Aplicacion.GestionInventarios;
using PtoVta.Aplicacion.GestionParametros;
using PtoVta.Aplicacion.GestionUsuario;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Inventarios;
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
    public class ServicioAplicacionArticuloTest
    {
        private IRepositorioArticulo _IRepositorioArticulo;
        private IServicioAplicacionArticulo _IServicioAplicacionArticulo;

        public ServicioAplicacionArticuloTest()
        {
            _IRepositorioArticulo = new RepositorioArticulo(ConfiguracionGlobal.CadenaConexionBd);

            _IServicioAplicacionArticulo = new ServicioAplicacionArticulo(_IRepositorioArticulo);

           LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

           var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
           TipoAdaptadorFactory.EstablecerActual(adaptadorFactory); 
        }



        [Fact]
        public void ObtenerPorCategoriaYSubcategoria_Test() 
        {    
            ResultadoServicio<ArticuloDTO> articulos = _IServicioAplicacionArticulo
                                        .ObtenerPorCategoriaYSubcategoria("2", "201", "24");

            Assert.False(articulos == null);
        }        
    }
}