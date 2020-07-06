using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.GestionColaborador;
using PtoVta.Aplicacion.GestionUsuario;
using PtoVta.Dominio.Agregados.Colaborador;
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
    public class ServicioAplicacionVendedorTest
    {
        private IRepositorioAlmacen _IRepositorioAlmacen;
        private IRepositorioEstadoVendedor _IRepositorioEstadoVendedor;
        private IRepositorioUsuarioSistema _IIRepositorioUsuarioSistema;
        private IRepositorioVendedor _IRepositorioVendedor;
        private IServicioAplicacionVendedor _IServicioAplicacionVendedor;


        public ServicioAplicacionVendedorTest()
        {
            _IRepositorioAlmacen = new RepositorioAlmacen(ConfiguracionGlobal.CadenaConexionBd);            
            _IRepositorioEstadoVendedor = new RepositorioEstadoVendedor(ConfiguracionGlobal.CadenaConexionBd);            
            _IIRepositorioUsuarioSistema = new RepositorioUsuarioSistema(ConfiguracionGlobal.CadenaConexionBd);            
            _IRepositorioVendedor = new RepositorioVendedor(ConfiguracionGlobal.CadenaConexionBd);                                                

            _IServicioAplicacionVendedor = new ServicioAplicacionVendedor(_IRepositorioAlmacen, 
                    _IRepositorioEstadoVendedor, _IIRepositorioUsuarioSistema, _IRepositorioVendedor);

           LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

           var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
           TipoAdaptadorFactory.EstablecerActual(adaptadorFactory);             
        }

        [Fact]
        public void AgregarNuevoUsuarioVendedor_Test() 
        {
            var vendedorDTO = new VendedorDTO(){
                CodigoVendedor = "10412891",
                NombresVendedor = "AMMY ILLESCAS",
                DocumentoIdentidad = "10412891",
                Telefono = "5203124",
                Sexo="M",
                FechaInicio = DateTime.Now,
                FechaNacimiento = DateTime.Now,
                Clave = "456",
                DireccionPrimeroPais = "ARGENTINA",
                DireccionPrimeroDepartamento = "BUENOS AIRES",
                DireccionPrimeroProvincia = "ROSARIO",
                DireccionPrimeroDistrito = "CENTRAL",
                DireccionPrimeroUbicacion = "MZ . D LT. 14",
                CodigoAlmacen = "24",
                CodigoEstadoVendedor = "01",
                CodigoUsuarioSistema = "SYSADMIN",
                CodigoUsuarioSistemaAcceso = "VENDPLAYA"
            };            

            ResultadoServicio<VendedorDTO> vendedorNuevo = _IServicioAplicacionVendedor
                            .AgregarNuevoUsuarioVendedor(vendedorDTO);

            Assert.False(vendedorDTO == null);
        }        
    }
}