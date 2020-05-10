using System;

namespace PtoVta.Infraestructura.Transversales.Comun
{
    public class CierresPuntoDeVenta
    {
        public struct TipoResumenConsultado
        {
            public const string PorArticulo = "Articulo";
            public const string PorCategoria = "Categoria";
            public const string PorVendedor = "Vendedor";
            public const string PorCara = "Cara";
        }

        public struct TipoCierreProcesado
        {
            public const string CierreX = "CierreX";
            public const string CierreZeta = "CierreZeta";
        }
    }
}
