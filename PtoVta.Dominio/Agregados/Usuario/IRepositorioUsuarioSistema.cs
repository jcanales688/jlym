using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Usuario
{
    public interface IRepositorioUsuarioSistema : IRepositorio<UsuarioSistema>
    {
        UsuarioSistema ObtenerUsuarioSistemaPorUsuario(string pUsuarioDeSistema);        
        UsuarioSistema ObtenerUsuarioSistemaPorUsuario(string pUsuarioDeSistema, string pContraseña);        
    }
}
