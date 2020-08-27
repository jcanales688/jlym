using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Configuraciones;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.CuentasPorCobrar;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Dominio.BaseTrabajo.Funciones;
using PtoVta.Infraestructura.Transversales.Log;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.AmbientePuntoDeVenta;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.AmbienteVenta;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.GestionVentas
{
    public class ServicioAplicacionFacturacion : IServicioAplicacionFacturacion
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
        
        private IServicioDominioVentas _IServicioDominioVentas;
        private IServicioDominioMovimientosAlmacen _IServicioDominioMovimientosAlmacen;
        private IServicioDominioCuentaPorCobrar _IServicioDominioCuentaPorCobrar;
        private IServicioDominioListaPrecios _IServicioDominioListaPrecios;

        private IConfiguracionGlobalUnificado _IConfiguracionGlobalUnificado;

        public ServicioAplicacionFacturacion(IRepositorioVenta pIrepositorioVenta, IRepositorioEstadoDocumento pIrepositorioEstadoDocumento,
                                IRepositorioTipoDocumento pIrepositorioTipoDocumento, IRepositorioCliente pIrepositorioCliente,
                                IRepositorioClaseTipoCambio pIrepositorioClaseTipoCambio, IRepositorioVendedor pIrepositorioVendedor,
                                IRepositorioMoneda pIrepositorioMoneda, IRepositorioConfiguracionPuntoVenta pIrepositorioConfiguracionPtoVta,
                                IRepositorioConfiguracionGeneral pIRepositorioConfiguracionGeneral, IRepositorioTipoPago pIrepositorioTipoPago,
                                IRepositorioTipoMovimientoAlmacen pIrepositorioTipoMovimientoAlmacen, IRepositorioCondicionPago pIrepositorioCondicionPago,
                                IRepositorioArticulo pIrepositorioArticulo, IRepositorioTarjeta pIrepositorioTarjeta,
                                IRepositorioMovimientoAlmacen pIrepositorioMovimientoAlmacen, IRepositorioAlmacen pIRepositorioAlmacen,
                                IRepositorioTipoNegocio pIRepositorioTipoNegocio, IRepositorioUsuarioSistema pIRepositorioUsuarioSistema,
                                IRepositorioPedidoEESS pIRepositorioPedidoEESS, IRepositorioPedidoRetail pIRepositorioPedidoRetail,
                                IRepositorioListaPrecioCliente pIRepositorioListaPrecioCliente, IRepositorioListaPrecioInventario pIRepositorioListaPrecioInventario, 
                                IServicioDominioVentas pIServicioDominioVentas,
                                IServicioDominioMovimientosAlmacen pIServicioDominioMovimientosAlmacen,
                                IServicioDominioCuentaPorCobrar pIServicioDominioCuentaPorCobrar,
                                IServicioDominioListaPrecios pIServicioDominioListaPrecios,
                                IConfiguracionGlobalUnificado pIConfiguracionGlobalUnificado)
        {
            if (pIrepositorioVenta == null)
                throw new ArgumentNullException("pIrepositorioVenta Nulo en ServicioAplicacionFacturacion");

            if (pIrepositorioEstadoDocumento == null)
                throw new ArgumentNullException("pIrepositorioEstado Documento Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioTipoDocumento == null)
                throw new ArgumentNullException("pIrepositorioTipoDocumento Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioCliente == null)
                throw new ArgumentNullException("pIrepositorioCliente Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioClaseTipoCambio == null)
                throw new ArgumentNullException("pIrepositorioClaseTipoCambio Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioVendedor == null)
                throw new ArgumentNullException("pIrepositorioVendedor Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioMoneda == null)
                throw new ArgumentNullException("pIrepositorioMoneda Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioConfiguracionPtoVta == null)
                throw new ArgumentNullException("pIrepositorioConfiguracionPtoVta Nulo En ServicioAplicacionFacturacion");

            if (pIRepositorioConfiguracionGeneral == null)
                throw new ArgumentNullException("IrepositorioConfiguracionGeneral Nulo En ServicioAplicacionConfiguracion");

            if (pIrepositorioTipoPago == null)
                throw new ArgumentNullException("pIrepositorioTipoPago Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioTipoMovimientoAlmacen == null)
                throw new ArgumentNullException("pIrepositorioTipoMovimientoAlmacen Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioCondicionPago == null)
                throw new ArgumentNullException("pIrepositorioCondicionPago Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioArticulo == null)
                throw new ArgumentNullException("pIrepositorioArticulo Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioTarjeta == null)
                throw new ArgumentNullException("pIrepositorioTarjeta Nulo En ServicioAplicacionFacturacion");

            if (pIrepositorioMovimientoAlmacen == null)
                throw new ArgumentNullException("pIrepositorioMovimientoAlmacen Nulo En ServicioAplicacionFacturacion");

            if (pIRepositorioAlmacen == null)
                throw new ArgumentNullException("pIRepositorioAlmacen Nulo En ServicioAplicacionFacturacion");

            if (pIRepositorioTipoNegocio == null)
                throw new ArgumentNullException("pIRepositorioTipoNegocio Nulo En ServicioAplicacionFacturacion");

            if (pIRepositorioUsuarioSistema == null)
                throw new ArgumentNullException("pIRepositorioUsuarioSistema Nulo En ServicioAplicacionFacturacion");

            if (pIRepositorioPedidoEESS == null)
                throw new ArgumentNullException("pIRepositorioPedidoEESS Nulo En ServicioAplicacionFacturacion");

            if (pIRepositorioPedidoRetail == null)
                throw new ArgumentNullException("pIRepositorioPedidoRetail Nulo En ServicioAplicacionFacturacion");

            if (pIRepositorioListaPrecioCliente == null)
                throw new ArgumentNullException("pIRepositorioListaPrecioCliente Nulo En ServicioAplicacionFacturacion");

            if (pIRepositorioListaPrecioInventario == null)
                throw new ArgumentNullException("pIRepositorioListaPrecioInventario Nulo En ServicioAplicacionFacturacion");                

            if (pIServicioDominioVentas == null)
                throw new ArgumentNullException("pIServicioDominioVentas Nulo En ServicioAplicacionFacturacion");

            if (pIServicioDominioMovimientosAlmacen == null)
                throw new ArgumentNullException("pIServicioDominioMovimientosAlmacen Nulo En ServicioAplicacionFacturacion");

            if (pIServicioDominioCuentaPorCobrar == null)
                throw new ArgumentNullException("pIServicioDominioCuentaPorCobrar Nulo En ServicioAplicacionFacturacion");

            if (pIServicioDominioListaPrecios == null)
                throw new ArgumentNullException("pIServicioDominioListaPrecios Nulo En ServicioAplicacionFacturacion");                

            if (pIConfiguracionGlobalUnificado == null)
                throw new ArgumentNullException("pIConfiguracionGlobalUnificado Nulo En ServicioAplicacionFacturacion");

            _IRepositorioVenta = pIrepositorioVenta;
            _IRepositorioEstadoDocumento = pIrepositorioEstadoDocumento;
            _IRepositorioTipoDocumento = pIrepositorioTipoDocumento;
            _IRepositorioCliente = pIrepositorioCliente;
            _IRepositorioClaseTipoCambio = pIrepositorioClaseTipoCambio;
            _IRepositorioVendedor = pIrepositorioVendedor;
            _IRepositorioMoneda = pIrepositorioMoneda;
            _IRepositorioConfiguracionPuntoVenta = pIrepositorioConfiguracionPtoVta;
            _IRepositorioConfiguracionGeneral = pIRepositorioConfiguracionGeneral;
            _IRepositorioTipoPago = pIrepositorioTipoPago;
            _IRepositorioTipoMovimientoAlmacen = pIrepositorioTipoMovimientoAlmacen;
            _IRepositorioCondicionPago = pIrepositorioCondicionPago;
            _IRepositorioArticulo = pIrepositorioArticulo;
            _IRepositorioTarjeta = pIrepositorioTarjeta;
            _IRepositorioMovimientoAlmacen = pIrepositorioMovimientoAlmacen;
            _IRepositorioAlmacen = pIRepositorioAlmacen;
            _IRepositorioTipoNegocio = pIRepositorioTipoNegocio;
            _IRepositorioUsuarioSistema = pIRepositorioUsuarioSistema;
            _IRepositorioPedidoEESS = pIRepositorioPedidoEESS;
            _IRepositorioPedidoRetail = pIRepositorioPedidoRetail;
            _IRepositorioListaPrecioCliente = pIRepositorioListaPrecioCliente;
            _IRepositorioListaPrecioInventario = pIRepositorioListaPrecioInventario;            

            _IServicioDominioVentas = pIServicioDominioVentas;
            _IServicioDominioMovimientosAlmacen = pIServicioDominioMovimientosAlmacen;
            _IServicioDominioCuentaPorCobrar = pIServicioDominioCuentaPorCobrar;
            _IServicioDominioListaPrecios = pIServicioDominioListaPrecios;

            _IConfiguracionGlobalUnificado = pIConfiguracionGlobalUnificado;
        }



        public ResultadoServicio<ResultadoVentaGrabadaDTO> AgregarNuevaVenta(VentaDTO pVentaDTO)
        {
            string flagLocalizacionCalculoTotalVenta = EnumLocalizaCalculoTotalVenta.CalculoTotalVentaEnBackEnd;
            string flagCrudCliente = string.Empty;
            string nuevoCorrelativoDocumento = string.Empty;
            TipoDocumento tipoDocumentoYCorrelativo = null;
            TipoDeCambio ultimoTipoDeCambioAExtranjera = null;

            string configCodigoTipoDocumentoNotaCredito;
            string configCodigoTMAVentas;
            int configPermitirStockNegativo;
            DateTime configFechaTipoDeCambio;
            string configCodigoCondicionPagoDefault;
            string configCodigoEstadoDocumentoDefault;
            string configCodigoMonedaBase;
            string configCodigoMonedaExtranjera;

            bool esVentaACuentaPorCobrar = false;
            decimal saldoDisponibleAdelanto = 0;

            // Validacion Inicial
            if (pVentaDTO == null || string.IsNullOrEmpty(pVentaDTO.CodigoTipoDocumento.Trim()))
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_AdvertenciaVentaOTipoDocumentoInvalido);
                throw new ArgumentException(Mensajes.advertencia_AdvertenciaVentaOTipoDocumentoInvalido);
            }

            //Identificar Tipo de Pago
            IdentificacionInicialDeTipoPagoDeVenta(pVentaDTO);

            //Configuracion Global
            var configuracionGlobal = _IConfiguracionGlobalUnificado.UnificarConfiguracionGlobal();
            if (configuracionGlobal == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConfiguracionGlobalInvalido);
                throw new ArgumentException(Mensajes.advertencia_ConfiguracionGlobalInvalido);
            }
            else
            {
                configCodigoTipoDocumentoNotaCredito = configuracionGlobal.CodigoTipoDocumentoNotaCredito;
                configCodigoTMAVentas = configuracionGlobal.CodigoTMAVentas;
                configPermitirStockNegativo = configuracionGlobal.PermitirStockNegativo;
                configFechaTipoDeCambio = DateTime.Now;
                configCodigoCondicionPagoDefault = configuracionGlobal.CodigoCondicionPagoDefault;
                configCodigoEstadoDocumentoDefault = configuracionGlobal.CodigoEstadoDocumentoDefault;
                configCodigoMonedaBase = configuracionGlobal.CodigoMonedaBase;
                configCodigoMonedaExtranjera = configuracionGlobal.CodigoMonedaExtranjera;
            }


            // //Configuracion Generales
            // var configuracionGeneral = _IRepositorioConfiguracionGeneral.Obtener();
            // if (configuracionGeneral == null)
            // {
            //     LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConfiguracionDeAplicacionIncompleta);
            //     throw new ArgumentException(Mensajes.advertencia_ConfiguracionDeAplicacionIncompleta);
            // }

            //Configuracion Punto de Venta
            var configPuntoDeVenta = _IRepositorioConfiguracionPuntoVenta.ObtenerPorPuntoDeVenta(pVentaDTO.CodigoPuntoDeVenta);
            if (configPuntoDeVenta == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConfiguracionPuntoVentaAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_ConfiguracionPuntoVentaAsociadoAVentaNoExiste);
            }

            //Almacen
            var almacen = _IRepositorioAlmacen.ObtenerPorCodigo(pVentaDTO.CodigoAlmacen);
            if (almacen == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_AlmacenAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_AlmacenAsociadoAVentaNoExiste);
            }

            //Tipo Negocio
            var tipoNegocio = _IRepositorioTipoNegocio.ObtenerPorCodigo(pVentaDTO.CodigoTipoNegocio);
            if (tipoNegocio == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoNegocioAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_TipoNegocioAsociadoAVentaNoExiste);
            }

            //Usuario Sistema de Venta
            var usuarioSistema = _IRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario(pVentaDTO.CodigoUsuarioDeSistema);
            if (usuarioSistema == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_UsuarioSistemaAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_UsuarioSistemaAsociadoAVentaNoExiste);
            }

            //Tipo de Documento y Correlativo
            tipoDocumentoYCorrelativo = _IRepositorioTipoDocumento.ObtenerCorrelativoDocumento(pVentaDTO.CodigoAlmacen, pVentaDTO.CodigoPuntoDeVenta,
                                                                                        pVentaDTO.CodigoTipoDocumento, pVentaDTO.TipoDeVenta, 0);
            if (tipoDocumentoYCorrelativo == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoDocumentoAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_TipoDocumentoAsociadoAVentaNoExiste);
            }

            if (pVentaDTO.CodigoTipoDocumento.Trim() != EnumTipoDocumento.CodigoTipoDocumentoTicket)
            {
                //Correlativo desde Tipo Documento
                nuevoCorrelativoDocumento = FuncionesNegocio.FormatearCorrelativoDocumento(tipoDocumentoYCorrelativo.CorrelativosDocumento.FirstOrDefault().Serie,
                                                                    (long)tipoDocumentoYCorrelativo.CorrelativosDocumento.FirstOrDefault().Correlativo);
            }
            else
            {
                //Correlativo desde Configuracion Punto de Venta
                if (pVentaDTO.RucCliente.Trim().Length == EnumGenerales.AnchoDocumentoIdentidadRuc)
                {
                    nuevoCorrelativoDocumento = configPuntoDeVenta.SerieCorrelativoTickFactura;
                }
                else if (pVentaDTO.RucCliente.Trim().Length == EnumGenerales.AnchoDocumentoIdentidadDni ||
                                        pVentaDTO.RucCliente.Trim().Length == EnumGenerales.AnchoSinDocumentoIdentidad)
                {
                    nuevoCorrelativoDocumento = configPuntoDeVenta.SerieCorrelativoTickBoleta;
                }
            }

            //Validacion Correlativo Documento
            if (ExisteDocumentoDeVenta(pVentaDTO.CodigoTipoDocumento, nuevoCorrelativoDocumento, pVentaDTO.CodigoAlmacen))
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_CorrelativoDocumentoYaFueGenerado);
                throw new ArgumentException(Mensajes.advertencia_CorrelativoDocumentoYaFueGenerado);
            }
            else
            {
                pVentaDTO.NumeroDocumento = nuevoCorrelativoDocumento;
            }

            //Estado de Documento
            var estadoDocumento = _IRepositorioEstadoDocumento.ObtenerPorCodigo(pVentaDTO.CodigoEstadoDocumento);
            if (estadoDocumento == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_EstadoDocumentoAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_EstadoDocumentoAsociadoAVentaNoExiste);
            }


            //Clase de Tipo de Cambio y el Monto tipo de cambio del dia
            var claseTipoCambioYMontoTipoCambio = _IRepositorioClaseTipoCambio.ObtenerPorCodigo(pVentaDTO.CodigoClaseTipoCambio);
            if (claseTipoCambioYMontoTipoCambio == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ClaseTipoDeCambioAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_ClaseTipoDeCambioAsociadoAVentaNoExiste);
            }
            else
                ultimoTipoDeCambioAExtranjera =  (from tipoCambioExtranjera in claseTipoCambioYMontoTipoCambio.TiposDeCambio
                                                    where tipoCambioExtranjera.CodigoMonedaDestino == EnumMoneda.CodigoMonedaExtranjera
                                                    select tipoCambioExtranjera).FirstOrDefault();

            //Vendedor
            var vendedor = _IRepositorioVendedor.ObtenerPorCodigo(pVentaDTO.CodigoVendedor);
            if (vendedor == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_VendedorAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_VendedorAsociadoAVentaNoExiste);
            }

            //Moneda
            var moneda = _IRepositorioMoneda.ObtenerPorCodigo(pVentaDTO.CodigoMoneda);
            if (moneda == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_MonedaAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_MonedaAsociadoAVentaNoExiste);
            }

            //Tipo Movimiento Almacen de Ventas
            var tipoMovAlmacenVentas = _IRepositorioTipoMovimientoAlmacen.ObtenerPorCodigo(configCodigoTMAVentas);
            if (tipoMovAlmacenVentas == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoMovAlmacenVentasAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_TipoMovAlmacenVentasAsociadoAVentaNoExiste);
            }

            //Cliente
            var cliente = _IRepositorioCliente.ObtenerPorCodigo(pVentaDTO.CodigoCliente);
            cliente = MaterializarCliente(cliente, pVentaDTO.Cliente, out flagCrudCliente);


            //Condicion de Pago
            CondicionPago condicionPagoDeVentaActual = pVentaDTO.CodigoTipoDocumento == EnumTipoDocumento.CodigoTipoDocumentoTicket ?
                                                                cliente.CondicionPagoTicket : cliente.CondicionPagoDocumentoGenerado;

            //Tipo de Pago
            TipoPago tipoPagoDeVentaActual = _IRepositorioTipoPago.ObtenerPorCodigo(pVentaDTO.CodigoTipoPago);
            if (tipoPagoDeVentaActual == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);
                throw new ArgumentException(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);
            }

            //Defaults
            CondicionPago condicionPagoDefault = _IRepositorioCondicionPago.ObtenerPorCodigo(configCodigoCondicionPagoDefault);
            TipoPago tipoPagoDefault = _IRepositorioTipoPago.ObtenerPorCodigo(EnumTipoPago.CodigoTipoPagoPorDefecto);

            //Saldo Disponible
            if (cliente.ControlarSaldoDisponible == EnumCliente.ClienteConControlDeSaldoDisponible)
                saldoDisponibleAdelanto = ObtenerSaldoDisponibleDeVentaAdelantada(pVentaDTO.CodigoTipoPago, pVentaDTO.CodigoCliente,
                                                                        pVentaDTO.CodigoAlmacen, configCodigoTipoDocumentoNotaCredito,
                                                                        pVentaDTO.FechaProceso);


            _IServicioDominioVentas.ObtenerCondicionYTipoPagoDeVenta(pVentaDTO.CodigoTipoDocumento, condicionPagoDeVentaActual, condicionPagoDefault,
                                                                        tipoPagoDeVentaActual, tipoPagoDefault, cliente,
                                                                        configPuntoDeVenta, configCodigoTipoDocumentoNotaCredito, pVentaDTO.TotalNacional,
                                                                        esVentaACuentaPorCobrar, saldoDisponibleAdelanto);

            if (esVentaACuentaPorCobrar)
            {
                //Validar este comprobacion: debe obtener el estado del documento cuenta por cobrar (configCodigoEstadoDocumentoDefault), no es 'PE' ?
                var estadoDocumentoEnCuentaPorCobrar = _IRepositorioEstadoDocumento.ObtenerPorCodigo(configCodigoEstadoDocumentoDefault);
                if (estadoDocumentoEnCuentaPorCobrar == null)
                {
                    LogFactory.CrearLog().LogWarning(Mensajes.advertencia_EstadoDeDocumentoCuentasXCobrarAsociadoAVentaNoExiste);
                    throw new ArgumentException(Mensajes.advertencia_EstadoDeDocumentoCuentasXCobrarAsociadoAVentaNoExiste);
                }
            }
            else
            {
                pVentaDTO.CodigoTipoPago = tipoPagoDeVentaActual.CodigoTipoPago;
            }

            //Establecer Tipo de Cambio
            if(flagLocalizacionCalculoTotalVenta == EnumLocalizaCalculoTotalVenta.CalculoTotalVentaEnBackEnd)
                pVentaDTO.TipoCambio = ultimoTipoDeCambioAExtranjera.MontoTipoDeCambio;                    

            //Movimiento Ingreso o Salida
            var movAlmacenIngresoOSalida = _IServicioDominioMovimientosAlmacen.MovimientoAlmacenIngresoOSalida(pVentaDTO.CodigoTipoDocumento,
                                                                                configCodigoTipoDocumentoNotaCredito, tipoMovAlmacenVentas);

            //Crear Venta
            var nuevaVenta = CrearNuevaVenta(pVentaDTO, moneda, claseTipoCambioYMontoTipoCambio,
                                                cliente, tipoDocumentoYCorrelativo, estadoDocumento,
                                                vendedor, condicionPagoDeVentaActual, tipoPagoDeVentaActual,
                                                configPuntoDeVenta, almacen, tipoNegocio,
                                                usuarioSistema, pVentaDTO.EsVentaPagoAdelantado, esVentaACuentaPorCobrar,
                                                tipoMovAlmacenVentas, movAlmacenIngresoOSalida, configPermitirStockNegativo,
                                                configFechaTipoDeCambio, configuracionGlobal.CodigoClienteInterno,
                                                configuracionGlobal.CantidadDecimalPrecio, 
                                                configuracionGlobal, flagLocalizacionCalculoTotalVenta);

            //Calcular el Vuelto

            if(flagLocalizacionCalculoTotalVenta == EnumLocalizaCalculoTotalVenta.CalculoTotalVentaEnBackEnd)
            {
                if(nuevaVenta.CodigoTipoPago != EnumTipoPago.CodigoTipoPagoTarjeta)
                    _IServicioDominioVentas.CalcularVueltoVentaSegunMoneda(nuevaVenta, claseTipoCambioYMontoTipoCambio, pVentaDTO.FlagCambioDeMonedaEnVuelto,
                                                                            configuracionGlobal.CantidadDecimalPrecio, pVentaDTO.TotalVueltoExtranjera,
                                                                            pVentaDTO.TotalVueltoNacional, pVentaDTO.TotalFaltanteExtranjera,
                                                                            pVentaDTO.TotalFaltanteNacional, pVentaDTO.CodigoMonedaVuelto,
                                                                            configCodigoMonedaBase, configCodigoMonedaExtranjera);                
            }


            //Actualizar Correlativos
            configPuntoDeVenta.AumentarCorrelativoMovimientoAlmacenPorVenta();

            if (pVentaDTO.CodigoTipoDocumento.Trim() != EnumTipoDocumento.CodigoTipoDocumentoTicket)
            {
                tipoDocumentoYCorrelativo.AumentarCorrelativoDocumento();
            }
            else
            {
                if (pVentaDTO.RucCliente.Trim().Length == EnumGenerales.AnchoDocumentoIdentidadRuc)
                {
                    configPuntoDeVenta.AumentarSerieCorrelativoTickFactura();
                }
                else if (pVentaDTO.RucCliente.Trim().Length == EnumGenerales.AnchoDocumentoIdentidadDni ||
                                        pVentaDTO.RucCliente.Trim().Length == EnumGenerales.AnchoSinDocumentoIdentidad)
                {
                    configPuntoDeVenta.AumentarSerieCorrelativoTickBoleta();
                }
            }

            //Persistencia de Venta
            GrabarTransaccionDeVenta(nuevaVenta, tipoDocumentoYCorrelativo, configPuntoDeVenta, flagCrudCliente);

            if (nuevaVenta != null)
            {
                return new ResultadoServicio<ResultadoVentaGrabadaDTO>(7, Mensajes.advertencia_ExitosaCreacionNuevaVentaEnVenta,
                        string.Empty, nuevaVenta.ProyectadoComo<ResultadoVentaGrabadaDTO>(), null);
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_FalloCreacionNuevaVentaEnVenta);
                return new ResultadoServicio<ResultadoVentaGrabadaDTO>(6, Mensajes.advertencia_FalloCreacionNuevaVentaEnVenta,
                        string.Empty, nuevaVenta.ProyectadoComo<ResultadoVentaGrabadaDTO>(), null);
            }
        }


        public ResultadoServicio<ResultadoVentaGrabadaDTO> AgregarNuevaVentaDesdePedidoRetail(int pCorrelativoPedido)
        {
            var pedidoRetail = _IRepositorioPedidoRetail.ObtenerPorNumeroPedido(pCorrelativoPedido);
            if (pedidoRetail != null)
            {
                var ventaDto = MaterializarPedidoRetailAVentaDTO(pedidoRetail);
                var resultadoAgregaNuevaVentaDesdePedidoRetail = AgregarNuevaVenta(ventaDto);

                return resultadoAgregaNuevaVentaDesdePedidoRetail;
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_FalloCreacionNuevaVentaAPartirDePedidoRetail);
                return new ResultadoServicio<ResultadoVentaGrabadaDTO>(6, Mensajes.advertencia_FalloCreacionNuevaVentaAPartirDePedidoRetail,
                                                                        string.Empty,null, null);
            }
        }

        public ResultadoServicio<ResultadoVentaGrabadaDTO> AgregarNuevaVentaDesdePedidoEESS(int pCorrelativoPedido)
        {
            var pedidoEESS = _IRepositorioPedidoEESS.ObtenerPorNumeroPedido(pCorrelativoPedido);
            if (pedidoEESS != null)
            {
                var ventaDto = MaterializarPedidoEESSAVentaDTO(pedidoEESS);
                var resultadoAgregaNuevaVentaDesdePedidoEESS = AgregarNuevaVenta(ventaDto);

                return resultadoAgregaNuevaVentaDesdePedidoEESS;
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_FalloCreacionNuevaVentaAPartirDePedidoEESS);
                return new ResultadoServicio<ResultadoVentaGrabadaDTO>(6, Mensajes.advertencia_FalloCreacionNuevaVentaAPartirDePedidoEESS,
                                                                        string.Empty,null, null);
            }
        }


        public ResultadoServicio<VentaListadoDTO> BuscarVentasPorCliente(string pCodigoCliente)
        {
            var ventas = _IRepositorioVenta.ObtenerVentasPorCodigoCliente(pCodigoCliente);
            if (ventas != null && ventas.Any())
            {
                return new ResultadoServicio<VentaListadoDTO>(7, Mensajes.advertencia_ConsultaVentasPorClienteExitosa,
                                    string.Empty, null, ventas.ProyectadoComoColeccion<VentaListadoDTO>());
            }
            else
                return null;
        }

        public ResultadoServicio<VentaListadoDTO> BuscarVentas(string pCodigoAlmacen, string pFechaProcesoInicio,
                                        string pFechaProcesoFin, string pNumeroDocumento, string pCodigoTipoNegocio)
        {
            var ventas = _IRepositorioVenta.ObtenerTodos(pCodigoAlmacen, pFechaProcesoInicio,
                                                    pFechaProcesoFin, pNumeroDocumento, pCodigoTipoNegocio);
            if (ventas != null && ventas.Any())
            {
                return new ResultadoServicio<VentaListadoDTO>(7, Mensajes.advertencia_ConsultaVentasPorAlmacenExitosa,
                                    string.Empty, null, ventas.ProyectadoComoColeccion<VentaListadoDTO>());
            }
            else
                return null;
        }


        Venta CrearNuevaVenta(VentaDTO pVentaDTO, Moneda pMoneda, ClaseTipoCambio pClaseTipoCambio,
                                Cliente pCliente, TipoDocumento pTipoDocumento, EstadoDocumento pEstadoDocumento,
                                Vendedor pVendedor, CondicionPago pCondicionPago, TipoPago pTipoPago,
                                ConfiguracionPuntoVenta pConfiguracionPuntoVenta, Almacen pAlmacen, TipoNegocio pTipoNegocio,
                                UsuarioSistema pUsuarioSistema, bool pEsVentaPagoAdelantado, bool pEsVentaACuentaPorCobrar,
                                TipoMovimientoAlmacen pTipoMovimientoAlmacen, int pMovAlmacenVentaIngresoOSalida, int pPermitirStockNegativo,
                                DateTime pFechaTipoDeCambio, string pCodigoClienteInterno, int pCantidadDecimalPrecio,
                                ConfiguracionGlobalDTO pConfiguracionGlobalDTO, string pFlagLocalizacionCalculoTotalVenta)
        {
            try
            {
                decimal precioVentaDelArticulo;

                Venta nuevaVenta = VentaFactory.CrearVenta(pVentaDTO.NumeroDocumento, pVentaDTO.FechaDocumento, pVentaDTO.FechaProceso,
                                                pVentaDTO.Periodo, pVentaDTO.TotalNacional, pVentaDTO.TotalExtranjera,
                                                pVentaDTO.SubTotalNacional, pVentaDTO.SubTotalExtranjera, pVentaDTO.ImpuestoIgvNacional,
                                                pVentaDTO.ImpuestoIgvExtranjera, pVentaDTO.ImpuestoIscNacional, pVentaDTO.ImpuestoIscExtranjera,
                                                pVentaDTO.TotalNoAfectoNacional, pVentaDTO.TotalNoAfectoExtranjera, (decimal)pVentaDTO.TotalAfectoNacional,
                                                (decimal)pVentaDTO.ValorVenta, pVentaDTO.PorcentajeDescuentoPrimero, pVentaDTO.PorcentajeDescuentoSegundo,
                                                pVentaDTO.TotalDescuentoNacional, pVentaDTO.TotalDescuentoExtranjera, pVentaDTO.TotalVueltoNacional,
                                                pVentaDTO.TotalVueltoExtranjera, pVentaDTO.TotalEfectivoNacional, pVentaDTO.TotalEfectivoExtranjera,
                                                pVentaDTO.Placa, (decimal)pVentaDTO.NumeroVale, pVentaDTO.TipoCambio,
                                                pVentaDTO.ProcesadoCierreZ, pVentaDTO.ProcesadoCierreX, (int)pVentaDTO.Kilometraje, pVentaDTO.AfectaInventario,
                                                pMoneda, pClaseTipoCambio, pCliente,
                                                pTipoDocumento, pEstadoDocumento, pVendedor,
                                                pCondicionPago, pTipoPago, pConfiguracionPuntoVenta,
                                                pAlmacen, pTipoNegocio, pUsuarioSistema);

                //Agrega Detalle Venta
                if (pVentaDTO.VentaDetalles != null)
                {
                    foreach (var linea in pVentaDTO.VentaDetalles)
                    {
                        //Articulo(Precio e Inventario Fisico)
                        var articulo = _IRepositorioArticulo.ObtenerPorCodigo(linea.CodigoArticulo, pAlmacen.CodigoAlmacen);
                        if (articulo == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ArticuloAsociadoAVentaDetalleNoExiste);
                            throw new ArgumentException(Mensajes.advertencia_ArticuloAsociadoAVentaDetalleNoExiste);
                        }

                        bool enInventarioFisico = articulo.InventariosFisicos.Any() ? true : false;
                        
                        if(pFlagLocalizacionCalculoTotalVenta == EnumLocalizaCalculoTotalVenta.CalculoTotalVentaEnBackEnd)
                        {
                            precioVentaDelArticulo = ObtenerPrecioVentaDeArticulo(pConfiguracionGlobalDTO, pVentaDTO.CodigoCliente, 
                                                                                    linea.CodigoArticulo, pVentaDTO.CodigoAlmacen);
                        }
                        else          
                            precioVentaDelArticulo = linea.PrecioVenta;
                            
                        //Actualiza Stock Articulo
                        articulo.RecalcularStock(pPermitirStockNegativo, pMovAlmacenVentaIngresoOSalida, linea.Cantidad);                        

                        var detVenta = nuevaVenta.AgregarNuevaVentaDetalle(linea.Secuencia, linea.NumeroTurno, linea.NumeroCara,
                                                linea.PorcentajeImpuestoIgv, linea.PorcentajeImpuestoIsc, linea.TotalNacional,
                                                linea.TotalExtranjera, linea.ImpuestoNacional, linea.ImpuestoExtranjera,
                                                (int)linea.PorcentajeDescuentoPrimero, (int)linea.TotalDescuentoNacional, (int)linea.TotalDescuentoExtranjera,
                                                linea.Precio, precioVentaDelArticulo, articulo.DescripcionArticulo,
                                                linea.Cantidad, linea.EsFormula, linea.CodigoArticulo,
                                                linea.CodigoArticuloAlterno, articulo.EsInventariable, enInventarioFisico);
                                                
                        detVenta.EstablecerArticuloDeVentaDetalle(articulo);

                        //Agrega Movimiento Almacen
                        if(articulo.EsInventariable)
                            nuevaVenta.AgregarNuevoMovimientoAlmacen(pConfiguracionPuntoVenta.CorrelativoMovimientoAlmacenPorVenta.ToString(),
                                                pFechaTipoDeCambio, pMovAlmacenVentaIngresoOSalida, linea.Cantidad,
                                                articulo.ArticuloDetalle.CostoReposicionExtranjera,
                                                articulo.ArticuloDetalle.CostoReposicionNacional, articulo.EsFormula,
                                                precioVentaDelArticulo, articulo.InventariosFisicos != null ? articulo.InventariosFisicos.Count : 0,
                                                articulo.CodigoArticulo,pTipoMovimientoAlmacen.CodigoTipoMovimientoAlmacen);       

                    }
                }

                //Calcular Total Venta
                if(pFlagLocalizacionCalculoTotalVenta == EnumLocalizaCalculoTotalVenta.CalculoTotalVentaEnBackEnd)
                    nuevaVenta.CalcularTotalVenta();



                //Pago con Tarjeta
                if (pVentaDTO.VentaConTarjetas != null)
                {
                    decimal totalTarjetaNacional = nuevaVenta.TotalNacional;
                    decimal totalTarjetaExtranjera = nuevaVenta.TotalExtranjera;
                    string codigoMoneda = nuevaVenta.CodigoMoneda;

                    foreach (var vtaConTarjeta in pVentaDTO.VentaConTarjetas)
                    {
                        var moneda = _IRepositorioMoneda.ObtenerPorCodigo(vtaConTarjeta.CodigoMoneda);
                        if (moneda == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_MonedaAsociadoAPagoVentaConTarjetaNoExiste);
                            throw new ArgumentException(Mensajes.advertencia_MonedaAsociadoAPagoVentaConTarjetaNoExiste);
                        }

                        var tarjeta = _IRepositorioTarjeta.ObtenerPorCodigo(vtaConTarjeta.CodigoTarjeta);
                        if (tarjeta == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TarjetaAsociadoAPagoVentaConTarjetaNoExiste);
                            throw new ArgumentException(Mensajes.advertencia_TarjetaAsociadoAPagoVentaConTarjetaNoExiste);
                        }

                        if(nuevaVenta.CodigoTipoPago != EnumTipoPago.CodigoTipoPagoTarjeta)
                        {
                            totalTarjetaNacional = vtaConTarjeta.TotalTarjetaNacional;
                            totalTarjetaExtranjera = vtaConTarjeta.TotalTarjetaExtranjera;
                            codigoMoneda = vtaConTarjeta.CodigoMoneda;                            
                        }
                     
                        var ventaConTarjeta = nuevaVenta.AgregarNuevaVentaConTarjeta(vtaConTarjeta.Secuencia, vtaConTarjeta.NumeroTarjeta,
                                                                                    totalTarjetaNacional, totalTarjetaExtranjera,
                                                                                    codigoMoneda, vtaConTarjeta.CodigoTarjeta);
                    }
                }

                //Pago Con Vale
                if (pVentaDTO.VentaConVales != null)
                {
                    foreach (var vtaConVale in pVentaDTO.VentaConVales)
                    {
                        var ventaConVale = nuevaVenta.AgregarNuevaVentaConVale(vtaConVale.NumeroVale, vtaConVale.MontoVale);
                    }
                }

                //Pago Adelanto
                if (pEsVentaPagoAdelantado)
                    nuevaVenta.AgregarNuevoDocumentoAnticipado();

                //Pago al Credito
                if (pEsVentaACuentaPorCobrar)
                {
                    var fechaVencimiento = _IServicioDominioCuentaPorCobrar.ObtenerFechaVenceDocumentoCuentaPorCobrar(pVentaDTO.FechaDocumento,
                                                                                                                pCliente, pCondicionPago);

                    nuevaVenta.AgregarNuevaCuentaPorCobrar(0, fechaVencimiento, 0,
                                                            0, 0, 0,
                                                            pCliente.DiasDeGracia, 0, 
                                                            EnumEstadoDocumento.CodigoEstadoDocumentoPendiente,
                                                            pCliente.CodigoDiaDePago, string.Empty);
                }


                return nuevaVenta;
            }
            catch (Exception ex)
            {
                string detallesAsicionales = string.Empty;
                string cadenaExcepcion = ex.Message;

                if (ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);
                throw;
            }
        }


        decimal ObtenerPrecioVentaDeArticulo(ConfiguracionGlobalDTO pConfiguracionGlobalDTO, string pCodigoCliente, 
                                            string pCodigoArticulo, string pCodigoAlmacen)
        {
            DateTime fechaProcesoVenta = pConfiguracionGlobalDTO.FechaProcesoVenta; 
            string codigoClienteInterno = pConfiguracionGlobalDTO.CodigoClienteInterno;
            int cantidadDecimalPrecio = pConfiguracionGlobalDTO.CantidadDecimalPrecio;

            Articulo articulo = _IRepositorioArticulo.ObtenerPorCodigo(pCodigoArticulo, pCodigoAlmacen);
            if (articulo != null)
            {
                //Obtener Lista Precio Clientes
                ListaPrecioCliente listaPrecioCliente =
                            _IRepositorioListaPrecioCliente.ObtenerListaPrecioCliente(pCodigoCliente, pCodigoArticulo, 
                                                                        pCodigoAlmacen, fechaProcesoVenta.ToString("yyyyMMdd"));

                //Obtener Lista Precio Inventarios
                ListaPrecioInventario listaPrecioInventario =
                            _IRepositorioListaPrecioInventario.ObtenerListaPrecioInventario(pCodigoArticulo, pCodigoAlmacen);

                return _IServicioDominioListaPrecios.ObtenerPrecioVentaArticulo(articulo, listaPrecioCliente, listaPrecioInventario, 
                                                                        pCodigoCliente, codigoClienteInterno, cantidadDecimalPrecio);

            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ArticuloNoExiste, pCodigoArticulo);
                return 0;
            }
        }


        decimal ObtenerSaldoDisponibleDeVentaAdelantada(string pCodigoTipoPago, string pCodigoCliente, string pCodigoAlmacen,
                                                        string pCodigoTipoDocumento, DateTime pFechaProcesoVentas)
        {
            decimal saldoIniPagoAdelantado = 0;
            decimal saldoFinPagoAdelantado = 0;

            //Pagos Anticipados (AR_ANTICPAYMENT)
            var pagoInicial = _IRepositorioVenta.ObtenerPagoVentaAdelantada(pCodigoCliente, pCodigoAlmacen,
                                                                    pCodigoTipoDocumento, pFechaProcesoVentas);
            if (pagoInicial == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_NoSeObtuvoPagoDeVentaAdelantada);
                return 0;
            }

            //Consumos
            var consumos = _IRepositorioVenta.ObtenerConsumoVentaAdelantada(pCodigoTipoPago, pCodigoCliente,
                                                                    pCodigoAlmacen, pCodigoTipoDocumento, pFechaProcesoVentas);

            //Saldo Final
            _IServicioDominioVentas.CalcularSaldoVentaAdelantada(saldoIniPagoAdelantado, saldoFinPagoAdelantado, pagoInicial, consumos);

            return saldoFinPagoAdelantado;
        }


  
        bool ExisteDocumentoDeVenta(string pCodigoTipoDocumento, string pNuevoCorrelativoDocumento,
                                    string pCodigoAlmacen)
        {
            try
            {
                string correlativoDocumentoEncontrado = _IRepositorioVenta.ObtenerNumeroDocumentoVenta(pCodigoTipoDocumento,
                                                                        pNuevoCorrelativoDocumento, pCodigoAlmacen);

                return _IServicioDominioVentas.ExisteComprobanteDePagoDeVenta(pNuevoCorrelativoDocumento, correlativoDocumentoEncontrado);
            }
            catch (Exception ex)
            {
                string detallesAsicionales = string.Empty;
                string cadenaExcepcion = ex.Message;

                if (ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);
                throw;
            }
        }



        void GrabarTransaccionDeVenta(Venta pVenta, TipoDocumento pTipoDocumento, 
                    ConfiguracionPuntoVenta pConfiguracionPuntoVenta, string pFlagCrudCliente) 
        {
            try
            {
                //Uso de transaccion
                using (TransactionScope ambito = new TransactionScope(TransactionScopeOption.Suppress,
                                                                        new TransactionOptions
                                                                        {
                                                                            IsolationLevel = IsolationLevel.ReadCommitted,
                                                                            Timeout = TransactionManager.MaximumTimeout,
                                                                        },
                                                                        TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Persistir Venta
                    GrabarVenta(pVenta);

                    //Persistir Articulo - Detalles de Articulo (actualizacion Stock)
                    ActualizarArticulo(pVenta);

                    //Persistir Cliente
                    AgregarOActualizarCliente(pVenta.Cliente, pFlagCrudCliente);

                    //Persistir Correlativo de documento
                    ActualizarCorrelativoEnTipoDocumento(pTipoDocumento, pVenta.CodigoAlmacen, pVenta.NumeroDocumento.ToString().Substring(0, 3));
                    ActualizarCorrelativoEnConfiguracionPuntoDeVenta(pConfiguracionPuntoVenta);

                    //Completar transaccion
                    ambito.Complete();
                }
            }
            catch (Exception ex)
            {
                string detallesAsicionales = string.Empty;
                string cadenaExcepcion = ex.Message;

                if (ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);
                throw;

            }

        }


      void GrabarVenta(Venta pVenta)
        {
            if(pVenta != null)
            {
                _IRepositorioVenta.Agregar(pVenta);                                    
            }
            else
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_NoSeObtuvoResultadoDeVentaAPersistir);

            //Persistir Venta
            // var validarEntidad = ValidadorEntidadFactory.CrearValidador();
            // if (validarEntidad.EsValido(pVenta))
            // {
                // _IRepositorioVenta.Agregar(pVenta);
            // _IRepositorioVenta.UnidadTrabajo.Commit();
            // }
            // else
            //     throw new AplicacionExcepcionErrorValidacion(validarEntidad.RecibeMensajesInvalidos(pVenta));
        }

        void ActualizarCorrelativoEnTipoDocumento(TipoDocumento pTipoDocumentoActual, string pCodigoAlmacen, string pNumeroSerie)
        {
            if (pTipoDocumentoActual.CorrelativosDocumento.Any())
                _IRepositorioTipoDocumento.ActualizarCorrelativoDocumento(pTipoDocumentoActual, pCodigoAlmacen, pNumeroSerie); 
        }


        void ActualizarCorrelativoEnConfiguracionPuntoDeVenta(ConfiguracionPuntoVenta pConfiguracionPuntoVenta)
        {
            if (pConfiguracionPuntoVenta != null)
            {
                _IRepositorioConfiguracionPuntoVenta.ActualizarCorrelativos(pConfiguracionPuntoVenta);
            }
            else
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_NoSeObtuvoResultadoDeConsultaConfiguracionPuntoDeVenta);
        }

        void ActualizarArticulo(Venta pVenta)
        {
            foreach (var detalleVenta in pVenta.VentaDetalles)
            {
                var articuloAPersistir = _IRepositorioArticulo.ObtenerPorCodigo(detalleVenta.CodigoArticulo, pVenta.CodigoAlmacen);
                if (articuloAPersistir != null)
                {
                    _IRepositorioArticulo.Modificar(detalleVenta.Articulo);
                    // _IRepositorioArticulo.UnidadTrabajo.Commit();
                }
                else
                    LogFactory.CrearLog().LogWarning(Mensajes.advertencia_NoSeObtuvoResultadoDeConsultaArticuloAPersistir);                
            }                    
        }

        void AgregarOActualizarCliente(Cliente pCliente, string pFlagCrudCliente)
        {
            if(pCliente != null)
            {
                if(pFlagCrudCliente == EnumCrudCliente.CrearCliente)
                {
                    _IRepositorioCliente.Agregar(pCliente);                        
                }
                else if(pFlagCrudCliente == EnumCrudCliente.ActualizarCliente)
                {
                    _IRepositorioCliente.Modificar(pCliente);                        
                }
            }
            else    
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_NoSeObtuvoResultadoDeConsultaClienteAPersistir);                                

        }

        Cliente MaterializarCliente(Cliente pCliente, ClienteDTO pClienteDTO, out string pFlagCrudCliente)
        {
            pFlagCrudCliente = string.Empty;
            if (pCliente == null)
            {
                pFlagCrudCliente = EnumCrudCliente.CrearCliente;
                pCliente = MaterializarClienteDesdeClienteDTO(pClienteDTO);
            }
            else
            {
                if(pCliente.CodigoTipoCliente != EnumTipoCliente.CodigoTipoClienteCreditoCorporativo
                        && pCliente.CodigoTipoCliente != EnumTipoCliente.CodigoTipoClienteCreditoLocal
                        && pCliente.CodigoTipoCliente != EnumTipoCliente.CodigoTipoClientePagoAdelantado)
                {
                    pFlagCrudCliente = EnumCrudCliente.ActualizarCliente;
                    pCliente = MaterializarClienteDesdeClienteYClienteDTO(pCliente, pClienteDTO);                        
                }
            }

            return pCliente;
        }


        Cliente MaterializarClienteDesdeClienteDTO(ClienteDTO pClienteDTO)
        {
            var nuevoCliente = new Cliente(){
                CodigoCliente = pClienteDTO.CodigoCliente,
                CodigoContable = string.Empty,  
                Ruc = pClienteDTO.Ruc,
                NombresORazonSocial = pClienteDTO.NombresORazonSocial,
                Telefono = pClienteDTO.Telefono,
                Fax =  string.Empty,            
                FechaNacimiento = DateTime.Now,
                FechaInscripcion = DateTime.Now,
                DiasDeGracia = 0,
                MontoLimiteCredito = 0,
                Deuda = 0,
                EsAfecto = EnumCliente.ClienteNoAfecto,
                ControlarSaldoDisponible = EnumCliente.ClienteSinControlDeSaldoDisponible
            };

            nuevoCliente.EstablecerMonedaDeCliente(new Moneda{ CodigoMoneda = EnumCliente.ClienteCreaCodigoMoneda});
            nuevoCliente.EstablecerClaseTipoCambioDeCliente(new ClaseTipoCambio{ CodigoClaseTipoCambio = EnumCliente.ClienteCodigoClaseTipoCambioDefault});
            nuevoCliente.EstablecerTipoClienteDeCliente(new TipoCliente{ CodigoTipoCliente = EnumTipoCliente.CodigoTipoClienteContado});
            nuevoCliente.EstablecerZonaClienteDeCliente(new ZonaCliente{ CodigoZonaCliente = EnumCliente.ClienteCreaCodigoZonaCliente});
            nuevoCliente.EstablecerDiaDePagoDeCliente(new DiaDePago{CodigoDiaDePago = EnumCliente.ClienteCreaCodigoDiaDePago });
            nuevoCliente.EstablecerVendedorDeCliente(new Vendedor{ CodigoVendedor = EnumCliente.ClienteCreaCodigoVendedor });
            nuevoCliente.EstablecerImpuestoIgvDeCliente(new Impuesto{ CodigoImpuesto = EnumCliente.ClienteCreaCodigoImpuestoIgv });
            nuevoCliente.EstablecerImpuestoIscDeCliente(new Impuesto{ CodigoImpuesto = EnumCliente.ClienteCreaCodigoImpuestoIsc });
            nuevoCliente.EstablecerCondicionPagoDocumentoGeneradoDeCliente(new CondicionPago{ CodigoCondicionPago = EnumCondicionPago.CodigoCondicionPagoContraentrega });
            nuevoCliente.EstablecerCondicionPagoTicketDeCliente(new CondicionPago{ CodigoCondicionPago = EnumCondicionPago.CodigoCondicionPagoContraentrega });
            nuevoCliente.EstablecerEstadoDeClienteDeCliente(new EstadoDeCliente{ CodigoEstadoDeCliente = EnumCliente.ClienteCodigoEstadoDeClienteDefault});
            nuevoCliente.EstablecerUsuarioSistemaDeCliente(new UsuarioSistema{ CodigoUsuarioDeSistema = EnumCliente.ClienteUsuarioDeSistemaDefault});
            nuevoCliente.EstablecerPaisDeCliente(new Pais{ CodigoPais = EnumCliente.ClienteCreaCodigoPais });
            nuevoCliente.EstablecerDepartamentoDeCliente(new Departamento{ CodigoDepartamento = EnumCliente.ClienteCreaCodigoDepartamento });
            nuevoCliente.EstablecerDistritoDeCliente(new Distrito{ CodigoDistrito = EnumCliente.ClienteCreaCodigoDistrito });

            nuevoCliente.DireccionPrimero = new ClienteDireccion(pClienteDTO.DireccionPrimeroPais, 
                                                                pClienteDTO.DireccionPrimeroDepartamento, 
                                                                pClienteDTO.DireccionPrimeroProvincia, 
                                                                pClienteDTO.DireccionPrimeroDistrito, 
                                                                pClienteDTO.DireccionPrimeroUbicacion); 

            nuevoCliente.DireccionSegundo = new ClienteDireccion(pClienteDTO.DireccionSegundoUbicacion, 
                                                                pClienteDTO.DireccionSegundoDepartamento, 
                                                                pClienteDTO.DireccionSegundoProvincia, 
                                                                pClienteDTO.DireccionSegundoDistrito, 
                                                                pClienteDTO.DireccionSegundoUbicacion); 

            return nuevoCliente;
        }
        
        Cliente MaterializarClienteDesdeClienteYClienteDTO(Cliente pCliente, ClienteDTO pClienteDTO)
        {
            Cliente clienteActualizado = pCliente;
            clienteActualizado.NombresORazonSocial = pClienteDTO.NombresORazonSocial;
            clienteActualizado.Telefono = pClienteDTO.Telefono;
            clienteActualizado.DireccionPrimero.Ubicacion = pClienteDTO.DireccionPrimeroUbicacion;
            clienteActualizado.DireccionSegundo.Ubicacion = pClienteDTO.DireccionSegundoUbicacion;

            return clienteActualizado;
        }

        private void IdentificacionInicialDeTipoPagoDeVenta(VentaDTO pVentaDTO)
        {
            //Determinamos el Tipo de pago Verificar si es solo pago en efectivo en doble moneda y/o existe pago con tarjeta o Ambos

            //Caso Venta Adelantada (14)
            if (pVentaDTO.CodigoTipoPago == EnumTipoPago.CodigoTipoPagoContadoAdelantado) { return; }

            if ((pVentaDTO.TotalEfectivoNacional + pVentaDTO.TotalEfectivoExtranjera) > 0)
            {
                pVentaDTO.CodigoTipoPago = EnumTipoPago.CodigoTipoPagoEfectivo;             //Efectivo
            }

            if (pVentaDTO.VentaConTarjetas != null)
            {
                if (pVentaDTO.VentaConTarjetas.Count > 0)
                {
                    if (pVentaDTO.CodigoTipoPago == EnumTipoPago.CodigoTipoPagoEfectivo)
                    {
                        pVentaDTO.CodigoTipoPago = EnumTipoPago.CodigoTipoPagoOtros;        //Otros
                    }
                    else
                    {
                        pVentaDTO.CodigoTipoPago = EnumTipoPago.CodigoTipoPagoTarjeta;      //Tarjetas
                    }
                }
            }
        }




        VentaDTO MaterializarPedidoRetailAVentaDTO(PedidoRetail pPedidoRetail)
        {
            var nuevaVentaDto = new VentaDTO
            {
                NumeroDocumento = pPedidoRetail.NumeroDocumento,
                FechaDocumento = DateTime.Now,     //pPedidoRetail.FechaDocumento,
                FechaProceso = DateTime.Now, //pPedidoRetail.FechaProceso,
                Periodo = pPedidoRetail.Periodo,
                TotalNacional = pPedidoRetail.TotalNacional,
                TotalExtranjera = pPedidoRetail.TotalExtranjera,
                SubTotalNacional = pPedidoRetail.SubTotalNacional,
                SubTotalExtranjera = pPedidoRetail.SubTotalExtranjera,
                ImpuestoIgvNacional = pPedidoRetail.ImpuestoIgvNacional,
                ImpuestoIgvExtranjera = pPedidoRetail.ImpuestoIgvExtranjera,
                ImpuestoIscNacional = pPedidoRetail.ImpuestoIscNacional,
                ImpuestoIscExtranjera = pPedidoRetail.ImpuestoIscExtranjera,
                TotalNoAfectoNacional = pPedidoRetail.TotalNoAfectoNacional,
                TotalNoAfectoExtranjera = pPedidoRetail.TotalNoAfectoExtranjera,
                TotalAfectoNacional = 0,
                ValorVenta = 0,
                PorcentajeDescuentoPrimero = pPedidoRetail.PorcentajeDescuentoPrimero,
                PorcentajeDescuentoSegundo = pPedidoRetail.PorcentajeDescuentoSegundo,
                TotalDescuentoNacional = pPedidoRetail.TotalDescuentoNacional,
                TotalDescuentoExtranjera = pPedidoRetail.TotalDescuentoExtranjera,
                TotalVueltoNacional = pPedidoRetail.TotalVueltoNacional,
                TotalVueltoExtranjera = pPedidoRetail.TotalVueltoExtranjera,
                TotalEfectivoNacional = pPedidoRetail.TotalEfectivoNacional,
                TotalEfectivoExtranjera = pPedidoRetail.TotalEfectivoExtranjera,
                RucCliente = pPedidoRetail.RucCliente,
                NombreCompletoCliente = pPedidoRetail.NombreCompletoCliente,
                Placa = pPedidoRetail.Placa,
                NumeroVale = pPedidoRetail.NumeroVale,
                TipoCambio = pPedidoRetail.TipoCambio,
                ProcesadoCierreZ = false,
                ProcesadoCierreX = false,
                Kilometraje = pPedidoRetail.Kilometraje,
                AfectaInventario = pPedidoRetail.AfectaInventario,
                // TipoPagoCodigoTipoPago = string.Empty,

                CodigoMoneda = pPedidoRetail.CodigoMoneda,
                CodigoClaseTipoCambio = pPedidoRetail.CodigoClaseTipoCambio,
                CodigoCliente = pPedidoRetail.CodigoCliente,
                CodigoTipoDocumento = pPedidoRetail.CodigoTipoDocumento,
                CodigoEstadoDocumento = EnumEstadoDocumento.CodigoEstadoDocumentoPorDefecto,
                CodigoVendedor = pPedidoRetail.CodigoVendedor,
                CodigoCondicionPago = pPedidoRetail.CodigoCondicionPago,
                CodigoTipoPago = pPedidoRetail.CodigoTipoPago,
                CodigoPuntoDeVenta = pPedidoRetail.CodigoPuntoDeVenta,
                CodigoAlmacen = pPedidoRetail.CodigoAlmacen,
                CodigoTipoNegocio = pPedidoRetail.CodigoTipoNegocio,
                CodigoUsuarioDeSistema = pPedidoRetail.CodigoUsuarioDeSistema,
                CodigoImpuestoIgv = pPedidoRetail.CodigoImpuestoIgv,
                CodigoImpuestoIsc = pPedidoRetail.CodigoImpuestoIsc
            };

            if (pPedidoRetail.PedidoRetailDetalles != null && pPedidoRetail.PedidoRetailDetalles.Any())
            {
                foreach (var detallePedido in pPedidoRetail.PedidoRetailDetalles)
                {
                    nuevaVentaDto.VentaDetalles.Add(new VentaDetalleDTO
                    {
                        NumeroDocumento = detallePedido.NumeroDocumento,
                        Secuencia = detallePedido.Secuencia,
                        FechaDocumento = detallePedido.FechaDocumento,
                        FechaProceso = detallePedido.FechaProceso,
                        Periodo = detallePedido.Periodo,
                        NumeroTurno = detallePedido.NumeroTurno,
                        NumeroCara = string.Empty,
                        PorcentajeImpuestoIgv = detallePedido.PorcentajeImpuestoIgv,
                        PorcentajeImpuestoIsc = detallePedido.PorcentajeImpuestoIsc,
                        TotalNacional = detallePedido.TotalNacional,
                        TotalExtranjera = detallePedido.TotalExtranjera,
                        ImpuestoNacional = detallePedido.ImpuestoNacional,
                        ImpuestoExtranjera = detallePedido.ImpuestoExtranjera,
                        PorcentajeDescuentoPrimero = 0,
                        TotalDescuentoNacional = 0,
                        TotalDescuentoExtranjera = 0,
                        Precio = detallePedido.Precio,
                        PrecioVenta = detallePedido.PrecioVenta,
                        DescripcionArticulo = detallePedido.DescripcionArticulo,
                        Cantidad = detallePedido.Cantidad,
                        UsuarioSistema = pPedidoRetail.CodigoUsuarioDeSistema,
                        EsFormula = detallePedido.EsFormula,
                        // ArticuloCodigoArticulo = string.Empty,

                        CodigoArticulo = detallePedido.CodigoArticulo,
                        CodigoArticuloAlterno = detallePedido.CodigoArticuloAlterno,
                        CodigoMoneda = detallePedido.CodigoMoneda,
                        CodigoEstadoDocumento = EnumEstadoDocumento.CodigoEstadoDocumentoPorDefecto,
                    });
                }
            }

            if (pPedidoRetail.PedidoRetailConTarjetas != null && pPedidoRetail.PedidoRetailConTarjetas.Any())
            {
                foreach (var pedidoConTarjeta in pPedidoRetail.PedidoRetailConTarjetas)
                {
                    nuevaVentaDto.VentaConTarjetas.Add(new VentaConTarjetaDTO
                    {
                        NumeroDocumento = pPedidoRetail.NumeroDocumento,
                        Secuencia = pedidoConTarjeta.Secuencia,
                        NumeroTarjeta = pedidoConTarjeta.NumeroTarjeta,
                        TotalTarjetaNacional = pedidoConTarjeta.TotalTarjetaNacional,
                        TotalTarjetaExtranjera = pedidoConTarjeta.TotalTarjetaExtranjera,
                        FechaProceso = pPedidoRetail.FechaProceso,
                        TarjetaDescripcionTarjeta = pedidoConTarjeta.DescripcionTarjeta,

                        CodigoMoneda = pedidoConTarjeta.CodigoMoneda,
                        CodigoTarjeta = pedidoConTarjeta.CodigoTarjeta,
                        CodigoTipoDocumento = EnumEstadoDocumento.CodigoEstadoDocumentoPorDefecto,
                        CodigoAlmacen = pedidoConTarjeta.CodigoMoneda
                    });
                }
            }

            if (pPedidoRetail.PedidoRetailConVales != null && pPedidoRetail.PedidoRetailConVales.Any())
            {
                foreach (var pedidoConVale in pPedidoRetail.PedidoRetailConVales)
                {
                    nuevaVentaDto.VentaConVales.Add(new VentaConValeDTO
                    {
                        NumeroDocumento = pPedidoRetail.NumeroDocumento,
                        NumeroVale = pedidoConVale.NumeroVale,
                        FechaProceso = pPedidoRetail.FechaProceso,
                        MontoVale = pPedidoRetail.TotalNacional,

                        CodigoCliente = pedidoConVale.CodigoCliente,
                        CodigoAlmacen = pedidoConVale.CodigoAlmacen,
                        CodigoTipoDocumento = EnumEstadoDocumento.CodigoEstadoDocumentoPorDefecto,
                        CodigoMoneda = pPedidoRetail.CodigoMoneda
                    });
                }
            }

            return nuevaVentaDto;
        }

        VentaDTO MaterializarPedidoEESSAVentaDTO(PedidoEESS pPedidoEESS)
        {
            var nuevaVentaDto = new VentaDTO
            {
                NumeroDocumento = pPedidoEESS.NumeroDocumento,
                FechaDocumento = DateTime.Now,          //pPedidoEESS.FechaDocumento,
                FechaProceso = DateTime.Now,            //pPedidoEESS.FechaProceso,
                Periodo = pPedidoEESS.Periodo,
                TotalNacional = pPedidoEESS.TotalNacional,
                TotalExtranjera = pPedidoEESS.TotalExtranjera,
                SubTotalNacional = pPedidoEESS.SubTotalNacional,
                SubTotalExtranjera = pPedidoEESS.SubTotalExtranjera,
                ImpuestoIgvNacional = pPedidoEESS.ImpuestoIgvNacional,
                ImpuestoIgvExtranjera = pPedidoEESS.ImpuestoIgvExtranjera,
                ImpuestoIscNacional = pPedidoEESS.ImpuestoIscNacional,
                ImpuestoIscExtranjera = pPedidoEESS.ImpuestoIscExtranjera,
                TotalNoAfectoNacional = pPedidoEESS.TotalNoAfectoNacional,
                TotalNoAfectoExtranjera = pPedidoEESS.TotalNoAfectoExtranjera,
                TotalAfectoNacional = 0,
                ValorVenta = 0,
                PorcentajeDescuentoPrimero = pPedidoEESS.PorcentajeDescuentoPrimero,
                PorcentajeDescuentoSegundo = pPedidoEESS.PorcentajeDescuentoSegundo,
                TotalDescuentoNacional = pPedidoEESS.TotalDescuentoNacional,
                TotalDescuentoExtranjera = pPedidoEESS.TotalDescuentoExtranjera,
                TotalVueltoNacional = pPedidoEESS.TotalVueltoNacional,
                TotalVueltoExtranjera = pPedidoEESS.TotalVueltoExtranjera,
                TotalEfectivoNacional = pPedidoEESS.TotalEfectivoNacional,
                TotalEfectivoExtranjera = pPedidoEESS.TotalEfectivoExtranjera,
                RucCliente = pPedidoEESS.RucCliente,
                NombreCompletoCliente = pPedidoEESS.NombreCompletoCliente,
                Placa = pPedidoEESS.Placa,
                NumeroVale = pPedidoEESS.NumeroVale,
                TipoCambio = pPedidoEESS.TipoCambio,
                ProcesadoCierreZ = pPedidoEESS.ProcesadoCierreZ,
                ProcesadoCierreX = pPedidoEESS.ProcesadoCierreX,
                Kilometraje = pPedidoEESS.Kilometraje,
                AfectaInventario = pPedidoEESS.AfectaInventario,
                // TipoPagoCodigoTipoPago = string.Empty,

                CodigoMoneda = pPedidoEESS.CodigoMoneda,
                CodigoClaseTipoCambio = pPedidoEESS.CodigoClaseTipoCambio,
                CodigoCliente = pPedidoEESS.CodigoCliente,
                CodigoTipoDocumento = pPedidoEESS.CodigoTipoDocumento,
                CodigoEstadoDocumento = pPedidoEESS.CodigoEstadoDocumento,
                CodigoVendedor = pPedidoEESS.CodigoVendedor,
                CodigoCondicionPago = pPedidoEESS.CodigoCondicionPago,
                CodigoTipoPago = pPedidoEESS.CodigoTipoPago,
                CodigoPuntoDeVenta = pPedidoEESS.CodigoPuntoDeVenta,
                CodigoAlmacen = pPedidoEESS.CodigoAlmacen,
                CodigoTipoNegocio = EnumTipoNegocio.CodigoTipoNegocioEESS,
                CodigoUsuarioDeSistema = pPedidoEESS.CodigoUsuarioDeSistema,
                CodigoImpuestoIgv = pPedidoEESS.CodigoImpuestoIgv,
                CodigoImpuestoIsc = pPedidoEESS.CodigoImpuestoIsc
            };

            if (pPedidoEESS.PedidoEESSDetalles != null && pPedidoEESS.PedidoEESSDetalles.Any())
            {
                foreach (var detallePedido in pPedidoEESS.PedidoEESSDetalles)
                {
                    nuevaVentaDto.VentaDetalles.Add(new VentaDetalleDTO
                    {
                        NumeroDocumento = detallePedido.NumeroDocumento,
                        Secuencia = detallePedido.Secuencia,
                        FechaDocumento = detallePedido.FechaDocumento,
                        FechaProceso = detallePedido.FechaProceso,
                        Periodo = detallePedido.Periodo,
                        NumeroTurno = detallePedido.NumeroTurno,
                        NumeroCara = detallePedido.NumeroCara,
                        PorcentajeImpuestoIgv = detallePedido.PorcentajeImpuestoIgv,
                        PorcentajeImpuestoIsc = detallePedido.PorcentajeImpuestoIsc,
                        TotalNacional = detallePedido.TotalNacional,
                        TotalExtranjera = detallePedido.TotalExtranjera,
                        ImpuestoNacional = detallePedido.ImpuestoNacional,
                        ImpuestoExtranjera = detallePedido.ImpuestoExtranjera,
                        PorcentajeDescuentoPrimero = detallePedido.PorcentajeDescuentoPrimero,
                        TotalDescuentoNacional = 0,
                        TotalDescuentoExtranjera = 0,
                        Precio = detallePedido.Precio,
                        PrecioVenta = detallePedido.PrecioVenta,
                        DescripcionArticulo = detallePedido.DescripcionArticulo,
                        Cantidad = detallePedido.Cantidad,
                        UsuarioSistema = detallePedido.CodigoUsuarioDeSistema,
                        EsFormula = detallePedido.EsFormula,
                        // ArticuloCodigoArticulo = string.Empty,

                        CodigoArticulo = detallePedido.CodigoArticulo,
                        CodigoArticuloAlterno = detallePedido.CodigoArticuloAlterno,
                        CodigoMoneda = detallePedido.CodigoMoneda,
                        CodigoEstadoDocumento = detallePedido.CodigoEstadoDocumento
                    });
                }
            }

            if (!string.IsNullOrEmpty(pPedidoEESS.CodigoTarjeta))
            {
                nuevaVentaDto.VentaConTarjetas.Add(new VentaConTarjetaDTO
                {
                    NumeroDocumento = pPedidoEESS.NumeroDocumento,
                    Secuencia = 1,
                    NumeroTarjeta = pPedidoEESS.NumeroTarjeta,
                    TotalTarjetaNacional = pPedidoEESS.PagoTarjeta,
                    TotalTarjetaExtranjera = 0,
                    FechaProceso = pPedidoEESS.FechaProceso,
                    TarjetaDescripcionTarjeta = pPedidoEESS.DescripcionTarjeta,

                    CodigoMoneda = pPedidoEESS.CodigoMoneda,
                    CodigoTarjeta = pPedidoEESS.CodigoTarjeta,
                    CodigoTipoDocumento = pPedidoEESS.CodigoTipoDocumento,
                    CodigoAlmacen = pPedidoEESS.CodigoAlmacen
                });
            }

            if (pPedidoEESS.PedidoEESSConVales != null && pPedidoEESS.PedidoEESSConVales.Any())
            {
                foreach (var pedidoConVale in pPedidoEESS.PedidoEESSConVales)
                {
                    nuevaVentaDto.VentaConVales.Add(new VentaConValeDTO
                    {
                        NumeroDocumento = pPedidoEESS.NumeroDocumento,
                        NumeroVale = pedidoConVale.NumeroVale,
                        FechaProceso = pPedidoEESS.FechaProceso,
                        MontoVale = pPedidoEESS.TotalNacional,

                        CodigoCliente = pedidoConVale.CodigoCliente,
                        CodigoAlmacen = pedidoConVale.CodigoAlmacen,
                        CodigoTipoDocumento = pPedidoEESS.CodigoTipoDocumento,
                        CodigoMoneda = pPedidoEESS.CodigoMoneda
                    });
                }
            }

            return nuevaVentaDto;
        }

    }
}