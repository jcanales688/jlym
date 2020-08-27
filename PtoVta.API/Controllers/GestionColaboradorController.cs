using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Aplicacion.GestionColaborador;

namespace PtoVta.API.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class GestionColaboradorController : ControllerBase
    {
        readonly IServicioAplicacionVendedor _IServicioAplicacionVendedor;

        public GestionColaboradorController(IServicioAplicacionVendedor pIServicioAplicacionVendedor)
        {
            _IServicioAplicacionVendedor = pIServicioAplicacionVendedor;
        }


        [Route("agregarVendedor")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AgregarVendedor([FromBody]VendedorDTO pVendedor)
        {
            try
            { 
                if (pVendedor == null)
                    return BadRequest("Vendedor a grabar no puede ser nulo."); 

                var estadoUsuarioVendorAgregado = _IServicioAplicacionVendedor.AgregarNuevoUsuarioVendedor(pVendedor);

                if(estadoUsuarioVendorAgregado == null)
                {
                    return BadRequest(
                        new ResultadoServicio<VendedorDTO>(6,"Creacion de nuevo Vendedor fallo.", "", null, null)
                    );
                }

                return Ok(estadoUsuarioVendorAgregado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResultadoServicio<VendedorDTO>(6,"Creacion de nuevo Vendedor fallo.", ex.Message, null, null)
                );    
            }

        }
    }
}
