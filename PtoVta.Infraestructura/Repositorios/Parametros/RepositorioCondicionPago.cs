using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioCondicionPago : Repositorio<CondicionPago>, IRepositorioCondicionPago
    {
        public RepositorioCondicionPago(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public CondicionPago ObtenerPorCodigo(string pCodigoCondicionPago)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	TERMID		AS CodigoCondicionPago
                                            ,DUEINTRV	AS DiasPago
                                            ,DESCR		AS DescripcionCondicionPago
                                    FROM	PC_TERMS (NOLOCK)
                                    WHERE	TERMID		= @TERMID";

                var condicionDePago = cn.QueryFirstOrDefault<CondicionPago>(cadenaSQL,
                                    new { TERMID = pCodigoCondicionPago });

                if (condicionDePago != null)
                {
                    return condicionDePago;
                }
                else
                    return null;
            }
        }
    }
}