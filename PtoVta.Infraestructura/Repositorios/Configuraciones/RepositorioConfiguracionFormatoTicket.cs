using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Infraestructura.Repositorios.Configuraciones
{
    public class RepositorioConfiguracionFormatoTicket : Repositorio<ConfiguracionFormatoTicket>, IRepositorioConfiguracionFormatoTicket
    {
        public ConfiguracionFormatoTicket Obtener()
        {
           using (IDbConnection cn = new SqlConnection(this.CadenaConexion))
            {
                string cadenaSQL = @"SELECT	HEAD1			AS Cabecera01
                                            ,HEAD2			AS Cabecera02
                                            ,HEAD3			AS Cabecera03
                                            ,HEAD4			AS Cabecera04
                                            ,HEAD5			AS Cabecera05
                                            ,HEAD6			AS Cabecera06
                                            ,HEAD7			AS Cabecera07
                                            ,HEAD8			AS Cabecera08
                                            ,HEAD9			AS Cabecera09
                                            ,HEAD10			AS Cabecera10
                                            ,LINES1			AS Linea01
                                            ,LINES2			AS Linea02
                                            ,LINES3			AS Linea03
                                            ,LINES4			AS Linea04
                                            ,FOOT1			AS PiePagina01
                                            ,FOOT2			AS PiePagina02
                                            ,FOOT3			AS PiePagina03
                                            ,FOOT4			AS PiePagina04
                                            ,FOOT5			AS PiePagina05
                                            ,FOOT6			AS PiePagina06
                                            ,FOOT7			AS PiePagina07
                                            ,FOOT8			AS PiePagina08
                                            ,FOOT9			AS PiePagina09
                                            ,FOOT10			AS PiePagina10
                                            ,@AnchoTicket	AS AnchoTicket
                                    FROM	PC_OP_TICKETFORM (NOLOCK)";

                var configuracionFormatoTicket = cn.QueryFirstOrDefault<ConfiguracionFormatoTicket>(cadenaSQL,
                                        new {AnchoTicket = EnumGenerales.AnchoTicket});
                if (configuracionFormatoTicket != null)
                {
                    return configuracionFormatoTicket;
                }
                else
                    return null;
            }
        }
    }
}