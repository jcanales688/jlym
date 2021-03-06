using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public interface IRepositorioConfiguracionPuntoVenta : IRepositorio<ConfiguracionPuntoVenta>
    {
        void ActualizarCorrelativos(ConfiguracionPuntoVenta pConfiguracionPuntoVenta);        
        ConfiguracionPuntoVenta ObtenerPorTerminalYPuntoVenta(string pNombreTerminal, string pCodigoPuntoDeVenta);   
        ConfiguracionPuntoVenta ObtenerPorPuntoDeVenta(string pCodigoPuntoDeVenta);

    }
}