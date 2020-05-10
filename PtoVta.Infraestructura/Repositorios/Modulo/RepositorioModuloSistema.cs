using System;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Modulo
{
    public class RepositorioModuloSistema : Repositorio<Vendedor>, IRepositorioVendedor
    {
        public override void Unificar(Vendedor persistido, Vendedor actual){

        }

        
        public Vendedor ObtenerVendedorPorUsuario(string pUsuarioVendedor)
        {
            throw new NotImplementedException();
        }
    }
}
