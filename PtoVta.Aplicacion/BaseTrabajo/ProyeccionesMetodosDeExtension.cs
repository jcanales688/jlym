using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;
using PtoVta.Infraestructura.Transversales.Adaptador;

namespace PtoVta.Aplicacion.BaseTrabajo
{
    public static class ProyeccionesMetodosDeExtension
    {
        //Proyectar un tipo con un DTO
        //TProyeccion: La proyección dto
        //item: La entidad de origen para proyectar
        //Result: el tipo proyectado
        public static TProyeccion ProyectadoComo<TProyeccion>(this Entidad item)
            where TProyeccion : class, new()
        {
            var adaptador = TipoAdaptadorFactory.CrearAdaptador();
            return adaptador.Adapt<TProyeccion>(item);
        }
            
        //proyectado una colección enumerable de artículos
        //TProyeccion: El tipo de proyección dto
        //iems: la colección de elementos entidad
        //retorna: colección proyectado
        public static List<TProyeccion> ProyectadoComoColeccion<TProyeccion>(this IEnumerable<Entidad> items)
        {
            var adaptador = TipoAdaptadorFactory.CrearAdaptador();
            return adaptador.Adapt<List<TProyeccion>>(items);
        }       
    }
}
