using System;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Colaborador
{
    public class RepositorioVendedor : Repositorio<Vendedor>, IRepositorioVendedor
    {
        public Vendedor ObtenerVendedorPorUsuario(string pUsuarioVendedor)
        {
            throw new NotImplementedException();
        }
    }
}
