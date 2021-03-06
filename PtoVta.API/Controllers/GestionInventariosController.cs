using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Inventarios;
using PtoVta.Aplicacion.GestionInventarios;

namespace PtoVta.API.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class GestionInventariosController : ControllerBase
    {
        readonly IServicioAplicacionArticulo _IServicioAplicacionArticulo;

        public GestionInventariosController(IServicioAplicacionArticulo pIServicioAplicacionArticulo)
        {
            _IServicioAplicacionArticulo = pIServicioAplicacionArticulo;
        }


        [Route("todosArticulosPorCategoriaYSubCategoria/{pCodigoCategoria}/{pCodigoSubCategoria}/{pCodigoAlmacen}")]
        [HttpGet]
        [ProducesResponseType(typeof(ResultadoServicio<ArticuloDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public  IActionResult ConsultarArticulosPorCategoriaYSubcategoria(string pCodigoCategoria, string pCodigoSubCategoria, string pCodigoAlmacen)
        {
            try
            {
                var articulos = _IServicioAplicacionArticulo
                                        .ObtenerPorCategoriaYSubcategoria(pCodigoCategoria, pCodigoSubCategoria, pCodigoAlmacen);

                if (articulos == null)
                {
                    return NotFound();
                }

                return Ok(articulos);             
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<ArticuloDTO>(6,"Problemas al recuperar las Articulos.", ex.Message, null, null)
                );   
            }
        }



        [Route("consultarPrecioVentaDeArticulo/{pCodigoCliente}/{pCodigoArticulo}/{pCodigoAlmacen}")]
        [HttpGet]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public  IActionResult ConsultarPrecioVentaDeArticulo(string pCodigoCliente, string pCodigoArticulo, string pCodigoAlmacen)
        {
            try
            {
                var precioVentaArticulo = _IServicioAplicacionArticulo.ObtenerPrecioVentaDeArticulo(pCodigoCliente, 
                                                                                pCodigoArticulo, pCodigoAlmacen);

                if (precioVentaArticulo == 0)
                {
                    return NotFound();
                }

                return Ok(precioVentaArticulo);             
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ResultadoServicio<ArticuloDTO>(6,"Problemas al recuperar el precio de venta del articulo.", ex.Message, null, null)
                );   
            }
        }        
    }
}
