
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Inventarios
{
    public class RepositorioAlmacen : Repositorio<Almacen>, IRepositorioAlmacen
    {
        public RepositorioAlmacen(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }

        public Almacen ObtenerPorCodigo(string pCodigoAlmacen)
        {
            using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	SITEID	AS CodigoAlmacen 
                                            ,NAME	AS DescripcionAlmacen
                                            ,ADDR1	AS DireccionPrincipal
                                            ,ADDR2	AS DireccionAlterno
                                            ,FONO1	AS TelefonoPrincipal
                                            ,FONO2	AS TelefonoAlterno
                                            ,FAX	AS Fax
                                            ,ATTN	AS Responsable
                                            ,USERID	AS UsuarioSistemaCrea
                                    FROM	SITE (NOLOCK)
                                    WHERE	SITEID	= @SITEID";

                var almacen = cn.QueryFirstOrDefault<Almacen>(cadenaSQL,
                                    new { SITEID = pCodigoAlmacen});
                                                                                    
                if (almacen != null)
                {
                    return almacen;
                }
                else
                    return null;

            }
        }


        public IEnumerable<Almacen> ObtenerHabilitados()
        {
            // return unidadDeTrabajoActual.Almacenes
            //     .Where(a => a.EsHabilitado == true);


            return new List<Almacen>();
        }        
    }
}