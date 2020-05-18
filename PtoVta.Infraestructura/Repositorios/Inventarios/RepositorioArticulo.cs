
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Colaborador
{
    public class RepositorioArticulo : Repositorio<Articulo>, IRepositorioArticulo
    {
        public RepositorioArticulo(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public IEnumerable<Articulo> ObtenerPorCategoriaYSubcategoria(string pCodigoCategoria, string pCodigoSubCategoria)
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
                                    FROM	PC_IN_INVENTORY (NOLOCK)
                                    WHERE	CLASSID			= @CLASSID
                                            AND CLASSUBID	= @CLASSUBID;

                                    SELECT	STOCKMIN		AS StockMinimo
                                            ,STOCKMAX		AS StockMaximo
                                            ,STOCKSTAR		AS StockInicial
                                            ,QTYAVAIL		AS StockActual
                                            ,GETDATE()		AS FechaCreacion
                                            ,STARTDATEINV	AS FechaUltimoInv
                                            ,ENDDATEINV		AS StockUltimoInv
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
                                    FROM	PC_IN_INVENTORYDAT (NOLOCK)
                                    WHERE	INVTIDSKU IN(SELECT	INVTIDSKU
                                                        FROM	PC_IN_INVENTORY (NOLOCK)
                                                        WHERE	CLASSID			= @CLASSID
                                                                AND CLASSUBID	= @CLASSUBID)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { CLASSID = pCodigoCategoria, CLASSUBID = pCodigoSubCategoria});

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


        private List<Articulo> MapeoArticuloListado(List<Articulo> pArticulos, List<ArticuloDetalle> pArticuloDetalles)
        {
            var articulosSeleccionados = new List<Articulo>();

            foreach (var articulo in pArticulos)
            {
                var articuloAAgregar = new Articulo(){
                    CodigoArticulo = articulo.CodigoCategoriaArticulo,
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
                    CodigoMarcaArticulo = articulo.CodigoMarcaArticulo,
                    CodigoImpuestoIsc = articulo.CodigoImpuestoIsc,
                    CodigoImpuestoIgv = articulo.CodigoImpuestoIgv,
                    CodigoCategoriaArticulo = articulo.CodigoCategoriaArticulo,
                    CodigoSubCategoriaArticulo =articulo.CodigoSubCategoriaArticulo,
                    CodigoTipoInventario = articulo.CodigoTipoInventario,
                    CodigoUnidadDeMedida = articulo.CodigoUnidadDeMedida
                };
            
                var articuloDetallesAsociadas = pArticuloDetalles
                                    .Where(w => w.CodigoArticulo == articulo.CodigoArticulo);

                if(articuloDetallesAsociadas != null && articuloDetallesAsociadas.Any())
                {
                    foreach (var articuloDetalle in articuloDetallesAsociadas)
                    {
                        articuloAAgregar.ArticuloDetalles.Add(articuloDetalle);                    
                    }   
                }

                articulosSeleccionados.Add(articuloAAgregar);
            }

            return articulosSeleccionados;
        }                 
    }

}