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

        public Cliente ObtenerClientePorRUC(string pClienteRUC)
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
                                    new { BUSINESSTYPE = pClienteRUC });

                var cliente = resultado.Read<Cliente>().FirstOrDefault();
                var condicionPagoDocumentoGenerado = resultado.Read<CondicionPago>().FirstOrDefault();
                var condicionPagoTicket = resultado.Read<CondicionPago>().FirstOrDefault();
                var documentosLibre = resultado.Read<DocumentoLibre>().ToList();
                var diasDePago = resultado.Read<DiaDePago>().FirstOrDefault();

                if (cliente != null)
                {
                    return MapeoCliente(cliente, condicionPagoDocumentoGenerado, condicionPagoTicket, documentosLibre, diasDePago);
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
                string cadenaSQL = @"SELECT	USERID		AS CodigoUsuarioDeSistema
                                            ,EXPIRED	AS FechaExpiracion
                                            ,USERNAME	AS DescripcionUsuario
                                            ,PASSWORD	AS Contraseña
                                            ,STATUS AS EsHabilitado
                                    FROM    SE_USERREC (NOLOCK)
                                    WHERE	USERID			= @USERID";

                var cliente = cn.QueryFirstOrDefault<Cliente>(cadenaSQL,
                                            new { USERID = pCodigoCliente });

                if (cliente != null)
                {
                    return cliente;
                }
                else
                    return null;

            }
        }


        private Cliente MapeoCliente(Cliente pCliente, CondicionPago pCondicionPagoDocumentoGenerado, CondicionPago pCondicionPagoTicket,
                                        List<DocumentoLibre> pDocumentosLibre, DiaDePago pDiaDePago)
        {
            return new Cliente();
        }
    }

}