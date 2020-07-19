using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public interface IRepositorioConfiguracionVenta: IRepositorio<ConfiguracionVenta>
    {
        ConfiguracionVenta Obtener();
    }
}