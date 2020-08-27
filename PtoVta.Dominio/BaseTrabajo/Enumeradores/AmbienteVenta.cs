using System;

namespace PtoVta.Dominio.BaseTrabajo.Enumeradores
{
    public class AmbienteVenta
    {
        public struct EnumMoneda
        {
            public const string CodigoMonedaBase = "PEN";
            public const string CodigoMonedaExtranjera = "USD";
        }

        public struct EnumEstadoDocumento
        {
            public const string CodigoEstadoDocumentoCancelado = "CA";
            public const string CodigoEstadoDocumentoPendiente = "PE";
            public const string CodigoEstadoDocumentoAnulado = "AN";
            public const string CodigoEstadoDocumentoEmitido = "OK";
            public const string CodigoEstadoDocumentoNingunaAccion = "NA";

            public const string CodigoEstadoDocumentoPorDefecto = "OK";
            // public const string CodigoEstadoDocumentoAnulado = "AN";
            // public const string CodigoEstadoDocumentoPendiente = "PE";            
        }

        public struct EnumTipoDocumento
        {
            public const string CodigoTipoDocumentoTicket = "12";
        }


        public struct EnumTipoNegocio
        {
            public const string CodigoTipoNegocioDesactivado = "0";
            public const string CodigoTipoNegocioEESS = "1";
            public const string CodigoTipoNegocioRetail = "2";
            public const string CodigoTipoNegocioOficina = "3";

            public const string DescripcionTipoNegocioEESS = "PLAYA";
            public const string DescripcionTipoNegocioRetail = "TIENDA";
        }


        public struct EnumTipoVenta
        {
            public const string ModoTipoVentaManual = "M";
            public const string ModoTipoVentaAutomatico = "A";
        }

        public struct EnumTipoPrecioInventario
        {
            public const string CodigoTipoPrecioInventarioActualizableNacional = "003";
            public const string CodigoTipoPrecioInventarioActualizableExtranjera = "004";
        }

        public struct EnumTipoCliente
        {
            public const string CodigoTipoClienteContado = "00";
            public const string CodigoTipoClientePagoAdelantado = "01";
            public const string CodigoTipoClienteCreditoCorporativo = "02";
            public const string CodigoTipoClienteCreditoLocal = "03";
        }

        public struct EnumTipoPago
        {
            public const string CodigoTipoPagoOtros = "00";
            public const string CodigoTipoPagoEfectivo = "01";
            public const string CodigoTipoPagoTarjeta = "02";
            public const string CodigoTipoPagoChequeGerencia = "03";
            public const string CodigoTipoPagoChequeComercial = "04";
            public const string CodigoTipoPagoChequeDiferidoCredito = "05";
            public const string CodigoTipoPagoValesCredito = "06";
            public const string CodigoTipoPagoFacturaGenerada = "10";
            public const string CodigoTipoPagoNotaCreditoGenerada = "11";
            public const string CodigoTipoPagoContadoAdelantado = "14";
            public const string CodigoTipoPagoCalibracion = "15";
            public const string CodigoTipoPagoValeServitebca = "16";

            public const string CodigoTipoPagoPorDefecto = "01";
        }


        public struct EnumUsuarioSistema
        {
            public const string CodigoUsuarioDeSistemaVendedorPlaya = "VENDPLAYA";
        }

