using System;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.BaseTrabajo;

namespace PtoVta.Infraestructura.Repositorios.Usuario
{
    public class RepositorioUsuarioSistema : Repositorio<UsuarioSistema>, IRepositorioUsuarioSistema
    {
        public UsuarioSistema ObtenerUsuarioSistemaPorUsuario(string pUsuarioDeSistema, string pContraseña)
        {
            throw new NotImplementedException();
        }
    }
}
