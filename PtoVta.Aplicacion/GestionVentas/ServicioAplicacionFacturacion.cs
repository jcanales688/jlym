using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using PtoVta.Aplicacion.BaseTrabajo;
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
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.EstadosVenta;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.GestionVentas
{
    public class ServicioAplicacionFacturacion : IServicioAplicacionFacturacion
    {
        private List<Articulo> _ListaArticulosStockActualizado = new List<Articulo>();
        private List<MovimientoAlmacen> _ListaNuevosMovimientoAlmacen = new List<MovimientoAlmacen>();

        private IRepositorioVenta _IRepositorioVenta;
        private IRepositorioEstadoDocumento _IRepositorioEstadoDocumento;
        private IRepositorioTipoDocumento _IRepositorioTipoDocumento;
        private IRepositorioCliente _IRepositorioCliente;
        private IRepositorioClaseTipoCambio _IRepositorioClaseTipoCambio;               //ROOT DE TIPO DE CAMBIO
        private IRepositorioVendedor _IRepositorioVendedor;
        private IRepositorioMoneda _IRepositorioMoneda;
        private IRepositorioConfiguracionPuntoVenta _IRepositorioConfiguracionPuntoVenta;
        private IRepositorioConfiguracionGeneral _IRepositorioConfiguracionGeneral;
        private IRepositorioTipoPago _IRepositorioTipoPago;
        private IRepositorioTipoMovimientoAlmacen _IRepositorioTipoMovimientoAlmacen;
        private IRepositorioCondicionPago _IRepositorioCondicionPago;                   // SU ROOT ES CLIENTE 
        private IRepositorioArticulo _IRepositorioArticulo;
        private IRepositorioTarjeta _IRepositorioTarjeta;
        private IRepositorioMovimientoAlmacen _IRepositorioMovimientoAlmacen;
        private IRepositorioAlmacen _IRepositorioAlmacen;
        private IRepositorioTipoNegocio _IRepositorioTipoNegocio;
        private IRepositorioUsuarioSistema _IRepositorioUsuarioSistema;

        private IServicioDominioVentas _IServicioDominioVentas;
        private IServicioDominioMovimientosAlmacen _IServicioDominioMovimientosAlmacen;
        private IServicioDominioCuentaPorCobrar _IServicioDominioCuentaPorCobrar;

        public ServicioAplicacionFacturacion(IRepositorioVenta pIrepositorioVenta, IRepositorioEstadoDocumento pIrepositorioEstadoDocumento,
                                IRepositorioTipoDocumento pIrepositorioTipoDocumento, IRepositorioCliente pIrepositorioCliente,
                                IRepositorioClaseTipoCambio pIrepositorioClaseTipoCambio, IRepositorioVendedor pIrepositorioVendedor,
                                IRepositorioMoneda pIrepositorioMoneda, IRepositorioConfiguracionPuntoVenta pIrepositorioConfiguracionPtoVta,
                                IRepositorioConfiguracionGeneral pIRepositorioConfiguracionGeneral, IRepositorioTipoPago pIrepositorioTipoPago,
                                IRepositorioTipoMovimientoAlmacen pIrepositorioTipoMovimientoAlmacen, IRepositorioCondicionPago pIrepositorioCondicionPago,
                                IRepositorioArticulo pIrepositorioArticulo, IRepositorioTarjeta pIrepositorioTarjeta,
                                IRepositorioMovimientoAlmacen pIrepositorioMovimientoAlmacen, IRepositorioAlmacen pIRepositorioAlmacen,
                                IRepositorioTipoNegocio pIRepositorioTipoNegocio, IRepositorioUsuarioSistema pIRepositorioUsuarioSistema,
                                IServicioDominioVentas pIServicioDominioVentas
                                , IServicioDominioMovimientosAlmacen pIServicioDominioMovimientosAlmacen
                                , IServicioDominioCuentaPorCobrar pIServicioDominioCuentaPorCobrar)
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

            if (pIServicioDominioVentas == null)
                throw new ArgumentNullException("pIServicioDominioVentas Nulo En ServicioAplicacionFacturacion");

            if (pIServicioDominioMovimientosAlmacen == null)
                throw new ArgumentNullException("pIServicioDominioMovimientosAlmacen Nulo En ServicioAplicacionFacturacion");

            if (pIServicioDominioCuentaPorCobrar == null)
                throw new ArgumentNullException("pIServicioDominioCuentaPorCobrar Nulo En ServicioAplicacionFacturacion");

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

            _IServicioDominioVentas = pIServicioDominioVentas;
            _IServicioDominioMovimientosAlmacen = pIServicioDominioMovimientosAlmacen;
            _IServicioDominioCuentaPorCobrar = pIServicioDominioCuentaPorCobrar;
        }



        public ResultadoServicio<ResultadoVentaGrabadaDTO> AgregarNuevaVenta(VentaDTO pVentaDTO, string pCodigoTipoDocumentoNotaCredito, bool pEsVentaPagoAdelantado,
                                                    string pCodigoTMAVentas, int pPermitirStockNegativo, DateTime pFechaTipoDeCambio,
                                                    string pTipoDeVenta, string pCodigoCondicionPagoDefault, string pCodigoEstadoDocumentoDefault,
                                                    bool pFlagCambioMonedaVuelto, decimal pEfectivoVueltoExtranjera, decimal pTotalVueltoSegunMoneda,
                                                    decimal pTotalFaltanteExtranjera, decimal pTotalFaltanteNacional, string pCodigoMonedaVuelto,
                                                    string pCodigoMonedaBase, string pCodigoMonedaExtranjera, string pCodigoConfiguracionGeneral)
        {
            //Determina si se creara Venta a Cuenta por Cobrar
            bool esVentaACuentaPorCobrar = false;
            decimal saldoDisponibleAdelanto = 0;

            if (pVentaDTO == null || string.IsNullOrEmpty(pVentaDTO.CodigoTipoDocumento))
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_AdvertenciaVentaOTipoDocumentoInvalido);
                throw new ArgumentException(Mensajes.advertencia_AdvertenciaVentaOTipoDocumentoInvalido);
            }

            //Identificar tipo de Pago venta 
            IdentificarTipoPagoVenta(pVentaDTO);

            //Aqui podria procesarse el calculo del vuelto....

            //Obtener datos de configuracion
            var configuracionGeneral = _IRepositorioConfiguracionGeneral.Obtener();
            if (configuracionGeneral == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConfiguracionDeAplicacionIncompleta);
                throw new ArgumentException(Mensajes.advertencia_ConfiguracionDeAplicacionIncompleta);
            }

            //Obtener detalles de Punto de Venta
            var configPuntoDeVenta = _IRepositorioConfiguracionPuntoVenta.ObtenerPorPuntoDeVenta(pVentaDTO.CodigoConfiguracionPuntoVenta);
            if (configPuntoDeVenta == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConfiguracionPuntoVentaAsociadoAVentaNoExiste);
                return null;
            }

            //Obtener Almacen
            var almacen = _IRepositorioAlmacen.ObtenerPorCodigo(pVentaDTO.CodigoAlmacen);
            if (almacen == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_AlmacenAsociadoAVentaNoExiste);
                return null;
            }

            //Obtener Tipo Negocio
            var tipoNegocio = _IRepositorioTipoNegocio.ObtenerPorCodigo(pVentaDTO.CodigoTipoNegocio);
            if (tipoNegocio == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoNegocioAsociadoAVentaNoExiste);
                return null;
            }

            //Obteber Usuario de Punto Venta
            var usuarioSistema = _IRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario(pVentaDTO.CodigoUsuarioDeSistema);
            if (usuarioSistema == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_UsuarioSistemaAsociadoAVentaNoExiste);
                return null;
            }

            //Obtener Tipo de Documento y Correlativo (usuario no debe ver el correlativo)
            var tipoDocumento = _IRepositorioTipoDocumento.ObtenerCorrelativoDocumento(pVentaDTO.CodigoAlmacen, pVentaDTO.CodigoConfiguracionPuntoVenta,
                                                                                        pVentaDTO.CodigoTipoDocumento, pTipoDeVenta, 1);
            if (tipoDocumento == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoDocumentoAsociadoAVentaNoExiste);
                return null;
            }

            //Obtener Estado de Documento
            var estadoDocumento = _IRepositorioEstadoDocumento.ObtenerPorCodigo(pVentaDTO.CodigoEstadoDocumento);
            if (estadoDocumento == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_EstadoDocumentoAsociadoAVentaNoExiste);
                return null;
            }

            //Validacion y asignacion de corrlativo de documento. 2015-03-29
            var correlativoDocumento = FuncionesNegocio.CorrelativoDocumento(tipoDocumento.CorrelativosDocumento.Single().Serie,
                                                                (long)tipoDocumento.CorrelativosDocumento.FirstOrDefault().Correlativo);
            if (ExisteDocumentoDeVenta(pVentaDTO.CodigoTipoDocumento, correlativoDocumento, pVentaDTO.CodigoAlmacen) == true)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_CorrelativoDocumentoYaFueGenerado);
                throw new ArgumentException(Mensajes.advertencia_CorrelativoDocumentoYaFueGenerado);
            }
            else
            {
                pVentaDTO.NumeroDocumento = correlativoDocumento;
            }

            //Obtener el cliente
            var cliente = _IRepositorioCliente.ObtenerPorCodigo(pVentaDTO.CodigoCliente);
            if (cliente == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ClienteAsociadoAVentaNoExiste);
                return null;
            }

            //Obtener Clase de Tipo de Cambio y el monto del tipo de cambio
            var claseTipoCambio = _IRepositorioClaseTipoCambio.ObtenerPorCodigo(pVentaDTO.CodigoClaseTipoCambio);
            if (claseTipoCambio == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ClaseTipoDeCambioAsociadoAVentaNoExiste);
                return null;
            }

            //Obtener el Vendedor
            var vendedor = _IRepositorioVendedor.ObtenerPorCodigo(pVentaDTO.CodigoVendedor);
            if (vendedor == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_VendedorAsociadoAVentaNoExiste);
                return null;
            }

            //Obtener la Moneda
            var moneda = _IRepositorioMoneda.ObtenerPorCodigo(pVentaDTO.CodigoMoneda);
            if (moneda == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_MonedaAsociadoAVentaNoExiste);
                return null;
            }

            //Obtener Condicion de Pago a partir de entidad Cliente
            CondicionPago condicionPagoDeVentaActual = cliente.CondicionPagoTicket; //Determinar si es DOCUMENTO O TICKET

            //Obtener el Tipo de Pago
            TipoPago tipoPagoDeVentaActual = _IRepositorioTipoPago.ObtenerPorCodigo(pVentaDTO.TipoPagoCodigoTipoPago);
            if (tipoPagoDeVentaActual == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);
                return null;
            }

            CondicionPago condicionPagoDefault = _IRepositorioCondicionPago.ObtenerPorCodigo(pCodigoCondicionPagoDefault);

            TipoPago tipoPagoEfectivo = _IRepositorioTipoPago.ObtenerPorCodigo(VentaTipoPago.VentaEfectivo);


            //Obtener tipo de Movimiento de Almance para las ventas y determinar si es movimiento de ingreso o salida
            var tipoMovAlmacenVentas = _IRepositorioTipoMovimientoAlmacen.ObtenerPorCodigo(pCodigoTMAVentas);
            if (tipoMovAlmacenVentas == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoMovAlmacenVentasAsociadoAVentaNoExiste);
                return null;
            }

            var movAlmacenIngresoOSalida = _IServicioDominioMovimientosAlmacen.MovimientoAlmacenIngresoOSalida(pCodigoTipoDocumentoNotaCredito,
                                                                                                tipoDocumento, tipoMovAlmacenVentas);

            //
            if (cliente.ControlarSaldoDisponible == 1)
            {
                saldoDisponibleAdelanto = ObtenerSaldoVentaAdelantado(pVentaDTO.CodigoTipoPago, pVentaDTO.CodigoCliente,
                                                                        pVentaDTO.CodigoAlmacen, pCodigoTipoDocumentoNotaCredito,
                                                                        pVentaDTO.FechaProceso);
            }

            _IServicioDominioVentas.ObtenerModalidadPagoDeVenta(condicionPagoDeVentaActual, condicionPagoDefault, tipoPagoDeVentaActual,
                                                                tipoPagoEfectivo, cliente, tipoDocumento,
                                                                configPuntoDeVenta, pCodigoTipoDocumentoNotaCredito, pVentaDTO.TotalNacional,
                                                                esVentaACuentaPorCobrar, saldoDisponibleAdelanto);

            if (esVentaACuentaPorCobrar)
            {
                var estadoDocumentoCC = _IRepositorioEstadoDocumento.ObtenerPorCodigo(pCodigoEstadoDocumentoDefault);
                if (estadoDocumentoCC == null)
                {
                    LogFactory.CrearLog().LogWarning(Mensajes.advertencia_EstadoDeDocumentoCuentasXCobrarAsociadoAVentaNoExiste);
                    return null;
                }
            }
            else
            {
                pVentaDTO.CodigoTipoPago = tipoPagoDeVentaActual.CodigoTipoPago;

                pVentaDTO.TotalEfectivoNacional = pVentaDTO.TotalNacional;
                pVentaDTO.TotalEfectivoExtranjera = pVentaDTO.TotalExtranjera;
            }

            //Crear venta con detalles
            var nuevaVenta = CrearNuevaVenta(pVentaDTO, moneda, claseTipoCambio,
                                            cliente, tipoDocumento, estadoDocumento,
                                            vendedor, condicionPagoDeVentaActual, tipoPagoDeVentaActual,
                                            configPuntoDeVenta, almacen, tipoNegocio,
                                            usuarioSistema, pEsVentaPagoAdelantado, esVentaACuentaPorCobrar,
                                            tipoMovAlmacenVentas, movAlmacenIngresoOSalida, pPermitirStockNegativo,
                                            pFechaTipoDeCambio, configuracionGeneral.CodigoClienteInterno, configuracionGeneral.CantDecimalPrecio);

            //Calcular el vuelto
            _IServicioDominioVentas.CalcularVueltoVentaSegunMoneda(nuevaVenta, pFlagCambioMonedaVuelto,
                                                configuracionGeneral.CantDecimalPrecio, pEfectivoVueltoExtranjera,
                                                pTotalVueltoSegunMoneda, pTotalFaltanteExtranjera,
                                                pTotalFaltanteNacional,
                                                pCodigoMonedaVuelto, pCodigoMonedaBase, pCodigoMonedaExtranjera);

            //Actualizacion previas de correlativo
            tipoDocumento.ActualizaCorrelativoDocumento();

            //Persistir transaccion de Venta
            GrabarTransaccionDeVenta(nuevaVenta, tipoDocumento, _ListaArticulosStockActualizado,
                                                    _ListaNuevosMovimientoAlmacen);

            //devolver en DTO
            if (nuevaVenta != null)
            {
                return new ResultadoServicio<ResultadoVentaGrabadaDTO>(7, Mensajes.advertencia_ExitosaCreacionNuevaVentaEnVenta,
                        string.Empty, nuevaVenta.ProyectadoComo<ResultadoVentaGrabadaDTO>(), null);
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_FalloCreacionNuevaVentaEnVenta);
                return null;
            }
        }

        //Pasar esto a servicio del dominio.............................>>>> OKOK123 - P
        private void ObtenerModalidadPagoDeVenta(CondicionPago pCondicionPagoDeVenta, CondicionPago pCondicionPagoDefault, TipoPago pTipoPagoDeVenta,
                                            TipoPago pTipoPagoEfectivo, Cliente pCliente, TipoDocumento pTipoDocumento,
                                            ConfiguracionPuntoVenta pConfiguracionPuntoVenta, string pCodigoTipoDocumentoNotaCredito, decimal pTotalNacional,
                                            bool pEsVentaACuentaPorCobrar, decimal pSaldoDisponibleAdelanto)
        {
            switch (pTipoPagoDeVenta.CodigoTipoPago) //Credito
            {
                case VentaTipoPago.VentaValesCredito:
                    //Obtener Condicion de Pago a partir de entidad Cliente
                    if (pCondicionPagoDeVenta == null)
                    {
                        pCondicionPagoDeVenta = pCondicionPagoDefault; //Creado Parametro en Setup CondicionPagoDefault
                        if (pCondicionPagoDeVenta == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_CondicionPagoPorVentasAsociadoAVentaNoExiste);
                            //return null;
                        }
                    }

                    //*** Obtener tipo de documento Nota de Credito
                    if (pTipoDocumento.CodigoTipoDocumento != pCodigoTipoDocumentoNotaCredito)
                    {
                        // *** este tipo de moneda en verdad se trae desde Configuracion General 
                        if (pCliente.CodigoMoneda == pConfiguracionPuntoVenta.CodigoMonedaCaja)
                        {
                            //Verificar limite de credito : Excede limite de credito
                            if (pCliente.ValidarLimiteCredito(pTotalNacional) == true)
                            {
                                if (pCliente.DocumentosLibre != null)
                                {
                                    //Parte en que se Inicia el Grabado de Consumo de FP6  
                                    //Tiene una Opción más de que se le facture al Crédito - .....
                                    //// 'DOCUMENTOS LIBRES' que se consideran como venta consumo pago adelantado
                                    if (pCliente.DocumentosLibre.FirstOrDefault().TotalLibre >= pTotalNacional)
                                    {
                                        //Inserta registro en tabla : OP_DOCUMENTFREEDET
                                        //Actualizar Deuda del Cliente
                                        pCliente.ActualizarDeuda(pTotalNacional);

                                        //Adjuntar Cuentas por Cobrar si pago es al credito
                                        //Obtener Estado de Documento
                                        pEsVentaACuentaPorCobrar = true;
                                    }
                                    else
                                    {
                                        //Setear venta como Efectivo
                                        pTipoPagoDeVenta = pTipoPagoEfectivo;
                                        if (pTipoPagoDeVenta == null)
                                        {
                                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);
                                            //return null;
                                        }

                                        throw new ArgumentException(Mensajes.advertencia_ClienteExcedeLimiteCredito); //Comentado
                                    }
                                }
                                else
                                {
                                    //Setear venta como Efectivo
                                    pTipoPagoDeVenta = pTipoPagoEfectivo;
                                    if (pTipoPagoDeVenta == null)
                                    {
                                        LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);
                                        //return null;
                                    }

                                    throw new ArgumentException(Mensajes.advertencia_ClienteExcedeLimiteCredito); //Comentado
                                }
                            }
                            else
                            {
                                //Actualizar Deuda del Cliente
                                pCliente.ActualizarDeuda(pTotalNacional);

                                //Adjuntar Cuentas por Cobrar si pago es al credito
                                pEsVentaACuentaPorCobrar = true;
                            }
                        }
                    }
                    break;
                case VentaTipoPago.VentaContadoAdelantado:  //Adelantado

                    if (pCliente.ControlarSaldoDisponible == 1)
                    {
                        if (pTotalNacional > pSaldoDisponibleAdelanto)
                        {
                            //Excede el Saldo
                            pTipoPagoDeVenta = pTipoPagoEfectivo;
                            if (pTipoPagoDeVenta == null)
                            {
                                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TipoDePagoAsociadoAVentaNoExiste);
                                //return null;
                            }
                        }
                    }
                    break;
                case VentaTipoPago.VentaOtros:
                case VentaTipoPago.VentaTarjeta:
                    break;
            }
        }


        //Pasar esto a servicio del dominio.............................>>>> OKOK123 - P
        private static int MovimientoAlmacenIngresoOSalida(Guid pTipoDocumentoNotaCredId, TipoDocumento tipoDocumento,
                                                            TipoMovimientoAlmacen tipoMovAlmacenVentas)
        {
            var movAlmacenIngresoOSalida = tipoDocumento.Id == pTipoDocumentoNotaCredId ? 1 : tipoMovAlmacenVentas.IngresoOSalida;
            return movAlmacenIngresoOSalida;
        }


        public ResultadoServicio<VentaListadoDTO> BuscarVentasPorCliente(string pCodigoCliente)
        {
            //Obtenemos list de entidad Ventas
            var ventas = _IRepositorioVenta.ObtenerVentasPorCodigoCliente(pCodigoCliente);
            if (ventas != null && ventas.Any())
            {
                //retorna datos adaptador                
                return new ResultadoServicio<VentaListadoDTO>(7, Mensajes.advertencia_ConsultaVentasPorClienteExitosa,
                                    string.Empty, null, ventas.ProyectadoComoColeccion<VentaListadoDTO>());
            }
            else
                //no retorna
                return null;
        }


        Venta CrearNuevaVenta(VentaDTO pVentaDTO, Moneda pMoneda, ClaseTipoCambio pClaseTipoCambio,
                                Cliente pCliente, TipoDocumento pTipoDocumento, EstadoDocumento pEstadoDocumento,
                                Vendedor pVendedor, CondicionPago pCondicionPago, TipoPago pTipoPago,
                                ConfiguracionPuntoVenta pConfiguracionPuntoVenta, Almacen pAlmacen, TipoNegocio pTipoNegocio,
                                UsuarioSistema pUsuarioSistema, bool pEsVentaPagoAdelantado, bool pEsVentaACuentaPorCobrar,
                                TipoMovimientoAlmacen pTipoMovimientoAlmacen, int pMovAlmacenVentaIngresoOSalida, int pPermitirStockNegativo,
                                DateTime pFechaTipoDeCambio, string pCodigoClienteInterno, int pCantidadDecimalPrecio)
        {
            try
            {
                Venta nuevaVenta = VentaFactory.CrearVenta(pVentaDTO.NumeroDocumento, pVentaDTO.FechaDocumento, pVentaDTO.FechaProceso,
                                                pVentaDTO.Periodo, pVentaDTO.TotalNacional, pVentaDTO.TotalExtranjera,
                                                pVentaDTO.SubTotalNacional, pVentaDTO.SubTotalExtranjera, pVentaDTO.ImpuestoIgvNacional,
                                                pVentaDTO.ImpuestoIGVExtranjera, pVentaDTO.ImpuestoIscNacional, pVentaDTO.ImpuestoIscExtranjera,
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

                //Detalle de Venta
                if (pVentaDTO.VentaDetalles != null)
                {
                    foreach (var linea in pVentaDTO.VentaDetalles)
                    {
                        //Obtener Articulo y Precio
                        var articulo = _IRepositorioArticulo.ObtenerPorCodigo(linea.CodigoArticulo, pAlmacen.CodigoAlmacen);
                        if (articulo == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ArticuloAsociadoAVentaDetalleNoExiste);
                            return null;
                        }

                        //Obtener el precio real del articulo
                        decimal precioRealArticulo = 0;
                        bool enInventarioFisico = true; //Determinar a partir de tabla INVENTARIO FISICO
                        // decimal precioRealArticulo =
                        //             _IServicioAplicacionInventarios.ObtenerPrecioVentaDeArticulo(pCliente.Id,
                        //                 articulo.Id, pAlmacen.Id, pVentaDTO.FechaProceso, pCodigoClienteInterno, pCantidadDecimalPrecio);

                        var detVenta = nuevaVenta.AgregarNuevaVentaDetalle(linea.Secuencia, linea.NumeroTurno, linea.NumeroCara,
                                                linea.PorcentajeImpuestoIgv, linea.PorcentajeImpuestoIsc, linea.TotalNacional,
                                                linea.TotalExtranjera, linea.ImpuestoNacional, linea.ImpuestoExtranjera,
                                                (int)linea.PorcentajeDescuentoPrimero, (int)linea.TotalDescuentoNacional, (int)linea.TotalDescuentoExtranjera,
                                                linea.Precio, precioRealArticulo, articulo.DescripcionArticulo, 
                                                linea.Cantidad, linea.EsFormula, linea.CodigoArticulo, 
                                                linea.CodigoArticuloAlterno, articulo.EsInventariable, enInventarioFisico);

                        //*** EN EL EJEMPLO NLAYER NO ES NECESARIO EstablecerMonedaParaDetalleVenta
                        //Actualizacion Saldos Stock Articulo...Refactoriza??????
                        articulo.RecalcularStock(pPermitirStockNegativo, pMovAlmacenVentaIngresoOSalida, linea.Cantidad);

                        _ListaArticulosStockActualizado.Add(articulo);

                        //Registrar Movimientos Inventarios 
                        CrearMovimientoAlmacen(pVentaDTO, pAlmacen, articulo,
                                                pTipoMovimientoAlmacen, pTipoDocumento, pMovAlmacenVentaIngresoOSalida,
                                                pFechaTipoDeCambio, pConfiguracionPuntoVenta);

                    }
                }

                //pago con tarjeta
                if (pVentaDTO.VentaConTarjetas != null)
                {
                    foreach (var vtaConTarjeta in pVentaDTO.VentaConTarjetas)
                    {
                        var moneda = _IRepositorioMoneda.ObtenerPorCodigo(vtaConTarjeta.CodigoMoneda);
                        if (moneda == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_MonedaAsociadoAPagoVentaConTarjetaNoExiste);
                            return null;
                        }

                        var tarjeta = _IRepositorioTarjeta.ObtenerPorCodigo(vtaConTarjeta.CodigoTarjeta);
                        if (tarjeta == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TarjetaAsociadoAPagoVentaConTarjetaNoExiste);
                            return null;
                        }

                        var ventaConTarjeta = nuevaVenta.AgregarNuevaVentaConTarjeta(vtaConTarjeta.Secuencia, vtaConTarjeta.NumeroTarjeta, vtaConTarjeta.TotalTarjetaNacional, 
                                        vtaConTarjeta.TotalTarjetaExtranjera, vtaConTarjeta.CodigoMoneda, vtaConTarjeta.CodigoTarjeta);
                    }
                }

                //Pago Con Vale
                if (pVentaDTO.VentaConVales != null)
                {
                    foreach (var vtaConVale in pVentaDTO.VentaConVales)
                    {
                        var cliente = pCliente; //Si no tiene otra funcionalidad dejar con la variable, si no invocar a repositorio
                        var ventaConVale = nuevaVenta.AgregarNuevaVentaConVale(vtaConVale.NumeroVale, vtaConVale.MontoVale);
                    }
                }

                //Registro Documento Anticipado
                if (pEsVentaPagoAdelantado)
                    nuevaVenta.AgregarNuevoDocumentoAnticipado();

                //Registro Cuentas por Cobrar
                if (pEsVentaACuentaPorCobrar)
                {
                    var fechaVencimiento = _IServicioDominioCuentaPorCobrar.ObtenerFechaVenceDocumentoCuentaPorCobrar(pVentaDTO.FechaDocumento,
                                                                                                                pCliente, pCondicionPago);

                    nuevaVenta.AgregarNuevaCuentaPorCobrar(0, fechaVencimiento, 0,
                                            0, 0, 0,
                                            pCliente.DiasDeGracia, 0, EnumEstadoDocumento.CodigoEstadoDocumentoPendiente,
                                            pCliente.CodigoDiaDePago, string.Empty);
                }


                //Calcular el total de la venta
                nuevaVenta.CalcularTotalVenta();

                return nuevaVenta;
            }
            catch (Exception ex)
            {
                LogFactory.CrearLog().LogWarning(ex.Message);
                throw;
            }
        }


        //Registrar Movimientos Inventarios 
        void CrearMovimientoAlmacen(VentaDTO pVentaDTO, Almacen pAlmacen, Articulo pArticulo,
                                    TipoMovimientoAlmacen pTipoMovimientoAlmacen, TipoDocumento pTipoDocumento, int pMovAlmacenVentaIngresoOSalida,
                                    DateTime pFechaTipoDeCambio, ConfiguracionPuntoVenta pConfiguracionPuntoVenta)
        {
            if (pArticulo.EsInventariable)
            {
                foreach (var linea in pVentaDTO.VentaDetalles)
                {
                    MovimientoAlmacen movAlmacen = new MovimientoAlmacen();
                    movAlmacen.CorrelativoMovimiento = pConfiguracionPuntoVenta.CorrelativoMovimientoAlmacenPorVenta.ToString();
                    movAlmacen.FechaDocumento = pVentaDTO.FechaDocumento;
                    movAlmacen.FechaProceso = pVentaDTO.FechaProceso;
                    movAlmacen.MontoTipoDeCambio = pVentaDTO.TipoCambio;
                    movAlmacen.FechaTipoDeCambio = pFechaTipoDeCambio;
                    movAlmacen.Periodo = pVentaDTO.Periodo;
                    movAlmacen.FlagEntradaSalida = pMovAlmacenVentaIngresoOSalida;
                    movAlmacen.Cantidad = linea.Cantidad;
                    movAlmacen.CostoReposicionExtranjera = pArticulo.ArticuloDetalle.CostoReposicionExtranjera;
                    movAlmacen.CostoReposicionNacional = pArticulo.ArticuloDetalle.CostoReposicionNacional;
                    movAlmacen.EsArticuloFormula = pArticulo.EsFormula;
                    movAlmacen.Precio = linea.PrecioVenta; //el precio al que se le aplico los descuentos                        
                    movAlmacen.DocumentoReferencia = pVentaDTO.NumeroDocumento.ToString();
                    movAlmacen.EnInventarioFisico = pArticulo.InventariosFisicos != null ? pArticulo.InventariosFisicos.Count : 0; //Crear articulo en inv. fisico

                    movAlmacen.EstablecerAlmacenDeMovimientoAlmacen(pAlmacen);
                    movAlmacen.EstablecerArticuloDeMovimientoAlmacen(pArticulo);
                    movAlmacen.EstablecerTipoMovimientoAlmacenDeMovimientoAlmacen(pTipoMovimientoAlmacen);
                    movAlmacen.EstablecerTipoDocumentoDeMovimientoAlmacen(pTipoDocumento);

                    movAlmacen.GenerarNuevaIdentidad();

                    _ListaNuevosMovimientoAlmacen.Add(movAlmacen);
                }
            }
        }


        decimal ObtenerSaldoVentaAdelantado(string pCodigoTipoPago, string pCodigoCliente, string pCodigoAlmacen,
                                            string pCodigoTipoDocumento, DateTime pFechaProcesoVentas)
        {
            decimal saldoIniPagoAdelantado = 0;
            decimal saldoFinPagoAdelantado = 0;

            //Pago
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

            //Pagos anticipados
            //AR_ANTICPAYMENT

            //Saldo Final
            _IServicioDominioVentas.CalcularSaldoVentaAdelantada(saldoIniPagoAdelantado, saldoFinPagoAdelantado, pagoInicial, consumos);

            return saldoFinPagoAdelantado;
        }

        //Pasar esto a servicio del dominio.............................>>>>  OKOK123 - P
        private static void CalcularSaldoVentaAdelantada(ref decimal saldoIniPagoAdelantado, ref decimal saldoFinPagoAdelantado,
                                                            IEnumerable<Venta> pagoInicial, IEnumerable<Venta> consumos)
        {
            saldoIniPagoAdelantado = pagoInicial.Single().TotalNacional;
            if (consumos.Count() != 0)
            {
                foreach (var consumosDePagoAdelantado in consumos)
                {
                    saldoFinPagoAdelantado = saldoIniPagoAdelantado - consumosDePagoAdelantado.TotalNacional;
                }
            }
            else
            {
                saldoFinPagoAdelantado = saldoIniPagoAdelantado;
            }
        }


        //Pasar esto a servicio del dominio.............................>>>> OKOK123 - P
        DateTime ObtenerFechaVenceDocumentoCuentaPorCobrar(DateTime pFechaDocumentoVenta, Cliente pCliente, CondicionPago pCondicionPago)
        {

            Dictionary<int, int> DiccionarioDiasSemanaPago = new Dictionary<int, int>();
            DateTime fechaDeVenceDocCC = new DateTime();
            int cantidadDiasDelMes = 0;
            int diaPivot = 0;

            //Si es que no encontro DiaPagID para el cliente
            if ((pCondicionPago.DiasPago < 0 || pCondicionPago.DiasPago == 0)
                || (string.IsNullOrEmpty(pCliente.CodigoDiaDePago)))
            {
                fechaDeVenceDocCC = pFechaDocumentoVenta.AddDays(pCondicionPago.DiasPago < 0 ? 0 : pCondicionPago.DiasPago);
            }
            else
            {
                //Verifica si tiene marcado el check de dias de Semana
                fechaDeVenceDocCC = pFechaDocumentoVenta.AddDays(pCondicionPago.DiasPago);

                if (pCliente.DiaDePago.EstadoSemana == -1)
                {
                    // int diaSemana = 0;
                    int diasTotal = 0;
                    if (pCliente.DiaDePago.D1Lunes + pCliente.DiaDePago.D2Martes + pCliente.DiaDePago.D3Miercoles +
                        pCliente.DiaDePago.D4Jueves + pCliente.DiaDePago.D5Viernes + pCliente.DiaDePago.D6Sabado +
                        pCliente.DiaDePago.D7Domingo == 0)
                    {
                        return fechaDeVenceDocCC;
                    }

                    if (pCliente.DiaDePago.D7Domingo == -1) { DiccionarioDiasSemanaPago.Add(1, 0); }
                    if (pCliente.DiaDePago.D1Lunes == -1) { DiccionarioDiasSemanaPago.Add(2, 1); }
                    if (pCliente.DiaDePago.D2Martes == -1) { DiccionarioDiasSemanaPago.Add(3, 2); }
                    if (pCliente.DiaDePago.D3Miercoles == -1) { DiccionarioDiasSemanaPago.Add(4, 3); }
                    if (pCliente.DiaDePago.D4Jueves == -1) { DiccionarioDiasSemanaPago.Add(5, 4); }
                    if (pCliente.DiaDePago.D5Viernes == -1) { DiccionarioDiasSemanaPago.Add(6, 5); }
                    if (pCliente.DiaDePago.D6Sabado == -1) { DiccionarioDiasSemanaPago.Add(7, 6); }

                    //1er Caso, si el día de vencimiento conincide con el día de pago
                    int diaSemanaDeFechaVenceDocCC = Convert.ToInt16(fechaDeVenceDocCC.DayOfWeek) + 1; //por q los dias DayOfWeek empiezan desde Domingo = 0

                    var verificaDiaSemana = DiccionarioDiasSemanaPago.Where(k => k.Key == diaSemanaDeFechaVenceDocCC);
                    if (verificaDiaSemana.Count() != 0)
                    {
                        return fechaDeVenceDocCC;
                    }

                    //Buscando a la derecha
                    //diasTotal = Convert.ToInt16(DiccionarioDiasSemanaPago.Keys.Min(x => x > diaSemanaDeFechaVenceDocCC));
                    var selMenorDiaSemana = from dia in DiccionarioDiasSemanaPago
                                            where dia.Key > diaSemanaDeFechaVenceDocCC
                                            select dia;
                    var diaMin = new { valorMin = selMenorDiaSemana.Min(k => k.Key) };
                    diasTotal = diaMin.valorMin;

                    //Buscando a la Izquierda
                    if (diasTotal < 0 || diasTotal == 0)
                    {
                        diasTotal = DiccionarioDiasSemanaPago.Keys.Min();

                        fechaDeVenceDocCC =
                            fechaDeVenceDocCC.AddDays(diasTotal + 7 - diaSemanaDeFechaVenceDocCC);
                    }
                    else
                    {
                        fechaDeVenceDocCC = fechaDeVenceDocCC.AddDays(diasTotal - diaSemanaDeFechaVenceDocCC);
                    }
                }
                else
                {
                    /* Para Cuando se implemente dias de pago  >>>>*/
                    //CombinaDia1,CombinaDia2,CombinaDia3,CombinaDia4 deben de ser mayores que cero y
                    //menores<28,los demas dias deben ser considerados fin de mes.
                    if (pCliente.DiaDePago.CombinaDia1 + pCliente.DiaDePago.CombinaDia2 + pCliente.DiaDePago.CombinaDia3 == 0 &&
                        pCliente.DiaDePago.CombinaDia4 == 0)
                    {
                        return fechaDeVenceDocCC;
                    }

                    DiccionarioDiasSemanaPago.Add(1, (int)pCliente.DiaDePago.CombinaDia1);
                    DiccionarioDiasSemanaPago.Add(2, (int)pCliente.DiaDePago.CombinaDia2);
                    DiccionarioDiasSemanaPago.Add(3, (int)pCliente.DiaDePago.CombinaDia3);
                    if (pCliente.DiaDePago.CombinaDia4 == -1) { DiccionarioDiasSemanaPago.Add(4, 99); }

                    //1er Caso, si el d¡a de vencimiento coincide con el d¡a de pago
                    var dia = fechaDeVenceDocCC.Day;
                    var mes = fechaDeVenceDocCC.Month;
                    var año = fechaDeVenceDocCC.Year;

                    //Calcular la cantidad de dias del mes actual del vencimiento
                    switch (mes)
                    {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            cantidadDiasDelMes = 31;
                            break;
                        default:
                            if (mes == 2)
                            {
                                if (año % 4 == 0)
                                {
                                    cantidadDiasDelMes = 29;
                                }
                                else
                                {
                                    cantidadDiasDelMes = 28;
                                }
                            }
                            else
                            {
                                cantidadDiasDelMes = 30;
                            }
                            break;
                    }

                    var diaVencimiento = DiccionarioDiasSemanaPago.Where(x => x.Value == dia && x.Value != 99);
                    if (diaVencimiento.Count() != 0)
                    {
                        return fechaDeVenceDocCC;
                    }
                    //FIN 1er Caso, si el dia de vencimiento conincide con un dia de pago

                    //Buscando el min valor, mayor que dia de vencimiento actual
                    ///Buscando a la derecha
                    diaPivot = Convert.ToInt16(DiccionarioDiasSemanaPago.Min(x => x.Value > dia));
                    ///Fin de Buscando a la Derecha

                    if (diaPivot != 0)
                    {
                        //si diapivot=99, entoces vence fin de mes actual
                        if (diaPivot != 99)
                        {
                            fechaDeVenceDocCC = fechaDeVenceDocCC.AddDays(diaPivot - dia);
                        }
                        else
                        {
                            fechaDeVenceDocCC = fechaDeVenceDocCC.AddDays(cantidadDiasDelMes - dia);
                        }
                    }
                    else
                    {
                        //Buscando a la Izquierda
                        diaPivot = Convert.ToInt16(DiccionarioDiasSemanaPago.Values.Min());

                        fechaDeVenceDocCC = fechaDeVenceDocCC.AddMonths(1);
                        fechaDeVenceDocCC = fechaDeVenceDocCC.AddDays(diaPivot - dia);
                    }
                }
            }

            return fechaDeVenceDocCC;
        }


        bool ExisteDocumentoDeVenta(string pCodigoTipoDocumento, string pNuevoCorrelativoDocumento,
                                    string pCodigoAlmacen)
        {
            try
            {
                ////conventir pNuevoCorrelativoDocumento de String a Decimal
                //Obtener correlativo
                string actualCorrelativoDocumento = _IRepositorioVenta.ObtenerNumeroDocumentoVenta(pCodigoTipoDocumento,
                                                                        pNuevoCorrelativoDocumento, pCodigoAlmacen);

                return _IServicioDominioVentas.ExisteComprobanteDePagoDeVenta(pNuevoCorrelativoDocumento, actualCorrelativoDocumento);
            }
            catch (Exception ex)
            {
                LogFactory.CrearLog().LogWarning(ex.Message);
                throw;
            }
        }


        //Pasar esto a servicio del dominio.............................>>>> OKOK123 - P
        private static bool ExisteComprobanteDePagoDeVenta(decimal pNuevoCorrelativoDocumento, ref bool existeDoc, decimal correlEncontrado)
        {
            if (correlEncontrado <= 0)
            {
                //Este mensaje es necesario???
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_NoSeObtuvoVentaConNumeroDeDocumentoBuscado);
                return false;
            }

            //devolver verdadero o falso
            if (pNuevoCorrelativoDocumento == correlEncontrado)
            {
                existeDoc = true;
            }

            return existeDoc;
        }


        void GrabarVenta(Venta pVenta)
        {
            //Persistir Venta
            // var validarEntidad = ValidadorEntidadFactory.CrearValidador();

            // if (validarEntidad.EsValido(pVenta))
            // {
            _IRepositorioVenta.Agregar(pVenta);
            // _IRepositorioVenta.UnidadTrabajo.Commit();
            // }
            // else
            //     throw new AplicacionExcepcionErrorValidacion(validarEntidad.RecibeMensajesInvalidos(pVenta));

        }

        void ActualizarTipoDocumento(TipoDocumento pTipoDocumentoActual, string pCodigoAlmacen, string pNumeroSerie)
        {
            //Persistir TipoDocumento            
            // var validarEntidad = ValidadorEntidadFactory.CrearValidador();

            // if (validarEntidad.EsValido(pTipoDocumentoActual))
            // {
            var tipoDocumentoAPersistido = _IRepositorioTipoDocumento.ObtenerPorCodigo(pTipoDocumentoActual.CodigoTipoDocumento);
            if (tipoDocumentoAPersistido != null)
            {
                _IRepositorioTipoDocumento.ActualizarCorrelativoDocumento(pTipoDocumentoActual,pCodigoAlmacen, pNumeroSerie ); //CAMBIAR POR ACTUALIZAR
                                                                            // _IRepositorioTipoDocumento.UnidadTrabajo.Commit();
            }
            else
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_NoSeObtuvoResultadoDeConsultaTipoDocumento);
            // }
            // else
            //     throw new AplicacionExcepcionErrorValidacion(validarEntidad.RecibeMensajesInvalidos(pTipoDocumentoActual));
        }

        void ActualizarArticulo(Articulo pArticuloActual, string pCodigoArticulo)
        {
            //Persistir Articulo 
            // var validarEntidad = ValidadorEntidadFactory.CrearValidador();
            // if (validarEntidad.EsValido(pArticuloActual))
            // {
            var articuloAPersistido = _IRepositorioArticulo.ObtenerPorCodigo(pArticuloActual.CodigoArticulo, pCodigoArticulo);
            if (articuloAPersistido != null)
            {
                _IRepositorioArticulo.Modificar(pArticuloActual);
                // _IRepositorioArticulo.UnidadTrabajo.Commit();
            }
            else
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_NoSeObtuvoResultadoDeConsultaArticuloAPersistir);
            // }
            // else
            //     throw new AplicacionExcepcionErrorValidacion(validarEntidad.RecibeMensajesInvalidos(pArticuloActual));
        }


        void GrabarMovimientoAlmacen(MovimientoAlmacen pMovimientoAlmacen)
        {
            //Persistir MovimientoAlmacen 
            // var validarEntidad = ValidadorEntidadFactory.CrearValidador();
            // if (validarEntidad.EsValido(pMovimientoAlmacen))
            // {
            _IRepositorioMovimientoAlmacen.Agregar(pMovimientoAlmacen);
            // _IRepositorioMovimientoAlmacen.UnidadTrabajo.Commit();
            // }
            // else
            //     throw new AplicacionExcepcionErrorValidacion(validarEntidad.RecibeMensajesInvalidos(pMovimientoAlmacen));
        }


        void GrabarTransaccionDeVenta(Venta pVenta, TipoDocumento pTipoDocumento,
                                List<Articulo> pArticulosStockActualizado, List<MovimientoAlmacen> pMovimientoAlmacen)
        {
            try
            {
                //En proceso de Unidad de Trabajo, Persiste todo la transaccion de venta
                if (pArticulosStockActualizado == null || !pArticulosStockActualizado.Any())
                    throw new ArgumentException(Mensajes.advertencia_NoSeObtuvoParametroArticulosStockActualizado);

                if (pMovimientoAlmacen == null || !pMovimientoAlmacen.Any())
                    throw new ArgumentException(Mensajes.advertencia_NoSeObtuvoParametroMovimientosDeAlmacen);

                //Uso de transaccion
                using (TransactionScope ambito = new TransactionScope())
                {
                    //Persistir Venta
                    GrabarVenta(pVenta);

                    //Persistir Articulo - Detalles de Articulo (actualizacion Stock
                    foreach (var articulo in pArticulosStockActualizado)
                    {
                        ActualizarArticulo(articulo, pVenta.CodigoAlmacen);
                    }

                    //Persistir Movimientos de Almacen
                    foreach (var movAlmacen in pMovimientoAlmacen)
                    {
                        GrabarMovimientoAlmacen(movAlmacen);
                    }

                    //Actualizar correlativo de documento
                    ActualizarTipoDocumento(pTipoDocumento, pVenta.CodigoAlmacen, pVenta.NumeroDocumento.ToString().PadRight(7));

                    //Completar transaccion
                    ambito.Complete();

                }
            }
            catch (Exception ex)
            {
                string cadenaExcepcion = ex.Message +
                    ".Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";
                LogFactory.CrearLog().LogWarning(cadenaExcepcion);
                throw;
            }

        }


        private void IdentificarTipoPagoVenta(VentaDTO pVentaDTO)
        {
            //Determinamos el Tipo de pago
            //Verificar si es solo pago en efectivo en doble moneda
            //y/o existe pago con tarjeta
            //o Ambos

            //Solo si es tipo de pago venta adelantado
            if (pVentaDTO.TipoPagoCodigoTipoPago == VentaTipoPago.VentaContadoAdelantado) { return; } //antes 14

            if (pVentaDTO.TotalEfectivoNacional + pVentaDTO.TotalEfectivoExtranjera > 0)
            {
                pVentaDTO.TipoPagoCodigoTipoPago = VentaTipoPago.VentaEfectivo;      //Efectivo
            }

            if (pVentaDTO.VentaConTarjetas != null)
            {
                if (pVentaDTO.VentaConTarjetas.Count > 0)
                {
                    if (pVentaDTO.TipoPagoCodigoTipoPago == VentaTipoPago.VentaEfectivo) //antes : 1
                    {
                        pVentaDTO.TipoPagoCodigoTipoPago = VentaTipoPago.VentaOtros;  //Otros; antes : 0
                    }
                    else
                    {
                        pVentaDTO.TipoPagoCodigoTipoPago = VentaTipoPago.VentaTarjeta; //Tarjetas; antes : 2
                    }
                }
            }
        }

    }
}