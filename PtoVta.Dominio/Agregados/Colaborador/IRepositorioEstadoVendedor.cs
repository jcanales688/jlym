using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public interface IRepositorioEstadoVendedor : IRepositorio<EstadoVendedor>
    {
        EstadoVendedor ObtenerPorCodigo(string pCodigoEstadoVendedor);
    }
}
