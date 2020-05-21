using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class SubCategoriaArticulo : Entidad
    {
        public string CodigoSubCategoriaArticulo { get; set; }
        public string DescripcionSubCategoriaArticulo { get; set; }
        public decimal PorcentajeDiferencia { get; set; }


        public string CodigoCategoriaArticulo { get; set; }

        public string CodigoTipoMovInvFisIngreso { get; set; }
        public string CodigoTipoMovInvFisSalida { get; set; }
        public byte[] Imagen{get; set; }

    }
}    