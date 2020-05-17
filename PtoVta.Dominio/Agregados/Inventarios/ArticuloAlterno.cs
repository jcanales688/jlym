using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class ArticuloAlterno: Entidad
    {
        public string CodigoArticuloAlterno { get; set; }
        public string DescripcionArticuloAlterno { get; set; }

        public string CodigoArticulo { get; set; }
        // public Guid ArticuloId { get; set; }


    }
}