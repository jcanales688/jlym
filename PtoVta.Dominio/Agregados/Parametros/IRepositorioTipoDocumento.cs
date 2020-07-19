using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioTipoDocumento: IRepositorio<TipoDocumento>
    {
        void ActualizarCorrelativoDocumento(TipoDocumento pTipoDocumento, string pCodigoAlmacen, string pNumeroSerie);
        TipoDocumento ObtenerCorrelativoDocumento(string pCodigoAlmacen, string pCodigoPuntoDeVenta, 
                                            string pCodigoTipoDocumento,string pTipoDeVenta, int pEstado);    

        TipoDocumento ObtenerPorCodigo(string pCodigoTipoDocumento);                                                 
    }
}