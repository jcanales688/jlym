using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public interface IRepositorioPedidoEESS: IRepositorio<PedidoEESS>
    {
        PedidoEESS ObtenerPorNumeroPedido(int pCorrelativo);
        IEnumerable<PedidoEESS> ObtenerTodos(string pCodigoConfiguracionPuntoVenta);        
    }
}