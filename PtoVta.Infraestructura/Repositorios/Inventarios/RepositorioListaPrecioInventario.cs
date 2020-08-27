using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Enumeradores.AmbienteVenta;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

namespace PtoVta.Infraestructura.Repositorios.Inventarios
{
    public class RepositorioListaPrecioInventario : Repositorio<ListaPrecioInventario>, IRepositorioListaPrecioInventario
    {
        public RepositorioListaPrecioInventario(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }


        public ListaPrecioInventario ObtenerListaPrecioInventario(string pCodigoArticulo, string pCodigoAlmacen)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                    string cadenaSQL = @"SELECT 	BATNBRPRICELEVELTEM		AS CodigoListaPrecioInventario
                                                    ,DATETIMEAUTORIZED01	AS FechaHoraCrea
                                                    ,DATETIMEAUTORIZED02	AS FechaHoraAprueba
                                                    ,STARTDATEPRICE			AS FechaInicioPrecio
                                                    ,ENDDATEPRICE			AS FechaFinPrecio
                                                    ,STARTTIMEPRICE			AS HoraInicioPrecio
                                                    ,ENDTIMEPRICE			AS HoraFinPrecio
                                                    ,STATUS					AS EsActivo
                                                    ,STKPRICESEND			AS EnviarAprobacion

                                                    ,PRECLVID				AS CodigoTipoPrecioInventario
                                                    ,USERIDAUTORIZED01		AS CodigoUsuarioSistemaCrea
                                                    ,USERIDAUTORIZED02		AS CodigoUsuarioSistemaAprueba
                                                    ,SITEID					AS CodigoAlmacen
                                                    ,SITEIDSOURCE			AS CodigoAlmacenOrigen
                                            FROM	" + BaseDatos.PrefijoTabla + @"IN_PRICELEVELTEM (NOLOCK)
                                            WHERE	SITEID				= @SITEID
                                                    AND STATUS			= @STATUS
                                                    AND STKPRICESEND	= @STKPRICESEND
                                                    AND PRECLVID		NOT IN(@PRECIOACTUALIZANACIONAL, @PRECIOACTUALIZAEXTRANJERO)

                                            SELECT	SEQUENCE   AS Secuencia
                                                    ,DESCR AS DescripcionArticulo
                                                    ,SLS_PRICE AS PrecioAntesLista
                                                    ,DISCMTO AS MontoDescuento
                                                    ,NEWSLS_PRICE AS NuevoPrecioInventario
                                                    ,STDCOST AS CostoReposicion
                                                    ,INVTIDSKU AS CodigoArticulo

                                                    ,BATNBRPRICELEVELTEM AS CodigoListaPrecioInventario
                                            FROM	" + BaseDatos.PrefijoTabla + @"IN_PRICELEVELTEMDET
                                            WHERE	BATNBRPRICELEVELTEM IN (SELECT	BATNBRPRICELEVELTEM
                                                                            FROM	" + BaseDatos.PrefijoTabla + @"IN_PRICELEVELTEM (NOLOCK)
                                                                            WHERE	SITEID				= @SITEID
                                                                                    AND STATUS			= @STATUS
                                                                                    AND STKPRICESEND	= @STKPRICESEND
                                                                                    AND PRECLVID		NOT IN(@PRECIOACTUALIZANACIONAL, @PRECIOACTUALIZAEXTRANJERO))
                                                    AND INVTIDSKU		= @INVTIDSKU";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { 
                                            SITEID = pCodigoAlmacen,
                                            STATUS = 1, 
                                            STKPRICESEND = 1,                                             
                                            PRECIOACTUALIZANACIONAL  = EnumTipoPrecioInventario.CodigoTipoPrecioInventarioActualizableNacional, 
                                            PRECIOACTUALIZAEXTRANJERO = EnumTipoPrecioInventario.CodigoTipoPrecioInventarioActualizableExtranjera, 
                                            INVTIDSKU = pCodigoArticulo,                                            
                                        });

                var listaPrecioInventario = resultado.Read<ListaPrecioInventario>().FirstOrDefault();
                var listaPrecioInventarioDetalle = resultado.Read<ListaPrecioInventarioDetalle>().ToList();

                if (listaPrecioInventario != null)
                {
                    return MapeoListaPrecioInventario(listaPrecioInventario, listaPrecioInventarioDetalle);
                }
                else
                  
                    return null;
            }

        }


        private ListaPrecioInventario MapeoListaPrecioInventario(ListaPrecioInventario pListaPrecioInventario, 
                                        List<ListaPrecioInventarioDetalle> pListaPrecioInventarioDetalles)

        {
            var listaPrecioInventario = new ListaPrecioInventario();
            listaPrecioInventario = pListaPrecioInventario;

            listaPrecioInventario.EstablecerReferenciaTipoPrecioInventarioDeListaPrecioInventario(listaPrecioInventario.CodigoTipoPrecioInventario);
            listaPrecioInventario.EstablecerReferenciaUsuarioSistemaCreaDeListaPrecioInventario(listaPrecioInventario.CodigoUsuarioDeSistemaCrea);
            listaPrecioInventario.EstablecerReferenciaUsuarioSistemaApruebaDeListaPrecioInventario(listaPrecioInventario.CodigoUsuarioDeSistemaAprueba);
            listaPrecioInventario.EstablecerReferenciaAlmacenDeListaPrecioInventario(listaPrecioInventario.CodigoAlmacen);
            listaPrecioInventario.EstablecerReferenciaAlmacenOrigenDeListaPrecioInventario(listaPrecioInventario.CodigoAlmacenOrigen);

            if (pListaPrecioInventarioDetalles != null && pListaPrecioInventarioDetalles.Any())
            {
                foreach (var listaPrecioInventarioDetalle in pListaPrecioInventarioDetalles)
                {
                    listaPrecioInventario.AgregarNuevaListaPrecioInventarioDetalle(listaPrecioInventarioDetalle.Secuencia,
                                    listaPrecioInventarioDetalle.DescripcionArticulo, listaPrecioInventarioDetalle.PrecioAntesLista,
                                    listaPrecioInventarioDetalle.MontoDescuento, listaPrecioInventarioDetalle.NuevoPrecioInventario,
                                    listaPrecioInventarioDetalle.CostoReposicion, listaPrecioInventarioDetalle.CodigoArticulo);
                }
            }

            return listaPrecioInventario;
        } 
    }

}