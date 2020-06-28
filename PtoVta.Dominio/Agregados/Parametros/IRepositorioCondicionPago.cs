using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioCondicionPago: IRepositorio<CondicionPago>
    {
        CondicionPago ObtenerPorCodigo(string pCodigoCondicionPago);
    }
}