using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.MensajesDominio;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class Articulo: Entidad
    {
        HashSet<ArticuloAlterno> _lineasArticuloAlterno;
        // HashSet<ArticuloDetalle> _lineasArticuloDetalle;
        HashSet<KardexHistorico> _lineasKardexHistorico;
        HashSet<InventarioFisico> _lineasInventarioFisico;        


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

        public virtual ICollection<KardexHistorico> KardexHistoricos
        {
            get
            {
                if (_lineasKardexHistorico == null)
                    _lineasKardexHistorico = new HashSet<KardexHistorico>();

                return _lineasKardexHistorico;
            }
            set
            {
                _lineasKardexHistorico = new HashSet<KardexHistorico>(value);
            }
        }

        public virtual ICollection<InventarioFisico> InventariosFisicos
        {
            get
            {
                if (_lineasInventarioFisico == null)
                    _lineasInventarioFisico = new HashSet<InventarioFisico>();

                return _lineasInventarioFisico;
            }
            set
            {
                _lineasInventarioFisico = new HashSet<InventarioFisico>(value);
            }
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

        public KardexHistorico AgregarNuevoKardexHistorico(
                            int pAnioHistorico, decimal pCantidadFinal00, decimal pCantidadFinal01,
                            decimal pCantidadFinal02, decimal pCantidadFinal03, decimal pCantidadFinal04,
                            decimal pCantidadFinal05, decimal pCantidadFinal06, decimal pCantidadFinal07,
                            decimal pCantidadFinal08, decimal pCantidadFinal09, decimal pCantidadFinal10,
                            decimal pCantidadFinal11, decimal pCantidadFinal12, decimal pCantidadFinal13,
                            decimal pCostoPromNacional00, decimal pCostoPromNacional01, decimal pCostoPromNacional02,
                            decimal pCostoPromNacional03, decimal pCostoPromNacional04, decimal pCostoPromNacional05,
                            decimal pCostoPromNacional06, decimal pCostoPromNacional07, decimal pCostoPromNacional08,
                            decimal pCostoPromNacional09, decimal pCostoPromNacional10, decimal pCostoPromNacional11,
                            decimal pCostoPromNacional12, decimal pCostoPromNacional13, decimal pCostoPromExtranjera00,
                            decimal pCostoPromExtranjera01, decimal pCostoPromExtranjera02, decimal pCostoPromExtranjera03,
                            decimal pCostoPromExtranjera04, decimal pCostoPromExtranjera05, decimal pCostoPromExtranjera06,
                            decimal pCostoPromExtranjera07, decimal pCostoPromExtranjera08, decimal pCostoPromExtranjera09,
                            decimal pCostoPromExtranjera10, decimal pCostoPromExtranjera11, decimal pCostoPromExtranjera12,
                            decimal pCostoPromExtranjera13, decimal pTotCostoPromNacional00, decimal pTotCostoPromNacional01,
                            decimal pTotCostoPromNacional02, decimal pTotCostoPromNacional03, decimal pTotCostoPromNacional04,
                            decimal pTotCostoPromNacional05, decimal pTotCostoPromNacional06, decimal pTotCostoPromNacional07,
                            decimal pTotCostoPromNacional08, decimal pTotCostoPromNacional09, decimal pTotCostoPromNacional10,
                            decimal pTotCostoPromNacional11, decimal pTotCostoPromNacional12, decimal pTotCostoPromNacional13,
                            decimal pTotCostoPromExtranjera00, decimal pTotCostoPromExtranjera01, decimal pTotCostoPromExtranjera02,
                            decimal pTotCostoPromExtranjera03, decimal pTotCostoPromExtranjera04, decimal pTotCostoPromExtranjera05,
                            decimal pTotCostoPromExtranjera06, decimal pTotCostoPromExtranjera07, decimal pTotCostoPromExtranjera08,
                            decimal pTotCostoPromExtranjera09, decimal pTotCostoPromExtranjera10, decimal pTotCostoPromExtranjera11,
                            decimal pTotCostoPromExtranjera12, decimal pTotCostoPromExtranjera13, string pCodigoAlmacen)
        {

            var nuevaLineaKardexHistorico = new KardexHistorico()
            {
                CodigoArticulo = this.CodigoArticulo,
                CodigoAlmacen = pCodigoAlmacen,
                AnioHistorico = pAnioHistorico,
                CantidadFinal00 = pCantidadFinal00,
                CantidadFinal01 = pCantidadFinal01,
                CantidadFinal02 = pCantidadFinal02,
                CantidadFinal03 = pCantidadFinal03,
                CantidadFinal04 = pCantidadFinal04,
                CantidadFinal05 = pCantidadFinal05,
                CantidadFinal06 = pCantidadFinal06,
                CantidadFinal07 = pCantidadFinal07,
                CantidadFinal08 = pCantidadFinal08,
                CantidadFinal09 = pCantidadFinal09,
                CantidadFinal10 = pCantidadFinal10,
                CantidadFinal11 = pCantidadFinal11,
                CantidadFinal12 = pCantidadFinal12,
                CantidadFinal13 = pCantidadFinal13,
                CostoPromNacional00 = pCostoPromNacional00,
                CostoPromNacional01 = pCostoPromNacional01,
                CostoPromNacional02 = pCostoPromNacional02,
                CostoPromNacional03 = pCostoPromNacional03,
                CostoPromNacional04 = pCostoPromNacional04,
                CostoPromNacional05 = pCostoPromNacional05,
                CostoPromNacional06 = pCostoPromNacional06,
                CostoPromNacional07 = pCostoPromNacional07,
                CostoPromNacional08 = pCostoPromNacional08,
                CostoPromNacional09 = pCostoPromNacional09,
                CostoPromNacional10 = pCostoPromNacional10,
                CostoPromNacional11 = pCostoPromNacional11,
                CostoPromNacional12 = pCostoPromNacional12,
                CostoPromNacional13 = pCostoPromNacional13,
                CostoPromExtranjera00 = pCostoPromExtranjera00,
                CostoPromExtranjera01 = pCostoPromExtranjera01,
                CostoPromExtranjera02 = pCostoPromExtranjera02,
                CostoPromExtranjera03 = pCostoPromExtranjera03,
                CostoPromExtranjera04 = pCostoPromExtranjera04,
                CostoPromExtranjera05 = pCostoPromExtranjera05,
                CostoPromExtranjera06 = pCostoPromExtranjera06,
                CostoPromExtranjera07 = pCostoPromExtranjera07,
                CostoPromExtranjera08 = pCostoPromExtranjera08,
                CostoPromExtranjera09 = pCostoPromExtranjera09,
                CostoPromExtranjera10 = pCostoPromExtranjera10,
                CostoPromExtranjera11 = pCostoPromExtranjera11,
                CostoPromExtranjera12 = pCostoPromExtranjera12,
                CostoPromExtranjera13 = pCostoPromExtranjera13,
                TotCostoPromNacional00 = pTotCostoPromNacional00,
                TotCostoPromNacional01 = pTotCostoPromNacional01,
                TotCostoPromNacional02 = pTotCostoPromNacional02,
                TotCostoPromNacional03 = pTotCostoPromNacional03,
                TotCostoPromNacional04 = pTotCostoPromNacional04,
                TotCostoPromNacional05 = pTotCostoPromNacional05,
                TotCostoPromNacional06 = pTotCostoPromNacional06,
                TotCostoPromNacional07 = pTotCostoPromNacional07,
                TotCostoPromNacional08 = pTotCostoPromNacional08,
                TotCostoPromNacional09 = pTotCostoPromNacional09,
                TotCostoPromNacional10 = pTotCostoPromNacional10,
                TotCostoPromNacional11 = pTotCostoPromNacional11,
                TotCostoPromNacional12 = pTotCostoPromNacional12,
                TotCostoPromNacional13 = pTotCostoPromNacional13,
                TotCostoPromExtranjera00 = pTotCostoPromExtranjera00,
                TotCostoPromExtranjera01 = pTotCostoPromExtranjera01,
                TotCostoPromExtranjera02 = pTotCostoPromExtranjera02,
                TotCostoPromExtranjera03 = pTotCostoPromExtranjera03,
                TotCostoPromExtranjera04 = pTotCostoPromExtranjera04,
                TotCostoPromExtranjera05 = pTotCostoPromExtranjera05,
                TotCostoPromExtranjera06 = pTotCostoPromExtranjera06,
                TotCostoPromExtranjera07 = pTotCostoPromExtranjera07,
                TotCostoPromExtranjera08 = pTotCostoPromExtranjera08,
                TotCostoPromExtranjera09 = pTotCostoPromExtranjera09,
                TotCostoPromExtranjera10 = pTotCostoPromExtranjera10,
                TotCostoPromExtranjera11 = pTotCostoPromExtranjera11,
                TotCostoPromExtranjera12 = pTotCostoPromExtranjera12,
                TotCostoPromExtranjera13 = pTotCostoPromExtranjera13


            };

            //Establecer la identidad
            nuevaLineaKardexHistorico.GenerarNuevaIdentidad();

            this.KardexHistoricos.Add(nuevaLineaKardexHistorico);

            return nuevaLineaKardexHistorico;


        }

        public void RecalcularStock(int pPermitirStockNegativo, int pMovAlmacenVentaIngresoOSalida,
            decimal pCantidadMovAlmacen)
        {
            var nuevoStock = 0M;

            //Solo si es Articulo inventariable
            if (this.EsInventariable)
            {
                //si es movimiento de salida de inventario por venta 
                if (pMovAlmacenVentaIngresoOSalida == 0)
                {
                    nuevoStock = this.ArticuloDetalle.StockActual - pCantidadMovAlmacen;

                    //validar stock actual vs cantidad movimiento Almacen 
                    if (nuevoStock < 0)
                    {
                        //No se premite stock negativo
                        if (pPermitirStockNegativo == 0)
                        {
                            throw new ArgumentException(string.Format(Mensajes.excepcion_CantidadVentaExcedeStockDisponibleArticulo,
                                            this.ArticuloDetalle.CodigoArticulo));
                        }
                    }


                }
                //si es movimiento de entrada
                else
                {
                    nuevoStock = this.ArticuloDetalle.StockActual + pCantidadMovAlmacen;
                }

                this.ArticuloDetalle.StockActual = nuevoStock;

            }
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

    
    }

}