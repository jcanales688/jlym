using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioEstadoDocumento : IRepositorio<EstadoDocumento>
    {
        EstadoDocumento ObtenerPorCodigo(string CodigoEstadoDocumento);
    }
}