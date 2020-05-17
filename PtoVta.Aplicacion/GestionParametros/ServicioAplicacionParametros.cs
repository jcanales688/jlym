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

        public List<CategoriaArticuloDTO> ObtenerCategorias()
        {
            var categorias = _IRepositorioCategoriaArticulo.ObtenerTodos();

            if (categorias != null && categorias.Any())
            {
                return categorias.ProyectadoComoColeccion<CategoriaArticuloDTO>();
            }
            else
                return null;
        }
    }
}