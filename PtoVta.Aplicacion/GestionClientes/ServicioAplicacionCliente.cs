using System;
using System.Linq;
using System.Transactions;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.Transversales.Log;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.GestionClientes
{
    public class ServicioAplicacionCliente : IServicioAplicacionCliente
    {
        private IRepositorioCliente _IRepositorioCliente;

        public ServicioAplicacionCliente(IRepositorioCliente pIRepositorioCliente)
        {
            if (pIRepositorioCliente == null)
                throw new ArgumentNullException("pIRepositorioCliente Nulo en ServicioAplicacionCliente");      

            _IRepositorioCliente = pIRepositorioCliente;                      
        }

        public ResultadoServicio<ResultadoClienteGrabadoDTO> AgregarNuevoCliente(ClienteDTO pClienteDTO)
        {
            var nuevoCliente = CrearNuevoCliente(pClienteDTO);

            GrabarTransaccionNuevoCliente(nuevoCliente);
                                                        
            if (nuevoCliente != null)
            {
                return new ResultadoServicio<ResultadoClienteGrabadoDTO>(7, Mensajes.advertencia_ExitosaCreacionNuevoPedidoEESS,
                        string.Empty, nuevoCliente.ProyectadoComo<ResultadoClienteGrabadoDTO>(), null);
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_FalloCreacionNuevaVentaEnVenta);
                return null;
            }
        }

        public ResultadoServicio<ClienteDTO> BuscarClientePorRUC(string pClienteRUC, string pCodigoAlmacen)
        {
            var clientePorRUC = _IRepositorioCliente.ObtenerClientePorRUC(pClienteRUC, pCodigoAlmacen);
            if (clientePorRUC != null)
            {          
                return new ResultadoServicio<ClienteDTO>(7, Mensajes.advertencia_ConsultaPedidoEESSPorNumeroPedidoExitosa,
                                                                string.Empty, clientePorRUC.ProyectadoComo<ClienteDTO>(), null);
            }
            else                
                return null;
        }

        public ResultadoServicio<ClienteListadoDTO> BuscarTodosClientes()
        {
            //Obtenemos list de entidad Ventas
            var clientes = _IRepositorioCliente.ObtenerTodos();
            if (clientes != null && clientes.Any())
            {
                //retorna datos adaptador                
                return new ResultadoServicio<ClienteListadoDTO>(7, Mensajes.advertencia_ConsultaVentasPorClienteExitosa,
                                    string.Empty, null, clientes.ProyectadoComoColeccion<ClienteListadoDTO>());
            }
            else
                //no retorna
                return null;
        }


        Cliente CrearNuevoCliente(ClienteDTO pClienteDTO)
        {
            try
            {
                Cliente nuevoCliente = ClienteFactory.CrearCliente(pClienteDTO.CodigoCliente, pClienteDTO.CodigoContable, pClienteDTO.Ruc,
                                pClienteDTO.NombresORazonSocial, pClienteDTO.Telefono, pClienteDTO.Fax,
                                pClienteDTO.FechaNacimiento, pClienteDTO.FechaInscripcion, pClienteDTO.DiasDeGracia,
                                pClienteDTO.MontoLimiteCredito, pClienteDTO.Deuda, pClienteDTO.EsAfecto,
                                pClienteDTO.ControlarSaldoDisponible, pClienteDTO.CodigoMoneda, pClienteDTO.CodigoClaseTipoCambio,
                                pClienteDTO.CodigoTipoCliente, pClienteDTO.CodigoZonaCliente, pClienteDTO.CodigoDiaDePago,
                                pClienteDTO.CodigoVendedor, pClienteDTO.CodigoImpuestoIgv, pClienteDTO.CodigoImpuestoIsc,
                                pClienteDTO.CodigoCondicionPagoDocumentoGenerado, pClienteDTO.CodigoCondicionPagoTicket, pClienteDTO.CodigoEstadoDeCliente,
                                pClienteDTO.CodigoUsuarioDeSistema, pClienteDTO.CodigoPais, pClienteDTO.CodigoDepartamento,
                                pClienteDTO.CodigoDistrito);

                //Placas
                if (pClienteDTO.ClientePlacas != null && pClienteDTO.ClientePlacas.Any())
                {
                    foreach (var placa in pClienteDTO.ClientePlacas)
                    {
                        var detalleDePedido = nuevoCliente.AgregarNuevoClientePlaca(placa.DescripcionPlaca);
                    }
                }

                return nuevoCliente;
            }
            catch (Exception ex)
            {
                LogFactory.CrearLog().LogWarning(ex.Message);
                throw;
            }
        } 

        void GrabarTransaccionNuevoCliente(Cliente pCliente)
        {
            try
            {
                if (pCliente == null)
                    throw new ArgumentException(Mensajes.advertencia_NoSePuedeGrabarClienteNulo);

                using (TransactionScope ambito = new TransactionScope())
                {
                    GrabarCliente(pCliente);

                    ambito.Complete();
                }
            }
            catch (Exception ex)
            {
                string cadenaExcepcion = ex.Message +
                    ".Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";
                LogFactory.CrearLog().LogWarning(cadenaExcepcion);                
                throw;
            }
        }


       void GrabarCliente(Cliente pCliente)
        {
            //Persistir Cliente
            // var validarEntidad = ValidadorEntidadFactory.CrearValidador();

            // if (validarEntidad.EsValido(pVenta))
            // {
            _IRepositorioCliente.Agregar(pCliente);
            // _IRepositorioVenta.UnidadTrabajo.Commit();
            // }
            // else
            //     throw new AplicacionExcepcionErrorValidacion(validarEntidad.RecibeMensajesInvalidos(pVenta));

        }               
    }
}