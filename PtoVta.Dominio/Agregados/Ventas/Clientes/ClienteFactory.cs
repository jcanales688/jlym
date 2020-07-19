using System;


namespace PtoVta.Dominio.Agregados.Ventas
{
    public static class ClienteFactory
    {
        public static Cliente CrearCliente(string pCodigoCliente, string pCodigoContable, string pRuc,
                                string pNombresORazonSocial, string pTelefono, string pFax,
                                DateTime pFechaNacimiento, DateTime pFechaInscripcion, int pDiasDeGracia,
                                decimal pMontoLimiteCredito, decimal pDeuda, int pEsAfecto,
                                int pControlarSaldoDisponible, string pCodigoMoneda, string pCodigoClaseTipoCambio,
                                string pCodigoTipoCliente, string pCodigoZonaCliente, string pCodigoDiaDePago,
                                string pCodigoVendedor, string pCodigoImpuestoIgv, string pCodigoImpuestoIsc,
                                string pCodigoCondicionPagoDocumentoGenerado, string pCodigoCondicionPagoTicket, string pCodigoEstadoDeCliente,
                                string pCodigoUsuarioDeSistema, string pCodigoPais, string pCodigoDepartamento,
                                string pCodigoDistrito)
        {
            var nuevoCliente = new Cliente();

            nuevoCliente.CodigoCliente = pCodigoCliente;
            nuevoCliente.CodigoContable = pCodigoContable;
            nuevoCliente.Ruc = pRuc;
            nuevoCliente.NombresORazonSocial = pNombresORazonSocial;
            nuevoCliente.Telefono = pTelefono;
            nuevoCliente.Fax = pFax;
            nuevoCliente.FechaNacimiento = pFechaNacimiento;
            nuevoCliente.FechaInscripcion = pFechaInscripcion;
            nuevoCliente.DiasDeGracia = pDiasDeGracia;
            nuevoCliente.MontoLimiteCredito = pMontoLimiteCredito;
            nuevoCliente.Deuda = pDeuda;
            nuevoCliente.EsAfecto = pEsAfecto;
            nuevoCliente.ControlarSaldoDisponible = pControlarSaldoDisponible;

            nuevoCliente.EstablecerReferenciaMonedaDeCliente(pCodigoMoneda);
            nuevoCliente.EstablecerReferenciaClaseTipoCambioDeCliente(pCodigoClaseTipoCambio);
            nuevoCliente.EstablecerReferenciaTipoClienteDeCliente(pCodigoTipoCliente);
            nuevoCliente.EstablecerReferenciaZonaClienteDeCliente(pCodigoZonaCliente);
            nuevoCliente.EstablecerReferenciaDiaDePagoDeCliente(pCodigoDiaDePago);
            nuevoCliente.EstablecerReferenciaVendedorDeCliente(pCodigoVendedor);
            nuevoCliente.EstablecerReferenciaImpuestoIgvDeCliente(pCodigoImpuestoIgv);
            nuevoCliente.EstablecerReferenciaImpuestoIscDeCliente(pCodigoImpuestoIsc);
            nuevoCliente.EstablecerReferenciaCondicionPagoDocumentoGeneradoDeCliente(pCodigoCondicionPagoDocumentoGenerado);
            nuevoCliente.EstablecerReferenciaCondicionPagoTicketDeCliente(pCodigoCondicionPagoTicket);
            nuevoCliente.EstablecerReferenciaEstadoDeClienteDeCliente(pCodigoEstadoDeCliente);
            nuevoCliente.EstablecerReferenciaUsuarioSistemaDeCliente(pCodigoUsuarioDeSistema);
            nuevoCliente.EstablecerReferenciaPaisDeCliente(pCodigoPais);
            nuevoCliente.EstablecerReferenciaDepartamentoDeCliente(pCodigoDepartamento);
            nuevoCliente.EstablecerReferenciaDistritoDeCliente(pCodigoDistrito);

            return nuevoCliente;
        }
    }

}