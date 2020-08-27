using System;
using PtoVta.Aplicacion.DTO.Configuraciones;

namespace PtoVta.Aplicacion.BaseTrabajo
{
    public interface IConfiguracionGlobalUnificado
    {
        ConfiguracionGlobalDTO UnificarConfiguracionGlobal();
    }
}