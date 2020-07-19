using System;

namespace PtoVta.Aplicacion.DTO.Configuraciones
{
    public class ConfiguracionPuntoVentaDTO
    {
        public string CodigoPuntoDeVenta { get; set; }
        public string NombreTerminal { get; set; }
        public string NumeroSerieMaquinaRegistradora { get; set; }
        public bool PermiteTicketFactura { get; set; }
        public bool PermiteTicketBoleta { get; set; }
        public string SimboloMonedaCaja { get; set; }
        public bool PermiteColaTransaccionesManual { get; set; }         //stkloadsale; Salvar Transacciones
        public string DispositivoTicketFactura { get; set; }
        public string TipoDispositivoSalidaTicketFactura { get; set; }
        public string DispositivoTicketBoleta { get; set; }
        public string TipoDispositivoSalidaTicketBoleta { get; set; }
        public string SerieCorrelativoTickFactura { get; set; }
        public string SerieCorrelativoTickBoleta { get; set; }
        public bool RealizoCierreZeta { get; set; }
        public bool RealizoCierreTurno { get; set; }
        public bool PermiteSaltoAutomatico { get; set; }
        public int CantidadSaltoAutomatico { get; set; }
        public bool PedirClaveAnulacionDocumento { get; set; }
        //public int PermiteRegistrarCantidadVentaAutomatico { get; set; } //igual a PermiteSaltoAutomatico
        public decimal CorrelativoMovimientoAlmacenPorVenta { get; set; }

        public string CodigoMonedaCaja { get; set; }
        public string CodigoTipoNegocio { get; set; }
        public string CodigoAlmacenPuntoVenta { get; set; }
        public string CodigoTipoImpresora { get; set; }
        public string CodigoEstadoDocumentoDefault { get;  set; }
        public string CodigoTipoPagoDefault { get; set; }
        public string CodigoEstadoDocumentoAnulado { get; private set; }        
    }
}