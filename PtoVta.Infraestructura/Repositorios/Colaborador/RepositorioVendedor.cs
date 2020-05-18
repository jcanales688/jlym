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
        public RepositorioVendedor(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }
        
        public override void Agregar(Vendedor item)
        {            
          using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string sqlAgregaCliente = @"INSERT INTO PC_OP_SALESPERSON(SALESPERID, SALESPERNAME, IDENTITYDOC, PHONE, SEX, INITIALDATE, 
                                                            BIRTHDATE, PASSWORD, SITEID, STATUSPERSONID, USERID, ACCESSUSERID) 
                                                            VALUES
                                                            (@SALESPERID, @SALESPERNAME, @IDENTITYDOC, @PHONE, @SEX, @INITIALDATE, 
                                                            @BIRTHDATE, @PASSWORD, @SITEID, @STATUSPERSONID, @USERID, @ACCESSUSERID)";

                var filasAfectadas = cn.Execute(sqlAgregaCliente, new {SALESPERID = String.Empty, SALESPERNAME = String.Empty,
                                                                        IDENTITYDOC = String.Empty, PHONE = String.Empty,
                                                                        SEX = String.Empty, INITIALDATE = string.Empty,
                                                                        BIRTHDATE = string.Empty, PASSWORD = string.Empty,
                                                                        SITEID = string.Empty, STATUSPERSONID = string.Empty,
                                                                        USERID = string.Empty, ACCESSUSERID = string.Empty});
            }                                
        }

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
                                            ,CASE STATUSPERSONID WHEN '01' THEN 1 ELSE 0 END AS EsHabilitado
                                    FROM	OP_SALESPERSON  (NOLOCK)
                                    WHERE	SALESPERID	= @SALESPERID;

                                    SELECT	STATUSPERSONID		AS CodigoEstadoVendedor
                                            ,DESCRSTATUSPERSON	AS DescripcionEstadoVendedor
                                    FROM	OP_STATUSPERSON (NOLOCK)
                                    WHERE	STATUSPERSONID	IN (SELECT	STATUSPERSONID
                                                                FROM	OP_SALESPERSON (NOLOCK)
                                                                WHERE	SALESPERID	= @SALESPERID);                                    

                                    SELECT	USERID		AS CodigoUsuarioDeSistema
                                            ,EXPIRED	AS FechaExpiracion
                                            ,USERNAME	AS DescripcionUsuario
                                            ,PASSWORD	AS Contraseña
                                            ,STATUS     AS EsHabilitado
                                    FROM	SE_USERREC (NOLOCK)
                                    WHERE	USERID	IN ( SELECT	ACCESSUSERID		
                                                        FROM	OP_SALESPERSON
                                                        WHERE	SALESPERID	= @SALESPERID)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { SALESPERID = pUsuarioVendedor});

                var vendedor = resultado.Read<Vendedor>().FirstOrDefault();                        
                var estadoVendedor = resultado.Read<EstadoVendedor>().FirstOrDefault();                        
                var usuarioSistemaAsociado = resultado.Read<UsuarioSistema>().FirstOrDefault();                                    
                if (vendedor != null)
                {
                    return MapeoVendedor(vendedor, estadoVendedor,usuarioSistemaAsociado);
                }
                else
                    return null;

            }
        }

        private Vendedor MapeoVendedor(Vendedor pVendedor, EstadoVendedor pEstadoVendedor, UsuarioSistema pUsuarioSistema)
        {
            var vendedorBuscado = new Vendedor();
            vendedorBuscado = pVendedor;
            vendedorBuscado.EstablecerEstadoVendedorDeVendedor(pEstadoVendedor);
            vendedorBuscado.EstablecerUsuarioSistemaAccesoDeVendedor(pUsuarioSistema);

            return vendedorBuscado;
        }        
    }
}
