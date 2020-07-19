using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Configuraciones;
using PtoVta.Aplicacion.GestionConfiguraciones;

namespace PtoVta.API.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]    
    public class GestionConfiguracionesController : ControllerBase
    {
        readonly IServicioAplicacionConfiguracion _IServicioAplicacionConfiguracion;


        public GestionConfiguracionesController(IServicioAplicacionConfiguracion pIServicioAplicacionConfiguracion)
        {
            _IServicioAplicacionConfiguracion = pIServicioAplicacionConfiguracion;                
        }


        [Route("consultarConfiguracionPuntoVenta/{pNombreTerminal}/{pCodigoPuntoDeVenta}")]
        [HttpGet]        
        [ProducesResponseType(typeof(ResultadoServicio<ConfiguracionPuntoVentaDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ConsultarConfiguracionPuntoVenta(string pNombreTerminal, string pCodigoPuntoDeVenta)
        {
            try
            {            
                var configuracionPuntoDeVenta = _IServicioAplicacionConfiguracion.BuscarConfiguracionPuntoVenta(pNombreTerminal, pCodigoPuntoDeVenta);
                if (configuracionPuntoDeVenta == null)
                {
                    return NotFound();
                }
                return Ok(configuracionPuntoDeVenta);
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<ConfiguracionPuntoVentaDTO>(0,"Configuracion de punto de venta no existe.", ex.Message, null, null)
                );                
            }
        }


        [Route("consultarConfiguracionGlobal")]
        [HttpGet]        
        [ProducesResponseType(typeof(ResultadoServicio<ConfiguracionGlobalDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ConsultarConfiguracionGlobal()
        {
            try
            {            
                var configuracionGlobal = _IServicioAplicacionConfiguracion.BuscarConfiguracionGlobal();
                if (configuracionGlobal == null)
                {
                    return NotFound();
                }
                return Ok(configuracionGlobal);
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<ConfiguracionGlobalDTO>(0,"No se pudo recuperar la configuracion global del sistema.", ex.Message, null, null)
                );                
            }
        }                
    }
}