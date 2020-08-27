using System;
using System.Linq;
using System.Transactions;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.Transversales.Log;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.GestionPedidos
{
    public class ServicioAplicacionPedidos : IServicioAplicacionPedidos
    {
        private IRepositorioPedidoEESS _IRepositorioPedidoEESS;
        private IRepositorioPedidoRetail _IRepositorioPedidoRetail;
        private IRepositorioArticulo _IRepositorioArticulo;
        private IRepositorioMoneda _IRepositorioMoneda;
        private IRepositorioTarjeta _IRepositorioTarjeta;

        public ServicioAplicacionPedidos(IRepositorioPedidoEESS pIRepositorioPedidoEESS, IRepositorioPedidoRetail pIRepositorioPedidoRetail,
                                    IRepositorioArticulo pIRepositorioArticulo, IRepositorioMoneda pIRepositorioMoneda,
                                    IRepositorioTarjeta pIRepositorioTarjeta)
        {
            if (pIRepositorioPedidoEESS == null)
                throw new ArgumentNullException("pIRepositorioPedidoEESS Nulo en ServicioAplicacionPedidos");

            if (pIRepositorioPedidoRetail == null)
                throw new ArgumentNullException("pIRepositorioPedidoRetail Nulo en ServicioAplicacionPedidos");

            if (pIRepositorioArticulo == null)
                throw new ArgumentNullException("pIRepositorioArticulo Nulo en ServicioAplicacionPedidos");

            if (pIRepositorioMoneda == null)
                throw new ArgumentNullException("pIRepositorioArticulo Nulo en ServicioAplicacionPedidos");                

            if (pIRepositorioTarjeta == null)
                throw new ArgumentNullException("pIRepositorioArticulo Nulo en ServicioAplicacionPedidos");                

            _IRepositorioPedidoEESS = pIRepositorioPedidoEESS;
            _IRepositorioPedidoRetail = pIRepositorioPedidoRetail;
            _IRepositorioArticulo = pIRepositorioArticulo;
            _IRepositorioMoneda = pIRepositorioMoneda;
            _IRepositorioTarjeta = pIRepositorioTarjeta;
        }
        
        public ResultadoServicio<ResultadoPedidoEESSGrabadoDTO> AgregarNuevoPedidoEESS(PedidoEESSDTO pPedidoEESSDTO)
        {
            var pedidoEESSExistente =_IRepositorioPedidoEESS.ObtenerPorNumeroPedido(pPedidoEESSDTO.Correlativo);
            if (pedidoEESSExistente != null)
            {                
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_PedidoEESSYaRegistrado);
                throw new ArgumentException(Mensajes.advertencia_PedidoEESSYaRegistrado);
            }  

            var nuevoPedidoEESS = CrearNuevoPedidoEESS(pPedidoEESSDTO);

            GrabarTransaccionNuevoPedidoEESS(nuevoPedidoEESS);
                                                        
            if (nuevoPedidoEESS != null)
            {
                return new ResultadoServicio<ResultadoPedidoEESSGrabadoDTO>(7, Mensajes.advertencia_ExitosaCreacionNuevoPedidoEESS,
                        string.Empty, nuevoPedidoEESS.ProyectadoComo<ResultadoPedidoEESSGrabadoDTO>(), null);
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_FalloCreacionNuevoPedidoEESS);
                return new ResultadoServicio<ResultadoPedidoEESSGrabadoDTO>(6, Mensajes.advertencia_FalloCreacionNuevoPedidoEESS,
                                string.Empty, nuevoPedidoEESS.ProyectadoComo<ResultadoPedidoEESSGrabadoDTO>(), null);
            }
        }

        public ResultadoServicio<ResultadoPedidoRetailGrabadoDTO> AgregarNuevoPedidoRetail(PedidoRetailDTO pPedidoRetailDTO)
        {
            var pedidoRetailExistente =_IRepositorioPedidoRetail.ObtenerPorNumeroPedido(pPedidoRetailDTO.Correlativo);
            if (pedidoRetailExistente != null)
            {                
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_PedidoRetailYaRegistrado);
                throw new ArgumentException(Mensajes.advertencia_PedidoRetailYaRegistrado);
            }  

            var nuevoPedidoRetail = CrearNuevoPedidoRetail(pPedidoRetailDTO);

            GrabarTransaccionNuevoPedidoRetail(nuevoPedidoRetail);
                                                        
            if (nuevoPedidoRetail != null)
            {
                return new ResultadoServicio<ResultadoPedidoRetailGrabadoDTO>(7, Mensajes.advertencia_ExitosaCreacionNuevoPedidoRetail,
                        string.Empty, nuevoPedidoRetail.ProyectadoComo<ResultadoPedidoRetailGrabadoDTO>(), null);
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_FalloCreacionNuevoPedidoRetail);
                return new ResultadoServicio<ResultadoPedidoRetailGrabadoDTO>(6, Mensajes.advertencia_FalloCreacionNuevoPedidoRetail,
                                string.Empty, nuevoPedidoRetail.ProyectadoComo<ResultadoPedidoRetailGrabadoDTO>(), null);
            }
        }

        public ResultadoServicio<PedidoEESSListadoDTO> BuscarPedidoEESSPorPuntoDeVenta(string pCodigoPuntoDeVenta)
        {
            var pedidosEESS = _IRepositorioPedidoEESS.ObtenerTodos(pCodigoPuntoDeVenta);
            if (pedidosEESS != null && pedidosEESS.Any())
            {          
                return new ResultadoServicio<PedidoEESSListadoDTO>(7, Mensajes.advertencia_ConsultaPedidosEESSPorPuntoDeVentaExitosa,
                                    string.Empty, null, pedidosEESS.ProyectadoComoColeccion<PedidoEESSListadoDTO>());
            }
            else                
                return null;
        }

        public ResultadoServicio<PedidoRetailListadoDTO> BuscarPedidoRetailPorPuntoDeVenta(string pCodigoPuntoDeVenta)
        {
            var pedidosRetail = _IRepositorioPedidoRetail.ObtenerTodos(pCodigoPuntoDeVenta);
            if (pedidosRetail != null && pedidosRetail.Any())
            {          
                return new ResultadoServicio<PedidoRetailListadoDTO>(7, Mensajes.advertencia_ConsultaPedidosRetailPorPuntoDeVentaExitosa,
                                    string.Empty, null, pedidosRetail.ProyectadoComoColeccion<PedidoRetailListadoDTO>());
            }
            else                
                return null;
        }


        public ResultadoServicio<PedidoEESSDTO> BuscarPedidoEESSPorNumero(int pCorrelativo)
        {
            var pedidoEESS = _IRepositorioPedidoEESS.ObtenerPorNumeroPedido(pCorrelativo);
            if (pedidoEESS != null)
            {          
                return new ResultadoServicio<PedidoEESSDTO>(7, Mensajes.advertencia_ConsultaPedidoEESSPorNumeroPedidoExitosa,
                                                                string.Empty, pedidoEESS.ProyectadoComo<PedidoEESSDTO>(), null);
            }
            else                
                return null;
        }

        public ResultadoServicio<PedidoRetailDTO> BuscarPedidoRetailPorNumero(int pCorrelativo)
        {
            var pedidoRetail = _IRepositorioPedidoRetail.ObtenerPorNumeroPedido(pCorrelativo);
            if (pedidoRetail != null)
            {          
                return new ResultadoServicio<PedidoRetailDTO>(7, Mensajes.advertencia_ConsultaPedidoRetailPorNumeroPedidoExitosa,
                                                                string.Empty, pedidoRetail.ProyectadoComo<PedidoRetailDTO>(), null);
            }
            else                
                return null;
        }
        

        PedidoRetail CrearNuevoPedidoRetail(PedidoRetailDTO pPedidoRetailDTO)
        {
            try
            {
                PedidoRetail nuevoPedido = PedidoRetailFactory.CrearPedidoRetail(pPedidoRetailDTO.Correlativo, pPedidoRetailDTO.NumeroDocumento, pPedidoRetailDTO.AfectaInventario,    
                                    pPedidoRetailDTO.FechaDocumento, pPedidoRetailDTO.FechaProceso, pPedidoRetailDTO.Periodo,
                                    pPedidoRetailDTO.TotalNacional, pPedidoRetailDTO.TotalExtranjera, pPedidoRetailDTO.SubTotalNacional,
                                    pPedidoRetailDTO.SubTotalExtranjera, pPedidoRetailDTO.ImpuestoIgvNacional, pPedidoRetailDTO.ImpuestoIgvExtranjera,
                                    pPedidoRetailDTO.ImpuestoIscNacional, pPedidoRetailDTO.ImpuestoIscExtranjera, pPedidoRetailDTO.TotalNoAfectoNacional,
                                    pPedidoRetailDTO.TotalNoAfectoExtranjera, pPedidoRetailDTO.PorcentajeDescuentoPrimero, pPedidoRetailDTO.PorcentajeDescuentoSegundo,
                                    pPedidoRetailDTO.TotalDescuentoNacional, pPedidoRetailDTO.TotalDescuentoExtranjera, pPedidoRetailDTO.TotalVueltoNacional,
                                    pPedidoRetailDTO.TotalVueltoExtranjera, pPedidoRetailDTO.TotalEfectivoNacional, pPedidoRetailDTO.TotalEfectivoExtranjera,
                                    pPedidoRetailDTO.RucCliente, pPedidoRetailDTO.NombreCompletoCliente, pPedidoRetailDTO.DireccionCliente,
                                    pPedidoRetailDTO.Placa, pPedidoRetailDTO.NumeroVale, pPedidoRetailDTO.TipoCambio,
                                    pPedidoRetailDTO.NumeroPuntos, pPedidoRetailDTO.Kilometraje, pPedidoRetailDTO.TransaccionPendiente,
                                    pPedidoRetailDTO.TipoVenta, pPedidoRetailDTO.TransaccionProcesada, pPedidoRetailDTO.AplicaDescuentoCupon,
                                    pPedidoRetailDTO.CentroDeCosto, pPedidoRetailDTO.CodigoTipoDocumento, pPedidoRetailDTO.CodigoTipoPago,
                                    pPedidoRetailDTO.CodigoAlmacen, pPedidoRetailDTO.CodigoMoneda, pPedidoRetailDTO.CodigoCondicionPago,
                                    pPedidoRetailDTO.CodigoVendedor, pPedidoRetailDTO.CodigoUsuarioDeSistema, pPedidoRetailDTO.CodigoImpuestoIgv,
                                    pPedidoRetailDTO.CodigoImpuestoIsc, pPedidoRetailDTO.CodigoCliente, pPedidoRetailDTO.CodigoClaseTipoCambio,
                                    pPedidoRetailDTO.CodigoTarjetaPromocion, pPedidoRetailDTO.CodigoPuntoDeVenta, pPedidoRetailDTO.CodigoTipoNegocio);

                //Detalle de Pedido
                if (pPedidoRetailDTO.PedidoRetailDetalles != null && pPedidoRetailDTO.PedidoRetailDetalles.Any())
                {
                    foreach (var pedidoDetalle in pPedidoRetailDTO.PedidoRetailDetalles)
                    {
                        //Obtener Articulo y Precio
                        var articulo = _IRepositorioArticulo.ObtenerPorCodigo(pedidoDetalle.CodigoArticulo, pedidoDetalle.CodigoAlmacen);
                        if (articulo == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ArticuloAsociadoAVentaDetalleNoExiste);
                            throw new ArgumentException(Mensajes.advertencia_ArticuloAsociadoAVentaDetalleNoExiste); 
                        }

                        var detalleDePedido = nuevoPedido.AgregarNuevoPedidoRetailDetalle(pedidoDetalle.Secuencia, pedidoDetalle.NumeroTurno, pedidoDetalle.PorcentajeImpuestoIgv, 
                                                pedidoDetalle.PorcentajeImpuestoIsc, pedidoDetalle.TotalNacional, pedidoDetalle.TotalExtranjera, 
                                                pedidoDetalle.ImpuestoNacional, pedidoDetalle.ImpuestoExtranjera,pedidoDetalle.EsInventariable, 
                                                pedidoDetalle.EnInventarioFisico, pedidoDetalle.Precio,pedidoDetalle.PrecioVenta, 
                                                pedidoDetalle.CostoEstandarNacional, pedidoDetalle.CostoEstandarExtranjera,pedidoDetalle.CodigoArticuloAlterno, 
                                                pedidoDetalle.DescripcionArticulo, pedidoDetalle.Cantidad,pedidoDetalle.EsFormula, 
                                                pedidoDetalle.NumeroPeaje, pedidoDetalle.CodigoArticulo, pedidoDetalle.CodigoUnidadDeMedida);
                    }
                }

                //pago con tarjeta
                if (pPedidoRetailDTO.PedidoRetailConTarjetas != null && pPedidoRetailDTO.PedidoRetailConTarjetas.Any())
                {
                    foreach (var pedidoConTarjeta in pPedidoRetailDTO.PedidoRetailConTarjetas)
                    {
                        var moneda = _IRepositorioMoneda.ObtenerPorCodigo(pedidoConTarjeta.CodigoMoneda);
                        if (moneda == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_MonedaAsociadoAPagoVentaConTarjetaNoExiste);
                            throw new ArgumentException(Mensajes.advertencia_MonedaAsociadoAPagoVentaConTarjetaNoExiste); 
                        }

                        var tarjeta = _IRepositorioTarjeta.ObtenerPorCodigo(pedidoConTarjeta.CodigoTarjeta);
                        if (tarjeta == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_TarjetaAsociadoAPagoVentaConTarjetaNoExiste);
                            throw new ArgumentException(Mensajes.advertencia_TarjetaAsociadoAPagoVentaConTarjetaNoExiste); 
                        }

                        var pedidoPagaTarjeta = nuevoPedido.AgregarNuevoPedidoRetailConTarjeta(pedidoConTarjeta.Secuencia, pedidoConTarjeta.NumeroTarjeta, pedidoConTarjeta.TotalTarjetaNacional,
                                                            pedidoConTarjeta.TotalTarjetaExtranjera, pedidoConTarjeta.EsTransaccionPinPad, pedidoConTarjeta.TipoTarjeta,
                                                            pedidoConTarjeta.DNIAsociadoATarjeta, pedidoConTarjeta.DescripcionTarjeta, pedidoConTarjeta.CodigoTarjeta);
                    }
                }

                //Pago Con Vale
                if (pPedidoRetailDTO.PedidoRetailConVales != null && pPedidoRetailDTO.PedidoRetailConVales.Any())
                {
                    foreach (var pedidoConVale in pPedidoRetailDTO.PedidoRetailConVales)
                    {
                        var pedidoPagaVale = nuevoPedido.AgregarNuevoPedidoRetailConVale(pedidoConVale.NumeroVale);
                    }
                }

                return nuevoPedido;
            }
            catch (Exception ex)
            {
                string detallesAsicionales = string.Empty;                
                string cadenaExcepcion = ex.Message;

                if(ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";                        
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);                
                throw;
            }
        }


        PedidoEESS CrearNuevoPedidoEESS(PedidoEESSDTO pPedidoEESSDTO)
        {
            try
            {
                PedidoEESS nuevoPedido = PedidoEESSFactory.CrearPedidoEESS(pPedidoEESSDTO.Correlativo,pPedidoEESSDTO.NumeroCara, pPedidoEESSDTO.NumeroDocumento,
                                        pPedidoEESSDTO.AfectaInventario, pPedidoEESSDTO.FechaDocumento, pPedidoEESSDTO.FechaProceso,
                                        pPedidoEESSDTO.Periodo, pPedidoEESSDTO.TotalNacional, pPedidoEESSDTO.TotalExtranjera,
                                        pPedidoEESSDTO.SubTotalNacional, pPedidoEESSDTO.SubTotalExtranjera,pPedidoEESSDTO.ImpuestoIgvNacional,
                                        pPedidoEESSDTO.ImpuestoIgvExtranjera, pPedidoEESSDTO.ImpuestoIscNacional, pPedidoEESSDTO.ImpuestoIscExtranjera,
                                        pPedidoEESSDTO.TotalNoAfectoNacional, pPedidoEESSDTO.TotalNoAfectoExtranjera, pPedidoEESSDTO.PorcentajeDescuentoPrimero,
                                        pPedidoEESSDTO.PorcentajeDescuentoSegundo, pPedidoEESSDTO.TotalDescuentoNacional, pPedidoEESSDTO.TotalDescuentoExtranjera,
                                        pPedidoEESSDTO.TotalVueltoNacional, pPedidoEESSDTO.TotalVueltoExtranjera, pPedidoEESSDTO.TotalEfectivoNacional,
                                        pPedidoEESSDTO.TotalEfectivoExtranjera, pPedidoEESSDTO.RucCliente, pPedidoEESSDTO.NombreCompletoCliente,
                                        pPedidoEESSDTO.Placa, pPedidoEESSDTO.NumeroVale, pPedidoEESSDTO.TipoCambio,
                                        pPedidoEESSDTO.ProcesadoCierreZ, pPedidoEESSDTO.ProcesadoCierreX, pPedidoEESSDTO.NumeroPuntos,
                                        pPedidoEESSDTO.NombreTerminal, pPedidoEESSDTO.Kilometraje, pPedidoEESSDTO.DireccionCliente,
                                        pPedidoEESSDTO.TipoCliente, pPedidoEESSDTO.DescripcionTipoCliente, pPedidoEESSDTO.DescripcionEstado,
                                        pPedidoEESSDTO.TipoCambioClienteCredito, pPedidoEESSDTO.DiasDeGraciaClienteCredito, pPedidoEESSDTO.LimiteCreditoClienteCredito,
                                        pPedidoEESSDTO.DeudaClienteClienteCredito, pPedidoEESSDTO.PlusCreditoClienteCredito, pPedidoEESSDTO.Afecto,
                                        pPedidoEESSDTO.NumeroTarjeta, pPedidoEESSDTO.PagoTarjeta, pPedidoEESSDTO.DescripcionTarjeta,
                                        pPedidoEESSDTO.CodigoTipoDocumento, pPedidoEESSDTO.CodigoTipoPago, pPedidoEESSDTO.CodigoAlmacen,
                                        pPedidoEESSDTO.CodigoMoneda, pPedidoEESSDTO.CodigoEstadoDocumento, pPedidoEESSDTO.CodigoCondicionPago,
                                        pPedidoEESSDTO.CodigoVendedor, pPedidoEESSDTO.CodigoUsuarioDeSistema, pPedidoEESSDTO.CodigoImpuestoIgv,
                                        pPedidoEESSDTO.CodigoImpuestoIsc, pPedidoEESSDTO.CodigoCliente, pPedidoEESSDTO.CodigoClaseTipoCambio,  
                                        pPedidoEESSDTO.CodigoPuntoDeVenta, pPedidoEESSDTO.CodigoEstado, pPedidoEESSDTO.CodigoMonedaCredito, 
                                        pPedidoEESSDTO.CodigoClaseTipoCambioClienteCredito, pPedidoEESSDTO.CodigoTarjetaPromocion, pPedidoEESSDTO.CodigoTarjeta,
                                        pPedidoEESSDTO.CodigoMonedaTarjeta);

                //Detalle de Pedido
                if (pPedidoEESSDTO.PedidoEESSDetalles != null && pPedidoEESSDTO.PedidoEESSDetalles.Any())
                {
                    foreach (var pedidoDetalle in pPedidoEESSDTO.PedidoEESSDetalles)
                    {
                        //Obtener Articulo y Precio
                        var articulo = _IRepositorioArticulo.ObtenerPorCodigo(pedidoDetalle.CodigoArticulo, pedidoDetalle.CodigoAlmacen);
                        if (articulo == null)
                        {
                            LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ArticuloAsociadoAVentaDetalleNoExiste);
                            throw new ArgumentException(Mensajes.advertencia_ArticuloAsociadoAVentaDetalleNoExiste); 
                        }

                        var detalleDePedido = nuevoPedido.AgregarNuevoPedidoEESSDetalle(pedidoDetalle.Secuencia,pedidoDetalle.NumeroTurno, pedidoDetalle.NumeroTransaccionCombustible, 
                                                                pedidoDetalle.PorcentajeDescuentoPrimero, pedidoDetalle.PorcentajeDescuentoSegundo, pedidoDetalle.PorcentajeDescuentoNacional, 
                                                                pedidoDetalle.PorcentajeDescuentoExtranjera, pedidoDetalle.PorcentajeImpuestoIgv, pedidoDetalle.PorcentajeImpuestoIsc, 
                                                                pedidoDetalle.TotalNacional, pedidoDetalle.TotalExtranjera,  pedidoDetalle.ImpuestoNacional, 
                                                                pedidoDetalle.ImpuestoExtranjera, pedidoDetalle.EsInventariable, pedidoDetalle.EnInventarioFisico, 
                                                                pedidoDetalle.Precio, pedidoDetalle.PrecioVenta, pedidoDetalle.CostoEstandarNacional, 
                                                                pedidoDetalle.CostoEstandarExtranjera, pedidoDetalle.DescripcionArticulo, pedidoDetalle.Cantidad, 
                                                                pedidoDetalle.EsFormula, pedidoDetalle.EsArticuloCombustible, pedidoDetalle.NumeroPeaje, 
                                                                pedidoDetalle.CodigoArticulo, pedidoDetalle.CodigoUnidadDeMedida, pedidoDetalle.CodigoArticuloAlterno);
                    }
                }

                //Pago Con Vale
                if (pPedidoEESSDTO.PedidoEESSConVales != null && pPedidoEESSDTO.PedidoEESSConVales.Any())
                {
                    foreach (var pedidoConVale in pPedidoEESSDTO.PedidoEESSConVales)
                    {
                        var pedidoPagaVale = nuevoPedido.AgregarNuevoPedidoEESSConVale(pedidoConVale.NumeroVale);
                    }
                }

                return nuevoPedido;
            }
            catch (Exception ex)
            {
                string detallesAsicionales = string.Empty;                
                string cadenaExcepcion = ex.Message;

                if(ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";                        
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);                
                throw;
            }
        }  


        void GrabarTransaccionNuevoPedidoEESS(PedidoEESS pPedidoEESS)
        {
            try
            {
                if (pPedidoEESS == null)
                    throw new ArgumentException(Mensajes.advertencia_NoSePuedeGrabarPedidoEESSNulo);

                // using (TransactionScope ambito = new TransactionScope(TransactionScopeOption.Suppress,
                //                                                         new TransactionOptions
                //                                                         {
                //                                                             IsolationLevel = IsolationLevel.ReadCommitted,
                //                                                             Timeout = TransactionManager.MaximumTimeout,
                //                                                         },
                //                                                         TransactionScopeAsyncFlowOption.Enabled))
                // {
                    GrabarPedidoEESS(pPedidoEESS);

                //     ambito.Complete();
                // }
            }
            catch (Exception ex)
            {
                string detallesAsicionales = string.Empty;                
                string cadenaExcepcion = ex.Message;

                if(ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";                        
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);                
                throw;
            }

        }

        void GrabarTransaccionNuevoPedidoRetail(PedidoRetail pPedidoRetail)
        {
            try
            {
                if (pPedidoRetail == null)
                    throw new ArgumentException(Mensajes.advertencia_NoSePuedeGrabarPedidoRetailNulo);

                // using (TransactionScope ambito = new TransactionScope())
                // {
                    GrabarPedidoRetail(pPedidoRetail);

                //     ambito.Complete();
                // }
            }
            catch (Exception ex)
            {
                string detallesAsicionales = string.Empty;                
                string cadenaExcepcion = ex.Message;

                if(ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";                        
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);                
                throw;
            }

        }


       void GrabarPedidoEESS(PedidoEESS pPedidoEESS)
        {
            //Persistir PedidoRetail
            // var validarEntidad = ValidadorEntidadFactory.CrearValidador();

            // if (validarEntidad.EsValido(pVenta))
            // {
            _IRepositorioPedidoEESS.Agregar(pPedidoEESS);
            // _IRepositorioVenta.UnidadTrabajo.Commit();
            // }
            // else
            //     throw new AplicacionExcepcionErrorValidacion(validarEntidad.RecibeMensajesInvalidos(pVenta));

        }    

       void GrabarPedidoRetail(PedidoRetail pPedidoRetail)
        {
            //Persistir PedidoRetail
            // var validarEntidad = ValidadorEntidadFactory.CrearValidador();

            // if (validarEntidad.EsValido(pVenta))
            // {
            _IRepositorioPedidoRetail.Agregar(pPedidoRetail);
            // _IRepositorioVenta.UnidadTrabajo.Commit();
            // }
            // else
            //     throw new AplicacionExcepcionErrorValidacion(validarEntidad.RecibeMensajesInvalidos(pVenta));

        }
    }
}