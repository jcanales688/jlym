
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

namespace PtoVta.Infraestructura.Repositorios.Inventarios
{
    public class RepositorioArticulo : Repositorio<Articulo>, IRepositorioArticulo
    {
        public RepositorioArticulo(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override void Modificar(Articulo pArticulo)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string sqlActualizaArticuloDetalle = @"UPDATE	" + BaseDatos.PrefijoTabla + @"IN_INVENTORYDAT
                                                        SET		QTYAVAIL		= @QTYAVAIL
                                                        WHERE	SITEID			= @SITEID
                                                                AND INVTIDSKU	= @INVTIDSKU";

                var filasAfectadas = cn.Execute(sqlActualizaArticuloDetalle, new
                {
                    INVTIDSKU = pArticulo.CodigoArticulo,
                    SITEID = pArticulo.ArticuloDetalle.CodigoAlmacen,                        
                    QTYAVAIL = pArticulo.ArticuloDetalle.StockActual
                });
                
                // cn.Open();
                // using (var transaccion = cn.BeginTransaction())
                // {
                //     string sqlActualizaArticuloDetalle = @"UPDATE	PC_IN_INVENTORYDAT
                //                                             SET		QTYAVAIL		= @QTYAVAIL
                //                                             WHERE	SITEID			= @SITEID
                //                                                     AND INVTIDSKU	= @INVTIDSKU";

                //     var filasAfectadasActualizaArticuloDetalle = cn.Execute(sqlActualizaArticuloDetalle, new
                //     {
                //         INVTIDSKU = pArticulo.CodigoArticulo,
                //         SITEID = pArticulo.ArticuloDetalle.CodigoAlmacen,                        
                //         QTYAVAIL = pArticulo.ArticuloDetalle.StockActual
                //     }, transaction: transaccion);

                //     transaccion.Commit();
                // }
            }
        }

        public IEnumerable<Articulo> ObtenerPorCategoriaYSubcategoria(string pCodigoCategoria, string pCodigoSubCategoria, string pCodigoAlmacen)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	INVTIDSKU	AS CodigoArticulo
                                            ,DESCR		AS DescripcionArticulo
                                            ,0			AS FactorGalon
                                            ,STKFUELS	AS ParaVentaPlaya
                                            ,STKSTORE	AS ParaVentaTienda
                                            ,STKOTHER	AS ParaOtrasVentas
                                            ,STKITEM	AS EsInventariable
                                            ,KIT		AS EsFormula
                                            ,DISCMARGIN	AS MargenUtilidad
                                            ,STKRECEPT	AS BloqueadoParaCompra
                                            ,STKSALES	AS BloqueadoParaVenta
                                            ,CONSIGNMENT	AS EsConsignacion
                                            ,DISASSEMBLE	AS EsDesensamble
                                            ,USERID			AS UsuarioSistema
                                            ,0				AS ParaVentaManualEnPlaya
                                            ,0				AS EditarPrecio
                                            ,''				AS CodigoMarcaArticulo
                                            ,TAXIGV			AS CodigoImpuestoIsc
                                            ,TAXISC			AS CodigoImpuestoIgv
                                            ,CLASSID		AS CodigoCategoriaArticulo
                                            ,CLASSUBID		AS CodigoSubCategoriaArticulo
                                            ,INVTYPEID		AS CodigoTipoInventario
                                            ,STKUNITID		AS CodigoUnidadDeMedida
                                            ," + CamposTabla.NombreCampoImagen + @" AS Imagen
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_INVENTORY (NOLOCK)
                                    WHERE	CLASSID			= @CLASSID
                                            AND CLASSUBID	= @CLASSUBID;

