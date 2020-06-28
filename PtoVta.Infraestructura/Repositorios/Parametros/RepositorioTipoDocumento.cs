using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioTipoDocumento : Repositorio<TipoDocumento>, IRepositorioTipoDocumento
    {
        public RepositorioTipoDocumento(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override void Modificar(TipoDocumento pTipoDocumento)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string sqlActualizaTipoDocumento = @"UPDATE INTO OP_SALESPERSON(SALESPERID, SALESPERNAME, IDENTITYDOC, PHONE, SEX, INITIALDATE, 
                                                            BIRTHDATE, PASSWORD, SITEID, STATUSPERSONID, USERID, ACCESSUSERID,  ADDRESS1, ADDRESS2) 
                                                            VALUES
                                                            (@SALESPERID, @SALESPERNAME, @IDENTITYDOC, @PHONE, @SEX, @INITIALDATE, 
                                                            @BIRTHDATE, @PASSWORD, @SITEID, @STATUSPERSONID, @USERID, @ACCESSUSERID, @ADDRESS1, @ADDRESS2)";

                var filasAfectadas = cn.Execute(sqlActualizaTipoDocumento, new
                {
                    SALESPERID = pTipoDocumento.CodigoTipoDocumento
                });
            }
        }

        public TipoDocumento ObtenerCorrelativoDocumento(string pCodigoAlmacen, string pCodigoConfiguracionPuntoVenta,
                                                        string pCodigoTipoDocumento, string pTipoDeVenta, int pEstado)
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
                                        BUSINESSTYPE = pCodigoAlmacen,
                                        BUSINESSTYPEq = pCodigoConfiguracionPuntoVenta,
                                        BUSINESSTYPEw = pCodigoTipoDocumento,
                                        BUSINESSTYPEe = pTipoDeVenta,
                                        BUSINESSTYPEr = pEstado
                                    });

                var tipoDocumento = resultado.Read<TipoDocumento>().FirstOrDefault();
                var correlativosDocumento = resultado.Read<CorrelativoDocumento>().ToList();

                if (tipoDocumento != null)
                {
                    return MapeoTipoDocumento(tipoDocumento, correlativosDocumento);
                }
                else
                    return null;
            }

            // var conjuntoTipoDocumento = unidadTrabajoActual.CrearConjunto<TipoDocumento>();

            // var correlativoDocumento = (from ctd in conjuntoTipoDocumento.Include(c => c.CorrelativosDocumento)
            //     from cd in ctd.CorrelativosDocumento
            //     where ctd.Id == cd.TipoDocumentoId
            //           && cd.AlmacenId == pCodigoAlmacen
            //           && cd.ConfiguracionPuntoVentaId == pCodigoConfiguracionPuntoVenta
            //           && cd.TipoDocumentoId == pCodigoTipoDocumento
            //           && cd.TipoDeVenta == pTipoDeVenta
            //           && cd.Estado == pEstado
            //     select ctd).FirstOrDefault();

            // return correlativoDocumento;
        }

        public TipoDocumento ObtenerPorCodigo(string pCodigoTipoDocumento)
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

                var tipoDocumento = cn.QueryFirstOrDefault<TipoDocumento>(cadenaSQL,
                                                new { USERID = pCodigoTipoDocumento });

                if (tipoDocumento != null)
                {
                    return tipoDocumento;
                }
                else
                    return null;
            }
        }


        private TipoDocumento MapeoTipoDocumento(TipoDocumento pTipoDocumento, List<CorrelativoDocumento> pCorrelativosDocumento)
        {
            return new TipoDocumento();
        }
    }
}