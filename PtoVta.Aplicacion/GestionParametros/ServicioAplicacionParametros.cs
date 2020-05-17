using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Dominio.Agregados.Parametros;

namespace PtoVta.Aplicacion.GestionParametros
{
    public class ServicioAplicacionParametros : IServicioAplicacionParametros
    {
        private readonly IRepositorioCategoriaArticulo _IRepositorioCategoriaArticulo;

        public ServicioAplicacionParametros(IRepositorioCategoriaArticulo pIRepositorioCategoriaArticulo)
        {
            if (pIRepositorioCategoriaArticulo == null)
                throw new ArgumentNullException("pIRepositorioCategoriaArticulo Nulo En ServicioAplicacionInicioSession");

            _IRepositorioCategoriaArticulo = pIRepositorioCategoriaArticulo;
        }
 
        public ResultadoServicio<CategoriaArticuloDTO> ObtenerCategorias()
        {
            var mensajeValidacion = string.Empty;
            var categorias = _IRepositorioCategoriaArticulo.ObtenerTodos();

            if (categorias != null && categorias.Any())
            {
                mensajeValidacion = "Consulta de Categorias exitosa.";
                return new ResultadoServicio<CategoriaArticuloDTO>(7,mensajeValidacion,
                        string.Empty, null,  categorias.ProyectadoComoColeccion<CategoriaArticuloDTO>());
            }
            else
            {
                mensajeValidacion = "Consulta de Categorias fallida.";
                return new ResultadoServicio<CategoriaArticuloDTO>(7,mensajeValidacion,
                        string.Empty, null,  null);                
            }
        }
    }
}