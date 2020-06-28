using System;

namespace PtoVta.Aplicacion.DTO.Parametros
{
    public class TipoPagoDTO
    {
        public int CodigoTipoPago { get; set; }  //CAMBIARLO A VALOR EUMERAOD (NUMERICO)
        public string DescripcionTipoPago { get; set; }
        public int Mostrar { get; set; }
    }
}