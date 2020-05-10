using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Infraestructura.BaseTrabajo
{
    public class Sql : ISql
    {
        public int EjecutarComando(string sqlCommand, params object[] parametros)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntidad> EjecutarQuery<TEntidad>(string sqlQuery, params object[] parametros)
        {
            throw new NotImplementedException();
        }
    }
}
