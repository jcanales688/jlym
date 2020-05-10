using System;
using System.Collections.Generic;

namespace PtoVta.Dominio.BaseTrabajo
{
    public interface ISql
    {
        /*
            Resumen: Ejecutar consulta específica con underliying almacén de persistencia
            Tipo de entidad para asignar resultados de consulta
            Dialecto Consulta:
            SELECT idCustomer, Nombre FROM dbo. [Clientes] where idCustomer> {0}
            Parámetros: un vector de parámetros de valores
            Devoluciones: resultados Enumerable
         */
        IEnumerable<TEntidad> EjecutarQuery<TEntidad>(string sqlQuery, params object[] parametros);

        /*
            Ejecución de comandos arbitrarios en underliying almacén de persistencia
            Comando para ejecutar:
            SELECT idCustomer, Nombre FROM dbo. [Clientes] where  idCustomer> {0}
            Parámetros: un vector de parámetros de valores
            Devuelve: el número de registros afectados
         */
        int EjecutarComando(string sqlCommand, params object[] parametros);        
    }
}
