using System;

namespace PtoVta.Dominio.BaseTrabajo.Funciones
{
    public static class FuncionesCadena
    {
        public static string Izquierda(string texto, int seleccion)
        {
            string resultado = texto.Substring(0, seleccion);

            return resultado;
        }

        public static string Derecha(string texto, int seleccion)
        {
            int valor = texto.Length - seleccion;

            string resultado = texto.Substring(valor, seleccion);

            return resultado;
        }

        public static string Extraer(string texto, int indiceInicial, int seleccion)
        {
            string resultado = texto.Substring(indiceInicial, seleccion);

            return resultado;
        }

        public static string Extraer(string texto, int indiceInicial)
        {
            string resultado = texto.Substring(indiceInicial);

            return resultado;
        }

        public static string Espacios(int cantidad)
        {
            return "".PadLeft(cantidad);
        }

        public static string Alineacion(
                        string pAlineacion, int pAnchoTicket,
                        int pLongitudTexto, string pTexto)
        {
            string nuevaAlineacion = string.Empty;

            if (pLongitudTexto > pAnchoTicket)
            {
                pLongitudTexto = pAnchoTicket;
                pTexto = pTexto.Substring(0, pAnchoTicket);
            }

            switch (pAlineacion)
            {
                case "D":
                    nuevaAlineacion = Espacios(pAnchoTicket - pLongitudTexto) + pTexto;

                    break;

                case "I":
                    nuevaAlineacion = pTexto + Espacios(pAnchoTicket - pLongitudTexto);

                    break;

                case "C":
                    nuevaAlineacion = Espacios((pAnchoTicket - pLongitudTexto) / 2) +
                        pTexto + Espacios((pAnchoTicket - pLongitudTexto) / 2);

                    break;

            }

            return nuevaAlineacion;

        }

        static string MascaraFormatoNumeros(int pNumerosDecimales)
        {
            string mascara = string.Empty;
            switch(pNumerosDecimales)
            {
                case 0:
                    mascara =  "#######0";
                    break;
                case 1:
                    mascara = "#######0.0";
                    break;
                case 2:
                    mascara = "#######0.00";
                    break;
                case 3:
                    mascara = "#######0.000";
                    break;
                case 4:
                    mascara = "#######0.0000";
                    break;
                case 5:
                    mascara = "#######0.00000";
                    break;
                case 6:
                    mascara = "#######0.000000";
                    break;
                case 7:
                    mascara = "#######0.0000000";
                    break;
                case 8:
                    mascara = "#######0.00000000";
                    break;
            }

            return mascara;
        }

        public static string FormatoDeNumero(decimal pNumero, int pDecimalesRedondeo)
        {
            return pNumero.ToString(MascaraFormatoNumeros(pDecimalesRedondeo));
        }        
    }
}
