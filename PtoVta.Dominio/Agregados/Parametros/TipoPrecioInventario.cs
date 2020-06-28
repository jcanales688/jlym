using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class TipoPrecioInventario : Entidad
    {
        public string CodigoTipoPrecioInventario { get; set; }
        public string DescripcionTipoPrecioInventario { get; set; }
        public string CodigoClaseTipoCambio { get; set; }    
        public string CodigoMoneda { get; set; }        
        
        public Moneda Moneda { get; private set; }        
    }

}