using System;

namespace PtoVta.Dominio.BaseTrabajo.Funciones
{
    public static class FuncionesNegocio
    {
        //public static decimal CorrelativoDocumento(int pSerieDocumento, long pCorrelativoDocumento)
        //{
        //    //Obtiene Correlativo de Documentos
        //    decimal correlativoDoc =
        //            Convert.ToDecimal(
        //                                FuncionesCadena.Izquierda(pSerieDocumento.ToString() + "000", 3) +
        //                                FuncionesCadena.Derecha("0000000" + pCorrelativoDocumento.ToString(), 7)
        //                             );

        //    return correlativoDoc;
        //}

        public static string FormatoTicket(decimal nroComprobanteDePago)
        {
            return nroComprobanteDePago.ToString("###-#######");
        }

        //public static decimal ObtenerConversionSegunTipoDeCambio(decimal pValor, decimal pTipoDecambio,
        //                                                   string pOperador, int pRedondeoConversionTipoDecambio)
        //{
        //    decimal valorConvertido = 0;

        //    if (pOperador == "/")
        //    {
        //        valorConvertido = Math.Round(pValor / pTipoDecambio, pRedondeoConversionTipoDecambio);
        //    }
        //    else
        //    {
        //        valorConvertido = Math.Round(pValor * pTipoDecambio, pRedondeoConversionTipoDecambio);
        //    }

        //    return valorConvertido;
        //}        
    }
}
