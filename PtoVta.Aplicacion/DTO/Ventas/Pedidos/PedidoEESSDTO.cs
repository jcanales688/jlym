using System;
using System.Collections.Generic;

namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class PedidoEESSDTO
    {
        public int Correlativo { get; set; }
        public string NumeroCara { get; set; }  
        public string NumeroDocumento { get; set; }
        public bool AfectaInventario { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaProceso { get; set; }
        public string Periodo { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjera { get; set; }
        public decimal SubTotalNacional { get; set; }
        public decimal SubTotalExtranjera { get; set; }
        public decimal ImpuestoIgvNacional { get; set; }
        public decimal ImpuestoIgvExtranjera { get; set; }
        public decimal ImpuestoIscNacional { get; set; }
        public decimal ImpuestoIscExtranjera { get; set; }
        public decimal TotalNoAfectoNacional { get; set; }
        public decimal TotalNoAfectoExtranjera { get; set; }
        public decimal PorcentajeDescuentoPrimero { get; set; }
        public decimal PorcentajeDescuentoSegundo { get; set; }
        public decimal TotalDescuentoNacional { get; set; }
        public decimal TotalDescuentoExtranjera { get; set; }
        public decimal TotalVueltoNacional { get; set; }
        public decimal TotalVueltoExtranjera { get; set; }
        public decimal TotalEfectivoNacional { get; set; }
        public decimal TotalEfectivoExtranjera { get; set; }
        public string RucCliente { get; set; }    
        public string NombreCompletoCliente { get; set; }
        public string Placa { get; set; }
        public decimal NumeroVale { get; set; }
        public decimal TipoCambio { get; set; }
        public bool ProcesadoCierreZ { get; set; }
        public bool ProcesadoCierreX { get; set; }    
        public int NumeroPuntos { get; set; }
        public string NombreTerminal { get; set; }
        public int Kilometraje { get; set; }        
        public string DireccionCliente { get; set; }
        public int TipoCliente { get; set; }
        public string DescripcionTipoCliente { get; set; }
        public string DescripcionEstado { get; set; }
        public decimal TipoCambioClienteCredito { get; set; }
        public int DiasDeGraciaClienteCredito { get; set; }
        public decimal LimiteCreditoClienteCredito { get; set; }
        public decimal DeudaClienteClienteCredito { get; set; }
        public decimal PlusCreditoClienteCredito { get; set; }
        public bool Afecto { get; set; }
        public string NumeroTarjeta { get; set; }
        public int PagoTarjeta { get; set; }
        public string DescripcionTarjeta { get; set; }


        public string CodigoTipoDocumento { get; set; }            
        public string CodigoTipoPago { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoMoneda { get; set; }
        public string CodigoEstadoDocumento { get; set; }
        public string CodigoCondicionPago { get; set; }
        public string CodigoVendedor { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }
        public string CodigoImpuestoIgv { get; set; }
        public string CodigoImpuestoIsc { get; set; }        
        public string CodigoCliente { get; set; }
        public string CodigoClaseTipoCambio { get; set; }
        public string CodigoPuntoDeVenta { get; set; }                  
        public string CodigoEstado { get; set; }        
        public string CodigoMonedaCredito { get; set; }
        public string CodigoClaseTipoCambioClienteCredito { get; set; }
        public string CodigoTarjetaPromocion { get; set; }   
        public string CodigoTarjeta { get;  set; }                
        public string CodigoMonedaTarjeta { get; set; }        

        public List<PedidoEESSDetalleDTO> PedidoEESSDetalles { get; set; }  
        public List<PedidoEESSConValeDTO> PedidoEESSConVales  { get; set; }  
    }
}