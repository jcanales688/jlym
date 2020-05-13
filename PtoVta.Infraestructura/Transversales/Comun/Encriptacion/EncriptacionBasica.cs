using System;
using System.Text;

namespace PtoVta.Infraestructura.Transversales.Comun
{
    public class EncriptacionBasica
    {
        public static string EncriptarYDesencriptar(string X) 
        {
            string Encrip = String.Empty;
            int i = 0;
            string aux = String.Empty;
            string Result = String.Empty;
            string ult = String.Empty;

            // ISSUE: Potential Substring problem; VB6 Original: Right(X, 1)
            ult = X.Substring(X.Length - 1);
            aux = "";
            Result = "";
            i = 1;

            if (ult != "-")
            {
                // si es que no esta encriptado
                // Primero vamos a cambiar de lugar a la cadena.
                // Ej: "DAEBFC" -> "ABCDEFG"
                // Primero los pares, luego los impares
                while(X.Length > i)
                {
                    // ISSUE: Potential Substring problem; VB6 Original: Left(X, i + 1)
                    // ISSUE: Potential Substring problem; VB6 Original: Right(X.Substring(0, i + 1), 1)
                    aux = aux + (X.Substring(0, i + 1)).Substring((X.Substring(0, i + 1)).Length - 1);
                    i = i + 2;
                } 
                i = 1;
                while(X.Length >= i)
                {
                    // ISSUE: Potential Substring problem; VB6 Original: Left(X, i)
                    // ISSUE: Potential Substring problem; VB6 Original: Right(X.Substring(0, i), 1)
                    aux = aux + (X.Substring(0, i)).Substring((X.Substring(0, i)).Length - 1);
                    i = i + 2;
                } 

                // Segundo, Vamos a manipular los ASCII
                i = 1;
                while(aux.Length >= i)
                {
                    // ISSUE: Potential Substring problem; VB6 Original: Left(aux, i)
                    // ISSUE: Potential Substring problem; VB6 Original: Right(aux.Substring(0, i), 1)
                    Result = Result + Convert.ToString((char)((short)((aux.Substring(0, i)).Substring((aux.Substring(0, i)).Length - 1)[0]) - 10));
                    i = i + 1;
                } 
                Encrip = Result + "-";

            }
            else
            {
                // Si es que ya esta encriptado
                // ISSUE: Potential Substring problem; VB6 Original: Left(X, X.Length - 1)
                X = X.Substring(0, X.Length - 1);

                // Primero vamos a devolver el orden inicial
                // Ej: ABCDEFG -> DAEBFC
                while(X.Length / 2 + 1 >= i)
                {
                    if (X.Length / 2 + 1 > i || X.Length % 2 != 0)
                    {
                        // ISSUE: Potential Substring problem; VB6 Original: Left(X, (X.Length / 2) + i)
                        // ISSUE: Potential Substring problem; VB6 Original: Right(X.Substring(0, (X.Length / 2) + i), 1)
                        aux = aux + (X.Substring(0, (X.Length / 2) + i)).Substring((X.Substring(0, (X.Length / 2) + i)).Length - 1);
                    }

                    if (X.Length / 2 >= i)
                    {
                        // ISSUE: Potential Substring problem; VB6 Original: Left(X, i)
                        // ISSUE: Potential Substring problem; VB6 Original: Right(X.Substring(0, i), 1)
                        aux = aux + (X.Substring(0, i)).Substring((X.Substring(0, i)).Length - 1);
                    }

                    i = i + 1;
                } 
                i = 1;
                while(aux.Length >= i)
                {
                    // ISSUE: Potential Substring problem; VB6 Original: Left(aux, i)
                    // ISSUE: Potential Substring problem; VB6 Original: Right(aux.Substring(0, i), 1)
                    Result = Result + Convert.ToString((char)((short)((aux.Substring(0, i)).Substring((aux.Substring(0, i)).Length - 1)[0]) + 10));
                    i = i + 1;
                } 
                Encrip = Result;
            }

            return Encrip;
        } 


    }
}