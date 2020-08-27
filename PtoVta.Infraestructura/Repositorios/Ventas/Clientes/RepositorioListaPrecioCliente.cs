using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

namespace PtoVta.Infraestructura.Repositorios.Ventas
{
    public class RepositorioListaPrecioCliente : Repositorio<ListaPrecioCliente>, IRepositorioListaPrecioCliente
    {
        public RepositorioListaPrecioCliente(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public ListaPrecioCliente ObtenerListaPrecioCliente(string pCodigoCliente, string pCodigoArticulo, 
                                                string pCodigoAlmacen, string pFechaProcesoVentas)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                    string cadenaSQL = @"SELECT   LPC.PRCLVLCUSTID			AS CodigoListaPrecioCliente
                                                ,LPC.DESCR					AS DescripcionListaPrecioCliente
                                                ,LPC.STARTDATEPRICE			AS FechaInicioPrecio
                                                ,LPC.ENDDATEPRICE			AS FechaFinPrecio
                                                ,LPC.APPROVA1				AS PrimeraAprobacion
                                                ,LPC.DATETIMEAUTORIZED1		AS FechaHoraPrimeraAprobacion
                                                ,LPC.APPROVA2				AS SegundaAprobacion
                                                ,LPC.DATETIMEAUTORIZED2		AS FechaHoraSegundaAprobacion
                                                ,LPC.STKPRICESEND			AS EnviarAprobacion
                                                ,LPC.TYPEPRCLVLCUST			AS ModalidadDescuento

                                                ,LPC.CURYID					AS CodigoMoneda
                                                ,LPC.USERIDAPPROVA1			AS CodigoUsuarioDeSistemaCrea
                                                ,LPC.DATETIMEAUTORIZED2		AS CodigoUsuarioDeSistemaAprueba
                                                ,LPC.SITEID					AS CodigoAlmacen
                                                ,LPC.SITEORIG				AS CodigoAlmacenOrigen
                                        FROM	" + BaseDatos.PrefijoTabla + @"OP_CUSTOMER_PRCLVL			(NOLOCK) RLPC
                                                INNER JOIN 	" + BaseDatos.PrefijoTabla + @"OP_PRCLVLCUST	(NOLOCK) LPC ON RLPC.PRCLVLCUSTID = LPC.PRCLVLCUSTID
                                        WHERE	RLPC.CUSTIDSS			= @CUSTIDSS
                                                AND LPC.SITEID			= @SITEID
                                                AND LPC.STATUS			= @STATUS
                                                AND LPC.STKPRICESEND	= @STKPRICESEND
                                                AND CONVERT(VARCHAR(8), LPC.ENDDATEPRICE, 112)	>= @ENDDATEPRICE;

                                        SELECT   0 AS Secuencia
                                                ,LPCD.DISCMTO AS MontoDescuento 
                                                ,LPCD.SLS_PRICE AS PrecioAntesLista
                                                ,LPCD.SLS_PRICECUST AS NuevoPrecioCliente
                                                ,LPCD.DESCR AS DescripcionArticulo

                                                ,LPCD.PRCLVLCUSTID AS CodigoListaPrecioCliente
                                                ,LPCD.SITEID AS CodigoAlmacen
                                                ,LPCD.INVTIDSKU AS CodigoArticulo
                                        FROM	" + BaseDatos.PrefijoTabla + @"OP_PRCLVLCUSTDET (NOLOCK) LPCD
                                        WHERE	LPCD.PRCLVLCUSTID	IN (SELECT	LPC.PRCLVLCUSTID
                                                                        FROM	" + BaseDatos.PrefijoTabla + @"OP_CUSTOMER_PRCLVL			(NOLOCK) RLPC
                                                                                INNER JOIN 	" + BaseDatos.PrefijoTabla + @"OP_PRCLVLCUST	(NOLOCK) LPC ON RLPC.PRCLVLCUSTID = LPC.PRCLVLCUSTID
                                                                        WHERE	RLPC.CUSTIDSS			= @CUSTIDSS
                                                                                AND LPC.SITEID			= @SITEID
                                                                                AND LPC.STATUS			= @STATUS
                                                                                AND LPC.STKPRICESEND	= @STKPRICESEND
                                                                                AND CONVERT(VARCHAR(8), LPC.ENDDATEPRICE, 112)	>= @ENDDATEPRICE)
                                                AND LPCD.INVTIDSKU = @INVTIDSKU";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { 
                                            CUSTIDSS = pCodigoCliente, 
                                            SITEID = pCodigoAlmacen,
                                            STATUS = 1, 
                                            STKPRICESEND = 1,                                             
                                            ENDDATEPRICE = pFechaProcesoVentas, 
                                            INVTIDSKU = pCodigoArticulo,                                            
                                        });

                var listaPrecioCliente = resultado.Read<ListaPrecioCliente>().FirstOrDefault();
                var listaPrecioClienteDetalle = resultado.Read<ListaPrecioClienteDetalle>().ToList();

                if (listaPrecioCliente != null)
                {
                    return MapeoListaPrecioCliente(listaPrecioCliente, listaPrecioClienteDetalle);
                }
                else
                    return null;
            }
        }



        private ListaPrecioCliente MapeoListaPrecioCliente(ListaPrecioCliente pListaPrecioCliente, 
                                        List<ListaPrecioClienteDetalle> pListaPrecioClienteDetalles)

        {
            var listaPrecioCliente = new ListaPrecioCliente();
            listaPrecioCliente = pListaPrecioCliente;

            listaPrecioCliente.EstablecerReferenciaMonedaDeListaPrecioCliente(listaPrecioCliente.CodigoMoneda);
            listaPrecioCliente.EstablecerReferenciaUsuarioSistemaCreaDeListaPrecioCliente(listaPrecioCliente.CodigoUsuarioDeSistemaCrea);
            listaPrecioCliente.EstablecerReferenciaUsuarioSistemaApruebaDeListaPrecioCliente(listaPrecioCliente.CodigoUsuarioDeSistemaAprueba);
            listaPrecioCliente.EstablecerReferenciaAlmacenDeListaPrecioCliente(listaPrecioCliente.CodigoAlmacen);
            listaPrecioCliente.EstablecerReferenciaAlmacenOrigenDeListaPrecioCliente(listaPrecioCliente.CodigoAlmacenOrigen);

            if (pListaPrecioClienteDetalles != null && pListaPrecioClienteDetalles.Any())
            {
                foreach (var listaPrecioClienteDetalle in pListaPrecioClienteDetalles)
                {
                    listaPrecioCliente.AgregarNuevaListaPrecioClienteDetalle(listaPrecioClienteDetalle.Secuencia,
                                    listaPrecioClienteDetalle.MontoDescuento, listaPrecioClienteDetalle.PrecioAntesLista,
                                    listaPrecioClienteDetalle.NuevoPrecioCliente, listaPrecioClienteDetalle.DescripcionArticulo,
                                    listaPrecioClienteDetalle.CodigoArticulo);
                }
            }

            return listaPrecioCliente;
        }        
    }

}