using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioMoneda : Repositorio<Moneda>, IRepositorioMoneda
    {
        public RepositorioMoneda(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public Moneda ObtenerPorCodigo(string pCodigoMoneda)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	CURYID		AS CodigoMoneda
                                            ,DESCR		AS DescripcionMoneda
                                            ,DESCRMONEY AS SimboloMoneda
                                    FROM	PC_CURRENCY (NOLOCK)
                                    WHERE	CURYID		= @CURYID";

                var moneda = cn.QueryFirstOrDefault<Moneda>(cadenaSQL,
                                    new { CURYID = pCodigoMoneda });

                if (moneda != null)
                {
                    return moneda;
                }
                else
                    return null;
            }
        }
    }
}