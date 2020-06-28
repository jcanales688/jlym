using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioTipoDocumento: IRepositorio<TipoDocumento>
    {
        TipoDocumento ObtenerCorrelativoDocumento(string pCodigoAlmacen, string pCodigoConfiguracionPuntoVenta, 
                                            string pCodigoTipoDocumento,string pTipoDeVenta, int pEstado);    

        TipoDocumento ObtenerPorCodigo(string pCodigoTipoDocumento);                                                 
    }
}