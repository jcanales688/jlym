using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Parametros;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public interface IServicioDominioMovimientosAlmacen
    {
        int MovimientoAlmacenIngresoOSalida(string pCodigoTipoDocumentoDeVenta, string pCodigoTipoDocumentoNotaCredito, 
                                                TipoMovimientoAlmacen tipoMovAlmacenVentas);

        void InicializarArticuloKardexHistorico(Articulo pArticulo,
                            int pAnioHistorico, decimal pCantidadFinal00, decimal pCantidadFinal01,
                            decimal pCantidadFinal02, decimal pCantidadFinal03, decimal pCantidadFinal04,
                            decimal pCantidadFinal05, decimal pCantidadFinal06, decimal pCantidadFinal07,
                            decimal pCantidadFinal08, decimal pCantidadFinal09, decimal pCantidadFinal10,
                            decimal pCantidadFinal11, decimal pCantidadFinal12, decimal pCantidadFinal13,
                            decimal pCostoPromNacional00, decimal pCostoPromNacional01, decimal pCostoPromNacional02,
                            decimal pCostoPromNacional03, decimal pCostoPromNacional04, decimal pCostoPromNacional05,
                            decimal pCostoPromNacional06, decimal pCostoPromNacional07, decimal pCostoPromNacional08,
                            decimal pCostoPromNacional09, decimal pCostoPromNacional10, decimal pCostoPromNacional11,
                            decimal pCostoPromNacional12, decimal pCostoPromNacional13, decimal pCostoPromExtranjera00,
                            decimal pCostoPromExtranjera01, decimal pCostoPromExtranjera02, decimal pCostoPromExtranjera03,
                            decimal pCostoPromExtranjera04, decimal pCostoPromExtranjera05, decimal pCostoPromExtranjera06,
                            decimal pCostoPromExtranjera07, decimal pCostoPromExtranjera08, decimal pCostoPromExtranjera09,
                            decimal pCostoPromExtranjera10, decimal pCostoPromExtranjera11, decimal pCostoPromExtranjera12,
                            decimal pCostoPromExtranjera13, decimal pTotCostoPromNacional00, decimal pTotCostoPromNacional01,
                            decimal pTotCostoPromNacional02, decimal pTotCostoPromNacional03, decimal pTotCostoPromNacional04,
                            decimal pTotCostoPromNacional05, decimal pTotCostoPromNacional06, decimal pTotCostoPromNacional07,
                            decimal pTotCostoPromNacional08, decimal pTotCostoPromNacional09, decimal pTotCostoPromNacional10,
                            decimal pTotCostoPromNacional11, decimal pTotCostoPromNacional12, decimal pTotCostoPromNacional13,
                            decimal pTotCostoPromExtranjera00, decimal pTotCostoPromExtranjera01, decimal pTotCostoPromExtranjera02,
                            decimal pTotCostoPromExtranjera03, decimal pTotCostoPromExtranjera04, decimal pTotCostoPromExtranjera05,
                            decimal pTotCostoPromExtranjera06, decimal pTotCostoPromExtranjera07, decimal pTotCostoPromExtranjera08,
                            decimal pTotCostoPromExtranjera09, decimal pTotCostoPromExtranjera10, decimal pTotCostoPromExtranjera11,
                            decimal pTotCostoPromExtranjera12, decimal pTotCostoPromExtranjera13, 
                            IEnumerable<Guid> pIdsAlmacenesEnKardexHistoricoPorArticulo,IEnumerable<Almacen> pAlmacenesActivos);
    }
}