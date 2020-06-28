using System;
using System.Collections.Generic;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;

namespace PtoVta.Aplicacion.GestionVentas
{
    public interface IServicioAplicacionFacturacion
    {
        ResultadoServicio<ResultadoVentaGrabadaDTO> AgregarNuevaVenta(VentaDTO pVentaDTO, string pCodigoTipoDocumentoNotaCredito,bool pEsVentaPagoAdelantado, 
                                                    string pCodigoTMAVentas,int pPermitirStockNegativo, DateTime pFechaTipoDeCambio,
                                                    string pTipoDeVenta, string pCodigoCondicionPagoDefault, string pCodigoEstadoDocumentoDefault,
                                                    bool pFlagCambioMonedaVuelto, decimal pEfectivoVueltoExtranjera, decimal pTotalVueltoSegunMoneda,
                                                    decimal pTotalFaltanteExtranjera, decimal pTotalFaltanteNacional,string pCodigoMonedaVuelto, 
                                                    string pCodigoMonedaBase, string pCodigoMonedaExtranjera,string pCodigoConfiguracionGeneral);

        ResultadoServicio<VentaListadoDTO> BuscarVentasPorCliente(string pCodigoCliente);        
    }

}