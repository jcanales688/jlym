using System;

namespace PtoVta.Dominio.BaseTrabajo.Enumeradores
{
    public class EstadosPuntoDeVenta
    {
        public struct TipoDeNegocio
        {
            public const int Playa = 1;
            public const string DescripcionPlaya = "PLAYA";

            public const int Tienda = 2;
            public const string DescripcionTienda = "TIENDA";
        }

        public struct EstadoCierre
        {
            public const int Procesado = 1;
            public const int Pendiente = 0;
        }        
    }
}
