using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public interface IRepositorioCliente : IRepositorio<Cliente>
    { 
        Cliente ObtenerClientePorRUC(string pClienteRUC);
        Cliente ObtenerPorCodigo(string pCodigoCliente);
    }
}