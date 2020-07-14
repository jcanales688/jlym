using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public interface IRepositorioPedidoRetail: IRepositorio<PedidoRetail>
    {
        PedidoRetail ObtenerPorNumeroPedido(int pCorrelativo);
        IEnumerable<PedidoRetail> ObtenerTodos(string pCodigoConfiguracionPuntoVenta);
    }
}