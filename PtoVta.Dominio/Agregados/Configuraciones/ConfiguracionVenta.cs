using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Configuraciones
{
    public class ConfiguracionVenta : Entidad
    {
       public int VentaAutomaticaCombustible { get; set; }
        public DateTime FechaProcesoVenta { get; set; }
        public int NoSaltaCorrelativo { get; set; }
        public string RutaReportesVenta { get; set; }
        public int TipoControlador { get; set; }


        public string CodigoCategoriaFuel { get; private set; }
        public string CodigoCategoriaLubricantes { get; private set; }  //AGREGAR TODO EL CODIGO NECESARIO PARA SOPORTAR ESTA NUEVA RELACION
        public string CodigoTipoClienteEfectivo { get; private set; }
        public string CodigoTipoClienteAdelanto { get; private set; }
        public string CodigoTipoClienteCreditoLocal { get; private set; }
        public string CodigoTipoClienteCreditoCorporativo { get; private set; }
        public string CodigoTipoClienteOtros { get; private set; }
        public string CodigoTipoDocumentoTicket { get; private set; }
        public string CodigoTipoDocumentoFactura { get; private set; }
        public string CodigoTipoDocumentoBoleta { get; private set; }
        public string CodigoTipoDocumentoNotaCredito { get; private set; }
        public string CodigoTipoDocumentoNotaDebito { get; private set; }
        public string CodigoTipoDocumentoNotaCreditoAjuste { get; private set; }
        public string CodigoCondicionPagoDefault { get; private set; } // "00"
        public string CodigoEstadoDocumentoDefault { get; private set; } // "PE"

        public virtual CategoriaArticulo CategoriaFuel { get; private set; }
        public virtual CategoriaArticulo CategoriaLubricantes { get; private set; }
        public virtual TipoCliente TipoClienteEfectivo { get; private set; }
        public virtual TipoCliente TipoClienteAdelanto { get; private set; }
        public virtual TipoCliente TipoClienteCreditoLocal { get; private set; }
        public virtual TipoCliente TipoClienteCreditoCorporativo { get; private set; }
        public virtual TipoCliente TipoClienteOtros { get; private set; }
        public virtual TipoDocumento TipoDocumentoTicket { get; private set; }
        public virtual TipoDocumento TipoDocumentoFactura { get; private set; }
        public virtual TipoDocumento TipoDocumentoBoleta { get; private set; }
        public virtual TipoDocumento TipoDocumentoNotaCred { get; private set; }
        public virtual TipoDocumento TipoDocumentoNotaDeb { get; private set; }
        public virtual TipoDocumento TipoDocumentoNotaCredAjuste { get; private set; }
        public virtual CondicionPago CondicionPagoDefault { get; private set; }
        public virtual EstadoDocumento EstadoDocumentoDefault { get; private set; }



        //CategoriaFuel
        public void EstablecerReferenciaCategoriaFuelDeConfiguracionVenta(string pCodigoCategoriaFuel)
        {
            if (!string.IsNullOrEmpty(pCodigoCategoriaFuel))
            {
                this.CodigoCategoriaFuel = pCodigoCategoriaFuel.Trim();
                // this.CategoriaFuel = null;
            }
        }

        //Categoria Lubricantes
        public void EstablecerReferenciaCategoriaLubricantesDeConfiguracionVenta(string pCodigoCategoriaLubricantes)
        {
            if (!string.IsNullOrEmpty(pCodigoCategoriaLubricantes))
            {
                this.CodigoCategoriaLubricantes = pCodigoCategoriaLubricantes.Trim();
                // this.CategoriaLubricantes = null;
            }
        }

        //TipoClienteEfectivo
        public void EstablecerReferenciaTipoClienteEfectivoDeConfiguracionVenta(string pCodigoTipoClienteEfectivo)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoClienteEfectivo))
            {
                this.CodigoTipoClienteEfectivo = pCodigoTipoClienteEfectivo.Trim();
                // this.TipoClienteEfectivo = null;
            }
        }

        //TipoClienteAdelanto
        public void EstablecerReferenciaTipoClienteAdelantoDeConfiguracionVenta(string pCodigoTipoClienteAdelanto)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoClienteAdelanto))
            {
                this.CodigoTipoClienteAdelanto = pCodigoTipoClienteAdelanto.Trim();
                // this.TipoClienteAdelanto = null;
            }
        }

        //TipoClienteCreditoLocal
        public void EstablecerReferenciaTipoClienteCreditoLocalDeConfiguracionVenta(string pCodigoTipoClienteCreditoLocal)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoClienteCreditoLocal))
            {
                this.CodigoTipoClienteCreditoLocal = pCodigoTipoClienteCreditoLocal.Trim();
                // this.TipoClienteCreditoLocal = null;
            }
        }

        //TipoClienteCreditoCorporativo
        public void EstablecerReferenciaTipoClienteCreditoCorporativoDeConfiguracionVenta(string pCodigoTipoClienteCreditoCorporativo)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoClienteCreditoCorporativo))
            {
                this.CodigoTipoClienteCreditoCorporativo = pCodigoTipoClienteCreditoCorporativo.Trim();
                // this.TipoClienteCreditoCorporativo = null;
            }
        }

        //TipoClienteOtros
         public void EstablecerReferenciaTipoClienteOtrosDeConfiguracionVenta(string pCodigoTipoClienteOtros)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoClienteOtros))
            {
                this.CodigoTipoClienteOtros = pCodigoTipoClienteOtros.Trim();
                // this.TipoClienteOtros = null;
            }
        }

        //TipoDocumentoTicket
        public void EstablecerReferenciaTipoDocumentoTicketDeConfiguracionVenta(string pCodigoTipoDocumentoTicket)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumentoTicket))
            {
                this.CodigoTipoDocumentoTicket = pCodigoTipoDocumentoTicket.Trim();
                // this.TipoDocumentoTicket = null;
            }
        }

        //TipoDocumentoFactura
        public void EstablecerReferenciaTipoDocumentoFacturaDeConfiguracionVenta(string pCodigoTipoDocumentoFactura)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumentoFactura))
            {
                this.CodigoTipoDocumentoFactura = pCodigoTipoDocumentoFactura.Trim();
                // this.TipoDocumentoFactura = null;
            }
        }

        //TipoDocumentoBoleta
         public void EstablecerReferenciaTipoDocumentoBoletaDeConfiguracionVenta(string pCodigoTipoDocumentoBoleta)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumentoBoleta))
            {
                this.CodigoTipoDocumentoBoleta = pCodigoTipoDocumentoBoleta.Trim();
                // this.TipoDocumentoBoleta = null;
            }
        }

        //TipoDocumentoNotaCred
        public void EstablecerReferenciaTipoDocumentoNotaCredDeConfiguracionVenta(string pCodigoTipoDocumentoNotaCredito)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumentoNotaCredito))
            {
                this.CodigoTipoDocumentoNotaCredito = pCodigoTipoDocumentoNotaCredito.Trim();
                // this.TipoDocumentoNotaCred = null;
            }
        }

        //TipoDocumentoNotaDeb
         public void EstablecerReferenciaTipoDocumentoNotaDebDeConfiguracionVenta(string pCodigoTipoDocumentoNotaDebito)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumentoNotaDebito))
            {
                this.CodigoTipoDocumentoNotaDebito = pCodigoTipoDocumentoNotaDebito.Trim();
                // this.TipoDocumentoNotaDeb = null;
            }
        }

        //TipoDocumentoNotaCredAjuste
        public void EstablecerReferenciaTipoDocumentoNotaCredAjusteDeConfiguracionVenta(string pCodigoTipoDocumentoNotaCreditoAjuste)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoDocumentoNotaCreditoAjuste))
            {
                this.CodigoTipoDocumentoNotaCreditoAjuste = pCodigoTipoDocumentoNotaCreditoAjuste.Trim();
                // this.TipoDocumentoNotaCredAjuste = null;
            }
        }

        //Condicion pago general
        public void EstablecerReferenciaCondicionPagoDefaultDeConfiguracionVenta(string pCodigoCondicionPagoDefault)
        {
            if (!string.IsNullOrEmpty(pCodigoCondicionPagoDefault))
            {
                this.CodigoCondicionPagoDefault = pCodigoCondicionPagoDefault.Trim();
                // this.CondicionPagoDefault = null;
            }
        }


        //Estado Documento Default
        public void EstablecerReferenciaEstadoDocumentoDefaultDeConfiguracionVenta(string pCodigoEstadoDocumentoDefault)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoDocumentoDefault))
            {
                this.CodigoEstadoDocumentoDefault = pCodigoEstadoDocumentoDefault.Trim();
                // this.EstadoDocumentoDefault = null;
            }
        }        
    }
}