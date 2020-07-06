using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class TipoDeCambio : Entidad
    {
        public DateTime FechaTipoDeCambio { get; set; }
        public decimal MontoTipoDeCambio { get; set; }
        public string Operador { get; set; }
        public string UsuarioDeSistema { get; set; }

        public string CodigoClaseTipoCambio { get; private set; }
        public string CodigoMonedaOrigen { get; private set; }
        public string CodigoMonedaDestino { get; private set; }

        public Moneda MonedaOrigen { get; private set; }
        public Moneda MonedaDestino { get; private set; }


        public TipoDeCambio(){}
        public TipoDeCambio(DateTime pFechaTipoDeCambio, decimal pMontoTipoDeCambio, string pOperador, 
                            string pUsuarioDeSistema, string pCodigoClaseTipoCambio, string pCodigoMonedaOrigen, 
                            string pCodigoMonedaDestino)
        {
            this.CodigoClaseTipoCambio = pCodigoClaseTipoCambio;
            this.CodigoMonedaOrigen = pCodigoMonedaOrigen;
            this.CodigoMonedaDestino = pCodigoMonedaDestino;
            this.FechaTipoDeCambio = pFechaTipoDeCambio;
            this.MontoTipoDeCambio = pMontoTipoDeCambio;
            this.Operador = pOperador;
            this.UsuarioDeSistema = pUsuarioDeSistema;
        }
    }
}