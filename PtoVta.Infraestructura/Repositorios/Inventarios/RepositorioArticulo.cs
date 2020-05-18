
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
                string cadenaSQL = @"SELECT	CLASSID				AS CodigoCategoriaArticulo
                                            ,DESCR				AS DescripcionCategoriaArticulo
                                            ,INVTIDSOLOMON		AS CodigoContable
                                            ,DESCRSPANISH		AS Comentario
                                            ,BUSINESSTYPE		AS CodigoTipoNegocio
                                    FROM	PC_IN_CATEGORY		(NOLOCK) 
                                    WHERE	BUSINESSTYPE		= @BUSINESSTYPE
                                        
                                    SELECT	CLASSUBID			AS CodigoSubCategoriaArticulo
                                            ,DESCR				AS DescripcionSubCategoriaArticulo
                                            ,PORCENTDIFFERENCE	AS PorcentajeDiferencia
                                            ,CLASSID			AS CodigoCategoriaArticulo
                                            ,TYPEDOCFISIN		AS CodigoTipoMovInvFisIngreso
                                            ,TYPEDOCFISOUT		AS CodigoTipoMovInvFisSalida
                                    FROM	PC_IN_SUBCATEGORY	(NOLOCK) 
                                    WHERE	CLASSID				IN(SELECT	CLASSID				
                                                                    FROM	PC_IN_CATEGORY		(NOLOCK) 
                                                                    WHERE	BUSINESSTYPE		= @BUSINESSTYPE)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { BUSINESSTYPE = pTipoNegocio});

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