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

    public class RepositorioVentaTest
    {
        private readonly IRepositorioVenta _IRepositorioVenta;
        public RepositorioVentaTest()
        {
            _IRepositorioVenta = new RepositorioVenta(ConfiguracionGlobal.CadenaConexionBd);

        }


        [Fact]
        public void Agregar_Test()
        {
            var codigoMoneda = "PEN";
            var codigoTarjeta = "01";
            var codigoCliente = "20167930868";
            var numeroDocumentoNuevo = "B04300212012";

            var ventaNueva = new Venta()
            {
                NumeroDocumento = numeroDocumentoNuevo,
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
                TotalAfectoNacional = 0,
                ValorVenta = 0,
                PorcentajeDescuentoPrimero = 0,
                PorcentajeDescuentoSegundo = 0,
                TotalDescuentoNacional = 0,
                TotalDescuentoExtranjera = 0,
                TotalVueltoNacional = 0,
                TotalVueltoExtranjera = 0,
                TotalEfectivoNacional = 0,
                TotalEfectivoExtranjera = 0,
                RucCliente = codigoCliente,
                NombreCompletoCliente = "PTS S.A - VENTA DE PRUEBA 2",
                Placa = "SQL-2020",
                NumeroVale = 7777,
                TipoCambio = 3.56M,
                ProcesadoCierreZ = false,
                ProcesadoCierreX = false,
                Kilometraje = 0,
                AfectaInventario = true
            };

            ventaNueva.EstablecerMonedaDeVenta(new Moneda{CodigoMoneda = "PEN"});
            ventaNueva.EstablecerClaseTipoCambioDeVenta(new ClaseTipoCambio{ CodigoClaseTipoCambio = "TCONV"});
            ventaNueva.EstablecerClienteDeVenta(new Cliente{CodigoCliente = codigoCliente});
            ventaNueva.EstablecerTipoDocumentoDeVenta(new TipoDocumento{ CodigoTipoDocumento = "12"});
            ventaNueva.EstablecerEstadoDocumentoDeVenta(new EstadoDocumento{ CodigoEstadoDocumento = "OK"});
            ventaNueva.EstablecerVendedorDeVenta(new Vendedor{ CodigoVendedor = "76408758" });
            ventaNueva.EstablecerCondicionPagoDeVenta(new CondicionPago{ CodigoCondicionPago = "00"});
            ventaNueva.EstablecerTipoPagoDeVenta(new TipoPago{ CodigoTipoPago = "01"});
            ventaNueva.EstablecerConfiguracionPuntoVentaDeVenta(new ConfiguracionPuntoVenta{CodigoPuntoDeVenta = "PTOVTA02"});
            ventaNueva.EstablecerAlmacenDeVenta(new Almacen{ CodigoAlmacen = "24"});
            ventaNueva.EstablecerTipoNegocioDeVenta(new TipoNegocio{ CodigoTipoNegocio = "1" });
            ventaNueva.EstablecerUsuarioSistemaDeVenta(new UsuarioSistema{ CodigoUsuarioDeSistema = "VENDPLAYA"});
            ventaNueva.EstablecerImpuestoIgvDeCliente(new Impuesto{CodigoImpuesto = "IV" });
            ventaNueva.EstablecerImpuestoIscDeCliente(new Impuesto{ CodigoImpuesto = "SC"});

            ventaNueva.AgregarNuevaVentaDetalle(1,1, "1",
                18, 0, 52.25M, 
                35.25M, 25.23M, 12.56M,
                0,0,0,
                12.56M, 12.56M,"PANETON BUON NATALE",
                10, 0,"40101",
                string.Empty, true, false);

            ventaNueva.AgregarNuevaVentaConTarjeta(1, "7557", 56.23M,
                35.26M,codigoMoneda, codigoTarjeta);

            ventaNueva.AgregarNuevaVentaConVale(7777, 56.52M);

            ventaNueva.AgregarNuevaCuentaPorCobrar(8888, DateTime.Now,
                        0, 0, 0,
                        0, 0, 0,
                        "PE", "DEFAULT0",string.Empty);
                    
            ventaNueva.AgregarNuevoDocumentoAnticipado();

            _IRepositorioVenta.Agregar(ventaNueva);


            var numeroDocumentoExistente = _IRepositorioVenta.ObtenerNumeroDocumentoVenta("12", numeroDocumentoNuevo, "24");

            Assert.True(numeroDocumentoExistente == numeroDocumentoNuevo);
        }

        [Fact]
        public void ObtenerNumeroDocumentoVenta_Test()
        {
            var numeroDocumentoExistente = _IRepositorioVenta.ObtenerNumeroDocumentoVenta("12", "B04300212009", "24");

            Assert.False(numeroDocumentoExistente == string.Empty);
        }

        [Fact]
        public void ObtenerVentasPorCodigoCliente_Test()
        {
            var ventasPorCliente = _IRepositorioVenta.ObtenerVentasPorCodigoCliente("20167930868");

            Assert.False(ventasPorCliente == null);
        }

        [Fact]
        public void ObtenerPagoVentaAdelantada_Test()
        {
            var ventaPagoAdelantado = _IRepositorioVenta.ObtenerPagoVentaAdelantada("20493077211", "24",
                                                                            "01", DateTime.Now);

            Assert.False(ventaPagoAdelantado == null);
        }

        [Fact]
        public void ObtenerConsumoVentaAdelantada_Test()
        {
            var consumosVentasAdelantada = _IRepositorioVenta.ObtenerConsumoVentaAdelantada("14", "20493077211",
                                                                                        "24", "12", DateTime.Now);

            Assert.False(consumosVentasAdelantada == null);
        }

        [Fact]
        public void ObtenerTodos_Test()
        {
            var ventas = _IRepositorioVenta.ObtenerTodos("24", "20191001", "20191031",
                                                        "F04800000166", "3");

            Assert.False(ventas == null);
        }

    }

}