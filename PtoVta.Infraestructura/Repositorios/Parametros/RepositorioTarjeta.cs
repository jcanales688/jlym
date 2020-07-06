using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Parametros
{
    public class RepositorioTarjeta : Repositorio<Tarjeta>, IRepositorioTarjeta
    {
        public RepositorioTarjeta(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public Tarjeta ObtenerPorCodigo(string pCodigoTarjeta)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	CARDID			AS CodigoTarjeta
                                            ,ACCOUNT		AS Cuenta
                                            ,DESCR			AS DescripcionTarjeta
                                            ,NUMBERORDER	AS NumeroOrden
                                            ,CARDADJUST		AS AjusteTarjeta
                                    FROM	PC_OP_CARD (NOLOCK)
                                    WHERE	CARDID			= @CARDID";

                var tarjeta = cn.QueryFirstOrDefault<Tarjeta>(cadenaSQL,
                                            new { CARDID = pCodigoTarjeta });

                if (tarjeta != null)
                {
                    return tarjeta;
                }
                else
                    return null;
            }
        }
    }
}