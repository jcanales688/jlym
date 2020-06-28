using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioMoneda : IRepositorio<Moneda>
    {
        Moneda ObtenerPorCodigo(string pCodigoMoneda);
    }
}