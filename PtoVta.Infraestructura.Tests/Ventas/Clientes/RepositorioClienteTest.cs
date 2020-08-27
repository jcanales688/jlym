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
        public RepositorioClienteTest()
        {
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
            var nuevoCliente = new Cliente()
            {
                CodigoCliente = "1010104128891",
                CodigoContable = "CLI-1029",
                Ruc = "10104128891",
                NombresORazonSocial = "ROGER ILLESCAS CARBAJAL",
                Telefono = "5203121",
                Fax = "185663251",
                FechaNacimiento = DateTime.Now,
                FechaInscripcion = DateTime.Now,
                DiasDeGracia = 2,
                MontoLimiteCredito = 7500.00M,
                Deuda = 4200.00M,
                EsAfecto = 1,
                ControlarSaldoDisponible = 1
            };

            nuevoCliente.EstablecerMonedaDeCliente(new Moneda { CodigoMoneda = "PEN" });
            nuevoCliente.EstablecerClaseTipoCambioDeCliente(new ClaseTipoCambio { CodigoClaseTipoCambio = "TCONV" });
            nuevoCliente.EstablecerTipoClienteDeCliente(new TipoCliente { CodigoTipoCliente = "03" });
            nuevoCliente.EstablecerZonaClienteDeCliente(new ZonaCliente { CodigoZonaCliente = "1" });
            nuevoCliente.EstablecerDiaDePagoDeCliente(new DiaDePago { CodigoDiaDePago = "DEFAULT0" });
            nuevoCliente.EstablecerVendedorDeCliente(new Vendedor { CodigoVendedor = "99999999" });
            nuevoCliente.EstablecerImpuestoIgvDeCliente(new Impuesto { CodigoImpuesto = "IV" });
            nuevoCliente.EstablecerImpuestoIscDeCliente(new Impuesto { CodigoImpuesto = "SC" });
            nuevoCliente.EstablecerCondicionPagoDocumentoGeneradoDeCliente(new CondicionPago { CodigoCondicionPago = "98" });
            nuevoCliente.EstablecerCondicionPagoTicketDeCliente(new CondicionPago { CodigoCondicionPago = "98" });
            nuevoCliente.EstablecerEstadoDeClienteDeCliente(new EstadoDeCliente { CodigoEstadoDeCliente = "A" });
            nuevoCliente.EstablecerUsuarioSistemaDeCliente(new UsuarioSistema { CodigoUsuarioDeSistema = "SYSADMIN" });
            nuevoCliente.EstablecerPaisDeCliente(new Pais { CodigoPais = "PER" });
            nuevoCliente.EstablecerDepartamentoDeCliente(new Departamento { CodigoDepartamento = "LI" });
            nuevoCliente.EstablecerDistritoDeCliente(new Distrito { CodigoDistrito = "01" });

            nuevoCliente.DireccionPrimero = new ClienteDireccion("Peru", "Lima", "Lima", "Puente Piedra", "AV. 100");
            nuevoCliente.DireccionSegundo = new ClienteDireccion("Peru", "Arequipa", "Arequipa", "Lagos Azules", "AV. 200");

            nuevoCliente.AgregarNuevoClientePlaca("ORACLE-2020");

            _IRepositorioCliente.Agregar(nuevoCliente);

            var clienteBuscado = _IRepositorioCliente.ObtenerPorCodigo("1010104128891");

            Assert.True(nuevoCliente.Ruc.Trim() == clienteBuscado.Ruc.Trim());
        }


        [Fact]
        public void Modificar_ClienteNoCredito_Test()
        {
            var clienteAModificar = _IRepositorioCliente.ObtenerPorCodigo("20167930868");
            clienteAModificar.NombresORazonSocial = "PTS S.A.C.";
            clienteAModificar.Telefono = "7777777";

            clienteAModificar.DireccionPrimero = new ClienteDireccion("Peru", "Lima", "Lima", "Puente Piedra", "AV. LOS FRUTALES NRO. 945 URB. SANTA MAGDALENA SOFIA LIMA  - LIMA  - LA MOLINA (7777777)");
            clienteAModificar.DireccionSegundo = new ClienteDireccion("Peru", "Arequipa", "Arequipa", "Lagos Azules", "Conde de Avenue (7777777)");

            _IRepositorioCliente.Modificar(clienteAModificar);


            var clienteBuscado = _IRepositorioCliente.ObtenerPorCodigo("20167930868");

            Assert.True(clienteAModificar.NombresORazonSocial.Trim() == clienteBuscado.NombresORazonSocial.Trim());
            Assert.True(clienteAModificar.Telefono.Trim() == clienteBuscado.Telefono.Trim());
            Assert.True(clienteAModificar.DireccionPrimero.Ubicacion.Trim() == clienteBuscado.DireccionPrimero.Ubicacion.Trim());
            Assert.True(clienteAModificar.DireccionSegundo.Ubicacion.Trim() == clienteBuscado.DireccionSegundo.Ubicacion.Trim());
        }


        [Fact]
        public void Modificar_ClienteCredito_Test()
        {
            var clienteAModificar = _IRepositorioCliente.ObtenerPorCodigo("20226547721");
            clienteAModificar.ActualizarDeuda(100);


            _IRepositorioCliente.Modificar(clienteAModificar);

            var clienteBuscado = _IRepositorioCliente.ObtenerPorCodigo("20226547721");

            Assert.True(clienteAModificar.NombresORazonSocial.Trim() == clienteBuscado.NombresORazonSocial.Trim());
            Assert.True(clienteAModificar.Telefono.Trim() == clienteBuscado.Telefono.Trim());
            Assert.True(clienteAModificar.DireccionPrimero.Ubicacion.Trim() == clienteBuscado.DireccionPrimero.Ubicacion.Trim());
            Assert.True(clienteAModificar.DireccionSegundo.Ubicacion.Trim() == clienteBuscado.DireccionSegundo.Ubicacion.Trim());
            Assert.True((clienteAModificar.ClienteLimiteCredito.Deuda) == clienteAModificar.ClienteLimiteCredito.Deuda);
        }        


        [Fact]
        public void ObtenerTodos_Test()
        {
            var clientes = _IRepositorioCliente.ObtenerTodos();

            Assert.False(clientes == null);
        }

    }

}