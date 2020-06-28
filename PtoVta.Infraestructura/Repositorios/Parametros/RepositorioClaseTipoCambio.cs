using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioClaseTipoCambio : Repositorio<ClaseTipoCambio>, IRepositorioClaseTipoCambio
    {
        public RepositorioClaseTipoCambio(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public ClaseTipoCambio ObtenerMontoTipoDeCambio(string pCodigoMonedaOrigen, string pCodigoMonedaDestino,
                                                        DateTime pFechaTipoDeCambio, string pCodigoClaseTipoCambio)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	CLASSID				AS CodigoCategoriaArticulo
                                            ,DESCR				AS DescripcionCategoriaArticulo
                                            ,INVTIDSOLOMON		AS CodigoContable
                                            ,DESCRSPANISH		AS Comentario
                                            ,BUSINESSTYPE		AS CodigoTipoNegocio
                                            ,ICONO              AS Imagen
                                    FROM	IN_CATEGORY		(NOLOCK) 
                                    WHERE	BUSINESSTYPE		= @BUSINESSTYPE
                                        
                                    SELECT	CLASSUBID			AS CodigoSubCategoriaArticulo
                                            ,DESCR				AS DescripcionSubCategoriaArticulo
                                            ,PORCENTDIFFERENCE	AS PorcentajeDiferencia
                                            ,CLASSID			AS CodigoCategoriaArticulo
                                            ,TYPEDOCFISIN		AS CodigoTipoMovInvFisIngreso
                                            ,TYPEDOCFISOUT		AS CodigoTipoMovInvFisSalida
                                            ,ICONO              AS Imagen                                            
                                    FROM	IN_SUBCATEGORY	(NOLOCK) 
                                    WHERE	CLASSID				IN(SELECT	CLASSID				
                                                                    FROM	IN_CATEGORY		(NOLOCK) 
                                                                    WHERE	BUSINESSTYPE		= @BUSINESSTYPE)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new
                                    {
                                        BUSINESSTYPE = pCodigoMonedaOrigen,
                                        BUSINESSTYPE2 = pCodigoMonedaDestino,
                                        BUSINESSTYPE4 = pFechaTipoDeCambio,
                                        BUSINESSTYPE5 = pCodigoClaseTipoCambio
                                    });

                var claseTipoDeCambio = resultado.Read<ClaseTipoCambio>().FirstOrDefault();
                var ultimoTipoDeCambio = resultado.Read<TipoDeCambio>().FirstOrDefault();

                if (claseTipoDeCambio != null)
                {
                    return MapeoClaseTipoCambio(claseTipoDeCambio, ultimoTipoDeCambio);
                }
                else
                    return null;
            }

            // var conjunto = unidadTrabajoActual.CrearConjunto<ClaseTipoCambio>();

            // var query = from ctc in conjunto.Include(t => t.TiposDeCambio)
            //                       from tc in ctc.TiposDeCambio
            //                       where ctc.Id == tc.ClaseTipoCambioId 
            //                           && tc.ClaseTipoCambioId == pClaseTipoCambioId
            //                           && tc.MonedaOrigenId == pMonedaOrigenId
            //                           && tc.MonedaDestinoId == pMonedaDestinoId
            //                           && tc.FechaTipoDeCambio == (from mxfecha in ctc.TiposDeCambio
            //                                                       where mxfecha.ClaseTipoCambioId == pClaseTipoCambioId &&
            //                                                               mxfecha.FechaTipoDeCambio <= pFechaTipoDeCambio
            //                                                       select mxfecha.FechaTipoDeCambio
            //                                                       ).Max()
            //                       select new 
            //                       {
            //                           Id = ctc.Id,
            //                           DescripcionClaseTipoCambio = ctc.DescripcionClaseTipoCambio,
            //                           EsHabilitado = ctc.EsHabilitado,
            //                           TiposDeCambio = new HashSet<TipoDeCambio>() { tc }

            //                       };


            // var consulta = query.FirstOrDefault();

            // //Convertir a entidad
            // var claseTipoCambioDia = new ClaseTipoCambio();
            // claseTipoCambioDia.CambiarIdentidadActual(consulta.Id);
            // claseTipoCambioDia.DescripcionClaseTipoCambio = consulta.DescripcionClaseTipoCambio;
            // if (consulta.EsHabilitado)
            // {
            //     claseTipoCambioDia.Habilitar();
            // }
            // else
            // {
            //     claseTipoCambioDia.Deshabilitar();
            // }
            // claseTipoCambioDia.TiposDeCambio = consulta.TiposDeCambio.ToList();

            // return claseTipoCambioDia;

        }

        public ClaseTipoCambio ObtenerPorCodigo(string pCodigoClaseTipoCambio)
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

                var claseDeTipoCambio = cn.QueryFirstOrDefault<ClaseTipoCambio>(cadenaSQL,
                                                        new { USERID = pCodigoClaseTipoCambio });

                if (claseDeTipoCambio != null)
                {
                    return claseDeTipoCambio;
                }
                else
                    return null;
            }
        }

        private ClaseTipoCambio MapeoClaseTipoCambio(ClaseTipoCambio pClaseTipoCambio, TipoDeCambio pTipoDeCambio)
        {
            return new ClaseTipoCambio();
        }
    }
}