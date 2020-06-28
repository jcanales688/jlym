
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.BaseTrabajo;

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
                string sqlActualizaArticulo = @"UPDATE IN_INVENTORY(SALESPERID, SALESPERNAME, IDENTITYDOC, PHONE, SEX, INITIALDATE, 
                                                            BIRTHDATE, PASSWORD, SITEID, STATUSPERSONID, USERID, ACCESSUSERID,  ADDRESS1, ADDRESS2) 
                                                            VALUES
                                                            (@SALESPERID, @SALESPERNAME, @IDENTITYDOC, @PHONE, @SEX, @INITIALDATE, 
                                                            @BIRTHDATE, @PASSWORD, @SITEID, @STATUSPERSONID, @USERID, @ACCESSUSERID, @ADDRESS1, @ADDRESS2)";

                var filasAfectadas = cn.Execute(sqlActualizaArticulo, new
                {
                    SALESPERID = pArticulo.CodigoArticulo
                });
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
                                            ,ICONO         AS Imagen
                                    FROM	IN_INVENTORY (NOLOCK)
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
                                    FROM	IN_INVENTORYDAT (NOLOCK)
                                    WHERE	SITEID      = @SITEID
                                            AND INVTIDSKU IN(SELECT	INVTIDSKU
                                                        FROM	IN_INVENTORY (NOLOCK)
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

        public Articulo ObtenerPorCodigo(string pCodigoArticulo)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	USERID		AS CodigoUsuarioDeSistema
                                            ,EXPIRED	AS FechaExpiracion
                                            ,USERNAME	AS DescripcionUsuario
                                            ,PASSWORD	AS Contraseña
                                            ,STATUS AS EsHabilitado
                                    FROM    SE_USERREC (NOLOCK)
                                    WHERE	USERID			= @USERID";

                var articulo = cn.QueryFirstOrDefault<Articulo>(cadenaSQL,
                                                new { USERID = pCodigoArticulo });

                if (articulo != null)
                {
                    return articulo;
                }
                else
                    return null;
            }
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