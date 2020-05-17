using System;
using System.Collections.Generic;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Parametros;

namespace PtoVta.Aplicacion.GestionParametros
{
    public interface IServicioAplicacionParametros
    {
        ResultadoServicio<CategoriaArticuloDTO> ObtenerCategorias();      
    }    
}