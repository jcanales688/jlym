using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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


        [Route("todasCategorias")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoriaArticuloDTO>), (int)HttpStatusCode.OK)]
        public  IActionResult ConsultarCategorias()
        {
            var categorias = _IServicioAplicacionParametros.ObtenerCategorias();

            return Ok(categorias);
        }
    }
}
