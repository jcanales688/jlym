using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Configuraciones;

namespace PtoVta.Aplicacion.GestionConfiguraciones
{
    public interface IServicioAplicacionConfiguracion
    {
        ResultadoServicio<ConfiguracionPuntoVentaDTO> BuscarConfiguracionPuntoVenta(string pNombreTerminal, string pCodigoPuntoDeVenta);
        ResultadoServicio<ConfiguracionGlobalDTO> BuscarConfiguracionGlobal();        
    }
}   