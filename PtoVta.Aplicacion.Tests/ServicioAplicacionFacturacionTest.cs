using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Aplicacion.GestionVentas;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.CuentasPorCobrar;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Inventarios;
using PtoVta.Infraestructura.Repositorios.Parametros;
using PtoVta.Infraestructura.Repositorios.Usuario;
using PtoVta.Infraestructura.Repositorios.Ventas;
using PtoVta.Infraestructura.Transversales.Adaptador;
using PtoVta.Infraestructura.Transversales.Log;
using PtoVta.Infraestructura.TransversalesNET.Adaptador;
using PtoVta.Infraestructura.TransversalesNET.Log;
using Xunit;

namespace PtoVta.Aplicacion.Tests
{
    public class ServicioAplicacionFacturacionTest
    {
        private IRepositorioVenta _IRepositorioVenta;
        private IRepositorioEstadoDocumento _IRepositorioEstadoDocumento;
        private IRepositorioTipoDocumento _IRepositorioTipoDocumento;
        private IRepositorioCliente _IRepositorioCliente;
        private IRepositorioClaseTipoCambio _IRepositorioClaseTipoCambio;
        private IRepositorioVendedor _IRepositorioVendedor;
        private IRepositorioMoneda _IRepositorioMoneda;
        private IRepositorioConfiguracionPuntoVenta _IRepositorioConfiguracionPuntoVenta;
        private IRepositorioConfiguracionGeneral _IRepositorioConfiguracionGeneral;
        private IRepositorioTipoPago _IRepositorioTipoPago;
        private IRepositorioTipoMovimientoAlmacen _IRepositorioTipoMovimientoAlmacen;
        private IRepositorioCondicionPago _IRepositorioCondicionPago;
        private IRepositorioArticulo _IRepositorioArticulo;
        private IRepositorioTarjeta _IRepositorioTarjeta;
        private IRepositorioMovimientoAlmacen _IRepositorioMovimientoAlmacen;
        private IRepositorioAlmacen _IRepositorioAlmacen;
        private IRepositorioTipoNegocio _IRepositorioTipoNegocio;
        private IRepositorioUsuarioSistema _IRepositorioUsuarioSistema;
        private IRepositorioPedidoEESS _IRepositorioPedidoEESS;
        private IRepositorioPedidoRetail _IRepositorioPedidoRetail;
        private IRepositorioListaPrecioCliente _IRepositorioListaPrecioCliente;
        private IRepositorioListaPrecioInventario _IRepositorioListaPrecioInventario;
        private IRepositorioConfiguracionFormatoTicket _IRepositorioConfiguracionFormatoTicket;
        private IRepositorioConfiguracionInventario _IRepositorioConfiguracionInventario;
        private IRepositorioConfiguracionVenta _IRepositorioConfiguracionVenta;
        private IServicioDominioListaPrecios _IServicioDominioListaPrecios;
        private IConfiguracionGlobalUnificado _IConfiguracionGlobalUnificado;

        private IServicioDominioVentas _IServicioDominioVentas;
        private IServicioDominioMovimientosAlmacen _IServicioDominioMovimientosAlmacen;
        private IServicioDominioCuentaPorCobrar _IServicioDominioCuentaPorCobrar;

        private IServicioAplicacionFacturacion _IServicioAplicacionFacturacion;


