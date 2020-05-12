using System;
using System.Collections.Generic;

namespace PtoVta.Aplicacion.BaseTrabajo
{
    public class ResultadoServicio<TEntidad> where TEntidad : class
    {
        public int ResultadoId { get; private set; }

        public string DescripcionResultado { get; private set; }

        public string Detalles { get; private set; }

        public object Datos { get; private set; }
        // public IEnumerable<TEntidad> Datos { get; private set; }

        public ResultadoServicio(int pResultadoId, string pDescripcionResultado, 
                                            string pDetalles, object pDatos) //IEnumerable<TEntidad> pDatos
        {
            this.ResultadoId = pResultadoId;
            this.DescripcionResultado = pDescripcionResultado;
            this.Detalles = pDetalles;
            this.Datos = pDatos;
        }
    }
}