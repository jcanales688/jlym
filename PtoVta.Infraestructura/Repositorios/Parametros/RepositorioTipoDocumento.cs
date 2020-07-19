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

        public void ActualizarCorrelativoDocumento(TipoDocumento pTipoDocumento, string pCodigoAlmacen, string pNumeroSerie)
        {
            var correlativoUtilizado = pTipoDocumento.CorrelativosDocumento.FirstOrDefault(w => w.Serie == pNumeroSerie
                                    && w.CodigoAlmacen == pCodigoAlmacen && w.CodigoTipoDocumento == pTipoDocumento.CodigoTipoDocumento);

            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string sqlActualizaCorrelativoTipoDocumento = @"UPDATE	PC_OP_DOCSERIES
                                                                SET		NBRDOC			= @NBRDOC
                                                                WHERE	DOCTYPEID		= @DOCTYPEID 
                                                                        AND SITEID		= @SITEID
                                                                        AND NBRSERIES	= @NBRSERIES";

                var filasAfectadas = cn.Execute(sqlActualizaCorrelativoTipoDocumento, new
                {
                    DOCTYPEID = pTipoDocumento.CodigoTipoDocumento,
                    SITEID = correlativoUtilizado.CodigoAlmacen,
                    NBRSERIES = correlativoUtilizado.Serie,
                    NBRDOC = correlativoUtilizado.Correlativo
                });
            }
        }

        public TipoDocumento ObtenerCorrelativoDocumento(string pCodigoAlmacen, string pCodigoPuntoDeVenta,
                                                        string pCodigoTipoDocumento, string pTipoDeVenta, int pEstado)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	DOCTYPEID	AS CodigoTipoDocumento
                                            ,DESCR		AS DescripcionTipoDocumento
                                            ,MINDES		AS Abreviatura
                                    FROM	PC_DOCTYPE (NOLOCK)
                                    WHERE	DOCTYPEID	= @DOCTYPEID;

                                    SELECT	NBRSERIES	AS Serie
                                            ,NBRDOC		AS Correlativo
                                            ,''			AS TipoDeVenta
                                            ,EXONERADO	AS Estado
                                            ,DOCTYPEID	AS CodigoTipoDocumento
                                            ,SITEID		AS CodigoAlmacen
                                            ,''			AS CodigoPuntoDeVenta
                                    FROM	PC_OP_DOCSERIES (NOLOCK)
                                    WHERE	DOCTYPEID	= @DOCTYPEID
                                            AND SITEID	= @SITEID
                                            AND ISNULL(USER1, '') = @USER1
                                            AND ISNULL(USER2, '') = @USER2
                                            AND EXONERADO		  = @EXONERADO";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new
                                    {
                                        SITEID = pCodigoAlmacen,
                                        USER1 = pCodigoPuntoDeVenta,
                                        DOCTYPEID = pCodigoTipoDocumento,
                                        USER2 = pTipoDeVenta,
                                        EXONERADO = pEstado
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
            //           && cd.ConfiguracionPuntoVentaId == pCodigoPuntoDeVenta
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
                string cadenaSQL = @"SELECT	DOCTYPEID	AS CodigoTipoDocumento
                                            ,DESCR		AS DescripcionTipoDocumento
                                            ,MINDES		AS Abreviatura
                                    FROM	PC_DOCTYPE (NOLOCK)
                                    WHERE	DOCTYPEID	= @DOCTYPEID";

                var tipoDocumento = cn.QueryFirstOrDefault<TipoDocumento>(cadenaSQL,
                                                new { DOCTYPEID = pCodigoTipoDocumento });

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
            var tipoDocumento = new TipoDocumento();
            tipoDocumento = pTipoDocumento;

            if (pCorrelativosDocumento != null)
            {
                foreach (var correlativoDocumento in pCorrelativosDocumento)
                {
                    tipoDocumento.AgregarNuevoCorrelativoDocumento(correlativoDocumento.Serie, correlativoDocumento.Correlativo,
                                                    correlativoDocumento.TipoDeVenta, correlativoDocumento.Estado,
                                                    correlativoDocumento.CodigoAlmacen, correlativoDocumento.CodigoPuntoDeVenta);
                }
            }

            return tipoDocumento;
        }
    }
}