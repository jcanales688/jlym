using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public interface IRepositorioConfiguracionPuntoVenta : IRepositorio<ConfiguracionPuntoVenta>
    {
        ConfiguracionPuntoVenta ConsultarTerminalPuntoVenta(string pNombreTerminal, string pNombrePuntoDeVenta);   

        ConfiguracionPuntoVenta ObtenerPorCodigo(string pCodigoConfiguracionPuntoVenta);

    }
}