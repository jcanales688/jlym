using System;
using PtoVta.Dominio.Agregados.Modulo;

namespace PtoVta.Dominio.Agregados.Usuario
{
    public interface IServicioDominioValidarUsuarioSistema
    {
        bool ValidarUsuarioSistema(UsuarioSistema pUsuarioSistema, ModuloSistema pModuloSistema, string pClave);
    }
}
