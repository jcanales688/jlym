using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.GestionUsuario;

namespace PtoVta.API.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class GestionUsuarioController : ControllerBase
    {
        readonly IServicioAplicacionInicioSession _IServicioAplicacionInicioSession;

        public GestionUsuarioController(IServicioAplicacionInicioSession pIServicioAplicacionInicioSession)
        {
            _IServicioAplicacionInicioSession = pIServicioAplicacionInicioSession;
        }


        [Route("autenticacionUsuario/{pUsuario}/{pClave}/{pCodigoModuloSistema}")]
        [HttpGet]        
        [ProducesResponseType(typeof(ResultadoServicio<ModuloSistemaDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult AutenticacionUsuario(string pUsuario, string pClave, string pCodigoModuloSistema)
        {
            try
            {            
                var autenticacionUsuario = _IServicioAplicacionInicioSession
                            .GestionInicioSesion(pUsuario, pClave, pCodigoModuloSistema);

                if (autenticacionUsuario == null)
                {
                    return NotFound();
                }
                return Ok(autenticacionUsuario);
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<ModuloSistemaDTO>(0,"Usuario no existe. Usuario o Clave invalidos", ex.Message, null, null)
                );                
                // return NotFound();
            }
        }
    }
}
