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
            var clienteExistente =_IRepositorioCliente.ObtenerPorCodigo(pClienteDTO.CodigoCliente);
            if (clienteExistente != null)
            {                
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_RucYClienteYaRegistrado);
                throw new ArgumentException(Mensajes.advertencia_RucYClienteYaRegistrado);
            }  

            ClienteDireccion direccionClientePrimero = new ClienteDireccion(pClienteDTO.DireccionPrimeroPais, 
                                pClienteDTO.DireccionPrimeroDepartamento,pClienteDTO.DireccionPrimeroProvincia,
                                pClienteDTO.DireccionPrimeroDistrito, pClienteDTO.DireccionPrimeroUbicacion);

            ClienteDireccion direccionClienteSegundo = new ClienteDireccion(pClienteDTO.DireccionPrimeroPais, 
                                pClienteDTO.DireccionPrimeroDepartamento,pClienteDTO.DireccionPrimeroProvincia,
                                pClienteDTO.DireccionPrimeroDistrito, pClienteDTO.DireccionSegundoUbicacion);

            var nuevoCliente = CrearNuevoCliente(pClienteDTO, direccionClientePrimero, direccionClienteSegundo);

            GrabarTransaccionNuevoCliente(nuevoCliente);
                                                        
            if (nuevoCliente != null)
            {
                return new ResultadoServicio<ResultadoClienteGrabadoDTO>(7, Mensajes.advertencia_ExitosaCreacionNuevoCliente,
                        string.Empty, nuevoCliente.ProyectadoComo<ResultadoClienteGrabadoDTO>(), null);
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_FalloCreacionNuevaVentaEnVenta);
                return new ResultadoServicio<ResultadoClienteGrabadoDTO>(6, Mensajes.advertencia_FalloCreacionNuevaVentaEnVenta,
                        string.Empty, nuevoCliente.ProyectadoComo<ResultadoClienteGrabadoDTO>(), null);
            }
        }

        public ResultadoServicio<ClienteDTO> BuscarClientePorRUC(string pClienteRUC, string pCodigoAlmacen)
        {
            var clientePorRUC = _IRepositorioCliente.ObtenerClientePorRUC(pClienteRUC, pCodigoAlmacen);
            if (clientePorRUC != null)
            {          
                return new ResultadoServicio<ClienteDTO>(7, Mensajes.advertencia_ConsultaClientePorRUCExitosa,
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


        Cliente CrearNuevoCliente(ClienteDTO pClienteDTO, ClienteDireccion pClienteDireccionPrimero, 
                                                ClienteDireccion pClienteDireccionSegundo)
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
                                pClienteDTO.CodigoDistrito, pClienteDireccionPrimero, pClienteDireccionSegundo);                                

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
                string detallesAsicionales = string.Empty;                
                string cadenaExcepcion = ex.Message;

                if(ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";                        
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);                
                throw;
            }
        } 

        void GrabarTransaccionNuevoCliente(Cliente pCliente)
        {
            try
            {
                if (pCliente == null)
                    throw new ArgumentException(Mensajes.advertencia_NoSePuedeGrabarClienteNulo);

                // using (TransactionScope ambito = new TransactionScope(TransactionScopeOption.Suppress,
                //                                                         new TransactionOptions
                //                                                         {
                //                                                             IsolationLevel = IsolationLevel.ReadCommitted,
                //                                                             Timeout = TransactionManager.MaximumTimeout,
                //                                                         },
                //                                                         TransactionScopeAsyncFlowOption.Enabled))
                // {
                    GrabarCliente(pCliente);

                //     ambito.Complete();
                // }
            }
            catch (Exception ex)
            {
                string detallesAsicionales = string.Empty;                
                string cadenaExcepcion = ex.Message;

                if(ex.InnerException != null)
                {
                    detallesAsicionales = " .Detalles Interno: " + ex.InnerException != null && ex.InnerException.InnerException != null ?
                                    ex.InnerException.InnerException.Message : "Ver Detalles.";                        
                }

                LogFactory.CrearLog().LogWarning(cadenaExcepcion + detallesAsicionales);                
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