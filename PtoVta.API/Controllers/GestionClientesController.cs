using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Aplicacion.GestionClientes;

namespace PtoVta.API.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]    
    public class GestionClientesController : ControllerBase
    {
        readonly IServicioAplicacionCliente _IServicioAplicacionCliente;


        public GestionClientesController(IServicioAplicacionCliente pIServicioAplicacionCliente)
        {
            _IServicioAplicacionCliente = pIServicioAplicacionCliente;
        }


        [Route("agregarCliente")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AgregarNuevoCliente([FromBody]ClienteDTO pClienteDTO)
        {
            try
            { 
                if (pClienteDTO == null)
                    return BadRequest("Cliente a grabar no puede ser nulo."); 

                var estadoClienteAgregado = _IServicioAplicacionCliente.AgregarNuevoCliente(pClienteDTO);
                if(estadoClienteAgregado == null)
                {
                    return BadRequest(
                        new ResultadoServicio<ResultadoClienteGrabadoDTO>(6,"Creacion de nuevo Cliente fallo.", "", null, null)
                    );
                }

                return Ok(estadoClienteAgregado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResultadoServicio<ResultadoClienteGrabadoDTO>(6,"Creacion de nuevo Cliente fallo.", ex.Message, null, null)
                );    
            }

        }


        [Route("consultarClientePorRUC/{pClienteRUC}/{pCodigoAlmacen}")]
        [HttpGet]        
        [ProducesResponseType(typeof(ResultadoServicio<ClienteDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ConsultarClientePorRUC(string pClienteRUC, string pCodigoAlmacen)
        {
            try
            {            
                var cliente = _IServicioAplicacionCliente.BuscarClientePorRUC(pClienteRUC, pCodigoAlmacen);
                if (cliente == null)
                {
                    return NotFound();
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<ClienteDTO>(0,"Cliente consultado no existe.", ex.Message, null, null)
                );                
            }
        }


        [Route("consultarTodosClientes")]
        [HttpGet]
        [ProducesResponseType(typeof(ResultadoServicio<ClienteListadoDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public  IActionResult ConsultarTodosClientes()
        {
            try
            {
                var clientes = _IServicioAplicacionCliente.BuscarTodosClientes();
                if (clientes == null)
                {
                    return NotFound();
                }

                return Ok(clientes);             
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<ClienteListadoDTO>(0,"Consulta de clientes no obtuvo resultados.", ex.Message, null, null)
                );   
            }
        }                
    }
}