using System;
using System.Collections.Generic;

namespace PtoVta.Aplicacion.DTO.Inventarios
{
    public class ArticuloDTO
    {
        public Guid Id { get; set; }

        public string CodigoArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal FactorGalon { get; set; }
        public bool ParaVentaPlaya { get; set; }
        public bool ParaVentaTienda { get; set; }
        public bool ParaOtrasVentas { get; set; }
        public bool EsInventariable { get; set; }
        public bool EsFormula { get; set; }
        public decimal MargenUtilidad { get; set; }
        public bool BloqueadoParaCompra { get; set; }
        public bool BloqueadoParaVenta { get; set; }
        public bool EsConsignacion { get; set; }
        public bool EsDesensamble { get; set; }
        public string UsuarioSistema { get; set; }
        public bool ParaVentaManualEnPlaya { get; set; }               
        public bool EditarPrecio { get; set; }
        public byte[] Imagen{get; set; }
        

        public string CodigoMarcaArticulo { get;  set; }
        public string CodigoImpuestoIsc { get;  set; }
        public string CodigoImpuestoIgv { get;  set; }
        public string CodigoCategoriaArticulo { get;  set; }
        public string CodigoSubCategoriaArticulo { get;  set; }
        public string CodigoTipoInventario { get;  set; }
        public string CodigoUnidadDeMedida { get;  set; }


        public List<ArticuloAlternoDTO> ArticulosAlternos { get; set; }
        public ArticuloDetalleDTO ArticuloDetalle { get; set; }

    }

}