using System;
using System.Linq;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Inventarios;
using PtoVta.Dominio.Agregados.Inventarios;

namespace PtoVta.Aplicacion.GestionInventarios
{
    public class ServicioAplicacionArticulo : IServicioAplicacionArticulo
    {
        private readonly IRepositorioArticulo _IRepositorioArticulo;
        public ServicioAplicacionArticulo(IRepositorioArticulo pIRepositorioArticulo)
        {
            if (pIRepositorioArticulo == null)
                throw new ArgumentNullException("pIRepositorioArticulo Nulo En ServicioAplicacionInicioSession");

            _IRepositorioArticulo = pIRepositorioArticulo;
        }


        public ResultadoServicio<ArticuloDTO> ObtenerPorCategoriaYSubcategoria(string pCodigoCategoria, string pCodigoSubCategoria)
        {
            var mensajeValidacion = string.Empty;
            var articulos = _IRepositorioArticulo.ObtenerPorCategoriaYSubcategoria(pCodigoCategoria, pCodigoSubCategoria);

            if (articulos != null && articulos.Any())
            {
                mensajeValidacion = "Consulta de Articulos exitosa.";
                return new ResultadoServicio<ArticuloDTO>(7,mensajeValidacion,
                        string.Empty, null,  articulos.ProyectadoComoColeccion<ArticuloDTO>());
            }
            else
            {
                mensajeValidacion = "Consulta de Articulos fallida.";
                return new ResultadoServicio<ArticuloDTO>(7,mensajeValidacion,
                        string.Empty, null,  null);                
            }
        }
    }
}