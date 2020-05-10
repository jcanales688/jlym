using System;

namespace PtoVta.Dominio.BaseTrabajo.Enumeradores
{
    public class EstadosVenta
    {
        public struct VentaTipoPago
        {
            public const string VentaOtros = "00";
            public const string VentaEfectivo = "01";
            public const string VentaTarjeta = "02";
            public const string VentaChequeGerencia = "03";
            public const string VentaChequeComercial = "04";
            public const string VentaChequeDiferidoCredito = "05";
            public const string VentaValesCredito = "06";
            public const string VentaFacturaGenerada = "10";
            public const string VentaNotaCreditoGenerada = "11";
            public const string VentaContadoAdelantado = "14";
            public const string Calibracion = "15";
            public const string ValeServitebca = "16";
        }


        public struct VentaEstadoDocumento
        {
            public const string Cancelado = "CA";
            public const string Pendiente = "PE";
            public const string Anulado = "AN";
            public const string Emitido = "OK";
            public const string NingunaAccion = "NA";
        }        
    }
}
