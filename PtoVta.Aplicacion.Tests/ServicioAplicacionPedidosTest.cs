using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Aplicacion.GestionPedidos;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura;
using PtoVta.Infraestructura.Repositorios.Inventarios;
using PtoVta.Infraestructura.Repositorios.Parametros;
using PtoVta.Infraestructura.Repositorios.Ventas;
using PtoVta.Infraestructura.Transversales.Adaptador;
using PtoVta.Infraestructura.Transversales.Log;
using PtoVta.Infraestructura.TransversalesNET.Adaptador;
using PtoVta.Infraestructura.TransversalesNET.Log;
using Xunit;

namespace PtoVta.Aplicacion.Tests
{
    public class ServicioAplicacionPedidosTest
    {
        private IRepositorioPedidoEESS _IRepositorioPedidoEESS;
        private IRepositorioPedidoRetail _IRepositorioPedidoRetail;
        private IRepositorioArticulo _IRepositorioArticulo;
        private IRepositorioMoneda _IRepositorioMoneda;
        private IRepositorioTarjeta _IRepositorioTarjeta;

        private IServicioAplicacionPedidos _IServicioAplicacionPedidos;

        public ServicioAplicacionPedidosTest()
        {
            _IRepositorioPedidoEESS = new RepositorioPedidoEESS(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioPedidoRetail = new RepositorioPedidoRetail(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioArticulo = new RepositorioArticulo(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioMoneda = new RepositorioMoneda(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioTarjeta = new RepositorioTarjeta(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);

            _IServicioAplicacionPedidos = new ServicioAplicacionPedidos(_IRepositorioPedidoEESS, _IRepositorioPedidoRetail,
                                    _IRepositorioArticulo, _IRepositorioMoneda, _IRepositorioTarjeta);

            LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

            var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
            TipoAdaptadorFactory.EstablecerActual(adaptadorFactory);
        }

        [Fact]
        public void AgregarNuevoPedidoEESS_Test()
        {
            var correlativo = 2001;
            var ruCliente = "20482215999";
            var codigoMoneda = "PEN";
            var codigoTarjeta = "01";
            var numeroDocumentoNuevo = "B04300212011";
            var codigoAlmacen = "24";

            var nuevoPedidoEESS = new PedidoEESSDTO()
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
                RucCliente = ruCliente,
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
                DescripcionTarjeta = "VISA",

                CodigoTipoDocumento = "12",
                CodigoTipoPago = "01",
                CodigoAlmacen = "24",
                CodigoMoneda = codigoMoneda,
                CodigoEstadoDocumento = "OK",
                CodigoCondicionPago = "00",
                CodigoVendedor = "76408758",
                CodigoUsuarioDeSistema = "VENDPLAYA",
                CodigoImpuestoIgv = "IV",
                CodigoImpuestoIsc = "SC",
                CodigoCliente = ruCliente,
                CodigoClaseTipoCambio = "TCONV",
                CodigoPuntoDeVenta = "PTOVTA02",
                CodigoEstado = "1",
                CodigoMonedaCredito = "PEN",
                CodigoClaseTipoCambioClienteCredito = "TCONV",
                CodigoTarjetaPromocion = "232323",
                CodigoTarjeta = codigoTarjeta,
                CodigoMonedaTarjeta = codigoMoneda
            };

            nuevoPedidoEESS.PedidoEESSDetalles = new List<PedidoEESSDetalleDTO>(){
                new PedidoEESSDetalleDTO{
                    // Correlativo = correlativo,
                    Secuencia = 1,
                    // NumeroDocumento = "",
                    // FechaDocumento = "",
                    // FechaProceso = "",
                    // Periodo = "",
                    // ProcesadoCierreZ = "",
                    // ProcesadoCierreX = "",
                    NumeroTurno = 1,
                    // NumeroCara = "",  
                    NumeroTransaccionCombustible = "7452",
                    PorcentajeDescuentoPrimero = 0,
                    PorcentajeDescuentoSegundo = 0,
                    PorcentajeDescuentoNacional = 0,
                    PorcentajeDescuentoExtranjera = 0,
                    PorcentajeImpuestoIgv = 19,
                    PorcentajeImpuestoIsc = 0,
                    TotalNacional = 23.00M,
                    TotalExtranjera = 0,
                    ImpuestoNacional = 12.90M,
                    ImpuestoExtranjera = 0,
                    EsInventariable = true,
                    EnInventarioFisico = true,
                    Precio = 2.6M,
                    PrecioVenta = 2.6M,
                    CostoEstandarNacional = 0,
                    CostoEstandarExtranjera = 0,
                    DescripcionArticulo = "ARTICULO PRUEBA",
                    Cantidad = 1,
                    EsFormula = 0,
                    EsArticuloCombustible = true,
                    NumeroPeaje = "",     

                    // CodigoTipoDocumento  = "",
                    CodigoAlmacen = codigoAlmacen,
                    CodigoArticulo = "40101",
                    // CodigoMoneda = "",
                    // CodigoEstadoDocumento = "",          
                    // CodigoPuntoDeVenta = "",
                    CodigoUnidadDeMedida = "UND", 
                    // CodigoUsuarioDeSistema = "",   
                    CodigoArticuloAlterno = ""
                },
                new PedidoEESSDetalleDTO{
                    // Correlativo = correlativo,
                    Secuencia = 2,
                    // NumeroDocumento = "",
                    // FechaDocumento = "",
                    // FechaProceso = "",
                    // Periodo = "",
                    // ProcesadoCierreZ = "",
                    // ProcesadoCierreX = "",
                    NumeroTurno = 1,
                    // NumeroCara = "",  
                    NumeroTransaccionCombustible = "7452",
                    PorcentajeDescuentoPrimero = 0,
                    PorcentajeDescuentoSegundo = 0,
                    PorcentajeDescuentoNacional = 0,
                    PorcentajeDescuentoExtranjera = 0,
                    PorcentajeImpuestoIgv = 19,
                    PorcentajeImpuestoIsc = 0,
                    TotalNacional = 23.00M,
                    TotalExtranjera = 0,
                    ImpuestoNacional = 12.90M,
                    ImpuestoExtranjera = 0,
                    EsInventariable = true,
                    EnInventarioFisico = true,
                    Precio = 2.6M,
                    PrecioVenta = 2.6M,
                    CostoEstandarNacional = 0,
                    CostoEstandarExtranjera = 0,
                    DescripcionArticulo = "ARTICULO PRUEBA",
                    Cantidad = 1,
                    EsFormula = 0,
                    EsArticuloCombustible = true,
                    NumeroPeaje = "",     

                    // CodigoTipoDocumento  = "",
                    CodigoAlmacen = codigoAlmacen,
                    CodigoArticulo = "40101",
                    // CodigoMoneda = "",
                    // CodigoEstadoDocumento = "",          
                    // CodigoPuntoDeVenta = "",
                    CodigoUnidadDeMedida = "UND", 
                    // CodigoUsuarioDeSistema = "",   
                    CodigoArticuloAlterno = ""
                }
            };

            nuevoPedidoEESS.PedidoEESSConVales = new List<PedidoEESSConValeDTO>(){
                new PedidoEESSConValeDTO{
                    // Correlativo = correlativo,
                    NumeroVale = 7777,
                    // CodigoCliente = "",
                    // CodigoAlmacen = ""
                }
            };

            ResultadoServicio<ResultadoPedidoEESSGrabadoDTO> pedidoEESSCreado = _IServicioAplicacionPedidos
                            .AgregarNuevoPedidoEESS(nuevoPedidoEESS);

            ResultadoServicio<PedidoEESSDTO> pedidoEESPorCorrelativoBuscado = _IServicioAplicacionPedidos
                            .BuscarPedidoEESSPorNumero(correlativo);

            Assert.True(pedidoEESSCreado.Dato.Correlativo == pedidoEESPorCorrelativoBuscado.Dato.Correlativo);
            Assert.True(pedidoEESSCreado.Dato.RucCliente.Trim() == pedidoEESPorCorrelativoBuscado.Dato.RucCliente.Trim());
        }


        [Fact]
        public void AgregarNuevoPedidoRetail_Test()
        {
            var correlativo = 1778;
            var ruCliente = "20167930868";
            var codigoMoneda = "PEN";
            var codigoTarjeta = "01";
            var numeroDocumentoNuevo = "B04300212012";
            var codigoAlmacen = "24";

            var nuevoPedidoRetail = new PedidoRetailDTO()
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
                RucCliente = ruCliente,
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
                CentroDeCosto = "0000-1111102222",

                CodigoTipoDocumento = "12",
                CodigoTipoPago = "01",
                CodigoAlmacen = codigoAlmacen,
                CodigoMoneda = codigoMoneda,
                CodigoCondicionPago = "00",
                CodigoVendedor = "76408758",
                CodigoUsuarioDeSistema = "VENDPLAYA",
                CodigoImpuestoIgv = "IV",
                CodigoImpuestoIsc = "SC ",
                CodigoCliente = ruCliente,
                CodigoClaseTipoCambio = "TCONV",
                CodigoTarjetaPromocion = "232323",
                CodigoPuntoDeVenta = "PTOVTA02",
                CodigoTipoNegocio = "2",
            };

            nuevoPedidoRetail.PedidoRetailDetalles = new List<PedidoRetailDetalleDTO>(){
                new PedidoRetailDetalleDTO{
                    Secuencia = 1, 
                    NumeroTurno = 1,
                    PorcentajeImpuestoIgv = 19, 
                    PorcentajeImpuestoIsc = 0, 
                    TotalNacional = 23.00M,
                    TotalExtranjera = 0,  
                    ImpuestoNacional = 12, 
                    ImpuestoExtranjera = 4,
                    EsInventariable = false, 
                    EnInventarioFisico = false, 
                    Precio = 12.90M,
                    PrecioVenta = 12.90M, 
                    CostoEstandarNacional = 0, 
                    CostoEstandarExtranjera = 0,
                    CodigoArticuloAlterno = "", 
                    DescripcionArticulo = "ARTICULO PRUEBA", 
                    Cantidad = 1,
                    EsFormula = 0, 
                    NumeroPeaje = "", 
                    CodigoAlmacen = codigoAlmacen,
                    CodigoArticulo = "40101", 
                    CodigoUnidadDeMedida = "UND"                   
                },
                new PedidoRetailDetalleDTO{
                    Secuencia = 2, 
                    NumeroTurno = 1,
                    PorcentajeImpuestoIgv = 19, 
                    PorcentajeImpuestoIsc = 0, 
                    TotalNacional = 23.00M,
                    TotalExtranjera = 0,  
                    ImpuestoNacional = 12, 
                    ImpuestoExtranjera = 4,
                    EsInventariable = false, 
                    EnInventarioFisico = false, 
                    Precio = 12.90M,
                    PrecioVenta = 12.90M, 
                    CostoEstandarNacional = 0, 
                    CostoEstandarExtranjera = 0,
                    CodigoArticuloAlterno = "", 
                    DescripcionArticulo = "ARTICULO PRUEBA", 
                    Cantidad = 1,
                    EsFormula = 0, 
                    NumeroPeaje = "", 
                    CodigoAlmacen = codigoAlmacen,
                    CodigoArticulo = "40101", 
                    CodigoUnidadDeMedida = "UND"                       
                }
            };  

            nuevoPedidoRetail.PedidoRetailConTarjetas = new List<PedidoRetailConTarjetaDTO>(){
                new PedidoRetailConTarjetaDTO{
                    Secuencia = 1, 
                    NumeroTarjeta = "2356", 
                    TotalTarjetaNacional = 23.0M,
                    TotalTarjetaExtranjera = 0, 
                    EsTransaccionPinPad = 0, 
                    TipoTarjeta = "A",
                    DNIAsociadoATarjeta = "", 
                    DescripcionTarjeta = "VISA",  
                    CodigoTarjeta = codigoTarjeta,
                    CodigoMoneda = codigoMoneda
                }
            };

            nuevoPedidoRetail.PedidoRetailConVales = new List<PedidoRetailConValeDTO>(){
                new PedidoRetailConValeDTO{
                    NumeroVale = 5466
                }
            };       

            ResultadoServicio<ResultadoPedidoRetailGrabadoDTO> categorias = _IServicioAplicacionPedidos
                            .AgregarNuevoPedidoRetail(nuevoPedidoRetail);

            ResultadoServicio<PedidoRetailDTO> pedidoRetailPorCorrelativoBuscado = _IServicioAplicacionPedidos
                            .BuscarPedidoRetailPorNumero(correlativo);

            Assert.True(categorias.Dato.Correlativo == pedidoRetailPorCorrelativoBuscado.Dato.Correlativo);
            Assert.True(categorias.Dato.RucCliente.Trim() == pedidoRetailPorCorrelativoBuscado.Dato.RucCliente.Trim());
        }


        [Fact]
        public void BuscarPedidoEESSPorPuntoDeVenta_Test()
        {
            var codigoPuntoDeVenta = "PTOVTA02";
            ResultadoServicio<PedidoEESSListadoDTO> pedidosEESS = _IServicioAplicacionPedidos
                            .BuscarPedidoEESSPorPuntoDeVenta(codigoPuntoDeVenta);

            Assert.True(pedidosEESS.Datos.Any() == true);
        }


        [Fact]
        public void BuscarPedidoEESSPorNumero_Test()
        {
            var correlativo = 1777;
            ResultadoServicio<PedidoEESSDTO> pedidoEESPorCorrelativo = _IServicioAplicacionPedidos
                            .BuscarPedidoEESSPorNumero(correlativo);

            Assert.True(pedidoEESPorCorrelativo.Dato.Correlativo == correlativo);
        }


        [Fact]
        public void BuscarPedidoRetailPorPuntoDeVenta_Test()
        {
            var codigoPuntoDeVenta = "PTOVTA02";
            ResultadoServicio<PedidoRetailListadoDTO> pedidosRestail = _IServicioAplicacionPedidos
                            .BuscarPedidoRetailPorPuntoDeVenta(codigoPuntoDeVenta);

            Assert.True(pedidosRestail.Datos.Any() == true);
        }


        [Fact]
        public void BuscarPedidoRetailPorNumero_Test()
        {
            var correlativo = 1778;
            ResultadoServicio<PedidoRetailDTO> pedidoRetailPorCorrelativo = _IServicioAplicacionPedidos
                            .BuscarPedidoRetailPorNumero(correlativo);

            Assert.True(pedidoRetailPorCorrelativo.Dato.Correlativo == correlativo);
        }

    }
}