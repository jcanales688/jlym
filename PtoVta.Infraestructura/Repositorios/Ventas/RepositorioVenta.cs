using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.CuentasPorCobrar;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Ventas
{
    public class RepositorioVenta : Repositorio<Venta>, IRepositorioVenta
    {
        public RepositorioVenta(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override void Agregar(Venta pVenta)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string sqlAgregaArticulo = @"INSERT INTO OP_SALES(SALESPERID, SALESPERNAME, IDENTITYDOC, PHONE, SEX, INITIALDATE, 
                                                            BIRTHDATE, PASSWORD, SITEID, STATUSPERSONID, USERID, ACCESSUSERID,  ADDRESS1, ADDRESS2) 
                                                            VALUES
                                                            (@SALESPERID, @SALESPERNAME, @IDENTITYDOC, @PHONE, @SEX, @INITIALDATE, 
                                                            @BIRTHDATE, @PASSWORD, @SITEID, @STATUSPERSONID, @USERID, @ACCESSUSERID, @ADDRESS1, @ADDRESS2)";

                var filasAfectadas = cn.Execute(sqlAgregaArticulo, new
                {
                    SALESPERID = pVenta.CodigoVendedor,
                    SITEID = pVenta.CodigoAlmacen
                  });
            }
        }

        public decimal ObtenerNumeroDocumentoVenta(string pCodigoTipoDocumento, decimal pCorrelativoDocumento,
                                            string pCodigoAlmacen)
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

                var numeroDocumento = cn.ExecuteScalar<decimal>(cadenaSQL,
                                    new
                                    {
                                        USERID = pCodigoTipoDocumento,
                                        USERID3 = pCorrelativoDocumento,
                                        USERID4 = pCodigoAlmacen
                                    });


                if (numeroDocumento != 0)
                {
                    return numeroDocumento;
                }
                else
                    return 0;

            }

            // var documentoVenta = unidadTrabajoActual.Ventas
            //                     .Where(v => v.TipoDocumentoId == pCodigoTipoDocumento
            //                             && v.NumeroDocumento == pCorrelativoDocumento
            //                             && v.AlmacenId == pCodigoAlmacen
            //                         )
            //                     .Select(s=> s.NumeroDocumento)
            //                     .FirstOrDefault();


            // return Convert.ToDecimal(documentoVenta);
        }

        public IEnumerable<Venta> ObtenerVentasPorCodigoCliente(string pCodigoCliente)
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

                var ventasPorCliente = cn.Query<Venta>(cadenaSQL,
                                                new { USERID = pCodigoCliente }).ToList();

                if (ventasPorCliente != null && ventasPorCliente.Any())
                {
                    return ventasPorCliente;
                }
                else
                    return null;
            }
            // return unidadTrabajoActual.Ventas
            //         .Where(v => v.ClienteId == pCodigoCliente
            //              );

        }

        public IEnumerable<Venta> ObtenerPagoVentaAdelantada(string pCodigoCliente, string pCodigoAlmacen,
                                                            string pCodigoTipoDocumento, DateTime pFechaProcesoVentas)
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
                                        BUSINESSTYPE1 = pCodigoCliente,
                                        BUSINESSTYPE2 = pCodigoAlmacen,
                                        BUSINESSTYPE3 = pCodigoTipoDocumento,
                                        BUSINESSTYPE4 = pFechaProcesoVentas
                                    });

                var ventas = resultado.Read<Venta>().ToList();
                var estadoDocumento = resultado.Read<EstadoDocumento>().FirstOrDefault();
                var documentoAnticipado = resultado.Read<DocumentoAnticipado>().FirstOrDefault();

                if (ventas != null)
                {
                    return MapeoVenta(ventas, estadoDocumento, documentoAnticipado);
                }
                else
                    return null;
            }

            // var conjuntoVenta = unidadTrabajoActual.CrearConjunto<Venta>();

            // var consVtaPorPagoAdelantado = (from cvta in conjuntoVenta.Include(e => e.EstadoDocumento)
            //     .Include(d => d.DocumentosAnticipado)
            //     from da in cvta.DocumentosAnticipado
            //     where da.TipoDocumentoId == cvta.TipoDocumentoId
            //           && da.NumeroDocumento == cvta.NumeroDocumento
            //           && da.AlmacenId == cvta.AlmacenId

            //           && cvta.EstadoDocumento.CodigoEstadoDocumento != VentaEstadoDocumento.Anulado
            //           && cvta.ClienteId == pCodigoCliente
            //           && cvta.AlmacenId == pCodigoAlmacen
            //           && cvta.TipoDocumentoId == pCodigoTipoDocumento
            //           && cvta.FechaProceso <= pFechaProcesoVentas
            //     select cvta).ToList();

            // return consVtaPorPagoAdelantado;
        }

        public IEnumerable<Venta> ObtenerConsumoVentaAdelantada(string pCodigoTipoPago, string pCodigoCliente, string pCodigoAlmacen,
                                        string pCodigoTipoDocumento, DateTime pFechaProcesoVentas)
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
                                    new { 
                                            BUSINESSTYPE = pCodigoTipoPago, 
                                            BUSINESSTYPE2 = pCodigoCliente, 
                                            BUSINESSTYPE3 = pCodigoAlmacen, 
                                            BUSINESSTYPE5 = pCodigoTipoDocumento,                                                                                                                                     
                                            BUSINESSTYPE6 = pFechaProcesoVentas                                              
                                        });

                var ventas = resultado.Read<Venta>().ToList();
                var estadoDocumento = resultado.Read<EstadoDocumento>().FirstOrDefault();

                if (ventas != null)
                {
                    return MapeoVenta(ventas, estadoDocumento, null);
                }
                else
                    return null;
            }

            // var conjuntoVenta = unidadTrabajoActual.CrearConjunto<Venta>();

            // var consConsumoVtaAdelanto = (from cvta in conjuntoVenta.Include(e => e.EstadoDocumento)
            //     where cvta.EstadoDocumento.CodigoEstadoDocumento != VentaEstadoDocumento.Anulado
            //           && cvta.TipoPagoId == pCodigoTipoPago
            //           && cvta.ClienteId == pCodigoCliente
            //           && cvta.AlmacenId == pCodigoAlmacen
            //           && cvta.TipoDocumentoId == pCodigoTipoDocumento
            //           && cvta.FechaProceso <= pFechaProcesoVentas
            //     select cvta).ToList();

            // return consConsumoVtaAdelanto;
        }

        private List<Venta> MapeoVenta(List<Venta> pVentas, EstadoDocumento pEstadoDocumento, 
                                                        DocumentoAnticipado pDocumentoAnticipado)
        {
            return new List<Venta>();
        }
    }
}