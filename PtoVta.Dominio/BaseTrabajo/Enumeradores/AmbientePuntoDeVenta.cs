using System;

namespace PtoVta.Dominio.BaseTrabajo.Enumeradores
{
    public class AmbientePuntoDeVenta
    {
        public struct EnumGenerales
        {
            public const int AnchoTicket = 40;
            public const int AnchoDocumentoIdentidadDni = 8;
            public const int AnchoDocumentoIdentidadRuc = 11;
            public const int AnchoSinDocumentoIdentidad = 0;
        }

        public struct EnumEstadoCierre
        {
            public const int Procesado = 1;
            public const int Pendiente = 0;
        }        

        public struct EnumCliente
        {
            public const int ClienteSinControlDeSaldoDisponible = 0;
            public const int ClienteConControlDeSaldoDisponible = 1;
            public const int ClienteNoAfecto = 0;
            public const int ClienteAfecto = 1;
            public const string ClienteCodigoClaseTipoCambioDefault = "TCONV";
            public const string ClienteCodigoEstadoDeClienteDefault = "A";
            public const string ClienteUsuarioDeSistemaDefault = "SYSADMIN";

            public const string ClienteCreaCodigoMoneda = "PEN";
            public const string ClienteCreaCodigoZonaCliente = "1";
            public const string ClienteCreaCodigoDiaDePago = "DEFAULT0";
            public const string ClienteCreaCodigoVendedor = "99999999";
            public const string ClienteCreaCodigoImpuestoIgv = "IV";
            public const string ClienteCreaCodigoImpuestoIsc = "SC";
            public const string ClienteCreaCodigoPais = "PER";
            public const string ClienteCreaCodigoDepartamento = "LI";
            public const string ClienteCreaCodigoDistrito = "01";


        }

        public struct EnumTipoMovimiento
        {
            public const int Salida = 0;
            public const int Ingreso = 1;               
        }

        public struct EnumStockMovimiento
        {
            public const int NoPermiteStockNegativo = 0;               
            public const int PermiteStockNegativo = 1;               
        }

        public struct EnumCrudCliente
        {
            public const string CrearCliente = "Crear";
            public const string ActualizarCliente = "Actualizar";
        }

        public struct EnumLocalizaCalculoTotalVenta
        {
            public const string CalculoTotalVentaEnBackEnd = "CalculoBackEnd";
            public const string CalculoTotalVentaEnFrontEnd = "CalculoFrontEnd";
        }
    }
}
