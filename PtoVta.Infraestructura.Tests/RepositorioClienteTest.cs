using System;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Parametros;
using PtoVta.Infraestructura.Repositorios.Ventas;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioClienteTest
    {
        private readonly IRepositorioCliente _IRepositorioCliente;
        public RepositorioClienteTest(){      
                _IRepositorioCliente = new RepositorioCliente(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]
        public void ObtenerClientePorRUC_Test()
        {
            var cliente = _IRepositorioCliente.ObtenerClientePorRUC("20481159939", "24");
            
            Assert.False(cliente == null);
        }

        [Fact]
        public void ObtenerPorCodigo_Test()
        {
            var cliente = _IRepositorioCliente.ObtenerPorCodigo("20481159939");
            
            Assert.False(cliente == null);
        }   


        [Fact]
        public void Agregar_Test()
        {
            var nuevoCliente = new Cliente(){
                CodigoCliente = "1010104128896",
                CodigoContable = "CLI-1010",
                Ruc = "10104128896",
                NombresORazonSocial = "JORGE ILLESCAS UZURIAGA",
                Telefono = "5203124",
                Fax = "185663251",
                FechaNacimiento = DateTime.Now,
                FechaInscripcion = DateTime.Now,
                DiasDeGracia = 2,
                MontoLimiteCredito = 7500.00M,
                Deuda = 4200.00M,
                EsAfecto = 1,
                ControlarSaldoDisponible = 1
            };

            nuevoCliente.EstablecerMonedaDeCliente(new Moneda{ CodigoMoneda = "PEN"});
            nuevoCliente.EstablecerClaseTipoCambioDeCliente(new ClaseTipoCambio{ CodigoClaseTipoCambio = "TCONV"});
            nuevoCliente.EstablecerTipoClienteDeCliente(new TipoCliente{ CodigoTipoCliente = "03"});
            nuevoCliente.EstablecerZonaClienteDeCliente(new ZonaCliente{ CodigoZonaCliente = "1"});
            nuevoCliente.EstablecerDiaDePagoDeCliente(new DiaDePago{CodigoDiaDePago = "DEFAULT0" });
            nuevoCliente.EstablecerVendedorDeCliente(new Vendedor{ CodigoVendedor = "99999999" });
            nuevoCliente.EstablecerImpuestoIgvDeCliente(new Impuesto{ CodigoImpuesto = "IV" });
            nuevoCliente.EstablecerImpuestoIscDeCliente(new Impuesto{ CodigoImpuesto = "SC" });
            nuevoCliente.EstablecerCondicionPagoDocumentoGeneradoDeCliente(new CondicionPago{ CodigoCondicionPago = "98" });
            nuevoCliente.EstablecerCondicionPagoTicketDeCliente(new CondicionPago{ CodigoCondicionPago = "98" });
            nuevoCliente.EstablecerEstadoDeClienteDeCliente(new EstadoDeCliente{ CodigoEstadoDeCliente = "A"});
            nuevoCliente.EstablecerUsuarioSistemaDeCliente(new UsuarioSistema{ CodigoUsuarioDeSistema = "SYSADMIN"});
            nuevoCliente.EstablecerPaisDeCliente(new Pais{ CodigoPais = "PER" });
            nuevoCliente.EstablecerDepartamentoDeCliente(new Departamento{ CodigoDepartamento = "LI" });
            nuevoCliente.EstablecerDistritoDeCliente(new Distrito{ CodigoDistrito = "01"  });

            nuevoCliente.DireccionPrimero = new ClienteDireccion("Peru", "Lima", "Lima", "Puente Piedra", "AV. 100");
            nuevoCliente.DireccionSegundo = new ClienteDireccion("Peru", "Arequipa", "Arequipa", "Lagos Azules", "AV. 200");

            _IRepositorioCliente.Agregar(nuevoCliente);
            
            var clienteBuscado = _IRepositorioCliente.ObtenerPorCodigo("1010104128896");
            
            Assert.True(nuevoCliente.Ruc == clienteBuscado.Ruc);
        }                  
    }

}