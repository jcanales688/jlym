using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Aplicacion.GestionVentas;

namespace PtoVta.API.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]    
    public class GestionVentasController : ControllerBase
    {
        readonly IServicioAplicacionFacturacion _IServicioAplicacionFacturacion;

        public GestionVentasController(IServicioAplicacionFacturacion pIServicioAplicacionFacturacion)
        {
            _IServicioAplicacionFacturacion = pIServicioAplicacionFacturacion;
        }


        [Route("agregarNuevaVenta")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AgregarNuevaVenta([FromBody]VentaDTO pVentaDTO)
        {
            try
            { 
                if (pVentaDTO == null)
                    return BadRequest("Venta a grabar no puede ser nulo."); 

                var estadoVentaAgregado = _IServicioAplicacionFacturacion.AgregarNuevaVenta(pVentaDTO);
                if(estadoVentaAgregado == null)
                {
                    return BadRequest(
                        new ResultadoServicio<ResultadoVentaGrabadaDTO>(0,"Creacion de nueva venta fallo.", "", null, null)
                    );
                }

                return Ok(estadoVentaAgregado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResultadoServicio<ResultadoVentaGrabadaDTO>(0,"Creacion de nueva venta fallo.", ex.Message, null, null)
                );    
            }
        }


        [Route("agregarNuevaVentaDesdePedidoRetail")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AgregarNuevaVentaDesdePedidoRetail([FromBody]int pCorrelativoPedido)
        {
            try
            { 
                if (pCorrelativoPedido == 0)
                    return BadRequest("Correlativo de pedido no puede ser vacio o nulo."); 

                var estadoVentaDesdePedidoRetailAgregado = _IServicioAplicacionFacturacion.AgregarNuevaVentaDesdePedidoRetail(pCorrelativoPedido);
                if(estadoVentaDesdePedidoRetailAgregado == null)
                {
                    return BadRequest(
                        new ResultadoServicio<ResultadoVentaGrabadaDTO>(0,"Creacion de nueva venta desde pedido retail fallo.", "", null, null)
                    );
                }

                return Ok(estadoVentaDesdePedidoRetailAgregado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResultadoServicio<ResultadoVentaGrabadaDTO>(0,"Creacion de nueva venta desde pedido retail fallo.", ex.Message, null, null)
                );    
            }
        }


        [Route("agregarNuevaVentaDesdePedidoEESS")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AgregarNuevaVentaDesdePedidoEESS([FromBody]int pCorrelativoPedido)
        {
            try
            { 
                if (pCorrelativoPedido == 0)
                    return BadRequest("Correlativo de pedido no puede ser vacio o nulo."); 

                var estadoVentaDesdePedidoEESSAgregado = _IServicioAplicacionFacturacion.AgregarNuevaVentaDesdePedidoEESS(pCorrelativoPedido);
                if(estadoVentaDesdePedidoEESSAgregado == null)
                {
                    return BadRequest(
                        new ResultadoServicio<ResultadoVentaGrabadaDTO>(0,"Creacion de nueva venta desde pedido EESS fallo.", "", null, null)
                    );
                }

                return Ok(estadoVentaDesdePedidoEESSAgregado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResultadoServicio<ResultadoVentaGrabadaDTO>(0,"Creacion de nueva venta desde pedido EESS fallo.", ex.Message, null, null)
                );    
            }
        }


        [Route("consultarVentas/{pCodigoAlmacen}/{pFechaProcesoInicio}/{pFechaProcesoFin}/{pNumeroDocumento}/{pCodigoTipoNegocio}")]
        [HttpGet]
        [ProducesResponseType(typeof(ResultadoServicio<VentaListadoDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public  IActionResult ConsultarVentas(string pCodigoAlmacen, string pFechaProcesoInicio, string pFechaProcesoFin, 
                                            string pNumeroDocumento, string pCodigoTipoNegocio)
        {
            try
            {
                var ventas = _IServicioAplicacionFacturacion.BuscarVentas(pCodigoAlmacen, pFechaProcesoInicio, pFechaProcesoFin, 
                                                                        pNumeroDocumento, pCodigoTipoNegocio);
                if (ventas == null)
                {
                    return NotFound();
                }

                return Ok(ventas);             
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<VentaListadoDTO>(0,"Consulta de ventas no obtuvo resultados.", ex.Message, null, null)
                );   
            }
        }        

    }
}