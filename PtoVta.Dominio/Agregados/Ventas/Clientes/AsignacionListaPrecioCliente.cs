using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class AsignacionListaPrecioCliente : Entidad
    {
        public DateTime FechaCreacion { get; set; }

        public string CodigoCliente { get; set; }

        public string CodigoAlmacen { get; set; }
        public string CodigoListaPrecioCliente { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }


        public Almacen Almacen { get; private set; }
        public ListaPrecioCliente ListaPrecioCliente { get; private set; }
        public UsuarioSistema UsuarioSistema { get; private set; }        
    }
}