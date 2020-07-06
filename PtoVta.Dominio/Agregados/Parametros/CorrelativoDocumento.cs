using System;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class CorrelativoDocumento : Entidad
    {
        public string Serie { get; set; }
        public long Correlativo { get; set; }
        public string TipoDeVenta { get; set; } //Automatico: A; Manual: M
        public int Estado { get; set; }


        public string CodigoTipoDocumento { get; private set; }
        public string CodigoAlmacen { get; private  set; }
        public string CodigoConfiguracionPuntoVenta { get;private  set; }

        public Almacen Almacen { get; private set; }
        public ConfiguracionPuntoVenta ConfiguracionPuntoVenta { get; private set; }

        public CorrelativoDocumento(){}
        public CorrelativoDocumento(string pCodigoTipoDocumento, string pCodigoAlmacen, 
                                    string pCodigoConfiguracionPuntoVenta, string pSerie,
                                    long pCorrelativo, string pTipoDeVenta, int pEstado)
        {
            CodigoTipoDocumento = pCodigoTipoDocumento;
            CodigoAlmacen = pCodigoAlmacen;
            CodigoConfiguracionPuntoVenta = pCodigoConfiguracionPuntoVenta;

            Serie = pSerie;
            Correlativo = pCorrelativo;
            TipoDeVenta = pTipoDeVenta;
            Estado = pEstado;
        }
    }
}