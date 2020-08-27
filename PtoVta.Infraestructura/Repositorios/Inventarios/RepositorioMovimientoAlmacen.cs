
using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Infraestructura.BaseTrabajo;
using static PtoVta.Infraestructura.BaseTrabajo.Globales.GlobalInfraestructura;

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
                string sqlAgregaMovimientoAlmacen = @"INSERT INTO " + BaseDatos.PrefijoTabla + @"INTRAMOV
                                                            (BATNBR
                                                            ,INTRDATE
                                                            ,INTRDATEPROCE
                                                            ,CURYRATE
                                                            ,CURYDATE
                                                            ,PERPOST
                                                            ,SWTINOUT
                                                            ,QTY
                                                            ,STDCOSTPEN
                                                            ,STDCOSTUSD
                                                            ,COMPONENT
                                                            ,SLSPRICE
                                                            ,REFNBR
                                                            ,STKFISI
                                                            ,SITEID
                                                            ,INVTIDSKU
                                                            ,TYPEDOCID
                                                            ,DOCTYPEID)
                                                    VALUES	(@BATNBR
                                                            ,@INTRDATE
                                                            ,@INTRDATEPROCE
                                                            ,@CURYRATE
                                                            ,@CURYDATE
                                                            ,@PERPOST
                                                            ,@SWTINOUT
                                                            ,@QTY
                                                            ,@STDCOSTPEN
                                                            ,@STDCOSTUSD
                                                            ,@COMPONENT
                                                            ,@SLSPRICE
                                                            ,@REFNBR
                                                            ,@STKFISI
                                                            ,@SITEID
                                                            ,@INVTIDSKU
                                                            ,@TYPEDOCID
                                                            ,@DOCTYPEID)";

                var filasAfectadas = cn.Execute(sqlAgregaMovimientoAlmacen, new
                {
                    BATNBR = pMovimientoAlmacen.CorrelativoMovimiento
                    ,INTRDATE = pMovimientoAlmacen.FechaDocumento
                    ,INTRDATEPROCE = pMovimientoAlmacen.FechaProceso
                    ,CURYRATE = pMovimientoAlmacen.MontoTipoDeCambio
                    ,CURYDATE = pMovimientoAlmacen.FechaTipoDeCambio
                    ,PERPOST = pMovimientoAlmacen.Periodo
                    ,SWTINOUT = pMovimientoAlmacen.FlagEntradaSalida
                    ,QTY = pMovimientoAlmacen.Cantidad
                    ,STDCOSTPEN = pMovimientoAlmacen.CostoReposicionNacional
                    ,STDCOSTUSD = pMovimientoAlmacen.CostoReposicionExtranjera
                    ,COMPONENT = pMovimientoAlmacen.EsArticuloFormula
                    ,SLSPRICE  = pMovimientoAlmacen.Precio
                    ,REFNBR = pMovimientoAlmacen.DocumentoReferencia
                    ,STKFISI = pMovimientoAlmacen.EnInventarioFisico
                    ,SITEID = pMovimientoAlmacen.CodigoAlmacen
                    ,INVTIDSKU = pMovimientoAlmacen.CodigoArticulo
                    ,TYPEDOCID = pMovimientoAlmacen.CodigoTipoMovimientoAlmacen
                    ,DOCTYPEID = pMovimientoAlmacen.CodigoTipoDocumentoReferencia
                });
            }
        }
    }
}