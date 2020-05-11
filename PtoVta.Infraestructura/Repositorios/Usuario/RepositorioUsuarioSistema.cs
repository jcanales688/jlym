using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Usuario
{
    public class RepositorioUsuarioSistema : Repositorio<UsuarioSistema>, IRepositorioUsuarioSistema
    {
        public UsuarioSistema ObtenerUsuarioSistemaPorUsuario(string pUsuarioDeSistema, string pContraseña)
        {
         using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	USERID		AS CodigoUsuarioDeSistema
                                            ,EXPIRED	AS FechaExpiracion
                                            ,USERNAME	AS DescripcionUsuario
                                            ,PASSWORD	AS Contraseña
                                            ,STATUS AS EsHabilitado
                                    FROM	PC_SE_USERREC (NOLOCK)
                                    WHERE	USERID			= @USERID
                                            AND	PASSWORD	= @PASSWORD";

                var usuarioSistema = cn.QueryFirstOrDefault<UsuarioSistema>(cadenaSQL,
                                    new { USERID = pUsuarioDeSistema, PASSWORD = pContraseña});

                                                                                    
                if (usuarioSistema != null)
                {
                    return usuarioSistema;
                }
                else
                    return null;

            }
        }
    }
}
