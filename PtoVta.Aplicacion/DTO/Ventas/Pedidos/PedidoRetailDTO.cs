using System;
using System.Collections.Generic;

namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class PedidoRetailDTO
    {
       public int Correlativo { get; set; }
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
        public string DireccionCliente { get; set; }
        public string Placa { get; set; }
        public decimal NumeroVale { get; set; }
        public decimal TipoCambio { get; set; }
        public int NumeroPuntos { get; set; }
        public int Kilometraje { get; set; }
        public bool TransaccionPendiente { get; set; }
        public string  TipoVenta { get; set; }
        public bool TransaccionProcesada { get; set; }
        public bool AplicaDescuentoCupon { get; set; }
        public string CentroDeCosto { get; set; }


        public string CodigoTipoDocumento { get; set; } 
        public string CodigoTipoPago { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoMoneda { get; set; }
        public string CodigoCondicionPago { get; set; }
        public string CodigoVendedor { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }
        public string CodigoImpuestoIgv { get; set; }
        public string CodigoImpuestoIsc { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoClaseTipoCambio { get; set; }
        public string CodigoTarjetaPromocion { get; set; } 
        public string CodigoPuntoDeVenta { get; set; }           
        public string CodigoTipoNegocio { get; set; }


        public List<PedidoRetailDetalleDTO> PedidoRetailDetalles { get; set; }              
        public List<PedidoRetailConValeDTO> PedidoRetailConVales { get; set; } 
        public List<PedidoRetailConTarjetaDTO> PedidoRetailConTarjetas { get; set; } 

    }
}