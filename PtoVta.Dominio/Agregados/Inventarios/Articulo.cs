using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class Articulo: Entidad
    {
        HashSet<ArticuloAlterno> _lineasArticuloAlterno;
        HashSet<ArticuloDetalle> _lineasArticuloDetalle;
        
        bool _EsHabilitado;


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

        public bool EsHabilitado
        {
            get
            {
                return _EsHabilitado;
            }
            private set
            {
                _EsHabilitado = value;
            }
        }

        public string CodigoMarcaArticulo { get; set; }
        public string CodigoImpuestoIsc { get; set; }
        public string CodigoImpuestoIgv { get; set; }
        public string CodigoCategoriaArticulo { get; set; }
        public string CodigoSubCategoriaArticulo { get; set; }
        public string CodigoTipoInventario { get; set; }
        public string CodigoUnidadDeMedida { get; set; }
         




        public void Habilitar()
        {
            if (!EsHabilitado)
                this._EsHabilitado = true;

        }

        public void Deshabilitar()
        {
            if (EsHabilitado)
                this._EsHabilitado = false;
        }


        public virtual ICollection<ArticuloAlterno> ArticulosAlternos 
        {
            get
            {
                if (_lineasArticuloAlterno == null)
                    _lineasArticuloAlterno = new HashSet<ArticuloAlterno>();

                return _lineasArticuloAlterno;
            }
            set
            {
                _lineasArticuloAlterno = new HashSet<ArticuloAlterno>(value);
            }
        }

        public virtual ICollection<ArticuloDetalle> ArticuloDetalles 
        {
            get
            {
                if (_lineasArticuloDetalle == null)
                    _lineasArticuloDetalle = new HashSet<ArticuloDetalle>();

                return _lineasArticuloDetalle;
            }
            set
            {
                _lineasArticuloDetalle = new HashSet<ArticuloDetalle>(value);
            }
        }
    }

}