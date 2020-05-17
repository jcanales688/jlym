using System;
using System.Collections.Generic;

namespace PtoVta.Aplicacion.DTO.Parametros
{
    public class CategoriaArticuloDTO
    {
        public Guid Id { get; set; }

        public string CodigoCategoriaArticulo { get; set; }
        public string DescripcionCategoriaArticulo { get; set; }
        public string CodigoContable { get; set; }

        public string CodigoTipoNegocio { get; private set; }

        public bool EsHabilitado { get; set; }

        public List<SubCategoriaArticuloDTO> SubCategoriasArticulo { get; set; }      
    }
}