                                    SELECT	STOCKMIN		AS StockMinimo
                                            ,STOCKMAX		AS StockMaximo
                                            ,STOCKSTAR		AS StockInicial
                                            ,QTYAVAIL		AS StockActual
                                            ,GETDATE()		AS FechaCreacion
                                            ,ENDDATEINV		AS FechaUltimoInv
                                            ,LASTINVFIS		AS StockUltimoInv
                                            ,AVGCOSTPEN		AS CostoPromedioNacional
                                            ,AVGCOSTUSD		AS CostoPromedioExtranjera
                                            ,STDCOSTPEN		AS CostoReposicionNacional
                                            ,STDCOSTUSD		AS CostoReposicionExtranjera
                                            ,LASTSTDCOSTPEN	AS CostoRepoNacionalUltimoInv
                                            ,LASTSTDCOSTUSD	AS CostoRepoExtranjeraUltimoInv
                                            ,SLS_PRICE		AS Precio
                                            ,INVTIDSTK		AS CodContableInventariable
                                            ,INVTIDNOSTK	AS CodContableNoInventariable
                                            ,INVTIDSKU		AS CodigoArticulo
                                            ,PRECLVID		AS CodigoTipoPrecioInventario
                                            ,SITEID			AS CodigoAlmacen
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_INVENTORYDAT (NOLOCK)
                                    WHERE	SITEID      = @SITEID
                                            AND INVTIDSKU IN(SELECT	INVTIDSKU
                                                        FROM	" + BaseDatos.PrefijoTabla + @"IN_INVENTORY (NOLOCK)
                                                        WHERE	CLASSID			= @CLASSID
                                                                AND CLASSUBID	= @CLASSUBID)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { CLASSID = pCodigoCategoria, CLASSUBID = pCodigoSubCategoria, SITEID = pCodigoAlmacen });

                var articulos = resultado.Read<Articulo>().ToList();
                var articuloDetalles = resultado.Read<ArticuloDetalle>().ToList();

                if (articulos != null && articuloDetalles != null)
                {
                    return MapeoArticuloListado(articulos, articuloDetalles);
                }
                else
                    return null;
            }
        }

        public Articulo ObtenerPorCodigo(string pCodigoArticulo, string pCodigoAlmacen)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	INVTIDSKU	AS CodigoArticulo
                                            ,DESCR		AS DescripcionArticulo
                                            ,0			AS FactorGalon
                                            ,STKFUELS	AS ParaVentaPlaya
                                            ,STKSTORE	AS ParaVentaTienda
                                            ,STKOTHER	AS ParaOtrasVentas
                                            ,STKITEM	AS EsInventariable
                                            ,KIT		AS EsFormula
                                            ,DISCMARGIN	AS MargenUtilidad
                                            ,STKRECEPT	AS BloqueadoParaCompra
                                            ,STKSALES	AS BloqueadoParaVenta
                                            ,CONSIGNMENT	AS EsConsignacion
                                            ,DISASSEMBLE	AS EsDesensamble
                                            ,USERID			AS UsuarioSistema
                                            ,0				AS ParaVentaManualEnPlaya
                                            ,0				AS EditarPrecio
                                            ,USER3          AS PermitirStockNegativo
                                            ,''				AS CodigoMarcaArticulo
                                            ,TAXIGV			AS CodigoImpuestoIsc
                                            ,TAXISC			AS CodigoImpuestoIgv
                                            ,CLASSID		AS CodigoCategoriaArticulo
                                            ,CLASSUBID		AS CodigoSubCategoriaArticulo
                                            ,INVTYPEID		AS CodigoTipoInventario
                                            ,STKUNITID		AS CodigoUnidadDeMedida
                                            ," + CamposTabla.NombreCampoImagen + @" AS Imagen
                                            ,STATUS         AS EsHabilitado
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_INVENTORY (NOLOCK)
                                    WHERE	INVTIDSKU		= @INVTIDSKU;

                                    SELECT	STOCKMIN		AS StockMinimo
                                            ,STOCKMAX		AS StockMaximo
                                            ,STOCKSTAR		AS StockInicial
                                            ,QTYAVAIL		AS StockActual
                                            ,GETDATE()		AS FechaCreacion
                                            ,ENDDATEINV		AS FechaUltimoInv
                                            ,LASTINVFIS		AS StockUltimoInv
                                            ,AVGCOSTPEN		AS CostoPromedioNacional
                                            ,AVGCOSTUSD		AS CostoPromedioExtranjera
                                            ,STDCOSTPEN		AS CostoReposicionNacional
                                            ,STDCOSTUSD		AS CostoReposicionExtranjera
                                            ,LASTSTDCOSTPEN	AS CostoRepoNacionalUltimoInv
                                            ,LASTSTDCOSTUSD	AS CostoRepoExtranjeraUltimoInv
                                            ,SLS_PRICE		AS Precio
                                            ,INVTIDSTK		AS CodContableInventariable
                                            ,INVTIDNOSTK	AS CodContableNoInventariable
                                            ,INVTIDSKU		AS CodigoArticulo
                                            ,PRECLVID		AS CodigoTipoPrecioInventario
                                            ,SITEID			AS CodigoAlmacen
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_INVENTORYDAT (NOLOCK)
                                    WHERE	INVTIDSKU           = @INVTIDSKU
                                            AND SITEID          = @SITEID;
                                            
                                    SELECT	INVTIDSKU		AS CodigoArticulo
                                            ,SITEID			AS CodigoAlmacen
                                            ,CLASSID		AS CodigoCategoriaArticulo
                                            ,CLASSUBID		AS CodigoSubCategoriaArticulo
                                            ,STOCKPHISICAL	AS StockFisico
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_INVENTPHISICAL (NOLOCK)
                                    WHERE	SITEID			= @SITEID
                                            AND INVTIDSKU	= @INVTIDSKU";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { INVTIDSKU = pCodigoArticulo, SITEID = pCodigoAlmacen });

                var articulo = resultado.Read<Articulo>().FirstOrDefault();
                var articuloDetalle = resultado.Read<ArticuloDetalle>().FirstOrDefault();
                var inventarioFisicos = resultado.Read<InventarioFisico>().ToList();

                if (articulo != null && articuloDetalle != null)
                {
                    return MapeoArticulo(articulo, articuloDetalle, inventarioFisicos);
                }
                else
                    return null;
            }
        }


        private Articulo MapeoArticulo(Articulo pArticulo, ArticuloDetalle pArticuloDetalle,
                                                    List<InventarioFisico> pInventarioFisicos)
        {
            var articuloBuscado = new Articulo();
            articuloBuscado = pArticulo;

            articuloBuscado.AgregarArticuloDetalle(pArticuloDetalle.StockMinimo,
                                                    pArticuloDetalle.StockMaximo,
                                                    pArticuloDetalle.StockInicial,
                                                    pArticuloDetalle.StockActual,
                                                    pArticuloDetalle.FechaCreacion,
                                                    pArticuloDetalle.FechaUltimoInv,
                                                    pArticuloDetalle.StockUltimoInv,
                                                    pArticuloDetalle.CostoPromedioNacional,
                                                    pArticuloDetalle.CostoPromedioExtranjera,
                                                    pArticuloDetalle.CostoReposicionNacional,
                                                    pArticuloDetalle.CostoReposicionExtranjera,
                                                    pArticuloDetalle.CostoRepoNacionalUltimoInv,
                                                    pArticuloDetalle.CostoRepoExtranjeraUltimoInv,
                                                    pArticuloDetalle.Precio,
                                                    pArticuloDetalle.CodContableInventariable,
                                                    pArticuloDetalle.CodContableNoInventariable,
                                                    pArticuloDetalle.CodigoArticulo,
                                                    pArticuloDetalle.CodigoTipoPrecioInventario,
                                                    pArticuloDetalle.CodigoAlmacen);

            if (pInventarioFisicos != null)
            {
                foreach (var inventarioFisico in pInventarioFisicos)
                {
                    articuloBuscado.AgregarInventarioFisico(inventarioFisico.StockFisico, inventarioFisico.CodigoAlmacen,
                                                            inventarioFisico.CodigoCategoriaArticulo, inventarioFisico.CodigoSubCategoriaArticulo);
                }
            }



            return articuloBuscado;
        }

        private List<Articulo> MapeoArticuloListado(List<Articulo> pArticulos, List<ArticuloDetalle> pArticuloDetalleAsociado)
        {
            var articulosSeleccionados = new List<Articulo>();

            foreach (var articulo in pArticulos)
            {
                var articuloAAgregar = new Articulo()
                {
                    CodigoArticulo = articulo.CodigoArticulo,
                    DescripcionArticulo = articulo.DescripcionArticulo,
                    FactorGalon = articulo.FactorGalon,
                    ParaVentaPlaya = articulo.ParaVentaPlaya,
                    ParaVentaTienda = articulo.ParaVentaTienda,
                    ParaOtrasVentas = articulo.ParaOtrasVentas,
                    EsInventariable = articulo.EsInventariable,
                    EsFormula = articulo.EsFormula,
                    MargenUtilidad = articulo.MargenUtilidad,
                    BloqueadoParaCompra = articulo.BloqueadoParaCompra,
                    BloqueadoParaVenta = articulo.BloqueadoParaVenta,
                    EsConsignacion = articulo.EsConsignacion,
                    EsDesensamble = articulo.EsDesensamble,
                    UsuarioSistema = articulo.UsuarioSistema,
                    ParaVentaManualEnPlaya = articulo.ParaVentaManualEnPlaya,
                    EditarPrecio = articulo.EditarPrecio,
                    Imagen = articulo.Imagen
                };

                articuloAAgregar.EstablecerReferenciaMarcaArticuloDeArticulo(articulo.CodigoMarcaArticulo);
                articuloAAgregar.EstablecerReferenciaImpuestoIscDeArticulo(articulo.CodigoImpuestoIsc);
                articuloAAgregar.EstablecerReferenciaImpuestoIgvDeArticulo(articulo.CodigoImpuestoIgv);
                articuloAAgregar.EstablecerReferenciaCategoriaArticuloDeArticulo(articulo.CodigoCategoriaArticulo);
                articuloAAgregar.EstablecerReferenciaSubCategoriaArticuloDeArticulo(articulo.CodigoSubCategoriaArticulo);
                articuloAAgregar.EstablecerReferenciaTipoInventarioDeArticulo(articulo.CodigoTipoInventario);
                articuloAAgregar.EstablecerReferenciaUnidadDeMedidaDeArticulo(articulo.CodigoUnidadDeMedida);


                var articuloDetalleAsociado = pArticuloDetalleAsociado
                                    .Where(w => w.CodigoArticulo == articulo.CodigoArticulo).FirstOrDefault();

                if (articuloDetalleAsociado != null)
                {
                    articuloAAgregar.AgregarArticuloDetalle(articuloDetalleAsociado.StockMinimo,
                                                            articuloDetalleAsociado.StockMaximo,
                                                            articuloDetalleAsociado.StockInicial,
                                                            articuloDetalleAsociado.StockActual,
                                                            articuloDetalleAsociado.FechaCreacion,
                                                            articuloDetalleAsociado.FechaUltimoInv,
                                                            articuloDetalleAsociado.StockUltimoInv,
                                                            articuloDetalleAsociado.CostoPromedioNacional,
                                                            articuloDetalleAsociado.CostoPromedioExtranjera,
                                                            articuloDetalleAsociado.CostoReposicionNacional,
                                                            articuloDetalleAsociado.CostoReposicionExtranjera,
                                                            articuloDetalleAsociado.CostoRepoNacionalUltimoInv,
                                                            articuloDetalleAsociado.CostoRepoExtranjeraUltimoInv,
                                                            articuloDetalleAsociado.Precio,
                                                            articuloDetalleAsociado.CodContableInventariable,
                                                            articuloDetalleAsociado.CodContableNoInventariable,
                                                            articuloDetalleAsociado.CodigoArticulo,
                                                            articuloDetalleAsociado.CodigoTipoPrecioInventario,
                                                            articuloDetalleAsociado.CodigoAlmacen);
                }

                articulosSeleccionados.Add(articuloAAgregar);
            }

            return articulosSeleccionados;
        }
    }

}