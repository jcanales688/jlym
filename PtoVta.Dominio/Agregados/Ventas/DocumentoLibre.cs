using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class DocumentoLibre : Entidad
    {
        public decimal NumeroDocumentoLibre { get; set; }
        public DateTime FechaProcesoInicial { get; set; }
        public DateTime FechaProcesoFinal { get; set; }
        public decimal TotalLibre { get; set; }

        public string CodigoCliente { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }

        public Almacen Almacen { get; private set; }
        public UsuarioSistema UsuarioSistema { get; private  set; }        
    }
}