using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public interface IRepositorioListaPrecioCliente: IRepositorio<ListaPrecioCliente>
    {
        ListaPrecioCliente ObtenerListaPrecioCliente(string pCodigoCliente, string pCodigoArticulo, 
                                                    string pCodigoAlmacen, string pFechaProcesoVentas);        
    }
}