        public struct EnumCondicionPago
        {
            public const int PagoContraEntrega = 0;
            public const int DiasDeCredito1 = 1;
            public const int DiasDeCredito2 = 2;
            public const int DiasDeCredito3 = 3;
            public const int DiasDeCredito4 = 4;
            public const int DiasDeCredito5 = 5;
            public const int DiasDeCredito6 = 6;
            public const int DiasDeCredito7 = 7;
            public const int DiasDeCredito8 = 8;
            public const int DiasDeCredito9 = 9;
            public const int DiasDeCredito10 = 10;
            public const int DiasDeCredito11 = 11;
            public const int DiasDeCredito12 = 12;
            public const int DiasDeCredito13 = 13;
            public const int DiasDeCredito14 = 14;
            public const int DiasDeCredito15 = 15;
            public const int DiasDeCredito16 = 16;
            public const int DiasDeCredito17 = 17;
            public const int DiasDeCredito18 = 18;
            public const int DiasDeCredito19 = 19;
            public const int DiasDeCredito20 = 20;
            public const int DiasDeCredito21 = 21;
            public const int DiasDeCredito22 = 22;
            public const int DiasDeCredito23 = 23;
            public const int DiasDeCredito24 = 24;
            public const int DiasDeCredito25 = 25;
            public const int DiasDeCredito26 = 26;
            public const int DiasDeCredito27 = 27;
            public const int DiasDeCredito28 = 28;
            public const int DiasDeCredito29 = 29;
            public const int DiasDeCredito30 = 30;
            public const int DiasDeCredito31 = 31;
            public const int DiasDeCredito32 = 32;
            public const int DiasDeCredito33 = 33;
            public const int DiasDeCredito34 = 34;
            public const int DiasDeCredito35 = 35;
            public const int DiasDeCredito36 = 36;
            public const int DiasDeCredito37 = 37;
            public const int DiasDeCredito38 = 38;
            public const int DiasDeCredito39 = 39;
            public const int DiasDeCredito40 = 40;
            public const int DiasDeCredito41 = 41;
            public const int DiasDeCredito42 = 42;
            public const int DiasDeCredito43 = 43;
            public const int DiasDeCredito44 = 44;
            public const int DiasDeCredito45 = 45;
            public const int DiasDeCredito46 = 46;
            public const int DiasDeCredito47 = 47;
            public const int DiasDeCredito48 = 48;
            public const int DiasDeCredito49 = 49;
            public const int DiasDeCredito50 = 50;
            public const int DiasDeCredito51 = 51;
            public const int DiasDeCredito52 = 52;
            public const int DiasDeCredito53 = 53;
            public const int DiasDeCredito54 = 54;
            public const int DiasDeCredito55 = 55;
            public const int DiasDeCredito56 = 56;
            public const int DiasDeCredito57 = 57;
            public const int DiasDeCredito58 = 58;
            public const int DiasDeCredito59 = 59;
            public const int DiasDeCredito60 = 60;
            public const int DiasDeCredito61 = 61;
            public const int DiasDeCredito62 = 62;
            public const int DiasDeCredito63 = 63;
            public const int DiasDeCredito64 = 64;
            public const int DiasDeCredito65 = 65;
            public const int DiasDeCredito66 = 66;
            public const int DiasDeCredito67 = 67;
            public const int DiasDeCredito68 = 68;
            public const int DiasDeCredito69 = 69;
            public const int DiasDeCredito70 = 70;
            public const int DiasDeCredito71 = 71;
            public const int DiasDeCredito72 = 72;
            public const int DiasDeCredito73 = 73;
            public const int DiasDeCredito74 = 74;
            public const int DiasDeCredito75 = 75;
            public const int DiasDeCredito76 = 76;
            public const int DiasDeCredito77 = 77;
            public const int DiasDeCredito78 = 78;
            public const int DiasDeCredito79 = 79;
            public const int DiasDeCredito80 = 80;
            public const int DiasDeCredito81 = 81;
            public const int DiasDeCredito82 = 82;
            public const int DiasDeCredito83 = 83;
            public const int DiasDeCredito84 = 84;
            public const int DiasDeCredito85 = 85;
            public const int DiasDeCredito86 = 86;
            public const int DiasDeCredito87 = 87;
            public const int DiasDeCredito88 = 88;
            public const int DiasDeCredito89 = 89;
            public const int DiasDeCredito90 = 90;
            public const int DiasDeCredito91 = 91;
            public const int DiasDeCredito92 = 92;
            public const int DiasDeCredito93 = 93;
            public const int DiasDeCredito94 = 94;
            public const int DiasDeCredito95 = 95;
            public const int DiasDeCredito96 = 96;
            public const int DiasDeCredito97 = 97;
            public const int DiasDeCredito98 = 98;

            public const string CodigoCondicionPagoContraentrega = "00";
        }
    }
}
