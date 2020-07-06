using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Ventas
{
    public class RepositorioCliente : Repositorio<Cliente>, IRepositorioCliente
    {
        public RepositorioCliente(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override void Agregar(Cliente pCliente)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string sqlAgregaCliente = @"INSERT INTO PC_CUSTOMER
                                                        (CUSTIDSS
                                                        ,DIAPAGID
                                                        ,TERMIDSUM
                                                        ,CUSTID
                                                        ,TAXREGNBR
                                                        ,CUSTNAME
                                                        ,ADDR1
                                                        ,ADDR2
                                                        ,PHONE
                                                        ,FAX
                                                        ,CUSTZONEID
                                                        ,COUNTRYID
                                                        ,STATEID
                                                        ,CITYID
                                                        ,CUSTCLASSID
                                                        ,TAXID1
                                                        ,TAXID2
                                                        ,CURYTYPEID
                                                        ,TERMIDDOC
                                                        ,DATEORIG
                                                        ,DATEPROC
                                                        ,DAYGRACE
                                                        ,CURYID
                                                        ,SALESPERID
                                                        ,USERID
                                                        ,CUSTSTATUSID
                                                        ,MTOTOTLIMIT
                                                        ,TOTCURRBAL
                                                        ,AFFECT)
                                                VALUES(@CUSTIDSS
                                                        ,@DIAPAGID
                                                        ,@TERMIDSUM
                                                        ,@CUSTID
                                                        ,@TAXREGNBR
                                                        ,@CUSTNAME
                                                        ,@ADDR1
                                                        ,@ADDR2
                                                        ,@PHONE
                                                        ,@FAX
                                                        ,@CUSTZONEID
                                                        ,@COUNTRYID
                                                        ,@STATEID
                                                        ,@CITYID
                                                        ,@CUSTCLASSID
                                                        ,@TAXID1
                                                        ,@TAXID2
                                                        ,@CURYTYPEID
                                                        ,@TERMIDDOC
                                                        ,@DATEORIG
                                                        ,@DATEPROC
                                                        ,@DAYGRACE
                                                        ,@CURYID
                                                        ,@SALESPERID
                                                        ,@USERID
                                                        ,@CUSTSTATUSID
                                                        ,@MTOTOTLIMIT
                                                        ,@TOTCURRBAL
                                                        ,@AFFECT)";

                var filasAfectadas = cn.Execute(sqlAgregaCliente, new
                {
                    CUSTIDSS = pCliente.CodigoCliente
                    ,DIAPAGID = pCliente.CodigoDiaDePago
                    ,TERMIDSUM = pCliente.CodigoCondicionPagoDocumentoGenerado
                    ,CUSTID = pCliente.CodigoCliente
                    ,TAXREGNBR = pCliente.Ruc
                    ,CUSTNAME = pCliente.NombresORazonSocial
                    ,ADDR1 = pCliente.DireccionPrimero.Ubicacion
                    ,ADDR2 = pCliente.DireccionSegundo.Ubicacion
                    ,PHONE = pCliente.Telefono
                    ,FAX = pCliente.Fax
                    ,CUSTZONEID = pCliente.CodigoZonaCliente
                    ,COUNTRYID = pCliente.CodigoPais
                    ,STATEID = pCliente.CodigoDepartamento
                    ,CITYID = pCliente.CodigoDistrito
                    ,CUSTCLASSID = pCliente.CodigoTipoCliente
                    ,TAXID1 = pCliente.CodigoImpuestoIgv
                    ,TAXID2 = pCliente.CodigoImpuestoIsc
                    ,CURYTYPEID = pCliente.CodigoClaseTipoCambio
                    ,TERMIDDOC = pCliente.CodigoCondicionPagoTicket
                    ,DATEORIG = pCliente.FechaNacimiento
                    ,DATEPROC = pCliente.FechaInscripcion
                    ,DAYGRACE = pCliente.DiasDeGracia
                    ,CURYID =   pCliente.CodigoMoneda
                    ,SALESPERID = pCliente.CodigoVendedor
                    ,USERID = pCliente.CodigoUsuarioDeSistema
                    ,CUSTSTATUSID = pCliente.CodigoEstadoDeCliente
                    ,MTOTOTLIMIT = pCliente.MontoLimiteCredito
                    ,TOTCURRBAL = pCliente.Deuda
                    ,AFFECT = pCliente.EsAfecto
                });
            }
        }

        public Cliente ObtenerClientePorRUC(string pClienteRUC, string pCodigoAlmacen)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	CUSTIDSS		AS CodigoCliente
                                            ,''				AS CodigoContable
                                            ,TAXREGNBR		AS Ruc
                                            ,CUSTNAME		AS NombresORazonSocial
                                            ,PHONE			AS Telefono
                                            ,FAX			AS Fax
                                            ,DATEORIG		AS FechaNacimiento
                                            ,DATEPROC		AS FechaInscripcion
                                            ,DAYGRACE		AS DiasDeGracia
                                            ,MTOTOTLIMIT	AS MontoLimiteCredito
                                            ,TOTCURRBAL		AS Deuda
                                            ,AFFECT			AS EsAfecto
                                            ,USER9			AS ControlarSaldoDispo
                                            ,CURYID			AS CodigoMoneda
                                            ,CURYTYPEID		AS CodigoClaseTipoCambio
                                            ,CUSTCLASSID	AS CodigoTipoCliente
                                            ,CUSTZONEID		AS CodigoZonaCliente
                                            ,DIAPAGID		AS CodigoDiaDePago
                                            ,SALESPERID		AS CodigoVendedor
                                            ,TAXID1			AS CodigoImpuestoIgv
                                            ,TAXID2			AS CodigoImpuestoIsc
                                            ,TERMIDSUM		AS CodigoCondicionPagoDocumentoGenerado
                                            ,TERMIDDOC		AS CodigoCondicionPagoTicket
                                            ,CUSTSTATUSID	AS CodigoEstadoDeCliente
                                            ,USERID			AS CodigoUsuarioDeSistema
                                    FROM	PC_CUSTOMER (NOLOCK)
                                    WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR;

                                    SELECT	TERMID		AS CodigoCondicionPago
                                            ,DUEINTRV	AS DiasPago
                                            ,DESCR		AS DescripcionCondicionPago
                                    FROM	PC_TERMS (NOLOCK)
                                    WHERE	TERMID IN (	SELECT  TERMIDSUM
                                                        FROM	PC_CUSTOMER (NOLOCK)
                                                        WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR);

                                    SELECT	TERMID		AS CodigoCondicionPago
                                            ,DUEINTRV	AS DiasPago
                                            ,DESCR		AS DescripcionCondicionPago
                                    FROM	PC_TERMS (NOLOCK)
                                    WHERE	TERMID IN (	SELECT  TERMIDDOC
                                                        FROM	PC_CUSTOMER (NOLOCK)
                                                        WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR);

                                    SELECT	 DIAPAGID		AS CodigoDiaDePago
                                            ,COMBDIA1		AS CombinaDia1
                                            ,COMBDIA2		AS CombinaDia2
                                            ,COMBDIA3		AS CombinaDia3
                                            ,COMBDIA4		AS CombinaDia4
                                            ,DESCR			AS DescripcionDiaDePago
                                            ,D1LUNES		AS D1Lunes
                                            ,D2MARTES		AS D2Martes
                                            ,D3MIERCOLES	AS D3Miercoles
                                            ,D4JUEVES		AS D4Jueves
                                            ,D5VIERNES		AS D5Viernes
                                            ,D6SABADO		AS D6Sabado
                                            ,D7DOMINGO		AS D7Domingo
                                            ,FECCREACION	AS FechaCreacion
                                            ,FECULTACT		AS FechaUltimaActualiza
                                            ,STATUSSEM		AS EstadoSemana
                                    FROM	PC_XDIAS_PAGO (NOLOCK)
                                    WHERE	DIAPAGID	IN	(	SELECT  DIAPAGID
                                                                FROM	PC_CUSTOMER (NOLOCK)
                                                                WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR);

                                    SELECT	 DOCUMENTFREEID AS NumeroDocumentoLibre
                                            ,DATEPROCEINI	AS FechaProcesoInicial 
                                            ,DATEPROCEFIN	AS FechaProcesoFinal 
                                            ,FREETOTAL		AS TotalLibre 
                                            ,CUSTIDSS		AS CodigoCliente 
                                            ,SITEID			AS CodigoAlmacen 
                                            ,USERID			AS CodigoUsuarioDeSistema 
                                    FROM	PC_OP_DOCUMENTFREE (NOLOCK)
                                    WHERE	SITEID			= @SITEID
                                            AND	CUSTIDSS	IN	(	SELECT  CUSTIDSS
                                                                    FROM	PC_CUSTOMER (NOLOCK)
                                                                    WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { TAXREGNBR = pClienteRUC, SITEID = pCodigoAlmacen });

                var cliente = resultado.Read<Cliente>().FirstOrDefault();
                var condicionPagoDocumentoGenerado = resultado.Read<CondicionPago>().FirstOrDefault();
                var condicionPagoTicket = resultado.Read<CondicionPago>().FirstOrDefault();
                var diasDePago = resultado.Read<DiaDePago>().FirstOrDefault();
                var documentosLibre = resultado.Read<DocumentoLibre>().ToList();

                if (cliente != null)
                {
                    return MapeoCliente(cliente, condicionPagoDocumentoGenerado, condicionPagoTicket, diasDePago, documentosLibre);
                }
                else
                    return null;
            }

            //Esta busqueda incluirla en el Patro Especificacion

            // var conjunto = unidadTrabajoActual.CrearConjunto<Cliente>();

            // var cliente = (from ccli in conjunto
            //     .Include(c => c.CondicionPagoDocumentoGenerado)
            //     .Include(c => c.CondicionPagoTicket)
            //     .Include(c => c.DocumentosLibre)
            //     .Include(c => c.DiaDePago)
            //     where ccli.Ruc == pClienteRUC
            //     select ccli).FirstOrDefault();
        }

        public Cliente ObtenerPorCodigo(string pCodigoCliente)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	CUSTIDSS		AS CodigoCliente
                                            ,''				AS CodigoContable
                                            ,TAXREGNBR		AS Ruc
                                            ,CUSTNAME		AS NombresORazonSocial
                                            ,PHONE			AS Telefono
                                            ,FAX			AS Fax
                                            ,DATEORIG		AS FechaNacimiento
                                            ,DATEPROC		AS FechaInscripcion
                                            ,DAYGRACE		AS DiasDeGracia
                                            ,MTOTOTLIMIT	AS MontoLimiteCredito
                                            ,TOTCURRBAL		AS Deuda
                                            ,AFFECT			AS EsAfecto
                                            ,USER9			AS ControlarSaldoDispo
                                            ,CURYID			AS CodigoMoneda
                                            ,CURYTYPEID		AS CodigoClaseTipoCambio
                                            ,CUSTCLASSID	AS CodigoTipoCliente
                                            ,CUSTZONEID		AS CodigoZonaCliente
                                            ,DIAPAGID		AS CodigoDiaDePago
                                            ,SALESPERID		AS CodigoVendedor
                                            ,TAXID1			AS CodigoImpuestoIgv
                                            ,TAXID2			AS CodigoImpuestoIsc
                                            ,TERMIDSUM		AS CodigoCondicionPagoDocumentoGenerado
                                            ,TERMIDDOC		AS CodigoCondicionPagoTicket
                                            ,CUSTSTATUSID	AS CodigoEstadoDeCliente
                                            ,USERID			AS CodigoUsuarioDeSistema
                                    FROM	PC_CUSTOMER (NOLOCK)
                                    WHERE	RTRIM(CUSTIDSS)		= @CUSTIDSS";

                var cliente = cn.QueryFirstOrDefault<Cliente>(cadenaSQL,
                                            new { CUSTIDSS = pCodigoCliente });

                if (cliente != null)
                {
                    return cliente;
                }
                else
                    return null;

            }
        }


        private Cliente MapeoCliente(Cliente pCliente, CondicionPago pCondicionPagoDocumentoGenerado, CondicionPago pCondicionPagoTicket,
                                        DiaDePago pDiaDePago, List<DocumentoLibre> pDocumentosLibre)
        {
            var cliente = new Cliente();
            cliente = pCliente;

            cliente.EstablecerCondicionPagoDocumentoGeneradoDeCliente(pCondicionPagoDocumentoGenerado);
            cliente.EstablecerCondicionPagoTicketDeCliente(pCondicionPagoTicket);
            cliente.EstablecerDiaDePagoDeCliente(pDiaDePago);

            if (pDocumentosLibre != null)
            {
                foreach (var documentoLibre in pDocumentosLibre)
                {
                    cliente.AgregarNuevoDocumentoLibre(documentoLibre.NumeroDocumentoLibre, documentoLibre.FechaProcesoInicial,
                                                        documentoLibre.FechaProcesoFinal, documentoLibre.TotalLibre,
                                                        documentoLibre.CodigoAlmacen, documentoLibre.CodigoUsuarioDeSistema);
                }
            }

            return cliente;
        }
    }

}