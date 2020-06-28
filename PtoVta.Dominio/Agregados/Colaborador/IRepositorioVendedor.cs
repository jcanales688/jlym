using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public interface IRepositorioVendedor : IRepositorio<Vendedor>
    {
        Vendedor ObtenerVendedorPorUsuario(string pUsuarioVendedor);

        Vendedor ObtenerVendedorPendienteCierre(DateTime pFechaProceso, string pCodigoConfiguracionPuntoVenta);    

        Vendedor ObtenerPorCodigo(string pCodigoVendedor);            
    }
}
