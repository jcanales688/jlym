using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioEstadoVendedor : Repositorio<EstadoVendedor>, IRepositorioEstadoVendedor
    {
        public RepositorioEstadoVendedor(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public EstadoVendedor ObtenerPorCodigo(string pCodigoEstadoVendedor)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	STATUSPERSONID		AS CodigoEstadoVendedor
                                            ,DESCRSTATUSPERSON	AS DescripcionEstadoVendedor
                                    FROM	" + BaseDatos.PrefijoTabla + @"OP_STATUSPERSON (NOLOCK)
                                    WHERE	STATUSPERSONID = @STATUSPERSONID";

                var estadoVendedor = cn.QueryFirstOrDefault<EstadoVendedor>(cadenaSQL,
                                            new { STATUSPERSONID = pCodigoEstadoVendedor});
                                                                                    
                if (estadoVendedor != null)
                {
                    return estadoVendedor;
                }
                else
                    return null;

            }
        }
    }
}
