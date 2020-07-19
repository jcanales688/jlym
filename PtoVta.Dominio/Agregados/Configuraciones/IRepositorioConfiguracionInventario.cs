using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public interface IRepositorioConfiguracionInventario : IRepositorio<ConfiguracionInventario>
    {
         ConfiguracionInventario Obtener();
    }
}