        public ServicioAplicacionFacturacionTest()
        {
            _IRepositorioVenta = new RepositorioVenta(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioEstadoDocumento = new RepositorioEstadoDocumento(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioTipoDocumento = new RepositorioTipoDocumento(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioCliente = new RepositorioCliente(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioClaseTipoCambio = new RepositorioClaseTipoCambio(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioVendedor = new RepositorioVendedor(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioMoneda = new RepositorioMoneda(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionPuntoVenta = new RepositorioConfiguracionPuntoVenta(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionGeneral = new RepositorioConfiguracionGeneral(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioTipoPago = new RepositorioTipoPago(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioTipoMovimientoAlmacen = new RepositorioTipoMovimientoAlmacen(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioCondicionPago = new RepositorioCondicionPago(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioArticulo = new RepositorioArticulo(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioTarjeta = new RepositorioTarjeta(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioMovimientoAlmacen = new RepositorioMovimientoAlmacen(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioAlmacen = new RepositorioAlmacen(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioTipoNegocio = new RepositorioTipoNegocio(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioUsuarioSistema = new RepositorioUsuarioSistema(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioPedidoEESS = new RepositorioPedidoEESS(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioPedidoRetail = new RepositorioPedidoRetail(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioListaPrecioCliente = new RepositorioListaPrecioCliente(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioListaPrecioInventario = new RepositorioListaPrecioInventario(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionFormatoTicket = new RepositorioConfiguracionFormatoTicket(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionInventario = new RepositorioConfiguracionInventario(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);
            _IRepositorioConfiguracionVenta = new RepositorioConfiguracionVenta(Infraestructura.ConfiguracionGlobal.CadenaConexionBd);

            _IConfiguracionGlobalUnificado = new ConfiguracionGlobalUnificado(_IRepositorioConfiguracionFormatoTicket,
                                                _IRepositorioConfiguracionGeneral, _IRepositorioConfiguracionInventario,
                                                _IRepositorioConfiguracionVenta);

            _IServicioDominioVentas = new ServicioDominioVentas();
            _IServicioDominioMovimientosAlmacen = new ServicioDominioMovimientosAlmacen();
            _IServicioDominioCuentaPorCobrar = new ServicioDominioCuentaPorCobrar();
            _IServicioDominioListaPrecios = new ServicioDominioListaPrecios();


            _IServicioAplicacionFacturacion = new ServicioAplicacionFacturacion(_IRepositorioVenta, _IRepositorioEstadoDocumento,
                                                        _IRepositorioTipoDocumento, _IRepositorioCliente,
                                                        _IRepositorioClaseTipoCambio, _IRepositorioVendedor,
                                                        _IRepositorioMoneda, _IRepositorioConfiguracionPuntoVenta,
                                                        _IRepositorioConfiguracionGeneral, _IRepositorioTipoPago,
                                                         _IRepositorioTipoMovimientoAlmacen, _IRepositorioCondicionPago,
                                                        _IRepositorioArticulo, _IRepositorioTarjeta,
                                                        _IRepositorioMovimientoAlmacen, _IRepositorioAlmacen,
                                                        _IRepositorioTipoNegocio, _IRepositorioUsuarioSistema,
                                                        _IRepositorioPedidoEESS, _IRepositorioPedidoRetail,
                                                        _IRepositorioListaPrecioCliente, _IRepositorioListaPrecioInventario,
                                                        _IServicioDominioVentas, _IServicioDominioMovimientosAlmacen,
                                                        _IServicioDominioCuentaPorCobrar, _IServicioDominioListaPrecios,
                                                        _IConfiguracionGlobalUnificado);

            LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

            var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
            TipoAdaptadorFactory.EstablecerActual(adaptadorFactory);
        }


        [Fact]
        public void AgregarNuevaVenta_Test()
        {
            var rucCliente = "20440484345";
            var codigoMoneda = "PEN";
            var codigoTarjeta = "01";
            var numeroDocumentoNuevo = "F04200040931"; //"1200001200";
            var codigoAlmacen = "24";
            var codigoTipoNegocio = "1";
            var fechaInicioVentas = "20200101";
            var fechaFinVentas = "20201231";

            var nuevoVentaDto = new VentaDTO()
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
                RucCliente = rucCliente,
                NombreCompletoCliente = "PTS S.A - VENTA DE PRUEBA 2",
                Placa = "SQL-2020",
                NumeroVale = 7777,
                TipoCambio = 3.56M,
                ProcesadoCierreZ = false,
                ProcesadoCierreX = false,
                Kilometraje = 0,
                // TipoPagoCodigoTipoPago = "",

                EsVentaPagoAdelantado = false,
                TipoDeVenta = "M",
                FlagCambioDeMonedaEnVuelto = false,
                TotalFaltanteExtranjera = 0,
                TotalFaltanteNacional = 0,
                CodigoMonedaVuelto = codigoMoneda,

                CodigoMoneda = codigoMoneda,
                CodigoClaseTipoCambio = "TCONV",
                CodigoCliente = rucCliente,
                CodigoTipoDocumento = "12",
                CodigoEstadoDocumento = "OK",
                CodigoVendedor = "76408758",
                CodigoCondicionPago = "00",
                CodigoTipoPago = "01",
                CodigoPuntoDeVenta = "PTOVTA02",
                CodigoAlmacen = codigoAlmacen,
                CodigoTipoNegocio = "1",
                CodigoUsuarioDeSistema = "VENDPLAYA",
                CodigoImpuestoIgv = "IV",
                CodigoImpuestoIsc = "SC",
            };

            nuevoVentaDto.TipoPago = new TipoPagoDTO { };

            nuevoVentaDto.VentaDetalles = new List<VentaDetalleDTO>(){
                new VentaDetalleDTO{
                            Secuencia = 1,
                            NumeroTurno = 1,
                            NumeroCara = "1",
                            PorcentajeImpuestoIgv = 18,
                            PorcentajeImpuestoIsc = 0,
                            TotalNacional = 52.25M,
                            TotalExtranjera = 35.25M,
                            ImpuestoNacional = 25.23M,
                            ImpuestoExtranjera = 12.56M,
                            PorcentajeDescuentoPrimero = 0,
                            TotalDescuentoNacional = 0,
                            TotalDescuentoExtranjera = 0,
                            Precio = 12.56M,
                            PrecioVenta = 12.56M,
                            DescripcionArticulo = "PECSA LUBRIMAX GASOLINERO SAE 40 API *1/4 GLN",
                            Cantidad = 2,
                            EsFormula = 0,
                            EsInventariable = true,
                            EnInventarioFisico = false,
                            CodigoArticulo = "030109",
                            CodigoArticuloAlterno = ""
                }
                ,
                new VentaDetalleDTO{
                            Secuencia = 2,
                            NumeroTurno = 1,
                            NumeroCara = "1",
                            PorcentajeImpuestoIgv = 18,
                            PorcentajeImpuestoIsc = 0,
                            TotalNacional = 52.25M,
                            TotalExtranjera = 35.25M,
                            ImpuestoNacional = 25.23M,
                            ImpuestoExtranjera = 12.56M,
                            PorcentajeDescuentoPrimero = 0,
                            TotalDescuentoNacional = 0,
                            TotalDescuentoExtranjera = 0,
                            Precio = 12.56M,
                            PrecioVenta = 12.56M,
                            DescripcionArticulo = "ACEITE 2 T",
                            Cantidad = 2,
                            EsFormula = 0,
                            EsInventariable = true,
                            EnInventarioFisico = false,
                            CodigoArticulo = "20101",
                            CodigoArticuloAlterno = ""
                }
            };

            nuevoVentaDto.VentaConTarjetas = new List<VentaConTarjetaDTO>(){
                new VentaConTarjetaDTO{
                    Secuencia = 1,
                    NumeroTarjeta = "7557",
                    TotalTarjetaNacional = 56.23M,
                    TotalTarjetaExtranjera = 35.26M,
                    CodigoMoneda = codigoMoneda,
                    CodigoTarjeta = codigoTarjeta
                }
            };

            nuevoVentaDto.VentaConVales = new List<VentaConValeDTO>(){
                new VentaConValeDTO{
                    NumeroVale = 5555,
                    MontoVale = 78.23M
                }
            };

            // nuevoVentaDto.DocumentosAnticipado = new List<DocumentoAnticipadoDTO>(){
            //     new DocumentoAnticipadoDTO{

            //     }
            // };

            // nuevoVentaDto.CuentasPorCobrar = new List<CuentaPorCobrarDTO>()
            // {
            //     new CuentaPorCobrarDTO{
            //         Referencia = 9999, 
            //         FechaVencimiento = DateTime.Now, 
            //         PagoDocumentoNacional = 0, 
            //         PagoDocumentoExtranjera = 0,
            //         SaldoDocumentoNacional = 0, 
            //         SaldoDocumentoExtranjera = 0, 
            //         DiasDeGracia = 0, 
            //         NumeroVale = 0, 
            //         CodigoEstadoDocumento = "PE", 
            //         CodigoDiaDePago = "DEFAULT0", 
            //         CodigoTipoDocumentoReferencia = string.Empty
            //     }
            // };


            ResultadoServicio<ResultadoVentaGrabadaDTO> nuevaVentaCreado =
                        _IServicioAplicacionFacturacion.AgregarNuevaVenta(nuevoVentaDto);

            ResultadoServicio<VentaListadoDTO> ventaBuscada =
                        _IServicioAplicacionFacturacion.BuscarVentas(codigoAlmacen, fechaInicioVentas,
                                                fechaFinVentas, numeroDocumentoNuevo, codigoTipoNegocio);

            Assert.True(nuevaVentaCreado.Dato.NumeroDocumento.Trim() == ventaBuscada.Datos.FirstOrDefault().NumeroDocumento.Trim());
            Assert.True(nuevaVentaCreado.Dato.RucCliente.Trim() == ventaBuscada.Datos.FirstOrDefault().RucCliente.Trim());
        }




        #region VentasEfectivo

        [Fact]
        public void AgregarNuevaVenta_TFactura_Efectivo_Exacto_Test()
        {
            //PAGO EFECTIVO
            var rucCliente = "20167930868";
            var razonSocialCliente = "PTS S.A.C.a.";
            var codigoMoneda = "PEN";
            var codigoAlmacen = "24";
            var codigoTipoNegocio = "2";
            var numeroPlaca = "";
            var tipoDeCambio = 3.56M;
            var tipoDeVenta = "A";
            var codigoTipoDocumento = "12";
            var numeroCara = "";
            var porcentajeImpuestoIgv = 18;
            var numeroTurno = 1;
            var numeroVale = 77777;
            var fechaInicioVentas = "20200101";
            var fechaFinVentas = "20201231";
            // var codigoTarjeta = "01";
            var numeroDocumentoNuevo = "F04200040953"; //"1200001200";

            var nuevoVentaDto = new VentaDTO()
            {
                NumeroDocumento = numeroDocumentoNuevo,
                FechaDocumento = DateTime.Now,
                FechaProceso = DateTime.Now,
                Periodo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString(),
                TotalNacional = 0,
                TotalExtranjera = 0,
                SubTotalNacional = 0,
                SubTotalExtranjera = 0,
                ImpuestoIgvNacional = 0,
                ImpuestoIgvExtranjera = 0,
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
                TotalEfectivoNacional = 40.80M,
                TotalEfectivoExtranjera = 0,
                RucCliente = rucCliente,
                NombreCompletoCliente = razonSocialCliente,
                Placa = numeroPlaca,
                NumeroVale = numeroVale,
                TipoCambio = tipoDeCambio,
                ProcesadoCierreZ = false,
                ProcesadoCierreX = false,
                Kilometraje = 0,
                // TipoPagoCodigoTipoPago = "",

                EsVentaPagoAdelantado = false,
                TipoDeVenta = tipoDeVenta,
                FlagCambioDeMonedaEnVuelto = false,
                TotalFaltanteExtranjera = 0,
                TotalFaltanteNacional = 0,
                CodigoMonedaVuelto = codigoMoneda,

                CodigoMoneda = codigoMoneda,
                CodigoClaseTipoCambio = "TCONV",
                CodigoCliente = rucCliente,
                CodigoTipoDocumento = codigoTipoDocumento,
                CodigoEstadoDocumento = "OK",
                CodigoVendedor = "76408758",
                CodigoCondicionPago = "00",
                CodigoTipoPago = "01",
                CodigoPuntoDeVenta = "PTOVTA02",
                CodigoAlmacen = codigoAlmacen,
                CodigoTipoNegocio = codigoTipoNegocio,
                CodigoUsuarioDeSistema = "VENDPLAYA",
                CodigoImpuestoIgv = "IV",
                CodigoImpuestoIsc = "SC",
            };

            nuevoVentaDto.Cliente = new ClienteDTO
            {
                CodigoCliente = rucCliente,
                CodigoContable = string.Empty,
                Ruc = rucCliente,
                NombresORazonSocial = razonSocialCliente,
                Telefono = "88888888",
                DireccionPrimeroPais = "PERU",
                DireccionPrimeroDepartamento = "LIMA",
                DireccionPrimeroProvincia = "LIMA",
                DireccionPrimeroDistrito = "LIMA",
                DireccionPrimeroUbicacion = "AV. LOS FRUTALES NRO. 945 URB. SANTA MAGDALENA SOFIA LIMA  - LIMA  - LA MOLINA (88888888)",
                DireccionSegundoPais = "PERU",
                DireccionSegundoDepartamento = "LIMA",
                DireccionSegundoProvincia = "LIMA",
                DireccionSegundoDistrito = "LIMA",
                DireccionSegundoUbicacion = "Conde de Avenue (88888888)"

            };

            nuevoVentaDto.TipoPago = new TipoPagoDTO { };

            nuevoVentaDto.VentaDetalles = new List<VentaDetalleDTO>(){
                new VentaDetalleDTO{
                            Secuencia = 1,
                            NumeroTurno = numeroTurno,
                            NumeroCara = numeroCara,
                            PorcentajeImpuestoIgv = porcentajeImpuestoIgv,
                            PorcentajeImpuestoIsc = 0,
                            TotalNacional = 0,
                            TotalExtranjera = 0,
                            ImpuestoNacional = 0,
                            ImpuestoExtranjera = 0,
                            PorcentajeDescuentoPrimero = 0,
                            TotalDescuentoNacional = 0,
                            TotalDescuentoExtranjera = 0,
                            Precio = 12.56M,
                            PrecioVenta = 0,
                            DescripcionArticulo = "PANETON BUON NATALE ",
                            Cantidad = 2,
                            EsFormula = 0,
                            EsInventariable = true,
                            EnInventarioFisico = false,
                            CodigoArticulo = "40101",
                            CodigoArticuloAlterno = ""
                }
                ,
                new VentaDetalleDTO{
                            Secuencia = 2,
                            NumeroTurno = numeroTurno,
                            NumeroCara = numeroCara,
                            PorcentajeImpuestoIgv = porcentajeImpuestoIgv,
                            PorcentajeImpuestoIsc = 0,
                            TotalNacional = 0,
                            TotalExtranjera = 0,
                            ImpuestoNacional = 0,
                            ImpuestoExtranjera = 0,
                            PorcentajeDescuentoPrimero = 0,
                            TotalDescuentoNacional = 0,
                            TotalDescuentoExtranjera = 0,
                            Precio = 12.56M,
                            PrecioVenta = 0,
                            DescripcionArticulo = "PANETON GIORGINO 900 G",
                            Cantidad = 2,
                            EsFormula = 0,
                            EsInventariable = true,
                            EnInventarioFisico = false,
                            CodigoArticulo = "40118",
                            CodigoArticuloAlterno = ""
                }
            };

            ResultadoServicio<ResultadoVentaGrabadaDTO> nuevaVentaCreado =
                        _IServicioAplicacionFacturacion.AgregarNuevaVenta(nuevoVentaDto);

            ResultadoServicio<VentaListadoDTO> ventaBuscada =
                        _IServicioAplicacionFacturacion.BuscarVentas(codigoAlmacen, fechaInicioVentas,
                                                fechaFinVentas, numeroDocumentoNuevo, codigoTipoNegocio);

            Assert.True(nuevaVentaCreado.Dato.NumeroDocumento.Trim() == ventaBuscada.Datos.FirstOrDefault().NumeroDocumento.Trim());
            Assert.True(nuevaVentaCreado.Dato.RucCliente.Trim() == ventaBuscada.Datos.FirstOrDefault().RucCliente.Trim());
        }



        [Fact]
        public void AgregarNuevaVenta_TFactura_Efectivo_Con_Vuelto_Test()
        {
            //PAGO EFECTIVO
            var rucCliente = "20167930868";
            var razonSocialCliente = "PTS S.A.C.a.";
            var codigoMoneda = "PEN";
            var codigoAlmacen = "24";
            var codigoTipoNegocio = "2";
            var numeroPlaca = "";
            var tipoDeCambio = 3.56M;
            var tipoDeVenta = "A";
            var codigoTipoDocumento = "12";
            var numeroCara = "";
            var porcentajeImpuestoIgv = 18;
            var numeroTurno = 1;
            var numeroVale = 77777;
            var fechaInicioVentas = "20200101";
            var fechaFinVentas = "20201231";
            // var codigoTarjeta = "01";
            var numeroDocumentoNuevo = "F04200040949"; //"1200001200";

            var nuevoVentaDto = new VentaDTO()
            {
                NumeroDocumento = numeroDocumentoNuevo,
                FechaDocumento = DateTime.Now,
                FechaProceso = DateTime.Now,
                Periodo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString(),
                TotalNacional = 0,
                TotalExtranjera = 0,
                SubTotalNacional = 0,
                SubTotalExtranjera = 0,
                ImpuestoIgvNacional = 0,
                ImpuestoIgvExtranjera = 0,
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
                TotalEfectivoNacional = 200,
                TotalEfectivoExtranjera = 0,
                RucCliente = rucCliente,
                NombreCompletoCliente = razonSocialCliente,
                Placa = numeroPlaca,
                NumeroVale = numeroVale,
                TipoCambio = tipoDeCambio,
                ProcesadoCierreZ = false,
                ProcesadoCierreX = false,
                Kilometraje = 0,
                // TipoPagoCodigoTipoPago = "",

                EsVentaPagoAdelantado = false,
                TipoDeVenta = tipoDeVenta,
                FlagCambioDeMonedaEnVuelto = false,
                TotalFaltanteExtranjera = 0,
                TotalFaltanteNacional = 0,
                CodigoMonedaVuelto = codigoMoneda,

                CodigoMoneda = codigoMoneda,
                CodigoClaseTipoCambio = "TCONV",
                CodigoCliente = rucCliente,
                CodigoTipoDocumento = codigoTipoDocumento,
                CodigoEstadoDocumento = "OK",
                CodigoVendedor = "76408758",
                CodigoCondicionPago = "00",
                CodigoTipoPago = "01",
                CodigoPuntoDeVenta = "PTOVTA02",
                CodigoAlmacen = codigoAlmacen,
                CodigoTipoNegocio = codigoTipoNegocio,
                CodigoUsuarioDeSistema = "VENDPLAYA",
                CodigoImpuestoIgv = "IV",
                CodigoImpuestoIsc = "SC",
            };

            nuevoVentaDto.Cliente = new ClienteDTO
            {
                CodigoCliente = rucCliente,
                CodigoContable = string.Empty,
                Ruc = rucCliente,
                NombresORazonSocial = razonSocialCliente,
                Telefono = "88888888",
                DireccionPrimeroPais = "PERU",
                DireccionPrimeroDepartamento = "LIMA",
                DireccionPrimeroProvincia = "LIMA",
                DireccionPrimeroDistrito = "LIMA",
                DireccionPrimeroUbicacion = "AV. LOS FRUTALES NRO. 945 URB. SANTA MAGDALENA SOFIA LIMA  - LIMA  - LA MOLINA (88888888)",
                DireccionSegundoPais = "PERU",
                DireccionSegundoDepartamento = "LIMA",
                DireccionSegundoProvincia = "LIMA",
                DireccionSegundoDistrito = "LIMA",
                DireccionSegundoUbicacion = "Conde de Avenue (88888888)"

            };

            nuevoVentaDto.TipoPago = new TipoPagoDTO { };

            nuevoVentaDto.VentaDetalles = new List<VentaDetalleDTO>(){
                new VentaDetalleDTO{
                            Secuencia = 1,
                            NumeroTurno = numeroTurno,
                            NumeroCara = numeroCara,
                            PorcentajeImpuestoIgv = porcentajeImpuestoIgv,
                            PorcentajeImpuestoIsc = 0,
                            TotalNacional = 0,
                            TotalExtranjera = 0,
                            ImpuestoNacional = 0,
                            ImpuestoExtranjera = 0,
                            PorcentajeDescuentoPrimero = 0,
                            TotalDescuentoNacional = 0,
                            TotalDescuentoExtranjera = 0,
                            Precio = 12.56M,
                            PrecioVenta = 0,
                            DescripcionArticulo = "PANETON BUON NATALE ",
                            Cantidad = 2,
                            EsFormula = 0,
                            EsInventariable = true,
                            EnInventarioFisico = false,
                            CodigoArticulo = "40101",
                            CodigoArticuloAlterno = ""
                }
                ,
                new VentaDetalleDTO{
                            Secuencia = 2,
                            NumeroTurno = numeroTurno,
                            NumeroCara = numeroCara,
                            PorcentajeImpuestoIgv = porcentajeImpuestoIgv,
                            PorcentajeImpuestoIsc = 0,
                            TotalNacional = 0,
                            TotalExtranjera = 0,
                            ImpuestoNacional = 0,
                            ImpuestoExtranjera = 0,
                            PorcentajeDescuentoPrimero = 0,
                            TotalDescuentoNacional = 0,
                            TotalDescuentoExtranjera = 0,
                            Precio = 12.56M,
                            PrecioVenta = 0,
                            DescripcionArticulo = "PANETON GIORGINO 900 G",
                            Cantidad = 2,
                            EsFormula = 0,
                            EsInventariable = true,
                            EnInventarioFisico = false,
                            CodigoArticulo = "40118",
                            CodigoArticuloAlterno = ""
                }
            };

            ResultadoServicio<ResultadoVentaGrabadaDTO> nuevaVentaCreado =
                        _IServicioAplicacionFacturacion.AgregarNuevaVenta(nuevoVentaDto);

            ResultadoServicio<VentaListadoDTO> ventaBuscada =
                        _IServicioAplicacionFacturacion.BuscarVentas(codigoAlmacen, fechaInicioVentas,
                                                fechaFinVentas, numeroDocumentoNuevo, codigoTipoNegocio);

            Assert.True(nuevaVentaCreado.Dato.NumeroDocumento.Trim() == ventaBuscada.Datos.FirstOrDefault().NumeroDocumento.Trim());
            Assert.True(nuevaVentaCreado.Dato.RucCliente.Trim() == ventaBuscada.Datos.FirstOrDefault().RucCliente.Trim());
        }



        [Fact]
        public void AgregarNuevaVenta_TFactura_Tarjeta_Exacto_Test()
        {
            //PAGO EFECTIVO
            var rucCliente = "20167930868";
            var razonSocialCliente = "PTS S.A.C.a.";
            var codigoMoneda = "PEN";
            var codigoAlmacen = "24";
            var codigoTipoNegocio = "2";
            var numeroPlaca = "";
            var tipoDeCambio = 3.56M;
            var tipoDeVenta = "A";
            var codigoTipoDocumento = "12";
            var numeroCara = "";
            var porcentajeImpuestoIgv = 18;
            var numeroTurno = 1;
            var numeroVale = 77777;
            var fechaInicioVentas = "20200101";
            var fechaFinVentas = "20201231";
            var codigoTarjeta = "01";
            var totalEfectivoNacional = 0;          //configurable 1
            var codigoTipoPago = "02";              //configurable 2
            var numeroDocumentoNuevo = "F04200040952"; //"1200001200";

            var nuevoVentaDto = new VentaDTO()
            {
                NumeroDocumento = numeroDocumentoNuevo,
                FechaDocumento = DateTime.Now,
                FechaProceso = DateTime.Now,
                Periodo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString(),
                TotalNacional = 0,
                TotalExtranjera = 0,
                SubTotalNacional = 0,
                SubTotalExtranjera = 0,
                ImpuestoIgvNacional = 0,
                ImpuestoIgvExtranjera = 0,
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
                TotalEfectivoNacional = totalEfectivoNacional,
                TotalEfectivoExtranjera = 0,
                RucCliente = rucCliente,
                NombreCompletoCliente = razonSocialCliente,
                Placa = numeroPlaca,
                NumeroVale = numeroVale,
                TipoCambio = tipoDeCambio,
                ProcesadoCierreZ = false,
                ProcesadoCierreX = false,
                Kilometraje = 0,
                // TipoPagoCodigoTipoPago = "",

                EsVentaPagoAdelantado = false,
                TipoDeVenta = tipoDeVenta,
                FlagCambioDeMonedaEnVuelto = false,
                TotalFaltanteExtranjera = 0,
                TotalFaltanteNacional = 0,
                CodigoMonedaVuelto = codigoMoneda,

                CodigoMoneda = codigoMoneda,
                CodigoClaseTipoCambio = "TCONV",
                CodigoCliente = rucCliente,
                CodigoTipoDocumento = codigoTipoDocumento,
                CodigoEstadoDocumento = "OK",
                CodigoVendedor = "76408758",
                CodigoCondicionPago = "00",
                CodigoTipoPago = codigoTipoPago,
                CodigoPuntoDeVenta = "PTOVTA02",
                CodigoAlmacen = codigoAlmacen,
                CodigoTipoNegocio = codigoTipoNegocio,
                CodigoUsuarioDeSistema = "VENDPLAYA",
                CodigoImpuestoIgv = "IV",
                CodigoImpuestoIsc = "SC",
            };

            nuevoVentaDto.Cliente = new ClienteDTO
            {
                CodigoCliente = rucCliente,
                CodigoContable = string.Empty,
                Ruc = rucCliente,
                NombresORazonSocial = razonSocialCliente,
                Telefono = "88888888",
                DireccionPrimeroPais = "PERU",
                DireccionPrimeroDepartamento = "LIMA",
                DireccionPrimeroProvincia = "LIMA",
                DireccionPrimeroDistrito = "LIMA",
                DireccionPrimeroUbicacion = "AV. LOS FRUTALES NRO. 945 URB. SANTA MAGDALENA SOFIA LIMA  - LIMA  - LA MOLINA (88888888)",
                DireccionSegundoPais = "PERU",
                DireccionSegundoDepartamento = "LIMA",
                DireccionSegundoProvincia = "LIMA",
                DireccionSegundoDistrito = "LIMA",
                DireccionSegundoUbicacion = "Conde de Avenue (88888888)"

            };

            nuevoVentaDto.TipoPago = new TipoPagoDTO { };

            nuevoVentaDto.VentaDetalles = new List<VentaDetalleDTO>(){
                new VentaDetalleDTO{
                            Secuencia = 1,
                            NumeroTurno = numeroTurno,
                            NumeroCara = numeroCara,
                            PorcentajeImpuestoIgv = porcentajeImpuestoIgv,
                            PorcentajeImpuestoIsc = 0,
                            TotalNacional = 0,
                            TotalExtranjera = 0,
                            ImpuestoNacional = 0,
                            ImpuestoExtranjera = 0,
                            PorcentajeDescuentoPrimero = 0,
                            TotalDescuentoNacional = 0,
                            TotalDescuentoExtranjera = 0,
                            Precio = 12.56M,
                            PrecioVenta = 0,
                            DescripcionArticulo = "PANETON BUON NATALE ",
                            Cantidad = 2,
                            EsFormula = 0,
                            EsInventariable = true,
                            EnInventarioFisico = false,
                            CodigoArticulo = "40101",
                            CodigoArticuloAlterno = ""
                }
                ,
                new VentaDetalleDTO{
                            Secuencia = 2,
                            NumeroTurno = numeroTurno,
                            NumeroCara = numeroCara,
                            PorcentajeImpuestoIgv = porcentajeImpuestoIgv,
                            PorcentajeImpuestoIsc = 0,
                            TotalNacional = 0,
                            TotalExtranjera = 0,
                            ImpuestoNacional = 0,
                            ImpuestoExtranjera = 0,
                            PorcentajeDescuentoPrimero = 0,
                            TotalDescuentoNacional = 0,
                            TotalDescuentoExtranjera = 0,
                            Precio = 12.56M,
                            PrecioVenta = 0,
                            DescripcionArticulo = "PANETON GIORGINO 900 G",
                            Cantidad = 2,
                            EsFormula = 0,
                            EsInventariable = true,
                            EnInventarioFisico = false,
                            CodigoArticulo = "40118",
                            CodigoArticuloAlterno = ""
                }
            };

            nuevoVentaDto.VentaConTarjetas = new List<VentaConTarjetaDTO>(){
                new VentaConTarjetaDTO{
                    Secuencia = 1,
                    NumeroTarjeta = "8558",
                    TotalTarjetaNacional = 0,
                    TotalTarjetaExtranjera = 0,
                    CodigoMoneda = codigoMoneda,
                    CodigoTarjeta = codigoTarjeta
                }
            };

            ResultadoServicio<ResultadoVentaGrabadaDTO> nuevaVentaCreado =
                        _IServicioAplicacionFacturacion.AgregarNuevaVenta(nuevoVentaDto);

            ResultadoServicio<VentaListadoDTO> ventaBuscada =
                        _IServicioAplicacionFacturacion.BuscarVentas(codigoAlmacen, fechaInicioVentas,
                                                fechaFinVentas, numeroDocumentoNuevo, codigoTipoNegocio);

            Assert.True(nuevaVentaCreado.Dato.NumeroDocumento.Trim() == ventaBuscada.Datos.FirstOrDefault().NumeroDocumento.Trim());
            Assert.True(nuevaVentaCreado.Dato.RucCliente.Trim() == ventaBuscada.Datos.FirstOrDefault().RucCliente.Trim());
        }

        #endregion



        [Fact]
        public void AgregarNuevaVentaDesdePedidoRetail_Test()
        {
            var correlativo = 1;
            var numeroDocumentoNuevo = "F04200040934"; //numeroDocumentoNuevo++
            var codigoAlmacen = "24";
            var codigoTipoNegocio = "2";
            var fechaInicioVentas = "20200101";
            var fechaFinVentas = "20201231";

            ResultadoServicio<ResultadoVentaGrabadaDTO> nuevaVentaCreado =
                        _IServicioAplicacionFacturacion.AgregarNuevaVentaDesdePedidoRetail(correlativo);

            ResultadoServicio<VentaListadoDTO> ventaBuscada =
                        _IServicioAplicacionFacturacion.BuscarVentas(codigoAlmacen, fechaInicioVentas,
                                                fechaFinVentas, numeroDocumentoNuevo, codigoTipoNegocio);

            Assert.True(nuevaVentaCreado.Dato.NumeroDocumento.Trim() == ventaBuscada.Datos.FirstOrDefault().NumeroDocumento.Trim());
            Assert.True(nuevaVentaCreado.Dato.RucCliente.Trim() == ventaBuscada.Datos.FirstOrDefault().RucCliente.Trim());
        }


        [Fact]
        public void AgregarNuevaVentaDesdePedidoEESS_Test()
        {
            var correlativo = 1;
            var numeroDocumentoNuevo = "F04200040936"; //numeroDocumentoNuevo++
            var codigoAlmacen = "24";
            var codigoTipoNegocio = "1";
            var fechaInicioVentas = "20200101";
            var fechaFinVentas = "20201231";

            ResultadoServicio<ResultadoVentaGrabadaDTO> nuevaVentaCreado =
                        _IServicioAplicacionFacturacion.AgregarNuevaVentaDesdePedidoEESS(correlativo);

            ResultadoServicio<VentaListadoDTO> ventaBuscada =
                        _IServicioAplicacionFacturacion.BuscarVentas(codigoAlmacen, fechaInicioVentas,
                                                fechaFinVentas, numeroDocumentoNuevo, codigoTipoNegocio);

            Assert.True(nuevaVentaCreado.Dato.NumeroDocumento.Trim() == ventaBuscada.Datos.FirstOrDefault().NumeroDocumento.Trim());
            Assert.True(nuevaVentaCreado.Dato.RucCliente.Trim() == ventaBuscada.Datos.FirstOrDefault().RucCliente.Trim());
        }


        [Fact]
        public void BuscarVentas_Todas_Test()
        {
            var numeroDocumentoNuevo = "";
            var codigoAlmacen = "24";
            var codigoTipoNegocio = "2";
            var fechaInicioVentas = "20200101";
            var fechaFinVentas = "20201231";

            ResultadoServicio<VentaListadoDTO> ventasBuscada =
                        _IServicioAplicacionFacturacion.BuscarVentas(codigoAlmacen, fechaInicioVentas,
                                                fechaFinVentas, numeroDocumentoNuevo, codigoTipoNegocio);

            Assert.True(ventasBuscada.Datos.Count() > 1);
        }


        [Fact]
        public void BuscarVentas_Una_Test()
        {
            var numeroDocumentoNuevo = "F04200040936";
            var codigoAlmacen = "24";
            var codigoTipoNegocio = "1";
            var fechaInicioVentas = "20200101";
            var fechaFinVentas = "20201231";

            ResultadoServicio<VentaListadoDTO> ventasBuscada =
                        _IServicioAplicacionFacturacion.BuscarVentas(codigoAlmacen, fechaInicioVentas,
                                                fechaFinVentas, numeroDocumentoNuevo, codigoTipoNegocio);

            Assert.True(ventasBuscada.Datos.Count() == 1);
        }

        [Fact]
        public void BuscarVentasPorCliente_Test()
        {
            var rucCliente = "20440484345";

            ResultadoServicio<VentaListadoDTO> ventaBuscadaPorCliente =
                        _IServicioAplicacionFacturacion.BuscarVentasPorCliente(rucCliente);

            Assert.True(ventaBuscadaPorCliente.Datos.Count() > 1);
        }
    }
}