using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public interface IRepositorioConfiguracionGeneral : IRepositorio<ConfiguracionGeneral>
    {
        ConfiguracionGeneral Obtener();
    }
}