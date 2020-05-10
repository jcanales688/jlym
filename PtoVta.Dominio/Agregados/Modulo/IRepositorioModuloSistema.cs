using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Modulo
{
    public interface IRepositorioModuloSistema: IRepositorio<ModuloSistema>
    {
        ModuloSistema ObtenerDerechosAccesosUsuario(Guid pUsuarioSistemaId, Guid pModuloSistemaId);        
    }
}
