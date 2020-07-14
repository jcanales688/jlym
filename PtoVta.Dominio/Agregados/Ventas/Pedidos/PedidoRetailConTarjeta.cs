using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class PedidoRetailConTarjeta:Entidad
    {
        // CORRNBR	int
        public int Correlativo { get; set; }

        // SEQUENCE	smallint
        public short Secuencia { get; set; }

        // NBRCARD	char
        public string NumeroTarjeta { get; set; }

        // TOTCARDPEN	numeric
        public decimal TotalTarjetaNacional { get; set; }

        // TOTCARDUSD	numeric
        public decimal TotalTarjetaExtranjera { get; set; }

        // TRANSACPINPAD	int
        public int EsTransaccionPinPad { get; set; }

        // TipoTarjeta	varchar
        public string TipoTarjeta { get; set; }

        // DNITarjeta	varchar
        public string DNIAsociadoATarjeta { get; set; }

        // NombreTarjeta	varchar
        public string DescripcionTarjeta { get; set; }

        

        // SITEID	UD_IDSITE
        public string CodigoAlmacen { get; set; }

        // CARDID	char 
        public string CodigoTarjeta { get; set; }   

        // curyid	UD_CURYID
        public string CodigoMoneda { get; set; }   
    }
}