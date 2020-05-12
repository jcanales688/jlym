using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Infraestructura.BaseTrabajo
{
    public class Repositorio<TEntidad> : IRepositorio<TEntidad>
        where TEntidad : Entidad
    {
        public string CadenaConexion => ConfiguracionGlobal.CadenaConexionBd;

        public virtual void Agregar(TEntidad item)
        {
            throw new NotImplementedException();
        }

        public virtual void Eliminar(TEntidad item)
        {
            throw new NotImplementedException();
        }

        public virtual void Modificar(TEntidad item)
        {
            throw new NotImplementedException();
        }

        public virtual TEntidad Obtener(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntidad> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public virtual void Unificar(TEntidad persistido, TEntidad actual)
        {
            throw new NotImplementedException();
        }

        // public void Dispose()
        // {
        //     throw new NotImplementedException();
        // }

    }
}
