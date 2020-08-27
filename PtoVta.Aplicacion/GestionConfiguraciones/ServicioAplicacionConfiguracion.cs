using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Configuraciones;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Infraestructura.Transversales.Log;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.GestionConfiguraciones
{
    public class ServicioAplicacionConfiguracion : IServicioAplicacionConfiguracion
    {
        private IRepositorioConfiguracionPuntoVenta _IRepositorioConfiguracionPuntoVenta;        
        private IRepositorioConfiguracionFormatoTicket _IRepositorioConfiguracionFormatoTicket;
        private IRepositorioConfiguracionGeneral _IRepositorioConfiguracionGeneral;
        private IRepositorioConfiguracionInventario _IRepositorioConfiguracionInventario;
        private IRepositorioConfiguracionVenta _IRepositorioConfiguracionVenta;

        private IConfiguracionGlobalUnificado _IConfiguracionGlobalUnificado;


        public ServicioAplicacionConfiguracion(IRepositorioConfiguracionPuntoVenta pIRepositorioConfiguracionPuntoVenta, 
                                            IRepositorioConfiguracionFormatoTicket pIRepositorioConfiguracionFormatoTicket,
                                            IRepositorioConfiguracionGeneral pIRepositorioConfiguracionGeneral, 
                                            IRepositorioConfiguracionInventario pIRepositorioConfiguracionInventario,                                             
                                            IRepositorioConfiguracionVenta pIRepositorioConfiguracionVenta,
                                            IConfiguracionGlobalUnificado pIConfiguracionGlobalUnificado)
        {
            if (pIRepositorioConfiguracionPuntoVenta == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionPuntoVenta Nulo en ServicioAplicacionConfiguracion"); 

            if (pIRepositorioConfiguracionFormatoTicket == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionFormatoTicket Nulo en ServicioAplicacionConfiguracion");

            if (pIRepositorioConfiguracionGeneral == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionGeneral Nulo en ServicioAplicacionConfiguracion");

            if (pIRepositorioConfiguracionInventario == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionInventario Nulo en ServicioAplicacionConfiguracion");    

            if (pIRepositorioConfiguracionVenta == null)
                throw new ArgumentNullException("pIRepositorioConfiguracionVenta Nulo en ServicioAplicacionConfiguracion"); 

            if (pIConfiguracionGlobalUnificado == null)
                throw new ArgumentNullException("pIConfiguracionGlobal Nulo en ServicioAplicacionConfiguracion"); 

            _IRepositorioConfiguracionPuntoVenta = pIRepositorioConfiguracionPuntoVenta; 
            _IRepositorioConfiguracionFormatoTicket = pIRepositorioConfiguracionFormatoTicket;                 
            _IRepositorioConfiguracionGeneral = pIRepositorioConfiguracionGeneral;
            _IRepositorioConfiguracionInventario = pIRepositorioConfiguracionInventario;                                                              
            _IRepositorioConfiguracionVenta = pIRepositorioConfiguracionVenta;
            _IConfiguracionGlobalUnificado = pIConfiguracionGlobalUnificado;
        }

        public ResultadoServicio<ConfiguracionPuntoVentaDTO> BuscarConfiguracionPuntoVenta(string pNombreTerminal, 
                                                                        string pCodigoPuntoDeVenta)
        {
            var configuracionPuntoDeVenta = _IRepositorioConfiguracionPuntoVenta.ObtenerPorTerminalYPuntoVenta(pNombreTerminal,
                                                                                            pCodigoPuntoDeVenta);
            if (configuracionPuntoDeVenta != null)
            {          
                return new ResultadoServicio<ConfiguracionPuntoVentaDTO>(7, Mensajes.advertencia_ConsultaConfiguracionPuntoDeVentaExitosa,
                                                string.Empty, configuracionPuntoDeVenta.ProyectadoComo<ConfiguracionPuntoVentaDTO>(), null);
            }
            else                
                return null;
        }

        public ResultadoServicio<ConfiguracionGlobalDTO> BuscarConfiguracionGlobal()
        {
            return new ResultadoServicio<ConfiguracionGlobalDTO>(7, Mensajes.advertencia_ConsultaConfiguracionGlobalExitosa,
                                            string.Empty, _IConfiguracionGlobalUnificado.UnificarConfiguracionGlobal(), null);                        

        }
    }
}