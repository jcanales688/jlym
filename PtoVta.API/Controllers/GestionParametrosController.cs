using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Aplicacion.GestionParametros;

namespace PtoVta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionParametrosController : ControllerBase
    {
        readonly IServicioAplicacionParametros _IServicioAplicacionParametros;

        public GestionParametrosController(IServicioAplicacionParametros pIServicioAplicacionParametros)
        {
            _IServicioAplicacionParametros = pIServicioAplicacionParametros;
        }


        [Route("todasCategorias/{pTipoNegocio}")]
        [HttpGet]
        [ProducesResponseType(typeof(ResultadoServicio<CategoriaArticuloDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ConsultarCategorias(string pTipoNegocio)
        {
            try
            {
                var categorias = _IServicioAplicacionParametros.ObtenerCategorias(pTipoNegocio);

                if (categorias == null)
                {
                    return NotFound();
                }

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<CategoriaArticuloDTO>(0, "Problemas al recuperar las Categorias.", ex.Message, null, null)
                );
            }
        }
    }
}
