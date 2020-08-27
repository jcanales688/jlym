using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.AmbienteVenta;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

namespace PtoVta.Infraestructura.Repositorios.Configuraciones
{
    public class RepositorioConfiguracionVenta : Repositorio<ConfiguracionVenta>, IRepositorioConfiguracionVenta
    {
       public RepositorioConfiguracionVenta(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public ConfiguracionVenta Obtener()
        {
           using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	AUTOMATICSALES					AS VentaAutomaticaCombustible
                                            ,DATEPROCESALES					AS FechaProcesoVenta
                                            ,STKCORRELATIVE					AS NoSaltaCorrelativo
                                            ,REPORTPATH						AS RutaReportesVenta
                                            ,TYPECONTROLLER					AS TipoControlador
                                            ,CATEGORYFUEL					AS CodigoCategoriaFuel
                                            ,CATEGORYLUBES					AS CodigoCategoriaLubricantes
                                            ,CCLASSIDCASH					AS CodigoTipoClienteEfectivo
                                            ,CCLASSIDANTICIPATE				AS CodigoTipoClienteAdelanto
                                            ,CCLASSIDCREDLOCAL				AS CodigoTipoClienteCreditoLocal
                                            ,CCLASSIDCREDCORPORATE			AS CodigoTipoClienteCreditoCorporativo
                                            ,CCLASSIDOTHER					AS CodigoTipoClienteOtros
                                            ,DOCTYPEIDTICKET				AS CodigoTipoDocumentoTicket
                                            ,DOCTYPEIDFAC					AS CodigoTipoDocumentoFactura
                                            ,DOCTYPEIDBOL					AS CodigoTipoDocumentoBoleta
                                            ,DOCTYPEIDNC					AS CodigoTipoDocumentoNotaCredito
                                            ,DOCTYPEIDND					AS CodigoTipoDocumentoNotaDebito
                                            ,DOCTYPEIDNCAJUSTE				AS CodigoTipoDocumentoNotaCreditoAjuste
                                            ,@CodigoCondicionPagoDefault	AS CodigoCondicionPagoDefault 
                                            ,@CodigoEstadoDocumentoDefault  AS CodigoEstadoDocumentoDefault 
                                    FROM	" + BaseDatos.PrefijoTabla + @"OP_SETUP (NOLOCK)";

                var configuracionVenta = cn.QueryFirstOrDefault<ConfiguracionVenta>(cadenaSQL,
                                                new { 
                                                        CodigoCondicionPagoDefault = EnumCondicionPago.CodigoCondicionPagoContraentrega,
                                                        CodigoEstadoDocumentoDefault = EnumEstadoDocumento.CodigoEstadoDocumentoPendiente 
                                                    });
                if (configuracionVenta != null)
                {
                    return configuracionVenta;
                }
                else
                    return null;
            }
        }
    }
}