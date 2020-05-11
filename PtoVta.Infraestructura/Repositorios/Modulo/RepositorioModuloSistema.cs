using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Modulo
{
    public class RepositorioModuloSistema : Repositorio<ModuloSistema>, IRepositorioModuloSistema
    {
        public override void Unificar(ModuloSistema persistido, ModuloSistema actual){

        } 

        public ModuloSistema ObtenerDerechosAccesosUsuario(Guid pUsuarioSistemaId, Guid pModuloSistemaId)
        {
          using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	M.MODULEID	AS CodigoModuloSistema
                                            ,M.MODULENAME	AS NombreModulo
                                    FROM	PC_SE_MODULE (NOLOCK)	M
                                    WHERE	MODULEID	= @MODULEID;

                                    SELECT	V.SCREENID		AS CodigoVentanaUsuario
                                            ,V.SCREENNAME	AS NombreVentana
                                            ,V.SCREENTYPE	AS TipoVentana
                                            ,V.MODULEID		AS CodigoModuloSistema
                                    FROM	PC_SE_SCREEN (NOLOCK)	V
                                            INNER JOIN PC_SE_ACCESSDETRIGHTS (NOLOCK) D	ON V.SCREENID = D.SCREENID
                                    WHERE	V.MODULEID		= @MODULEID
                                            AND D.USERID	= @USERID;
                                                                                        
                                    SELECT	D.VIEWRIGHTS		AS DerechoConsultar
                                            ,D.INSERTRIGHTS		AS DerechoInsertar
                                            ,D.UPDATERIGHTS		AS DerechoActualizar
                                            ,D.DELETERIGHTS		AS DerechoEliminar
                                            ,D.PRINTRIGHTS		AS DerechoImprimir	
                                            ,D.NULLRIGHTS		AS DerechoAnular
                                            ,D.CLOSERIGHTS		AS DerechoEmitir	
                                            ,D.SCREENID			AS CodigoVentanaUsuario
                                            ,D.USERID			AS CodigoUsuarioSistema
                                    FROM	PC_SE_ACCESSDETRIGHTS (NOLOCK) D
                                    WHERE	LTRIM(RTRIM(D.SCREENID)) + LTRIM(RTRIM(D.USERID)) IN(SELECT	LTRIM(RTRIM(V.SCREENID)) + LTRIM(RTRIM(D.USERID))
                                                                                                FROM	PC_SE_SCREEN (NOLOCK)	V
                                                                                                        INNER JOIN PC_SE_ACCESSDETRIGHTS (NOLOCK) D	ON V.SCREENID = D.SCREENID
                                                                                                WHERE	V.MODULEID		= @MODULEID
                                                                                                        AND D.USERID	= @USERID)";

                var resultado = cn.QueryMultiple(cadenaSQL,
                                    new { MODULEID = pModuloSistemaId, USERID = pUsuarioSistemaId});

                var moduloSistema = resultado.Read<ModuloSistema>().FirstOrDefault();                                    
                var ventanasAsociadas = resultado.Read<VentanaUsuario>().ToList();                                    
                var derechosAsociados = resultado.Read<DerechoAccesoUsuario>().ToList();                                    
                if (moduloSistema != null)
                {
                    return MapeoModuloSistema(moduloSistema, ventanasAsociadas, derechosAsociados);
                }
                else
                    return null;

            }
        }

        private ModuloSistema MapeoModuloSistema(ModuloSistema pModuloSistema, List<VentanaUsuario> pVentanasUsuario
                                                                ,List<DerechoAccesoUsuario> pDerechosAccesoUsuario)
        {
            var moduloDelSistema = new ModuloSistema();
            moduloDelSistema = pModuloSistema;

            foreach (var ventana in pVentanasUsuario)
            {
                var nuevaVentana = moduloDelSistema
                            .AgregarNuevaVentanaUsuario(ventana.CodigoVentanaUsuario, ventana.NombreVentana, 
                                                    ventana.TipoVentana, moduloDelSistema.CodigoModuloSistema);
                
                var derechoAcceso = pDerechosAccesoUsuario
                        .SingleOrDefault(w => w.CodigoVentanaUsuario == ventana.CodigoVentanaUsuario);
                
                nuevaVentana.AgregarNuevoDerechoAccesoUsuario(derechoAcceso.DerechoConsultar, derechoAcceso.DerechoInsertar,
                                derechoAcceso.DerechoActualizar, derechoAcceso.DerechoImprimir,
                                derechoAcceso.DerechoImprimir, derechoAcceso.DerechoAnular,
                                derechoAcceso.DerechoEmitir, derechoAcceso.CodigoUsuarioSistema);             
            }

            return moduloDelSistema;
        }         
        
    }
}
