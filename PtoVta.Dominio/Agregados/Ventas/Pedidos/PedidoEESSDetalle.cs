using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class PedidoEESSDetalle:Entidad
    {
        // CORRNBR	int
        public int Correlativo { get; set; }

        // SEQUENCE	smallint
        public short Secuencia { get; set; }

        // NBRDOCUMENT	varchar
        public string NumeroDocumento { get; set; }

        // DATEDOC	smalldatetime
        public DateTime FechaDocumento { get; set; }

        // DATEPROCESALES	smalldatetime
        public DateTime FechaProceso { get; set; }

        // PERPOST	UD_PERPOST
        public string Periodo { get; set; }

        // STKCLOSEDZ	bit
        public bool ProcesadoCierreZ { get; set; }

        // STKCLOSEDX	bit
        public bool ProcesadoCierreX { get; set; }

        // NBRTURN	tinyint
        public int NumeroTurno { get; set; }

        // NBRSIDE	UD_CARA
        public string NumeroCara { get; set; }  

        // NBRTRANSACFUEL	char
        public string NumeroTransaccionCombustible { get; set; }
        
        // DISCDSCTO1	numeric
        public decimal PorcentajeDescuentoPrimero { get; set; }

        // DISCDSCTO2	numeric
        public decimal PorcentajeDescuentoSegundo { get; set; }

        // DISCMTOPEN	numeric
        public decimal PorcentajeDescuentoNacional { get; set; }

        // DISCMTOUSD	numeric
        public decimal PorcentajeDescuentoExtranjera { get; set; }

        // PORCENTTAXIGV	numeric
        public decimal PorcentajeImpuestoIgv { get; set; }

        // PORCENTTAXISC	numeric
        public decimal PorcentajeImpuestoIsc { get; set; }

        // TOTALPEN	numeric
        public decimal TotalNacional { get; set; }

        // TOTALUSD	numeric
        public decimal TotalExtranjera { get; set; }

        // TAXPEN	numeric
        public decimal ImpuestoNacional { get; set; }

        // TAXUSD	numeric
        public decimal ImpuestoExtranjera { get; set; }

        // STKITEM	bit
        public bool EsInventariable { get; set; }

        // STKFISI	bit
        public bool EnInventarioFisico { get; set; }

        // SLS_PRICE	numeric
        public decimal Precio { get; set; }

        // SLSPRICESALE	numeric
        public decimal PrecioVenta { get; set; }

        // STDCOSTPEN	numeric
        public decimal CostoEstandarNacional { get; set; }

        // STDCOSTUSD	numeric
        public decimal CostoEstandarExtranjera { get; set; }

        // DESCRINVENTORY	UD_DESCRIPCION
        public string DescripcionArticulo { get; set; }

        // QTY	numeric
        public decimal Cantidad { get; set; }

        // KIT	bit
        public int EsFormula { get; set; }

        // COMBUSTIBLE	bit
        public bool EsArticuloCombustible { get; set; }

        // NUM_PEAJE	varchar   
        public string NumeroPeaje { get; set; }     



        // DOCTYPEID	UD_DOCTYPEID
        public string CodigoTipoDocumento  { get; set; }

        // SITEID	UD_IDSITE
        public string CodigoAlmacen { get; set; }

        // INVTIDSKU	UD_IDCORPORATIVO
        public string CodigoArticulo { get; set; }

        // CURYID	UD_CURYID
        public string CodigoMoneda { get; set; }

        // DOCSTATUSID	char
        public string CodigoEstadoDocumento { get; set; }  
        
        // PTOVTA	UD_PTOVTA
        public string CodigoConfiguracionPuntoVenta { get; set; }

        // STKUNITID	UD_STKUNITID
        public string CodigoUnidadDeMedida { get; set; } 

        // USERID	UD_USERID
        public string CodigoUsuarioDeSistema { get; set; }   

       // INVTIDALTER	UD_CODIGOBARRA
        public string CodigoArticuloAlterno { get; set; }                          
    }
}