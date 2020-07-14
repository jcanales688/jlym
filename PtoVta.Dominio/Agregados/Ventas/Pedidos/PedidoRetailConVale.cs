using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class PedidoRetailConVale:Entidad
    {
        // CORRNBR	int
        public int Correlativo { get; set; }
    
        // NBRBONUS	numeric
        public decimal NumeroVale { get; set; }

        // stknew	bit
        // stkmodify	bit
        // stkdelete	bit
        // stksend	bit
        // stkreceive	bit
        // USER01	UD_USER1
        // USER02	UD_USER2



        // custidss	UD_CODCLIENT     
        public string CodigoCliente { get; set; }

        // SITEID	UD_IDSITE
        public string CodigoAlmacen { get; set; }   
    }
}