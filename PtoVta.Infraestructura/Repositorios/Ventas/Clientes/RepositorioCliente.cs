using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.AmbienteVenta;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

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
                cn.Open();
                using (var transaccion = cn.BeginTransaction())
                {
                    string sqlAgregaCliente = @"INSERT INTO " + BaseDatos.PrefijoTabla + @"CUSTOMER
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

                    var filasAfectadasAgregaCliente = cn.Execute(sqlAgregaCliente, new
                    {
                        CUSTIDSS = pCliente.CodigoCliente,
                        DIAPAGID = pCliente.CodigoDiaDePago,
                        TERMIDSUM = pCliente.CodigoCondicionPagoDocumentoGenerado,
                        CUSTID = pCliente.CodigoCliente,
                        TAXREGNBR = pCliente.Ruc,
                        CUSTNAME = pCliente.NombresORazonSocial,
                        ADDR1 = pCliente.DireccionPrimero.Ubicacion,
                        ADDR2 = pCliente.DireccionSegundo.Ubicacion,
                        PHONE = pCliente.Telefono,
                        FAX = pCliente.Fax,
                        CUSTZONEID = pCliente.CodigoZonaCliente,
                        COUNTRYID = pCliente.CodigoPais,
                        STATEID = pCliente.CodigoDepartamento,
                        CITYID = pCliente.CodigoDistrito,
                        CUSTCLASSID = pCliente.CodigoTipoCliente,
                        TAXID1 = pCliente.CodigoImpuestoIgv,
                        TAXID2 = pCliente.CodigoImpuestoIsc,
                        CURYTYPEID = pCliente.CodigoClaseTipoCambio,
                        TERMIDDOC = pCliente.CodigoCondicionPagoTicket,
                        DATEORIG = pCliente.FechaNacimiento,
                        DATEPROC = pCliente.FechaInscripcion,
                        DAYGRACE = pCliente.DiasDeGracia,
                        CURYID = pCliente.CodigoMoneda,
                        SALESPERID = pCliente.CodigoVendedor,
                        USERID = pCliente.CodigoUsuarioDeSistema,
                        CUSTSTATUSID = pCliente.CodigoEstadoDeCliente,
                        MTOTOTLIMIT = pCliente.MontoLimiteCredito,
                        TOTCURRBAL = pCliente.Deuda,
                        AFFECT = pCliente.EsAfecto
                    }, transaction: transaccion);

                    //Placas
                    if (pCliente.ClientePlacas != null && pCliente.ClientePlacas.Any())
                    {
                        foreach (var clientePlaca in pCliente.ClientePlacas)
                        {
                            string sqlAgregaClientePlaca = @"INSERT INTO " + BaseDatos.PrefijoTabla + @"OP_CUSTBADGE
                                                                    (CUSTIDSS
                                                                    ,BADGE)
                                                            VALUES	(@CUSTIDSS
                                                                    ,@BADGE)";

                            var filasAfectadasAgregaClientePlaca = cn.Execute(sqlAgregaClientePlaca, new
                            {
                                CUSTIDSS = clientePlaca.CodigoCliente,
                                BADGE = clientePlaca.DescripcionPlaca
                            }, transaction: transaccion);
                        }
                    }

                    transaccion.Commit();
                }

            }
        }

        public override void Modificar(Cliente pCliente)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {

                string sqlActualizaCliente = @"UPDATE	" + BaseDatos.PrefijoTabla + @"CUSTOMER
                                                SET		DIAPAGID			= @DIAPAGID
                                                        ,TERMIDSUM			= @TERMIDSUM
                                                        ,CUSTNAME			= @CUSTNAME
                                                        ,ADDR1				= @ADDR1
                                                        ,ADDR2				= @ADDR2
                                                        ,PHONE				= @PHONE
                                                        ,FAX				= @FAX
                                                        ,CUSTZONEID			= @CUSTZONEID
                                                        ,COUNTRYID			= @COUNTRYID
                                                        ,STATEID			= @STATEID
                                                        ,CITYID				= @CITYID
                                                        ,CUSTCLASSID		= @CUSTCLASSID
                                                        ,TAXID1				= @TAXID1
                                                        ,TAXID2				= @TAXID2
                                                        ,CURYTYPEID			= @CURYTYPEID
                                                        ,TERMIDDOC			= @TERMIDDOC
                                                        ,DATEORIG			= @DATEORIG
                                                        ,DATEPROC			= @DATEPROC
                                                        ,DAYGRACE			= @DAYGRACE
                                                        ,CURYID				= @CURYID
                                                        ,SALESPERID			= @SALESPERID
                                                        ,USERID				= @USERID
                                                        ,CUSTSTATUSID		= @CUSTSTATUSID
                                                        ,MTOTOTLIMIT		= @MTOTOTLIMIT
                                                        ,TOTCURRBAL			= @TOTCURRBAL
                                                        ,AFFECT				= @AFFECT
                                                        --,USER1				= @USER1
                                                        --,USER2				= @USER2
                                                        --,USER3				= @USER3
                                                        --,USER4				= @USER4
                                                        --,USER5				= @USER5
                                                        --,USER6				= @USER6
                                                        --,USER7				= @USER7
                                                        --,USER8				= @USER8
                                                        --,USER9				= @USER9
                                                WHERE	CUSTIDSS			= @CUSTIDSS";

                var filasAfectadasActualizaCliente = cn.Execute(sqlActualizaCliente, new
                {
                    CUSTIDSS            = pCliente.CodigoCliente,
                    DIAPAGID			= pCliente.CodigoDiaDePago,
                    TERMIDSUM			= pCliente.CodigoCondicionPagoDocumentoGenerado,
                    CUSTNAME			= pCliente.NombresORazonSocial,
                    ADDR1				= pCliente.DireccionPrimero.Ubicacion,
                    ADDR2				= pCliente.DireccionSegundo.Ubicacion,
                    PHONE				= pCliente.Telefono,
                    FAX					= pCliente.Fax,
                    CUSTZONEID			= pCliente.CodigoZonaCliente,
                    COUNTRYID			= pCliente.CodigoPais,
                    STATEID				= pCliente.CodigoDepartamento,
                    CITYID				= pCliente.CodigoDistrito,
                    CUSTCLASSID			= pCliente.CodigoTipoCliente,
                    TAXID1				= pCliente.CodigoImpuestoIgv,
                    TAXID2				= pCliente.CodigoImpuestoIsc,
                    CURYTYPEID			= pCliente.CodigoClaseTipoCambio,
                    TERMIDDOC			= pCliente.CodigoCondicionPagoTicket,
                    DATEORIG			= pCliente.FechaNacimiento,
                    DATEPROC			= pCliente.FechaInscripcion,
                    DAYGRACE			= pCliente.DiasDeGracia,
                    CURYID				= pCliente.CodigoMoneda,
                    SALESPERID			= pCliente.CodigoVendedor,
                    USERID				= pCliente.CodigoUsuarioDeSistema,
                    CUSTSTATUSID		= pCliente.CodigoEstadoDeCliente,
                    MTOTOTLIMIT			= pCliente.MontoLimiteCredito,
                    TOTCURRBAL			= pCliente.Deuda,
                    AFFECT				= pCliente.EsAfecto
                    // USER1				= string.Empty,
                    // USER2				= string.Empty,
                    // USER3				= string.Empty,
                    // USER4				= string.Empty,
                    // USER5				= string.Empty,
                    // USER6				= string.Empty,
                    // USER7				= string.Empty,
                    // USER8				= string.Empty,
                    // USER9				= string.Empty 
                });

                if (pCliente.ClienteLimiteCredito != null)
                {
                    string sqlActualizaClienteLimiteCredito = @"UPDATE	" + BaseDatos.PrefijoTabla + @"OP_CUST_LIMIT
                                                                SET		PORCENTLIMITED		= @PORCENTLIMITED
                                                                        ,CREDLIMITED		= @CREDLIMITED
                                                                        ,CURRBAL			= @CURRBAL
                                                                        ,PORCENTSURPLUS		= @PORCENTSURPLUS		
                                                                        ,MTOSURPLUS			= @MTOSURPLUS
                                                                        ,USERID				= @USERID
                                                                        --,REST				= @REST
                                                                        --,USER1				= @USER1
                                                                        --,USER2				= @USER2
                                                                        --,USER3				= @USER3
                                                                        --,USER4				= @USER4
                                                                        --,MOTIVO				= @MOTIVO
                                                                        --,CREDLIMITEDC		= @CREDLIMITEDC
                                                                        --,CURRBALC			= @CURRBALC
                                                                WHERE	CUSTIDSS			= @CUSTIDSS
                                                                        AND SITEID			= @SITEID";

                    var filasAfectadasActualizaClienteLimiteCredito = cn.Execute(sqlActualizaClienteLimiteCredito, new
                    {
                        CUSTIDSS			= pCliente.CodigoCliente,
                        SITEID				= pCliente.ClienteLimiteCredito.CodigoAlmacen,
                        PORCENTLIMITED		= pCliente.ClienteLimiteCredito.PorcentajeLimite,
                        CREDLIMITED			= pCliente.ClienteLimiteCredito.MontoLimite,
                        CURRBAL				= pCliente.ClienteLimiteCredito.Deuda,
                        PORCENTSURPLUS		= pCliente.ClienteLimiteCredito.PorcentajeExcede,
                        MTOSURPLUS			= pCliente.ClienteLimiteCredito.MontoExcedente,
                        USERID				= pCliente.ClienteLimiteCredito.CodigoUsuarioDeSistema
                        // REST				= string.Empty,
                        // USER1				= string.Empty,
                        // USER2				= string.Empty,
                        // USER3				= string.Empty,
                        // USER4				= string.Empty,
                        // MOTIVO				= string.Empty,
                        // CREDLIMITEDC		= string.Empty,
                        // CURRBALC			= string.Empty
                    });
                }
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
                                    FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                    WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR;

                                    SELECT	TERMID		AS CodigoCondicionPago
                                            ,DUEINTRV	AS DiasPago
                                            ,DESCR		AS DescripcionCondicionPago
                                    FROM	" + BaseDatos.PrefijoTabla + @"TERMS (NOLOCK)
                                    WHERE	TERMID IN (	SELECT  TERMIDSUM
                                                        FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                                        WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR);

                                    SELECT	TERMID		AS CodigoCondicionPago
                                            ,DUEINTRV	AS DiasPago
                                            ,DESCR		AS DescripcionCondicionPago
                                    FROM	" + BaseDatos.PrefijoTabla + @"TERMS (NOLOCK)
                                    WHERE	TERMID IN (	SELECT  TERMIDDOC
                                                        FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
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
                                    FROM	" + BaseDatos.PrefijoTabla + @"XDIAS_PAGO (NOLOCK)
                                    WHERE	DIAPAGID	IN	(	SELECT  DIAPAGID
                                                                FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                                                WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR);

                                    SELECT	CUSTIDSS	AS CodigoCliente
                                            ,BADGE		AS DescripcionPlaca
                                    FROM	" + BaseDatos.PrefijoTabla + @"OP_CUSTBADGE (NOLOCK)
                                    WHERE	CUSTIDSS IN	(   SELECT	CUSTIDSS
                                                            FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                                            WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR);

                                    SELECT	 DOCUMENTFREEID AS NumeroDocumentoLibre
                                            ,DATEPROCEINI	AS FechaProcesoInicial 
                                            ,DATEPROCEFIN	AS FechaProcesoFinal 
                                            ,FREETOTAL		AS TotalLibre 
                                            ,CUSTIDSS		AS CodigoCliente 
                                            ,SITEID			AS CodigoAlmacen 
                                            ,USERID			AS CodigoUsuarioDeSistema 
                                    FROM	" + BaseDatos.PrefijoTabla + @"OP_DOCUMENTFREE (NOLOCK)
                                    WHERE	SITEID			= @SITEID
                                            AND	CUSTIDSS	IN	(	SELECT  CUSTIDSS
                                                                    FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                                                    WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR);
                                                                    
                                    SELECT	PORCENTLIMITED		AS PorcentajeLimite
                                            ,CREDLIMITED		AS MontoLimite
                                            ,CURRBAL			AS Deuda
                                            ,PORCENTSURPLUS		AS PorcentajeExcede
                                            ,MTOSURPLUS			AS MontoExcedente
                                            ,CUSTIDSS			AS CodigoCliente
                                            ,SITEID				AS CodigoAlmacen
                                            ,USERID				AS CodigoUsuarioDeSistema
                                    FROM	" + BaseDatos.PrefijoTabla + @"OP_CUST_LIMIT (NOLOCK)
                                    WHERE	CUSTIDSS IN	(SELECT	CUSTIDSS
                                                        FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                                        WHERE	RTRIM(TAXREGNBR)	= @TAXREGNBR)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { TAXREGNBR = pClienteRUC, SITEID = pCodigoAlmacen });

                var cliente = resultado.Read<Cliente>().FirstOrDefault();
                var condicionPagoDocumentoGenerado = resultado.Read<CondicionPago>().FirstOrDefault();
                var condicionPagoTicket = resultado.Read<CondicionPago>().FirstOrDefault();
                var diasDePago = resultado.Read<DiaDePago>().FirstOrDefault();
                var placas = resultado.Read<ClientePlaca>().ToList();
                var documentosLibre = resultado.Read<DocumentoLibre>().ToList();
                var limiteCredito = resultado.Read<ClienteLimiteCredito>().FirstOrDefault();

                if (cliente != null)
                {
                    return MapeoCliente(cliente, condicionPagoDocumentoGenerado, condicionPagoTicket,
                                        diasDePago, placas, documentosLibre, limiteCredito);
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
                                            ,ADDR1          AS DireccionPrimeroUbicacion
                                            ,ADDR2          AS DireccionSegundoUbicacion
                                    FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                    WHERE	RTRIM(CUSTIDSS)		= @CUSTIDSS;

                                    SELECT	TERMID		AS CodigoCondicionPago
                                            ,DUEINTRV	AS  DiasPago
                                            ,DESCR		AS DescripcionCondicionPago
                                    FROM	" + BaseDatos.PrefijoTabla + @"TERMS (NOLOCK)
                                    WHERE	TERMID IN (	SELECT	TERMIDDOC
                                                        FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                                        WHERE	RTRIM(CUSTIDSS) = @CUSTIDSS);
                                                        
                                    SELECT	TAXID	AS CodigoImpuesto
                                            ,DESCR	AS DescripcionImpuesto
                                            ,VALUE	AS Valor 
                                    FROM	" + BaseDatos.PrefijoTabla + @"TAXS (NOLOCK)
                                    WHERE	TAXID IN (	SELECT	ISNULL(TAXID1, 'IV')
                                                        FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                                        WHERE	RTRIM(CUSTIDSS) = @CUSTIDSS);
                                                        
                                    SELECT	PORCENTLIMITED		AS PorcentajeLimite
                                            ,CREDLIMITED		AS MontoLimite
                                            ,CURRBAL			AS Deuda
                                            ,PORCENTSURPLUS		AS PorcentajeExcede
                                            ,MTOSURPLUS			AS MontoExcedente
                                            ,CUSTIDSS			AS CodigoCliente
                                            ,SITEID				AS CodigoAlmacen
                                            ,USERID				AS CodigoUsuarioDeSistema
                                    FROM	" + BaseDatos.PrefijoTabla + @"OP_CUST_LIMIT (NOLOCK)
                                    WHERE	CUSTIDSS        = @CUSTIDSS";

                var resultado = cn.QueryMultiple(cadenaSQL, new { CUSTIDSS = pCodigoCliente });

                var cliente = resultado.Read<Cliente>().FirstOrDefault();
                var condicionPagoTicket = resultado.Read<CondicionPago>().FirstOrDefault();
                var impuestoIgv = resultado.Read<Impuesto>().FirstOrDefault();
                var limiteCredito = resultado.Read<ClienteLimiteCredito>().FirstOrDefault();

                if (cliente != null)
                {
                    return MapeoClientePorCodigo(cliente, condicionPagoTicket, impuestoIgv, limiteCredito);
                }
                else
                    return null;
            }
        }


        public override IEnumerable<Cliente> ObtenerTodos()
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
                                    FROM	" + BaseDatos.PrefijoTabla + @"CUSTOMER (NOLOCK)
                                    ORDER BY CUSTNAME";

                var clientesConsultados = cn.Query<Cliente>(cadenaSQL).ToList();
                if (clientesConsultados != null && clientesConsultados.Any())
                {
                    return clientesConsultados;
                }
                else
                    return null;
            }
        }

        private Cliente MapeoCliente(Cliente pCliente, CondicionPago pCondicionPagoDocumentoGenerado, CondicionPago pCondicionPagoTicket,
                DiaDePago pDiaDePago, List<ClientePlaca> pClientePlacas, List<DocumentoLibre> pDocumentosLibre,
                ClienteLimiteCredito pClienteLimiteCredito)

        {
            var cliente = new Cliente();
            cliente = pCliente;

            cliente.EstablecerCondicionPagoDocumentoGeneradoDeCliente(pCondicionPagoDocumentoGenerado);
            cliente.EstablecerCondicionPagoTicketDeCliente(pCondicionPagoTicket);
            cliente.EstablecerDiaDePagoDeCliente(pDiaDePago);

            if (pClientePlacas != null && pClientePlacas.Any())
            {
                foreach (var clientePlaca in pClientePlacas)
                {
                    cliente.AgregarNuevoClientePlaca(clientePlaca.DescripcionPlaca);
                }
            }

            if (pDocumentosLibre != null && pDocumentosLibre.Any())
            {
                foreach (var documentoLibre in pDocumentosLibre)
                {
                    cliente.AgregarNuevoDocumentoLibre(documentoLibre.NumeroDocumentoLibre, documentoLibre.FechaProcesoInicial,
                                                        documentoLibre.FechaProcesoFinal, documentoLibre.TotalLibre,
                                                        documentoLibre.CodigoAlmacen, documentoLibre.CodigoUsuarioDeSistema);
                }
            }

            if (pClienteLimiteCredito != null)
                cliente.AgregarClienteLimiteCredito(pClienteLimiteCredito.PorcentajeLimite, pClienteLimiteCredito.MontoLimite,
                        pClienteLimiteCredito.Deuda, pClienteLimiteCredito.PorcentajeExcede, pClienteLimiteCredito.MontoExcedente,
                        pClienteLimiteCredito.CodigoAlmacen);


            return cliente;
        }


        private Cliente MapeoClientePorCodigo(Cliente pCliente, CondicionPago pCondicionPagoTicket,
                                            Impuesto pImpuestoIgv, ClienteLimiteCredito pClienteLimiteCredito)

        {
            var cliente = new Cliente();
            cliente = pCliente;
            cliente.DireccionPrimero = new ClienteDireccion(string.Empty, 
                                                            string.Empty, 
                                                            string.Empty,
                                                            string.Empty,
                                                            pCliente.DireccionPrimeroUbicacion);

            cliente.DireccionSegundo = new ClienteDireccion(string.Empty, 
                                                            string.Empty, 
                                                            string.Empty,
                                                            string.Empty,
                                                            pCliente.DireccionSegundoUbicacion);

            if (pClienteLimiteCredito != null)
                cliente.AgregarClienteLimiteCredito(pClienteLimiteCredito.PorcentajeLimite, pClienteLimiteCredito.MontoLimite,
                        pClienteLimiteCredito.Deuda, pClienteLimiteCredito.PorcentajeExcede, pClienteLimiteCredito.MontoExcedente,
                        pClienteLimiteCredito.CodigoAlmacen);

            cliente.EstablecerCondicionPagoTicketDeCliente(pCondicionPagoTicket);
            cliente.EstablecerImpuestoIgvDeCliente(pImpuestoIgv);

            return cliente;
        }
    }

}