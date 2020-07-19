using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;

namespace PtoVta.Aplicacion.GestionClientes
{
    public interface IServicioAplicacionCliente
    {
        ResultadoServicio<ResultadoClienteGrabadoDTO> AgregarNuevoCliente(ClienteDTO pClienteDTO);
        ResultadoServicio<ClienteDTO> BuscarClientePorRUC(string pClienteRUC, string pCodigoAlmacen);
        ResultadoServicio<ClienteListadoDTO> BuscarTodosClientes();
    }
}