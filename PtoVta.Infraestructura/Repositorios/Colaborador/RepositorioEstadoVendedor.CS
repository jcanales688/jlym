using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Colaborador
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
                                    FROM	PC_OP_STATUSPERSON (NOLOCK)
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
