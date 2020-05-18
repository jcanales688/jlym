using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Modulo
{
    public class RepositorioCategoriaArticulo : Repositorio<CategoriaArticulo>, IRepositorioCategoriaArticulo
    {
        public RepositorioCategoriaArticulo(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public IEnumerable<CategoriaArticulo> ObtenerTodos(string pTipoNegocio)
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

                var categorias = resultado.Read<CategoriaArticulo>().ToList();                        
                var subCategorias = resultado.Read<SubCategoriaArticulo>().ToList();                        
                if (categorias != null || subCategorias != null)
                {
                    return MapeoCategoriaListado(categorias, subCategorias);
                }
                else
                    return null;
            }
        }

        public CategoriaArticulo ObtenerPorCodigo(string pCodigoCategoriaArticulo)
        {
            throw new NotImplementedException();
        }


        private List<CategoriaArticulo> MapeoCategoriaListado(List<CategoriaArticulo> pCategorias, List<SubCategoriaArticulo> pSubCategorias)
        {
            var categoriasSeleccionadas = new List<CategoriaArticulo>();

            foreach (var categoria in pCategorias)
            {
                var categoriaAAgregar = new CategoriaArticulo(){
                    CodigoCategoriaArticulo = categoria.CodigoCategoriaArticulo,
                    DescripcionCategoriaArticulo = categoria.DescripcionCategoriaArticulo,
                    CodigoContable = categoria.CodigoContable,
                    Comentario = categoria.Comentario,
                    CodigoTipoNegocio = categoria.CodigoTipoNegocio
                };
            
                var subCategoriasAsociadas = pSubCategorias
                                    .Where(w => w.CodigoCategoriaArticulo == categoria.CodigoCategoriaArticulo);

                if(subCategoriasAsociadas != null && subCategoriasAsociadas.Any())
                {
                    foreach (var subCategoria in subCategoriasAsociadas)
                    {
                        categoriaAAgregar.SubCategoriasArticulo.Add(subCategoria);                    
                    }   
                }

                categoriasSeleccionadas.Add(categoriaAAgregar);
            }

            return categoriasSeleccionadas;
        }           
    }

}