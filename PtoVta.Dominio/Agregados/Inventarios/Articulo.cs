using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class Articulo: Entidad
    {
        HashSet<ArticuloAlterno> _lineasArticuloAlterno;
        // HashSet<ArticuloDetalle> _lineasArticuloDetalle;
        
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
        public byte[] Imagen{get; set; }

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

        public string CodigoMarcaArticulo { get; private set; }
        public string CodigoImpuestoIsc { get; private set; }
        public string CodigoImpuestoIgv { get; private set; }
        public string CodigoCategoriaArticulo { get; private set; }
        public string CodigoSubCategoriaArticulo { get; private set; }
        public string CodigoTipoInventario { get; private set; }
        public string CodigoUnidadDeMedida { get; private set; }

        public ArticuloDetalle ArticuloDetalle { get; private set; }



        public void AgregarArticuloDetalle(decimal pStockMinimo,
                    decimal pStockMaximo,decimal pStockInicial,decimal pStockActual,
                    Nullable<DateTime> pFechaCreacion,Nullable<DateTime> pFechaUltimoInv,Nullable<decimal> pStockUltimoInv,
                    decimal pCostoPromedioNacional,decimal pCostoPromedioExtranjera,decimal pCostoReposicionNacional,
                    decimal pCostoReposicionExtranjera,decimal pCostoRepoNacionalUltimoInv,decimal pCostoRepoExtranjeraUltimoInv,
                    decimal pPrecio,string pCodContableInventariable,string pCodContableNoInventariable,
                    string pCodigoArticulo,string  pCodigoTipoPrecioInventario, string pCodigoAlmacen)
        {
            var articuloDetalle = new ArticuloDetalle(){
                    StockMinimo = pStockMinimo,
                    StockMaximo = pStockMaximo,
                    StockInicial = pStockInicial,
                    StockActual = pStockActual,
                    FechaCreacion = pFechaCreacion,
                    FechaUltimoInv = pFechaUltimoInv,
                    StockUltimoInv = pStockUltimoInv,
                    CostoPromedioNacional = pCostoPromedioNacional,
                    CostoPromedioExtranjera = pCostoPromedioExtranjera,
                    CostoReposicionNacional = pCostoReposicionNacional,
                    CostoReposicionExtranjera = pCostoReposicionExtranjera,
                    CostoRepoNacionalUltimoInv = pCostoRepoNacionalUltimoInv,
                    CostoRepoExtranjeraUltimoInv = pCostoRepoExtranjeraUltimoInv,
                    Precio = pPrecio,
                    CodContableInventariable = pCodContableInventariable,
                    CodContableNoInventariable = pCodContableNoInventariable,
                    CodigoArticulo = pCodigoArticulo,
                    CodigoTipoPrecioInventario = pCodigoTipoPrecioInventario,
                    CodigoAlmacen = pCodigoAlmacen
            };

            this.ArticuloDetalle = articuloDetalle;

        }

        public void EstablecerReferenciaMarcaArticuloDeArticulo(string pCodigoMarcaArticulo)
        {
            if (!string.IsNullOrEmpty(pCodigoMarcaArticulo))
            {
                //relacion
                this.CodigoMarcaArticulo = pCodigoMarcaArticulo;
                // this.MarcaArticulo = null;
            }
        }

        public void EstablecerReferenciaImpuestoIscDeArticulo(string pCodigoImpuestoIsc)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIsc))
            {
                //relacion
                this.CodigoImpuestoIsc = pCodigoImpuestoIsc;
                // this.ImpuestoIsc = null;
            }
        }

        public void EstablecerReferenciaImpuestoIgvDeArticulo(string pCodigoImpuestoIgv)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIgv))
            {
                //relacion
                this.CodigoImpuestoIgv = pCodigoImpuestoIgv;
                // this.ImpuestoIgv = null;
            }
        }



        public void EstablecerReferenciaCategoriaArticuloDeArticulo(string pCodigoCategoriaArticulo)
        {
            if (!string.IsNullOrEmpty(pCodigoCategoriaArticulo))
            {
                //relacion
                this.CodigoCategoriaArticulo = pCodigoCategoriaArticulo;
                // this.CategoriaArticulo = null;
            }
        }

        public void EstablecerReferenciaSubCategoriaArticuloDeArticulo(string pCodigoSubCategoriaArticulo)
        {
            if (!string.IsNullOrEmpty(pCodigoSubCategoriaArticulo))
            {
                //relacion
                this.CodigoSubCategoriaArticulo = pCodigoSubCategoriaArticulo;
                // this.SubCategoriaArticulo = null;
            }
        }

        public void EstablecerReferenciaTipoInventarioDeArticulo(string pCodigoTipoInventario)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoInventario))
            {
                //relacion
                this.CodigoTipoInventario = pCodigoTipoInventario;
                // this.TipoInventario = null;
            }
        }         


        public void EstablecerReferenciaUnidadDeMedidaDeArticulo(string pCodigoUnidadDeMedida)
        {
            if (!string.IsNullOrEmpty(pCodigoUnidadDeMedida))
            {
                //relacion
                this.CodigoUnidadDeMedida = pCodigoUnidadDeMedida;
                // this.UnidadDeMedida = null;
            }
        }



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

        // public virtual ICollection<ArticuloDetalle> ArticuloDetalles 
        // {
        //     get
        //     {
        //         if (_lineasArticuloDetalle == null)
        //             _lineasArticuloDetalle = new HashSet<ArticuloDetalle>();

        //         return _lineasArticuloDetalle;
        //     }
        //     set
        //     {
        //         _lineasArticuloDetalle = new HashSet<ArticuloDetalle>(value);
        //     }
        // }
    }

}