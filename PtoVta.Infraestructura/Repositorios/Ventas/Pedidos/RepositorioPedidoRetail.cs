using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Ventas
{
    public class RepositorioPedidoRetail : Repositorio<PedidoRetail>, IRepositorioPedidoRetail
    {
        public RepositorioPedidoRetail(string pCadenaConexion)
        {
            this.CadenaConexion = pCadenaConexion;
        }        

        public override void  Agregar(PedidoRetail pPedidoRetail)
        {
            
        }

        public PedidoRetail ObtenerPorNumeroPedido(int pCorrelativo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoRetail> ObtenerTodos(string pCodigoConfiguracionPuntoVenta)
        {
            throw new NotImplementedException();
        }
    }
}