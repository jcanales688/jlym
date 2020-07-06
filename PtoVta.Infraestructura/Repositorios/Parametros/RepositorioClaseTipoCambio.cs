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
                string cadenaSQL = @"SELECT	CURYTYPEID	AS CodigoClaseTipoCambio
                                            ,DESCR		AS DescripcionClaseTipoCambio
                                    FROM	PC_CURYRATETYPE (NOLOCK)
                                    WHERE	CURYTYPEID	= @CURYTYPEID;

                                    SELECT TOP 1 CURYDATE	AS FechaTipoDeCambio
                                            ,RATE		AS MontoTipoDeCambio
                                            ,OPERADOR	AS Operador
                                            ,USERID		AS UsuarioDeSistema
                                            ,CURYTYPEID	AS CodigoClaseTipoCambio
                                            ,FROMCURYID AS CodigoMonedaOrigen
                                            ,TOCURYID	AS CodigoMonedaDestino
                                    FROM	PC_CURYRATE (NOLOCK)
                                    WHERE	CURYTYPEID		= @CURYTYPEID
                                            AND FROMCURYID	= @FROMCURYID
                                            AND TOCURYID	= @TOCURYID
                                            AND CURYDATE	= (	SELECT	MAX(CURYDATE) 
                                                                FROM	PC_CURYRATE (NOLOCK)
                                                                WHERE	CURYTYPEID		= @CURYTYPEID
                                                                        AND CURYDATE	<= @CURYDATE)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new
                                    {
                                        FROMCURYID = pCodigoMonedaOrigen,
                                        TOCURYID = pCodigoMonedaDestino,
                                        CURYDATE = pFechaTipoDeCambio,
                                        CURYTYPEID = pCodigoClaseTipoCambio
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
                string cadenaSQL = @"SELECT	CURYTYPEID	AS CodigoClaseTipoCambio
                                            ,DESCR		AS DescripcionClaseTipoCambio
                                    FROM	PC_CURYRATETYPE (NOLOCK)
                                    WHERE	CURYTYPEID	= @CURYTYPEID";

                var claseDeTipoCambio = cn.QueryFirstOrDefault<ClaseTipoCambio>(cadenaSQL,
                                                        new { CURYTYPEID = pCodigoClaseTipoCambio });

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
            var claseTipoDeCambio = new ClaseTipoCambio();
            claseTipoDeCambio = pClaseTipoCambio;

            if(pTipoDeCambio != null)
            {
                claseTipoDeCambio.AgregarNuevoTipoDeCambio(pTipoDeCambio.FechaTipoDeCambio, pTipoDeCambio.MontoTipoDeCambio,
                                                            pTipoDeCambio.Operador, pTipoDeCambio.UsuarioDeSistema,
                                                            pTipoDeCambio.CodigoMonedaOrigen, pTipoDeCambio.CodigoMonedaDestino);                    
            }

            return claseTipoDeCambio;
        }
    }
}