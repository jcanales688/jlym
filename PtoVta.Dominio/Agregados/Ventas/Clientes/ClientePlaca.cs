using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class ClientePlaca : Entidad
    {
        public string CodigoCliente { get; set; }        
        public string DescripcionPlaca { get; set; }
    }
}