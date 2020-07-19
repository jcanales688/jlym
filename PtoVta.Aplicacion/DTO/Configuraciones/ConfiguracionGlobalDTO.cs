using System;

namespace PtoVta.Aplicacion.DTO.Configuraciones
{
    public class ConfiguracionGlobalDTO
    {
        //Configuracion General
        // public string CodigoConfiguracionGeneral { get; set; }
        public int CantidadTurnos { get; set; }
        public int TurnoActual { get; set; }    
        public int CantidadCaras { get; set; }
        public string SimboloMonedaBase { get; set; }
        public string SimboloMonedaExtranjera { get; set; }
        public int CantidadDecimalPrecio { get; set; }
        public int CantidadDecimalCosto { get; set; }
        public int CantidadDecimalStock { get; set; }
        public int CantidadDecimalResultado { get; set; }
        public int CantidadDecimalDescuento { get; set; }
        public int PorcentajeImpuesto { get; set; }
        public DateTime FechaProceso { get; set; }
        public int TipoControlCombustible { get; set; }
        public decimal DiferenciaDiariaPermitida { get; set; }
        public int DiasCambioClave { get; set; }
        public string CodigoAlmacenOrigen { get; set; }
        public string CodigoMonedaBase { get;  set; }
        public string CodigoMonedaExtranjera { get;  set; }
        public string CodigoClaseTipoCambioVenta { get;  set; }
        public string CodigoClaseTipoCambioOrigen { get;  set; }
        public string CodigoImpuesto { get;  set; }
        public string CodigoClienteInterno { get;  set; }



        //Config Punto de Venta
        // public string CodigoConfiguracionPuntoDeVenta { get; set; }
        public string CodigoPuntoDeVenta { get; set; }
        public string NombreTerminal { get; set; }
        public string NumeroSerieMaquinaRegistradora { get; set; }
        public int PermiteTicketFactura { get; set; }
        public int PermiteTicketBoleta { get; set; }
        public string SimboloMonedaCaja { get; set; }
        public int PermiteColaTransaccionesManual { get; set; }
        public string TipoDispositivoSalidaTicketFactura { get; set; }
        public string DispositivoTicketFactura { get; set; }
        public string TipoDispositivoSalidaTicketBoleta { get; set; }
        public string DispositivoTicketBoleta { get; set; }
        public long SerieCorrelativoTickFac { get; set; }
        public long SerieCorrelativoTickBol { get; set; }
        public int RealizoCierreZeta { get; set; }
        public int RealizoCierreTurno { get; set; }
        public int PermiteSaltoAutomatico { get; set; }
        public int CantidadSaltoAutomatico { get; set; }
        public int PedirClaveAnulacionDocumento { get; set; }
        public int PermiteRegistrarCantidadVentaAutomatico { get; set; }
        public string CodigoMonedaCaja { get; set; }
        public string CodigoTipoNegocio { get; set; }
        public string CodigoAlmacenPuntoVenta { get; set; }
        public string CodigoTipoImpresora { get; set; }
        public int TipoImpresoraSaltarLineas { get; set; } //Campo Compuesto de entidades campo
        public decimal CorrelativoMovimientoAlmacenPorVenta { get; set; }

        //Configuracion Inventario
        // public string CodigoConfiguracionInventario { get; set; }
        public string PeriodoInventario { get; set; }
        public string RutaReportesInventario { get; set; }
        public int PermitirStockNegativo { get; set; }
        public decimal MaximoRedondeoInventario { get; set; }
        public string CodigoTMAIngresoTransferencia { get;  set; }
        public string CodigoTMASalidaTransferencia { get;  set; }
        public string CodigoTMACompraTienda { get;  set; }
        public string CodigoTMACompraPlaya { get;  set; }
        public string CodigoTMAReversaCompraPlaya { get;  set; }
        public string CodigoTMAReversaCompraTienda { get;  set; }
        public string CodigoTMAVentas { get;  set; }
        public string CodigoProveedorDefault { get; set; }  
        public string CodigoArticuloRedondeoInventario { get;  set; }


        //Configuracion Ventas
        // public string CodigoConfiguracionVenta { get; set; }
        public int VentaAutomatica { get; set; }  //antes: VentaAutomaticaCombustible
        public DateTime FechaProcesoVenta { get; set; }
        public int NoSaltaCorrelativo { get; set; }
        public string RutaReportesVenta { get; set; }
        public int TipoControlador { get; set; }
        public string CodigoCategoriaFuel { get;  set; }
        public string CodigoTipoClienteEfectivo { get;  set; }
        public string CodigoTipoClienteAdelanto { get;  set; }
        public string CodigoTipoClienteCreditoLocal { get;  set; }
        public string CodigoTipoClienteCreditoCorporativo { get;  set; }
        public string CodigoTipoClienteOtros { get;  set; }
        public string CodigoTipoDocumentoTicket { get;  set; }
        public string CodigoTipoDocumentoFactura { get;  set; }
        public string CodigoTipoDocumentoBoleta { get;  set; }
        public string CodigoTipoDocumentoNotaCredito { get;  set; }
        public string CodigoTipoDocumentoNotaDebito { get;  set; }
        public string CodigoTipoDocumentoNotaCreditoAjuste { get;  set; }
        public string CodigoCondicionPagoDefault { get; set; }   //"00"
        public string CodigoEstadoDocumentoDefault { get; set; } //"PE"

        //Configuracion Tickets
        // public string CodigoConfiguracionFormatoTicket { get; set; }
        public string Cabecera01 { get; set; }
        public string Cabecera02 { get; set; }
        public string Cabecera03 { get; set; }
        public string Cabecera04 { get; set; }
        public string Cabecera05 { get; set; }
        public string Cabecera06 { get; set; }
        public string Cabecera07 { get; set; }
        public string Cabecera08 { get; set; }
        public string Cabecera09 { get; set; }
        public string Cabecera10 { get; set; }
        public string Linea01 { get; set; }
        public string Linea02 { get; set; }
        public string Linea03 { get; set; }
        public string Linea04 { get; set; }
        public string PiePagina01 { get; set; }
        public string PiePagina02 { get; set; }
        public string PiePagina03 { get; set; }
        public string PiePagina04 { get; set; }
        public string PiePagina05 { get; set; }
        public string PiePagina06 { get; set; }
        public string PiePagina07 { get; set; }
        public string PiePagina08 { get; set; }
        public string PiePagina09 { get; set; }
        public string PiePagina10 { get; set; }
        public int AnchoTicket { get; set; }        
    }
}