using System;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.Repositorios.Ventas;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{
    public class RepositorioPedidoRetailTest
    {
        private readonly IRepositorioPedidoRetail _IRepositorioPedidoRetail;
        public RepositorioPedidoRetailTest(){      
                _IRepositorioPedidoRetail = new RepositorioPedidoRetail(ConfiguracionGlobal.CadenaConexionBd);

        }      

        [Fact]
        public void Agregar_Test()
        {
            var codigoMoneda = "PEN";
            var codigoTarjeta = "01";
            var codigoCliente = "20167930868";
            var numeroDocumentoNuevo = "B04300212011";
            var correlativo = 1;

            var nuevoPedidoRetail = new PedidoRetail()
            {
                Correlativo = correlativo,
                NumeroDocumento = numeroDocumentoNuevo,
                AfectaInventario = true,
                FechaDocumento = DateTime.Now,
                FechaProceso = DateTime.Now,
                Periodo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString(),
                TotalNacional = 50.00M,
                TotalExtranjera = 16.66M,
                SubTotalNacional = 45.50M,
                SubTotalExtranjera = 23.65M,
                ImpuestoIgvNacional = 12.23M,
                ImpuestoIgvExtranjera = 25.56M,
                ImpuestoIscNacional = 0,
                ImpuestoIscExtranjera = 0,
                TotalNoAfectoNacional = 0,
                TotalNoAfectoExtranjera = 0,      
                PorcentajeDescuentoPrimero = 0,
                PorcentajeDescuentoSegundo = 0,    
                TotalDescuentoNacional = 0,
                TotalDescuentoExtranjera = 0,  
                TotalVueltoNacional = 2.5M,
                TotalVueltoExtranjera = 0.00M,
                TotalEfectivoNacional = 60.00M,
                TotalEfectivoExtranjera = 0.00M,
                RucCliente = codigoCliente,
                NombreCompletoCliente = "PTS S.A - VENTA DE PRUEBA 2",
                DireccionCliente = "LIMA 100",
                Placa = "SQL-2020",
                NumeroVale = 7777,
                TipoCambio = 3.56M,
                NumeroPuntos = 60,
                Kilometraje = 45,
                TransaccionPendiente = true,
                TipoVenta = "A",
                TransaccionProcesada = false,
                AplicaDescuentoCupon = false,
                CentroDeCosto = "0000-1111102222"
            };

            nuevoPedidoRetail.EstablecerReferenciaTipoDocumentoDeVenta("12");
            nuevoPedidoRetail.EstablecerReferenciaTipoPagoDeVenta("01");
            nuevoPedidoRetail.EstablecerReferenciaAlmacenDeVenta("24");
            nuevoPedidoRetail.EstablecerReferenciaMonedaDeVenta(codigoMoneda);
            nuevoPedidoRetail.EstablecerReferenciaCondicionPagoDeVenta( "00");
            nuevoPedidoRetail.EstablecerReferenciaVendedorDeVenta("76408758");
            nuevoPedidoRetail.EstablecerReferenciaUsuarioSistemaDeVenta("VENDPLAYA");
            nuevoPedidoRetail.EstablecerReferenciaImpuestoIgvDeCliente("IV");
            nuevoPedidoRetail.EstablecerReferenciaImpuestoIscDeCliente("SC");
            nuevoPedidoRetail.EstablecerReferenciaClienteDeVenta(codigoCliente);
            nuevoPedidoRetail.EstablecerReferenciaClaseTipoCambioDeVenta("TCONV");
            nuevoPedidoRetail.EstablecerReferenciaTarjetaPromocionDeVenta("232323");
            nuevoPedidoRetail.EstablecerReferenciaConfiguracionPuntoVentaDeVenta("PTOVTA02");
            nuevoPedidoRetail.EstablecerReferenciaTipoNegocioDeVenta("2");

            nuevoPedidoRetail.AgregarNuevoPedidoRetailDetalle(1, 1,19, 
                                                        0,23.00M, 0,
                                                        12,4,false,
                                                        false,12.90M,12.90M, 
                                                        0, 0, "", 
                                                        "ARTICULO PRUEBA",1,0,                                                        
                                                        "","40101","UND");

            nuevoPedidoRetail.AgregarNuevoPedidoRetailConTarjeta(1, "2356", 23.0M,
                                                        0, 0, "A",
                                                        "","VISA", codigoTarjeta);

            nuevoPedidoRetail.AgregarNuevoPedidoRetailConVale(5465);                                                        

            _IRepositorioPedidoRetail.Agregar(nuevoPedidoRetail);

            var pedidoEncontrado = _IRepositorioPedidoRetail.ObtenerPorNumeroPedido(correlativo);

            Assert.True(correlativo == pedidoEncontrado.Correlativo);
        }

        [Fact]
        public void ObtenerPorNumeroPedido_Test()
        {
            var numeroPedido = 1;
            var pedidoRetailPorNumero = _IRepositorioPedidoRetail.ObtenerPorNumeroPedido(numeroPedido);

            Assert.True(pedidoRetailPorNumero.Correlativo == numeroPedido);               
        }     

        [Fact]
        public void ObtenerTodos_Test()
        {
            var pedidosRetail = _IRepositorioPedidoRetail.ObtenerTodos("PTOVTA02");

            Assert.False(pedidosRetail == null);             
        }  
    }
}