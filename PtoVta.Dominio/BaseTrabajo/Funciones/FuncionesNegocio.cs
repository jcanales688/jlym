using System;

namespace PtoVta.Dominio.BaseTrabajo.Funciones
{
    public static class FuncionesNegocio
    {
        public static string FormatearCorrelativoDocumento(string pSerieDocumento, long pCorrelativoDocumento)
        {
            //Obtiene Correlativo de Documentos
            string correlativoDoc = FuncionesCadena.Izquierda(pSerieDocumento.ToString() + "000", 3) +
                                        FuncionesCadena.Derecha("0000000" + pCorrelativoDocumento.ToString(), 7);

            return correlativoDoc;
        }

        public static string AumentarCorrelativoDocumentoFormateado(string pSerieCorrelativoDocumento)
        {
            int salidaComprueba = 0;
            int tamañoPrefijo = 0;            
            int tamañoSerie = 3;
            int indiceTamañoCorrelativo = 0;
            int tamañoCorrelativoFormateable = 0;
            string mascaraCadenaCorrelativo = "";

            if(!int.TryParse(FuncionesCadena.Extraer(pSerieCorrelativoDocumento.Trim(),0, 1), out salidaComprueba))
                tamañoPrefijo = 1;
                
            indiceTamañoCorrelativo = tamañoPrefijo + tamañoSerie;
            tamañoCorrelativoFormateable = pSerieCorrelativoDocumento.Trim().Length - (tamañoSerie + tamañoPrefijo);

            //Extraer parte numerica
            var prefijoYSerie = FuncionesCadena.Extraer(pSerieCorrelativoDocumento.Trim(), 0, tamañoPrefijo + tamañoSerie);
            double correlativo = Convert.ToDouble(FuncionesCadena.Extraer(pSerieCorrelativoDocumento.Trim(), tamañoPrefijo + tamañoSerie));

            //Aumentar correlativo
            correlativo++;

            //Devolver formateado                
            return prefijoYSerie + FuncionesCadena.Derecha(mascaraCadenaCorrelativo.PadLeft(tamañoCorrelativoFormateable,'0') + correlativo.ToString(), tamañoCorrelativoFormateable);                
        }

        public static string FormatoTicket(decimal nroComprobanteDePago)
        {
            return nroComprobanteDePago.ToString("###-#######");
        }

        public static decimal ObtenerConversionSegunTipoDeCambio(decimal pValor, decimal pTipoDecambio,
                                                           string pOperador, int pRedondeoConversionTipoDecambio)
        {
            decimal valorConvertido = 0;

            if (pOperador == "/")
            {
                valorConvertido = Math.Round(pValor / pTipoDecambio, pRedondeoConversionTipoDecambio);
            }
            else
            {
                valorConvertido = Math.Round(pValor * pTipoDecambio, pRedondeoConversionTipoDecambio);
            }

            return valorConvertido;
        }        
    }
}
