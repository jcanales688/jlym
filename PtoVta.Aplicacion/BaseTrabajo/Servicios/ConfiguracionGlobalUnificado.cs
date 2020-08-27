using System;
using System.Collections.Generic;
using PtoVta.Aplicacion.DTO.Configuraciones;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.Transversales.Log;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.BaseTrabajo
{
    public class ConfiguracionGlobalUnificado : IConfiguracionGlobalUnificado
    {
        private IRepositorioConfiguracionFormatoTicket _IRepositorioConfiguracionFormatoTicket;
        private IRepositorioConfiguracionGeneral _IRepositorioConfiguracionGeneral;
        private IRepositorioConfiguracionInventario _IRepositorioConfiguracionInventario;
        private IRepositorioConfiguracionVenta _IRepositorioConfiguracionVenta;

    
        public ConfiguracionGlobalUnificado(IRepositorioConfiguracionFormatoTicket pIRepositorioConfiguracionFormatoTicket,
                                        IRepositorioConfiguracionGeneral pIRepositorioConfiguracionGeneral, 
                                        IRepositorioConfiguracionInventario pIRepositorioConfiguracionInventario,                                             
                                        IRepositorioConfiguracionVenta pIRepositorioConfiguracionVenta)
        {
            if (pIRepositorioConfiguracionFormatoTicket == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionFormatoTicket Nulo en ServicioAplicacionConfiguracion");

            if (pIRepositorioConfiguracionGeneral == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionGeneral Nulo en ServicioAplicacionConfiguracion");

            if (pIRepositorioConfiguracionInventario == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionInventario Nulo en ServicioAplicacionConfiguracion");    

            if (pIRepositorioConfiguracionVenta == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionVenta Nulo en ServicioAplicacionConfiguracion"); 

            _IRepositorioConfiguracionFormatoTicket = pIRepositorioConfiguracionFormatoTicket;                 
            _IRepositorioConfiguracionGeneral = pIRepositorioConfiguracionGeneral;
            _IRepositorioConfiguracionInventario = pIRepositorioConfiguracionInventario;                                                              
            _IRepositorioConfiguracionVenta = pIRepositorioConfiguracionVenta;            
        }



        public ConfiguracionGlobalDTO UnificarConfiguracionGlobal()
        {
            var configuracionGeneral = _IRepositorioConfiguracionGeneral.Obtener();
            if (configuracionGeneral == null)
            {          
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConsultaConfiguracionGeneralFallo);
                return null;
            }

            var configuracionVenta = _IRepositorioConfiguracionVenta.Obtener();
            if (configuracionVenta == null)
            {          
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConsultaConfiguracionVentaFallo);
                return null;
            }            

            var configuracionInventario = _IRepositorioConfiguracionInventario.Obtener();
            if (configuracionInventario == null)
            {          
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConsultaConfiguracionInventarioFallo);
                return null;
            }

            var configuracionFormatoTicket = _IRepositorioConfiguracionFormatoTicket.Obtener();
            if (configuracionFormatoTicket == null)
            {          
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConsultaConfiguracionFormatoTicketFallo);
                return null;
            }                                    
            
            var configuracionGlobalDto = MaterializarConfiguracionesAConfiguracionGlobalDTO(configuracionGeneral, 
                                            configuracionVenta, configuracionInventario, configuracionFormatoTicket);

            return configuracionGlobalDto;                    
        }


        private ConfiguracionGlobalDTO MaterializarConfiguracionesAConfiguracionGlobalDTO(ConfiguracionGeneral pConfiguracionGeneral,
                                            ConfiguracionVenta pConfiguracionVenta, ConfiguracionInventario pConfiguracionInventario, 
                                            ConfiguracionFormatoTicket pConfiguracionFormatoTicket)
        {
            var configuracionGlobalDto = new ConfiguracionGlobalDTO();

            //--> Configuracion General
            configuracionGlobalDto.CantidadTurnos = pConfiguracionGeneral.CantidadTurnos;
            configuracionGlobalDto.TurnoActual = pConfiguracionGeneral.TurnoActual;
            configuracionGlobalDto.CantidadCaras = pConfiguracionGeneral.CantidadCaras;
            configuracionGlobalDto.CodigoAlmacenOrigen = pConfiguracionGeneral.CodigoAlmacenOrigen;
            configuracionGlobalDto.CodigoMonedaBase = pConfiguracionGeneral.CodigoMonedaBase;
            configuracionGlobalDto.SimboloMonedaBase = pConfiguracionGeneral.SimboloMonedaBase;
            configuracionGlobalDto.CodigoMonedaExtranjera = pConfiguracionGeneral.CodigoMonedaExtranjeraPorDefecto;
            configuracionGlobalDto.SimboloMonedaExtranjera= pConfiguracionGeneral.SimboloMonedaExtranjera;
            configuracionGlobalDto.CantidadDecimalPrecio = pConfiguracionGeneral.CantidadDecimalPrecio;
            configuracionGlobalDto.CantidadDecimalCosto = pConfiguracionGeneral.CantidadDecimalCosto;
            configuracionGlobalDto.CantidadDecimalStock = pConfiguracionGeneral.CantidadDecimalStock;
            configuracionGlobalDto.CantidadDecimalResultado = pConfiguracionGeneral.CantidadDecimalResultado;
            configuracionGlobalDto.CantidadDecimalDescuento = pConfiguracionGeneral.CantidadDecimalDescuento;
            configuracionGlobalDto.PorcentajeImpuesto = pConfiguracionGeneral.PorcentajeImpuesto;
            configuracionGlobalDto.CodigoImpuesto = pConfiguracionGeneral.CodigoImpuesto;
            configuracionGlobalDto.FechaProceso = pConfiguracionGeneral.FechaProceso;
            configuracionGlobalDto.TipoControlCombustible = pConfiguracionGeneral.TipoControlCombustible;
            configuracionGlobalDto.CodigoClaseTipoCambioVenta = pConfiguracionGeneral.CodigoClaseTipoCambioVenta;
            configuracionGlobalDto.CodigoClaseTipoCambioOrigen = pConfiguracionGeneral.CodigoClaseTipoCambioOrigen;
            configuracionGlobalDto.DiferenciaDiariaPermitida = pConfiguracionGeneral.DiferenciaDiariaPermitida;
            configuracionGlobalDto.DiasCambioClave = pConfiguracionGeneral.DiasCambioClave;
            configuracionGlobalDto.CodigoClienteInterno = pConfiguracionGeneral.CodigoClienteInterno;

            //--> Configuracion Ventas
            configuracionGlobalDto.VentaAutomatica =  pConfiguracionVenta.VentaAutomaticaCombustible;  //Cambiar nombre en base de datos a VentaAutomatica
            configuracionGlobalDto.CodigoCategoriaFuel = pConfiguracionVenta.CodigoCategoriaFuel;
            configuracionGlobalDto.FechaProcesoVenta = pConfiguracionVenta.FechaProcesoVenta;
            configuracionGlobalDto.CodigoTipoClienteEfectivo = pConfiguracionVenta.CodigoTipoClienteEfectivo;
            configuracionGlobalDto.CodigoTipoClienteAdelanto = pConfiguracionVenta.CodigoTipoClienteAdelanto;
            configuracionGlobalDto.CodigoTipoClienteCreditoCorporativo = pConfiguracionVenta.CodigoTipoClienteCreditoCorporativo;
            configuracionGlobalDto.CodigoTipoClienteCreditoLocal = pConfiguracionVenta.CodigoTipoClienteCreditoLocal;
            configuracionGlobalDto.CodigoTipoClienteOtros = pConfiguracionVenta.CodigoTipoClienteOtros;
            configuracionGlobalDto.CodigoTipoDocumentoTicket = pConfiguracionVenta.CodigoTipoDocumentoTicket;
            configuracionGlobalDto.CodigoTipoDocumentoFactura = pConfiguracionVenta.CodigoTipoDocumentoFactura;
            configuracionGlobalDto.CodigoTipoDocumentoBoleta = pConfiguracionVenta.CodigoTipoDocumentoBoleta;
            configuracionGlobalDto.CodigoTipoDocumentoNotaCredito = pConfiguracionVenta.CodigoTipoDocumentoNotaCredito;
            configuracionGlobalDto.CodigoTipoDocumentoNotaDebito = pConfiguracionVenta.CodigoTipoDocumentoNotaDebito;
            configuracionGlobalDto.NoSaltaCorrelativo = pConfiguracionVenta.NoSaltaCorrelativo;
            configuracionGlobalDto.RutaReportesVenta = pConfiguracionVenta.RutaReportesVenta;
            configuracionGlobalDto.CodigoTipoDocumentoNotaCreditoAjuste = pConfiguracionVenta.CodigoTipoDocumentoNotaCreditoAjuste;
            configuracionGlobalDto.TipoControlador = pConfiguracionVenta.TipoControlador;
            configuracionGlobalDto.CodigoCondicionPagoDefault = pConfiguracionVenta.CodigoCondicionPagoDefault;            

            //-->Configuracion Inventario
            configuracionGlobalDto.PeriodoInventario = pConfiguracionInventario.PeriodoInventario;
            configuracionGlobalDto.CodigoTMAIngresoTransferencia = pConfiguracionInventario.CodigoTMAIngresoTransferencia;
            configuracionGlobalDto.CodigoTMASalidaTransferencia = pConfiguracionInventario.CodigoTMASalidaTransferencia;
            configuracionGlobalDto.RutaReportesInventario = pConfiguracionInventario.RutaReportesInventario;
            configuracionGlobalDto.CodigoTMAVentas = pConfiguracionInventario.CodigoTMAVentas;
            configuracionGlobalDto.CodigoTMACompraTienda = pConfiguracionInventario.CodigoTMACompraTienda;
            configuracionGlobalDto.CodigoTMACompraPlaya = pConfiguracionInventario.CodigoTMACompraPlaya;
            configuracionGlobalDto.CodigoTMAReversaCompraTienda = pConfiguracionInventario.CodigoTMAReversaCompraTienda;
            configuracionGlobalDto.CodigoTMAReversaCompraPlaya = pConfiguracionInventario.CodigoTMAReversaCompraPlaya;
            configuracionGlobalDto.PermitirStockNegativo = pConfiguracionInventario.PermitirStockNegativo;
            configuracionGlobalDto.CodigoProveedorDefault = pConfiguracionInventario.CodigoProveedorDefault;
            configuracionGlobalDto.CodigoArticuloRedondeoInventario = pConfiguracionInventario.CodigoArticuloRedondeoInventario;
            configuracionGlobalDto.MaximoRedondeoInventario = pConfiguracionInventario.MaximoRedondeoInventario;

            //--> Configuracion Formato TIcket
            configuracionGlobalDto.Cabecera01 = pConfiguracionFormatoTicket.Cabecera01;
            configuracionGlobalDto.Cabecera02 = pConfiguracionFormatoTicket.Cabecera02;
            configuracionGlobalDto.Cabecera03 = pConfiguracionFormatoTicket.Cabecera03;
            configuracionGlobalDto.Cabecera04 = pConfiguracionFormatoTicket.Cabecera04;
            configuracionGlobalDto.Cabecera05 = pConfiguracionFormatoTicket.Cabecera05;
            configuracionGlobalDto.Cabecera06 = pConfiguracionFormatoTicket.Cabecera06;
            configuracionGlobalDto.Cabecera07 = pConfiguracionFormatoTicket.Cabecera08;
            configuracionGlobalDto.Cabecera08 = pConfiguracionFormatoTicket.Cabecera08;
            configuracionGlobalDto.Cabecera09 = pConfiguracionFormatoTicket.Cabecera09;
            configuracionGlobalDto.Cabecera10 = pConfiguracionFormatoTicket.Cabecera10;
            configuracionGlobalDto.Linea01 = pConfiguracionFormatoTicket.Linea01;
            configuracionGlobalDto.Linea02 = pConfiguracionFormatoTicket.Linea02;
            configuracionGlobalDto.Linea03 = pConfiguracionFormatoTicket.Linea03;
            configuracionGlobalDto.Linea04 = pConfiguracionFormatoTicket.Linea04;
            configuracionGlobalDto.PiePagina01 = pConfiguracionFormatoTicket.PiePagina01;
            configuracionGlobalDto.PiePagina02 = pConfiguracionFormatoTicket.PiePagina02;
            configuracionGlobalDto.PiePagina03 = pConfiguracionFormatoTicket.PiePagina03;
            configuracionGlobalDto.PiePagina04 = pConfiguracionFormatoTicket.PiePagina04;
            configuracionGlobalDto.PiePagina05 = pConfiguracionFormatoTicket.PiePagina05;
            configuracionGlobalDto.PiePagina06 = pConfiguracionFormatoTicket.PiePagina06;
            configuracionGlobalDto.PiePagina07 = pConfiguracionFormatoTicket.PiePagina07;
            configuracionGlobalDto.PiePagina08 = pConfiguracionFormatoTicket.PiePagina08;
            configuracionGlobalDto.PiePagina09 = pConfiguracionFormatoTicket.PiePagina09;
            configuracionGlobalDto.PiePagina10 = pConfiguracionFormatoTicket.PiePagina10;
            configuracionGlobalDto.AnchoTicket = pConfiguracionFormatoTicket.AnchoTicket;            

            return configuracionGlobalDto;
        }

    }    
}