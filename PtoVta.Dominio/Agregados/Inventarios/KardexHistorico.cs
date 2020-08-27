using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class KardexHistorico: Entidad
    {
        public int AnioHistorico { get; set; }
        public decimal CantidadFinal00 { get; set; }
        public decimal CantidadFinal01 { get; set; }
        public decimal CantidadFinal02 { get; set; }
        public decimal CantidadFinal03 { get; set; }
        public decimal CantidadFinal04 { get; set; }
        public decimal CantidadFinal05 { get; set; }
        public decimal CantidadFinal06 { get; set; }
        public decimal CantidadFinal07 { get; set; }
        public decimal CantidadFinal08 { get; set; }
        public decimal CantidadFinal09 { get; set; }
        public decimal CantidadFinal10 { get; set; }
        public decimal CantidadFinal11 { get; set; }
        public decimal CantidadFinal12 { get; set; }
        public decimal CantidadFinal13 { get; set; }
        public decimal CostoPromNacional00 { get; set; }
        public decimal CostoPromNacional01 { get; set; }
        public decimal CostoPromNacional02 { get; set; }
        public decimal CostoPromNacional03 { get; set; }
        public decimal CostoPromNacional04 { get; set; }
        public decimal CostoPromNacional05 { get; set; }
        public decimal CostoPromNacional06 { get; set; }
        public decimal CostoPromNacional07 { get; set; }
        public decimal CostoPromNacional08 { get; set; }
        public decimal CostoPromNacional09 { get; set; }
        public decimal CostoPromNacional10 { get; set; }
        public decimal CostoPromNacional11 { get; set; }
        public decimal CostoPromNacional12 { get; set; }
        public decimal CostoPromNacional13 { get; set; }
        public decimal CostoPromExtranjera00 { get; set; }
        public decimal CostoPromExtranjera01 { get; set; }
        public decimal CostoPromExtranjera02 { get; set; }
        public decimal CostoPromExtranjera03 { get; set; }
        public decimal CostoPromExtranjera04 { get; set; }
        public decimal CostoPromExtranjera05 { get; set; }
        public decimal CostoPromExtranjera06 { get; set; }
        public decimal CostoPromExtranjera07 { get; set; }
        public decimal CostoPromExtranjera08 { get; set; }
        public decimal CostoPromExtranjera09 { get; set; }
        public decimal CostoPromExtranjera10 { get; set; }
        public decimal CostoPromExtranjera11 { get; set; }
        public decimal CostoPromExtranjera12 { get; set; }
        public decimal CostoPromExtranjera13 { get; set; }
        public decimal TotCostoPromNacional00 { get; set; }
        public decimal TotCostoPromNacional01 { get; set; }
        public decimal TotCostoPromNacional02 { get; set; }
        public decimal TotCostoPromNacional03 { get; set; }
        public decimal TotCostoPromNacional04 { get; set; }
        public decimal TotCostoPromNacional05 { get; set; }
        public decimal TotCostoPromNacional06 { get; set; }
        public decimal TotCostoPromNacional07 { get; set; }
        public decimal TotCostoPromNacional08 { get; set; }
        public decimal TotCostoPromNacional09 { get; set; }
        public decimal TotCostoPromNacional10 { get; set; }
        public decimal TotCostoPromNacional11 { get; set; }
        public decimal TotCostoPromNacional12 { get; set; }
        public decimal TotCostoPromNacional13 { get; set; }
        public decimal TotCostoPromExtranjera00 { get; set; }
        public decimal TotCostoPromExtranjera01 { get; set; }
        public decimal TotCostoPromExtranjera02 { get; set; }
        public decimal TotCostoPromExtranjera03 { get; set; }
        public decimal TotCostoPromExtranjera04 { get; set; }
        public decimal TotCostoPromExtranjera05 { get; set; }
        public decimal TotCostoPromExtranjera06 { get; set; }
        public decimal TotCostoPromExtranjera07 { get; set; }
        public decimal TotCostoPromExtranjera08 { get; set; }
        public decimal TotCostoPromExtranjera09 { get; set; }
        public decimal TotCostoPromExtranjera10 { get; set; }
        public decimal TotCostoPromExtranjera11 { get; set; }
        public decimal TotCostoPromExtranjera12 { get; set; }
        public decimal TotCostoPromExtranjera13 { get; set; }

        public string CodigoArticulo { get; set; }
        public string CodigoAlmacen { get; set; }

        public Almacen Almacen { get; private set; }


        //
        public void EstablecerAlmacenDeKardexHistorico(Almacen pAlmacen)
        {
            if (pAlmacen == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ArticuloDeVentaDetalleEnEstadoNuloOTransitorio);

            }

            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeKardexHistorico(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {

                this.CodigoAlmacen = pCodigoAlmacen.Trim();
                this.Almacen = null;
            }
        }        
    }
}