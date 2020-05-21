using System;
using System.Collections.Generic;

namespace PtoVta.Aplicacion.DTO.Parametros
{
    public class SubCategoriaArticuloDTO
    {
        public Guid Id { get; set; }

        public string CodigoSubCategoriaArticulo { get; set; }
        public string DescripcionSubCategoriaArticulo { get; set; }
        public decimal PorcentajeDiferencia { get; set; }

        public string CodigoCategoriaArticulo { get; set; }
        public byte[] Imagen{get; set; }
    }

}