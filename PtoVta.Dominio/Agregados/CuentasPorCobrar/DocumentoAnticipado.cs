using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.CuentasPorCobrar 
{
    public class DocumentoAnticipado: Entidad
    {
        public string NumeroDocumento { get; set; }
        public DateTime FechaProceso { get; set; }


        // public Guid VentaId { get; set; }

        public string CodigoTipoDocumento { get; set; }
        public string CodigoAlmacen { get; set; }

        public TipoDocumento TipoDocumento { get; private set; }
        public Almacen Almacen { get; private set; }


        //TipoDocumento
        public void EstablecerTipoDocumentoDeDocumentoAnticipado(TipoDocumento pTipoDocumento)
        {
            if (pTipoDocumento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoDocumentoDeDocumentoAnticipadoNuloOTransitorio);
            }

            this.CodigoTipoDocumento = pTipoDocumento.CodigoTipoDocumento;
            this.TipoDocumento = pTipoDocumento;
        }

        public void EstablecerReferenciaTipoDocumentoDeDocumentoAnticipado(string pCodigoTipoDocumento)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumento))
            {
                this.CodigoTipoDocumento = pCodigoTipoDocumento.Trim();
                this.TipoDocumento = null;
            }
        }


        //Almacen
        public void EstablecerAlmacenDeDocumentoAnticipado(Almacen pAlmacen)
        {
            if (pAlmacen == null)
            {
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeDocumentoAnticipadoNuloOTransitorio);
            }

            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeDocumentoAnticipado(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {
                this.CodigoAlmacen = pCodigoAlmacen.Trim();
                this.Almacen = null;
            }
        }        
    }
}
