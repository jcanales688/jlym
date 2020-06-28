using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class ClienteLimiteCredito : Entidad
    {
        public decimal PorcentajeLimite { get; set; }
        public decimal MontoLimite { get; set; }
        public decimal Deuda { get; set; }
        public decimal PorcentajeExcede { get; set; }
        public decimal MontoExcedente { get; set; }


        public string CodigoCliente { get; set; }
        public string CodigoAlmacen { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }


        public Almacen Almacen { get; private set; }
        public UsuarioSistema UsuarioSistema { get; private set; }        
    }
}