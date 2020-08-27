using System;
using System.Linq;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Aplicacion.DTO.Inventarios;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Aplicacion.GestionInventarios;
using PtoVta.Aplicacion.GestionParametros;
using PtoVta.Aplicacion.GestionUsuario;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Inventarios;
using PtoVta.Infraestructura.Repositorios.Modulo;
using PtoVta.Infraestructura.Repositorios.Usuario;
using PtoVta.Infraestructura.Repositorios.Ventas;
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
        private IRepositorioListaPrecioCliente _IRepositorioListaPrecioCliente;
        private IRepositorioListaPrecioInventario _IRepositorioListaPrecioInventario;

        private IRepositorioConfiguracionFormatoTicket _IRepositorioConfiguracionFormatoTicket;
        private IRepositorioConfiguracionGeneral _IRepositorioConfiguracionGeneral;
        private IRepositorioConfiguracionInventario _IRepositorioConfiguracionInventario;
        private IRepositorioConfiguracionVenta _IRepositorioConfiguracionVenta;
        
         private IServicioDominioListaPrecios _IServicioDominioListaPrecios;
        private IConfiguracionGlobalUnificado _IConfiguracionGlobalUnificado;

        private IServicioAplicacionArticulo _IServicioAplicacionArticulo;

        public ServicioAplicacionArticuloTest()
        {
            _IRepositorioArticulo = new RepositorioArticulo(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioListaPrecioCliente = new RepositorioListaPrecioCliente(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioListaPrecioInventario = new RepositorioListaPrecioInventario(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);

            _IRepositorioConfiguracionFormatoTicket = new RepositorioConfiguracionFormatoTicket(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionGeneral = new RepositorioConfiguracionGeneral(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionInventario = new RepositorioConfiguracionInventario(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionVenta = new RepositorioConfiguracionVenta(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);

            _IServicioDominioListaPrecios = new ServicioDominioListaPrecios();
            _IConfiguracionGlobalUnificado = new ConfiguracionGlobalUnificado(_IRepositorioConfiguracionFormatoTicket,
                                                                    _IRepositorioConfiguracionGeneral, 
                                                                    _IRepositorioConfiguracionInventario,                                             
                                                                    _IRepositorioConfiguracionVenta);

            _IServicioAplicacionArticulo = new ServicioAplicacionArticulo(_IRepositorioArticulo, 
                                                                        _IRepositorioListaPrecioCliente,
                                                                        _IRepositorioListaPrecioInventario, 
                                                                        _IServicioDominioListaPrecios,
                                                                        _IConfiguracionGlobalUnificado);

           LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

           var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
           TipoAdaptadorFactory.EstablecerActual(adaptadorFactory); 
        }



        [Fact]
        public void ObtenerPorCategoriaYSubcategoria_Test() 
        {    
            ResultadoServicio<ArticuloDTO> articulos = _IServicioAplicacionArticulo
                                    .ObtenerPorCategoriaYSubcategoria("2", "201", "24");

            Assert.True(articulos.Datos.Any() == true);
        }      


        [Fact]
        public void ObtenerPrecioVentaDeArticuloDesdeListaPrecioInventario_Test() 
        {    
            var codigoCliente = "";
            var codigoArticulo = "40118";
            var codigoAlmacen = "24";
            var precioTentativo = 8.40M;

            decimal precioRealArticulo = _IServicioAplicacionArticulo
                                                .ObtenerPrecioVentaDeArticulo(codigoCliente, codigoArticulo, codigoAlmacen);

            Assert.True(precioRealArticulo == precioTentativo);
        }  



        [Fact]
        public void ObtenerPrecioVentaDeArticuloDesdeListaPrecioCliente_Test() 
        {    
            var codigoCliente = "20600380207";
            var codigoArticulo = "10005";          
            var codigoAlmacen = "24";
            var precioTentativo = 10.58M;

            decimal precioRealArticulo = _IServicioAplicacionArticulo
                                                .ObtenerPrecioVentaDeArticulo(codigoCliente, codigoArticulo, codigoAlmacen);

            Assert.True(precioRealArticulo == precioTentativo);
        }                      
    }
}