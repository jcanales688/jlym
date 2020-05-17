using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Aplicacion.GestionUsuario;

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
        public IActionResult AgregarVendedor(VendedorDTO pVendedor)
        {
            try
            { 
                if (pVendedor == null)
                    return BadRequest("Vendedor a grabar no puede ser nulo."); 

                var estadoUsuarioAgregado = _IServicioAplicacionVendedor.AgregarNuevoUsuarioVendedor(pVendedor);

                if(estadoUsuarioAgregado == null)
                {
                    return BadRequest(
                        new ResultadoServicio<VendedorDTO>(0,"Creacion de nuevo Vendedor fallo.", "", null, null)
                    );
                }

                return Ok(estadoUsuarioAgregado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResultadoServicio<VendedorDTO>(0,"Creacion de nuevo Vendedor fallo.", ex.Message, null, null)
                );    
            }

        }
    }
}