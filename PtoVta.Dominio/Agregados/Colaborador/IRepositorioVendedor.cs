using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public interface IRepositorioVendedor : IRepositorio<Vendedor>
    {
        Vendedor ObtenerVendedorPorUsuario(string pUsuarioVendedor);
    }
}
