using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public interface IRepositorioConfiguracionFormatoTicket : IRepositorio<ConfiguracionFormatoTicket>
    {
        ConfiguracionFormatoTicket Obtener();
    }
}