using System;
using System.Linq;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Configuraciones;
using PtoVta.Aplicacion.GestionConfiguraciones;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Transversales.Adaptador;
using PtoVta.Infraestructura.Transversales.Log;
using PtoVta.Infraestructura.TransversalesNET.Adaptador;
using PtoVta.Infraestructura.TransversalesNET.Log;
using Xunit;

namespace PtoVta.Aplicacion.Tests
{
    public class ServicioAplicacionConfiguracionTest
    {
        private IRepositorioConfiguracionPuntoVenta _IRepositorioConfiguracionPuntoVenta;        
        private IRepositorioConfiguracionFormatoTicket _IRepositorioConfiguracionFormatoTicket;
        private IRepositorioConfiguracionGeneral _IRepositorioConfiguracionGeneral;
        private IRepositorioConfiguracionInventario _IRepositorioConfiguracionInventario;
        private IRepositorioConfiguracionVenta _IRepositorioConfiguracionVenta;

        private IConfiguracionGlobalUnificado _IConfiguracionGlobalUnificado;
        private IServicioAplicacionConfiguracion _IServicioAplicacionConfiguracion;

        public ServicioAplicacionConfiguracionTest()
        {
            _IRepositorioConfiguracionPuntoVenta = new RepositorioConfiguracionPuntoVenta(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionFormatoTicket = new RepositorioConfiguracionFormatoTicket(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionGeneral = new RepositorioConfiguracionGeneral(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionInventario = new RepositorioConfiguracionInventario(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionVenta = new RepositorioConfiguracionVenta(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);                                                

            _IConfiguracionGlobalUnificado = new ConfiguracionGlobalUnificado(_IRepositorioConfiguracionFormatoTicket,
                                                    _IRepositorioConfiguracionGeneral, 
                                                    _IRepositorioConfiguracionInventario,                                             
                                                    _IRepositorioConfiguracionVenta);

            _IServicioAplicacionConfiguracion = new ServicioAplicacionConfiguracion(_IRepositorioConfiguracionPuntoVenta, 
                                                    _IRepositorioConfiguracionFormatoTicket,_IRepositorioConfiguracionGeneral, 
                                                    _IRepositorioConfiguracionInventario, _IRepositorioConfiguracionVenta,
                                                    _IConfiguracionGlobalUnificado);

                                                                

           LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

           var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
           TipoAdaptadorFactory.EstablecerActual(adaptadorFactory); 
        }


        [Fact]
        public void BuscarConfiguracionPuntoVenta_Test() 
        {   
            string nombreTerminal = "ISLA01-24"; 
            string codigoPuntoDeVenta = "PTOVTA01";

            ResultadoServicio<ConfiguracionPuntoVentaDTO> configuracionPtoVta = _IServicioAplicacionConfiguracion
                                                            .BuscarConfiguracionPuntoVenta(nombreTerminal, codigoPuntoDeVenta);

            Assert.True(configuracionPtoVta.Dato.NombreTerminal.Trim() == nombreTerminal);
            Assert.True(configuracionPtoVta.Dato.CodigoPuntoDeVenta.Trim() == codigoPuntoDeVenta);
        }    


        [Fact]
        public void BuscarConfiguracionGlobal_Test() 
        {    
            var codigoTransaccionAlmacenVentas = "301";
            var codigoMonedaBase = "PEN";
            ResultadoServicio<ConfiguracionGlobalDTO> configuracionGlobal = _IServicioAplicacionConfiguracion
                                                                                    .BuscarConfiguracionGlobal();

            Assert.False(configuracionGlobal.Dato == null);
            Assert.True(configuracionGlobal.Dato.CodigoTMAVentas.Trim() == codigoTransaccionAlmacenVentas);
            Assert.True(configuracionGlobal.Dato.CodigoMonedaBase.Trim() == codigoMonedaBase);
        }         

    }
}