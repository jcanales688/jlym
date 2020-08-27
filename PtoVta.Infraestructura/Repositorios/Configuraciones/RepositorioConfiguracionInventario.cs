using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

namespace PtoVta.Infraestructura.Repositorios.Configuraciones
{
    public class RepositorioConfiguracionInventario : Repositorio<ConfiguracionInventario>, IRepositorioConfiguracionInventario
    {
       public RepositorioConfiguracionInventario(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public ConfiguracionInventario Obtener()
        {
           using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	PERPOST				AS PeriodoInventario
                                            ,REPORTPATH			AS RutaReportesInventario
                                            ,STKSTOCK			AS PermitirStockNegativo
                                            ,MAXROUNDINV		AS MaximoRedondeoInventario
                                            ,INTYPETRANSFERIN	AS CodigoTMAIngresoTransferencia
                                            ,INTYPETRANSFEROUT	AS CodigoTMASalidaTransferencia
                                            ,INTYPETRBUYSTORE	AS CodigoTMACompraTienda
                                            ,INTYPETRBUYFUELS	AS CodigoTMACompraPlaya
                                            ,INTYPEREVBUYFUELS  AS CodigoTMAReversaCompraPlaya
                                            ,INTYPEREVBUYSTORE	AS CodigoTMAReversaCompraTienda
                                            ,INTYPESALES		AS CodigoTMAVentas
                                            ,IDVENDORSS			AS CodigoProveedorDefault  
                                            ,INVTIDSKUROUND		AS CodigoArticuloRedondeoInventario
                                    FROM	" + BaseDatos.PrefijoTabla + @"IN_SETUP (NOLOCK)";

                var configuracionInventario = cn.QueryFirstOrDefault<ConfiguracionInventario>(cadenaSQL);
                if (configuracionInventario != null)
                {
                    return configuracionInventario;
                }
                else
                    return null;
            }
        }
    }
}