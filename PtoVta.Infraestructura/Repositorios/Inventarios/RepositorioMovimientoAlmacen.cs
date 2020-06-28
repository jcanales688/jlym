
using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Inventarios
{
    public class RepositorioMovimientoAlmacen : Repositorio<MovimientoAlmacen>, IRepositorioMovimientoAlmacen
    {
        public RepositorioMovimientoAlmacen(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public override void Agregar(MovimientoAlmacen pMovimientoAlmacen)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string sqlAgregaMovimientoAlmacen = @"INSERT INTO OP_SALESPERSON(SALESPERID, SALESPERNAME, IDENTITYDOC, PHONE, SEX, INITIALDATE, 
                                                            BIRTHDATE, PASSWORD, SITEID, STATUSPERSONID, USERID, ACCESSUSERID,  ADDRESS1, ADDRESS2) 
                                                            VALUES
                                                            (@SALESPERID, @SALESPERNAME, @IDENTITYDOC, @PHONE, @SEX, @INITIALDATE, 
                                                            @BIRTHDATE, @PASSWORD, @SITEID, @STATUSPERSONID, @USERID, @ACCESSUSERID, @ADDRESS1, @ADDRESS2)";

                var filasAfectadas = cn.Execute(sqlAgregaMovimientoAlmacen, new
                {
                    SALESPERID = pMovimientoAlmacen.CodigoTipoDocumento
                });
            }
        }
    }
}