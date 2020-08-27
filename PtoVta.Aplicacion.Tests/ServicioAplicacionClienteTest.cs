using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Aplicacion.GestionClientes;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura;
using PtoVta.Infraestructura.Repositorios.Ventas;
using PtoVta.Infraestructura.Transversales.Adaptador;
using PtoVta.Infraestructura.Transversales.Log;
using PtoVta.Infraestructura.TransversalesNET.Adaptador;
using PtoVta.Infraestructura.TransversalesNET.Log;
using Xunit;

namespace PtoVta.Aplicacion.Tests
{
    public class ServicioAplicacionClienteTest
    {
        private IRepositorioCliente _IRepositorioCliente;
        private IServicioAplicacionCliente _IServicioAplicacionCliente;

        public ServicioAplicacionClienteTest()
        {
            _IRepositorioCliente = new RepositorioCliente(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);

            _IServicioAplicacionCliente = new ServicioAplicacionCliente(_IRepositorioCliente);

           LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

           var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
           TipoAdaptadorFactory.EstablecerActual(adaptadorFactory); 
        }


        [Fact]
        public void AgregarNuevo() {
            var codigoCliente = "20128525235";
            var codigoAlmacen = "24";
            var codigoUsuarioDeSistema = "SYSADMIN";

            var nuevoClienteDto = new ClienteDTO(){
                CodigoCliente = codigoCliente,
                CodigoContable = "100-200-30",
                Ruc =  codigoCliente,
                NombresORazonSocial = "",
                Telefono = "5203124",
                Fax = "5623666",
                FechaNacimiento = DateTime.Now,
                FechaInscripcion = DateTime.Now,
                DiasDeGracia = 0,
                MontoLimiteCredito = 0,
                Deuda = 0,
                EsAfecto = 1,
                ControlarSaldoDisponible = 0,
                DireccionPrimeroPais = "Peru",
                DireccionPrimeroDepartamento = "Lima",
                DireccionPrimeroProvincia = "Lima",
                DireccionPrimeroDistrito = "Puente Piedra",
                DireccionPrimeroUbicacion = "Av. Ancon",
                DireccionSegundoUbicacion = "Av. Ancon 2",

                CodigoMoneda = "PEN",
                CodigoClaseTipoCambio = "TCONV",
                CodigoTipoCliente = "03",
                CodigoZonaCliente = "1",
                CodigoDiaDePago = "DEFAULT0",
                CodigoVendedor = "99999999",
                CodigoImpuestoIgv = "IV",
                CodigoImpuestoIsc = "SC",
                CodigoCondicionPagoDocumentoGenerado = "98",
                CodigoCondicionPagoTicket = "98",
                CodigoEstadoDeCliente = "A",
                CodigoUsuarioDeSistema = codigoUsuarioDeSistema,
                CodigoPais = "PER",
                CodigoDepartamento = "LI",
                CodigoDistrito = "01"
            };
            
            nuevoClienteDto.CondicionPagoDocumentoGenerado = new CondicionPagoDTO(){
                CodigoCondicionPago = "98",
                DiasPago = 0,
                DescripcionCondicionPago = ""
            };
            
            nuevoClienteDto.CondicionPagoTicket = new CondicionPagoDTO(){
                CodigoCondicionPago = "98",
                DiasPago = 0,
                DescripcionCondicionPago = ""
            };

            nuevoClienteDto.DiaDePago = new DiaDePagoDTO(){
                CodigoDiaDePago = "DEFAULT0", 
                CombinaDia1 = 0, 
                CombinaDia2 = 0, 
                CombinaDia3 = 0, 
                CombinaDia4 = 0, 
                DescripcionDiaDePago = "", 
                D1Lunes = 0, 
                D2Martes = 0, 
                D3Miercoles = 0, 
                D4Jueves = 0, 
                D5Viernes = 0, 
                D6Sabado = 0, 
                D7Domingo = 0, 
                FechaCreacion = DateTime.Now, 
                FechaUltimaActualiza = DateTime.Now, 
                EstadoSemana = 1
            };
            
            nuevoClienteDto.ClientePlacas = new List<ClientePlacaDTO>(){
                new ClientePlacaDTO(){
                    CodigoCliente = codigoCliente,
                    DescripcionPlaca = "ORACLE-2029"
                }
            };


            ResultadoServicio<ResultadoClienteGrabadoDTO> clienteNuevo = 
                    _IServicioAplicacionCliente.AgregarNuevoCliente(nuevoClienteDto);

            var clienteEncontrado = _IServicioAplicacionCliente.BuscarClientePorRUC(codigoCliente, codigoAlmacen);

            Assert.True(clienteEncontrado.Dato.CodigoCliente.Trim() == clienteNuevo.Dato.CodigoCliente.Trim());
        }


        [Fact]
        public void BuscarClientePorRUC_Test()
        {
            var codigoCliente = "20128525236";
            var codigoAlmacen = "24";

            var clienteEncontrado = _IServicioAplicacionCliente.BuscarClientePorRUC(codigoCliente, codigoAlmacen);

            Assert.True(clienteEncontrado.Dato.Ruc.Trim() == codigoCliente);
        }


        [Fact]
        public void BuscarTodosClientes_Test()
        {
            var clientesEncontrados = _IServicioAplicacionCliente.BuscarTodosClientes();

            Assert.True(clientesEncontrados.Datos.Any() == true);
        }        

    }
}
