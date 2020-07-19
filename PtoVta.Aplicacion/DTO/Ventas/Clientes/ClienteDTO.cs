using System;
using System.Collections.Generic;
using PtoVta.Aplicacion.DTO.Parametros;

namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class ClienteDTO
    {
        public string CodigoCliente { get; set; }
        public string CodigoContable { get; set; }
        public string Ruc { get; set; }
        public string NombresORazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public int DiasDeGracia { get; set; }
        public decimal MontoLimiteCredito { get; set; }
        public decimal Deuda { get; set; }
        public int EsAfecto { get; set; }
        public int ControlarSaldoDisponible { get; set; }

        public string CodigoMoneda { get; set; }
        public string CodigoClaseTipoCambio { get; set; }
        public string  CodigoTipoCliente { get; set; }
        public string CodigoZonaCliente { get; set; }
        public string CodigoDiaDePago { get; set; }
        public string CodigoVendedor { get; set; }
        public string CodigoImpuestoIgv { get; set; }
        public string CodigoImpuestoIsc { get; set; }
        public string CodigoCondicionPagoDocumentoGenerado { get; set; }
        public string CodigoCondicionPagoTicket { get; set; }
        public string CodigoEstadoDeCliente { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoDepartamento { get; set; }
        public string CodigoDistrito { get; set; }        

        public CondicionPagoDTO CondicionPagoDocumentoGenerado { get; set; }
        public CondicionPagoDTO CondicionPagoTicket { get; set; }
        public DiaDePagoDTO DiaDePago { get; set; }

        public List<ClientePlacaDTO> ClientePlacas { get; set; }   
        public List<DocumentoLibreDTO> DocumentosLibre { get; set; }           
        public List<ClienteLimiteCreditoDTO> ClienteLimitesCredito { get; set; }           

    }
}