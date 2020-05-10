using System;
using System.Collections.Generic;

namespace PtoVta.Dominio.BaseTrabajo
{
    public interface IRepositorio<TEntidad> : IDisposable
        where TEntidad : Entidad
    {
        //Añadir elemento en el repositorio            
        void Agregar(TEntidad item);
        void Eliminar(TEntidad item);

        //Item marcado como modificado
        void Modificar(TEntidad item);

        //Merge
        /*
            Establece entidad modificado en el repositorio. Cuando se llama al método Commit () 
            en UnitOfWork estos cambios se guardarán en el almacenamiento
       */
        void Unificar(TEntidad persistido, TEntidad actual);

        //Obtener elemento por clave de entidad
        TEntidad Obtener(Guid id);

        //Obtener todos los elementos de tipo TEntity en el repositorio
        IEnumerable<TEntidad> ObtenerTodos();        
    }
}
