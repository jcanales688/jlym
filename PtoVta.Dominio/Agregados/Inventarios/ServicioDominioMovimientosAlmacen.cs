
using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.AmbienteVenta;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class ServicioDominioMovimientosAlmacen : IServicioDominioMovimientosAlmacen
    {
        public int MovimientoAlmacenIngresoOSalida(string pCodigoTipoDocumentoDeVenta, string pCodigoTipoDocumentoNotaCredito, 
                                                    TipoMovimientoAlmacen tipoMovAlmacenVentas)
        {
            var movAlmacenIngresoOSalida = pCodigoTipoDocumentoDeVenta == pCodigoTipoDocumentoNotaCredito ? 1 : tipoMovAlmacenVentas.IngresoOSalida;
            
            return movAlmacenIngresoOSalida;
        }


        public void InicializarArticuloKardexHistorico(Articulo pArticulo, 
                            int pAnioHistorico,decimal pCantidadFinal00, decimal pCantidadFinal01,
                            decimal pCantidadFinal02, decimal pCantidadFinal03,decimal pCantidadFinal04,
                            decimal pCantidadFinal05,decimal pCantidadFinal06,decimal pCantidadFinal07,
                            decimal pCantidadFinal08,decimal pCantidadFinal09,decimal pCantidadFinal10,
                            decimal pCantidadFinal11,decimal pCantidadFinal12,decimal pCantidadFinal13,
                            decimal pCostoPromNacional00,decimal pCostoPromNacional01,decimal pCostoPromNacional02,
                            decimal pCostoPromNacional03,decimal pCostoPromNacional04, decimal pCostoPromNacional05,
                            decimal pCostoPromNacional06, decimal pCostoPromNacional07,decimal pCostoPromNacional08,
                            decimal pCostoPromNacional09, decimal pCostoPromNacional10, decimal pCostoPromNacional11,
                            decimal pCostoPromNacional12,decimal pCostoPromNacional13,decimal pCostoPromExtranjera00,
                            decimal pCostoPromExtranjera01,decimal pCostoPromExtranjera02,decimal pCostoPromExtranjera03,
                            decimal pCostoPromExtranjera04,decimal pCostoPromExtranjera05, decimal pCostoPromExtranjera06,
                            decimal pCostoPromExtranjera07,decimal pCostoPromExtranjera08, decimal pCostoPromExtranjera09,
                            decimal pCostoPromExtranjera10, decimal pCostoPromExtranjera11,decimal pCostoPromExtranjera12,
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
                            IEnumerable<Guid> pIdsAlmacenesEnKardexHistoricoPorArticulo,IEnumerable<Almacen> pAlmacenesActivos)
        {
            // var almacenArticuloARegistrarKdxHistorico = (from almacen in pAlmacenesActivos
            //                                              where pIdsAlmacenesEnKardexHistoricoPorArticulo.Contains(almacen.Id)
            //                                              select almacen).ToList<Almacen>();

            // foreach (var almacen in almacenArticuloARegistrarKdxHistorico)
            // {
            //     pArticulo.AgregarNuevoKardexHistorico(pAnioHistorico, pCantidadFinal00, pCantidadFinal01,
            //                                     pCantidadFinal02, pCantidadFinal03, pCantidadFinal04,
            //                                     pCantidadFinal05, pCantidadFinal06, pCantidadFinal07,
            //                                     pCantidadFinal08, pCantidadFinal09, pCantidadFinal10,
            //                                     pCantidadFinal11, pCantidadFinal12, pCantidadFinal13,
            //                                     pCostoPromNacional00, pCostoPromNacional01, pCostoPromNacional02,
            //                                     pCostoPromNacional03, pCostoPromNacional04, pCostoPromNacional05,
            //                                     pCostoPromNacional06, pCostoPromNacional07, pCostoPromNacional08,
            //                                     pCostoPromNacional09, pCostoPromNacional10, pCostoPromNacional11,
            //                                     pCostoPromNacional12, pCostoPromNacional13, pCostoPromExtranjera00,
            //                                     pCostoPromExtranjera01, pCostoPromExtranjera02, pCostoPromExtranjera03,
            //                                     pCostoPromExtranjera04, pCostoPromExtranjera05, pCostoPromExtranjera06,
            //                                     pCostoPromExtranjera07, pCostoPromExtranjera08, pCostoPromExtranjera09,
            //                                     pCostoPromExtranjera10, pCostoPromExtranjera11, pCostoPromExtranjera12,
            //                                     pCostoPromExtranjera13, pTotCostoPromNacional00, pTotCostoPromNacional01,
            //                                     pTotCostoPromNacional02, pTotCostoPromNacional03, pTotCostoPromNacional04,
            //                                     pTotCostoPromNacional05, pTotCostoPromNacional06, pTotCostoPromNacional07,
            //                                     pTotCostoPromNacional08, pTotCostoPromNacional09, pTotCostoPromNacional10,
            //                                     pTotCostoPromNacional11, pTotCostoPromNacional12, pTotCostoPromNacional13,
            //                                     pTotCostoPromExtranjera00, pTotCostoPromExtranjera01, pTotCostoPromExtranjera02,
            //                                     pTotCostoPromExtranjera03, pTotCostoPromExtranjera04, pTotCostoPromExtranjera05,
            //                                     pTotCostoPromExtranjera06, pTotCostoPromExtranjera07, pTotCostoPromExtranjera08,
            //                                     pTotCostoPromExtranjera09, pTotCostoPromExtranjera10, pTotCostoPromExtranjera11,
            //                                     pTotCostoPromExtranjera12, pTotCostoPromExtranjera13, almacen.CodigoAlmacen);                
            // }


        }        
    }
}