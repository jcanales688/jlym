using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Colaborador
{
    public class RepositorioVendedor : Repositorio<Vendedor>, IRepositorioVendedor
    {
        public Vendedor ObtenerVendedorPorUsuario(string pUsuarioVendedor)
        {
          using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	SALESPERNAME		AS NombresVendedor
                                            ,IDENTITYDOC		AS DocumentoIdentidad
                                            ,SALESPERID			AS CodigoVendedor
                                            ,PASSWORD			AS Clave
                                            ,SITEID				AS CodigoAlmacen
                                            ,''					AS CodigoEstadoVendedor
                                            ,USERID				AS CodigoUsuarioSistema
                                            ,ACCESSUSERID		AS CodigoUsuarioSistemaAcceso
                                    FROM	PC_OP_SALESPERSON  (NOLOCK)
                                    WHERE	SALESPERID	= @SALESPERID;

                                    SELECT	USERID		AS CodigoUsuarioDeSistema
                                            ,EXPIRED	AS FechaExpiracion
                                            ,USERNAME	AS DescripcionUsuario
                                            ,PASSWORD	AS Contraseña
                                    FROM	PC_SE_USERREC (NOLOCK)
                                    WHERE	USERID	IN ( SELECT	ACCESSUSERID		
                                                        FROM	PC_OP_SALESPERSON
                                                        WHERE	SALESPERID	= @SALESPERID)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { SALESPERID = pUsuarioVendedor});

                var vendedor = resultado.Read<Vendedor>().FirstOrDefault();                                    
                var usuarioSistemaAsociado = resultado.Read<UsuarioSistema>().FirstOrDefault();                                    
                if (vendedor != null)
                {
                    return MapeoVendedor(vendedor, usuarioSistemaAsociado);
                }
                else
                    return null;

            }
        }

        private Vendedor MapeoVendedor(Vendedor pVendedor, UsuarioSistema pUsuarioSistema)
        {
            var vendedorBuscado = new Vendedor();
            vendedorBuscado = pVendedor;
            vendedorBuscado.EstablecerUsuarioSistemaAccesoDeVendedor(pUsuarioSistema);

            return vendedorBuscado;
        }        
    }
}
