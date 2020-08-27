using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Aplicacion.GestionPedidos;

namespace PtoVta.API.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]    
    public class GestionPedidosController : ControllerBase
    {
        readonly IServicioAplicacionPedidos _IServicioAplicacionPedidos;

        public GestionPedidosController(IServicioAplicacionPedidos pIServicioAplicacionPedidos)
        {
            _IServicioAplicacionPedidos =  pIServicioAplicacionPedidos;
        }


        [Route("agregarNuevoPedidoEESS")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AgregarNuevoPedidoEESS([FromBody]PedidoEESSDTO pPedidoEESSDTO)
        {
            try
            { 
                if (pPedidoEESSDTO == null)
                    return BadRequest("Pedido EESS a grabar no puede ser nulo."); 

                var estadoPedidoEESSAgregado = _IServicioAplicacionPedidos.AgregarNuevoPedidoEESS(pPedidoEESSDTO);
                if(estadoPedidoEESSAgregado == null)
                {
                    return BadRequest(
                        new ResultadoServicio<ResultadoPedidoEESSGrabadoDTO>(6,"Creacion de nuevo Pedido EESS fallo.", "", null, null)
                    );
                }

                return Ok(estadoPedidoEESSAgregado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResultadoServicio<ResultadoPedidoEESSGrabadoDTO>(6,"Creacion de nuevo Pedido EESS fallo.", ex.Message, null, null)
                );    
            }
        }

        [Route("agregarNuevoPedidoRetail")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AgregarNuevoPedidoRetail([FromBody]PedidoRetailDTO pPedidoRetailDTO)
        {
            try
            { 
                if (pPedidoRetailDTO == null)
                    return BadRequest("Pedido Retail a grabar no puede ser nulo."); 

                var estadoPedidoRetailAgregado = _IServicioAplicacionPedidos.AgregarNuevoPedidoRetail(pPedidoRetailDTO);
                if(estadoPedidoRetailAgregado == null)
                {
                    return BadRequest(
                        new ResultadoServicio<ResultadoPedidoRetailGrabadoDTO>(6,"Creacion de nuevo Pedido Retail fallo.", "", null, null)
                    );
                }

                return Ok(estadoPedidoRetailAgregado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResultadoServicio<ResultadoPedidoRetailGrabadoDTO>(6,"Creacion de nuevo Pedido Retail fallo.", ex.Message, null, null)
                );    
            }
        }


        [Route("consultarPedidoEESSPorNumero/{pCorrelativo}")]
        [HttpGet]        
        [ProducesResponseType(typeof(ResultadoServicio<PedidoEESSDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ConsultarPedidoEESSPorNumero(int pCorrelativo)
        {
            try
            {            
                var pedidoEESSConsultado = _IServicioAplicacionPedidos.BuscarPedidoEESSPorNumero(pCorrelativo);
                if (pedidoEESSConsultado == null)
                {
                    return NotFound();
                }
                return Ok(pedidoEESSConsultado);
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<PedidoEESSDTO>(6,"Pedido EESS consultado no existe.", ex.Message, null, null)
                );                
            }
        }

        [Route("ConsultarPedidoRetailPorNumero/{pCorrelativo}")]
        [HttpGet]        
        [ProducesResponseType(typeof(ResultadoServicio<PedidoRetailDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ConsultarPedidoRetailPorNumero(int pCorrelativo)
        {
            try
            {            
                var pedidoRetailConsultado = _IServicioAplicacionPedidos.BuscarPedidoRetailPorNumero(pCorrelativo);
                if (pedidoRetailConsultado == null)
                {
                    return NotFound();
                }
                return Ok(pedidoRetailConsultado);
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<PedidoRetailDTO>(6,"Pedido Retail consultado no existe.", ex.Message, null, null)
                );                
            }
        }

        [Route("consultarPedidosEESSPorPuntoDeVenta/{pCodigoPuntoDeVenta}")]
        [HttpGet]
        [ProducesResponseType(typeof(ResultadoServicio<PedidoEESSListadoDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public  IActionResult ConsultarPedidosEESSPorPuntoDeVenta(string pCodigoPuntoDeVenta)
        {
            try
            {
                var pedidosEESS = _IServicioAplicacionPedidos.BuscarPedidoEESSPorPuntoDeVenta(pCodigoPuntoDeVenta);
                if (pedidosEESS == null)
                {
                    return NotFound();
                }

                return Ok(pedidosEESS);             
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<PedidoEESSListadoDTO>(6,"Consulta de pedidos EESS no obtuvo resultados.", ex.Message, null, null)
                );   
            }
        }


        [Route("consultarPedidoRetailPorPuntoDeVenta/{pCodigoPuntoDeVenta}")]
        [HttpGet]
        [ProducesResponseType(typeof(ResultadoServicio<PedidoRetailListadoDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public  IActionResult ConsultarPedidosRetailPorPuntoDeVenta(string pCodigoPuntoDeVenta)
        {
            try
            {
                var pedidosRetail = _IServicioAplicacionPedidos.BuscarPedidoRetailPorPuntoDeVenta(pCodigoPuntoDeVenta);
                if (pedidosRetail == null)
                {
                    return NotFound();
                }

                return Ok(pedidosRetail);             
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<PedidoRetailListadoDTO>(6,"Consulta de pedidos retail no obtuvo resultados.", ex.Message, null, null)
                );   
            }
        }                

    }
}