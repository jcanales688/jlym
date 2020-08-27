using System;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.Repositorios.Ventas;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{
    public class RepositorioPedidoEESSTest
    {
        private readonly IRepositorioPedidoEESS _IRepositorioPedidoEESS;
        public RepositorioPedidoEESSTest(){      
                _IRepositorioPedidoEESS = new RepositorioPedidoEESS(ConfiguracionGlobal.CadenaConexionBd);

        }      

        [Fact]
        public void Agregar_Test()
        {
            var codigoMoneda = "PEN";
            var codigoTarjeta = "01";
            var codigoCliente = "20167930868";
            var numeroDocumentoNuevo = "B04300212011";
            var correlativo = 1;

            var nuevoPedidoEESS = new PedidoEESS()
            {
                Correlativo = correlativo,
                NumeroCara = "03",
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
                Placa = "SQL-2020",
                NumeroVale = 7777,
                TipoCambio = 3.56M,
                ProcesadoCierreZ = false,
                ProcesadoCierreX = false,
                NumeroPuntos = 60,
                NombreTerminal = "PC-100",
                Kilometraje = 45,
                DireccionCliente = "LIMA 100",
                TipoCliente = 1,
                DescripcionTipoCliente = "",
                DescripcionEstado = "ACTIVO",
                TipoCambioClienteCredito = 3.35m,
                DiasDeGraciaClienteCredito = 4,
                LimiteCreditoClienteCredito = 24.89M,
                DeudaClienteClienteCredito = 45.78M,
                PlusCreditoClienteCredito = 23.9M,
                Afecto = false,
                NumeroTarjeta = "7898",
                PagoTarjeta = 1,
                DescripcionTarjeta = "VISA"
            };

            nuevoPedidoEESS.EstablecerReferenciaTipoDocumentoDeVenta("12");
            nuevoPedidoEESS.EstablecerReferenciaTipoPagoDeVenta("01");
            nuevoPedidoEESS.EstablecerReferenciaAlmacenDeVenta("24");
            nuevoPedidoEESS.EstablecerReferenciaMonedaDeVenta(codigoMoneda);
            nuevoPedidoEESS.EstablecerReferenciaEstadoDocumentoDeVenta("OK");
            nuevoPedidoEESS.EstablecerReferenciaCondicionPagoDeVenta( "00");
            nuevoPedidoEESS.EstablecerReferenciaVendedorDeVenta("76408758");
            nuevoPedidoEESS.EstablecerReferenciaUsuarioSistemaDeVenta("VENDPLAYA");
            nuevoPedidoEESS.EstablecerReferenciaImpuestoIgvDeCliente("IV");
            nuevoPedidoEESS.EstablecerReferenciaImpuestoIscDeCliente("SC");
            nuevoPedidoEESS.EstablecerReferenciaClienteDeVenta(codigoCliente);
            nuevoPedidoEESS.EstablecerReferenciaClaseTipoCambioDeVenta("TCONV");
            nuevoPedidoEESS.EstablecerReferenciaConfiguracionPuntoVentaDeVenta("PTOVTA02");
            nuevoPedidoEESS.EstablecerReferenciaEstadoDeVenta("1");
            nuevoPedidoEESS.EstablecerReferenciaMonedaCreditoDeVenta("PEN");
            nuevoPedidoEESS.EstablecerReferenciaClaseTipoCambioClienteCreditoDeVenta("TCONV");
            nuevoPedidoEESS.EstablecerReferenciaTarjetaPromocionDeVenta("232323");
            nuevoPedidoEESS.EstablecerReferenciaTarjetaDeVenta(codigoTarjeta);            
            nuevoPedidoEESS.EstablecerReferenciaMonedaTarjetaDeVenta(codigoMoneda);

            nuevoPedidoEESS.AgregarNuevoPedidoEESSDetalle(1, 1, "7452", 
                                                        0,0, 0,
                                                        0,19,0,
                                                        23.00M,0,12.90M, 
                                                        0, true, true, 
                                                        2.6M, 2.6M,0,
                                                        0,"ARTICULO PRUEBA",1,
                                                        0,true, "",
                                                        "40101","UND", "");

            nuevoPedidoEESS.AgregarNuevoPedidoEESSConVale(5465);                                                        

            _IRepositorioPedidoEESS.Agregar(nuevoPedidoEESS);

            var pedidoEncontrado = _IRepositorioPedidoEESS.ObtenerPorNumeroPedido(correlativo);

            Assert.True(correlativo == pedidoEncontrado.Correlativo);
        }

        [Fact]
        public void ObtenerPorNumeroPedido_Test()
        {
            var numeroPedido = 1;
            var pedidoEESSPorNumero = _IRepositorioPedidoEESS.ObtenerPorNumeroPedido(numeroPedido);

            Assert.True(pedidoEESSPorNumero.Correlativo == numeroPedido);            
        }     

        [Fact]
        public void ObtenerTodos_Test()
        {
            var pedidosEESS = _IRepositorioPedidoEESS.ObtenerTodos("PTOVTA02");

            Assert.False(pedidosEESS == null);            
        }            
    }
}