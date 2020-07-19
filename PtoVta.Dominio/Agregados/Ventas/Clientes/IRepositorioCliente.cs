using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public interface IRepositorioCliente : IRepositorio<Cliente>
    { 
        Cliente ObtenerClientePorRUC(string pClienteRUC, string pCodigoAlmacen) ;
        Cliente ObtenerPorCodigo(string pCodigoCliente);

    }
}