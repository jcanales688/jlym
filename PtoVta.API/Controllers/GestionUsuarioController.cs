using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.GestionUsuario;

namespace PtoVta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestionUsuarioController : ControllerBase
    {
        readonly IServicioAplicacionInicioSession _IServicioAplicacionInicioSession;

        public GestionUsuarioController(IServicioAplicacionInicioSession pIServicioAplicacionInicioSession)
        {
            _IServicioAplicacionInicioSession = pIServicioAplicacionInicioSession;
        }


        [Route("autenticacionUsuario/{pUsuario}/{pClave}/{pModuloSistemaId}")]
        [HttpGet]        
        [ProducesResponseType(typeof(ModuloSistemaDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult AutenticacionUsuario(string pUsuario, string pClave, Guid pModuloSistemaId)
        {
            try
            {            
                var autenticacionUsuario = _IServicioAplicacionInicioSession.GestionInicioSesion(pUsuario, pClave, pModuloSistemaId);

                if (autenticacionUsuario == null)
                {
                    return NotFound();
                }
                return Ok(autenticacionUsuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
