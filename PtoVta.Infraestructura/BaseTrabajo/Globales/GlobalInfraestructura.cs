using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Infraestructura.BaseTrabajo.Globales
{
    public class GlobalInfraestructura
    {
        public struct BaseDatos
        {
            public const string PrefijoTabla = "PC_";
            public const string PrefijoTablaProduccion = "";
            public const string PrefijoTablaDesarrollo = "PC_";
        }

        public struct CamposTabla{
            public const string NombreCampoImagen = "IMAGEN";
            public const string NombreCampoImagenProduccion = "ICONO";
            public const string NombreCampoImagenDesarrollo = "IMAGEN";
        }

        public struct NombresTabla{
            public const string TablaVentas = "VENTAS";
            public const string TablaVentasProduccion = "OP_SALES";
            public const string TablaVentasDesarrollo = "VENTAS";

            public const string TablaVentasDetalle = "VENTAS_DET";
            public const string TablaVentasDetalleProduccion = "OP_SALES_DET";
            public const string TablaVentasDetalleDesarrollo = "VENTAS_DET";            
        }
    }

}