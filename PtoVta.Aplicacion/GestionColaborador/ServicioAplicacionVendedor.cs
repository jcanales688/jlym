using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.Transversales.Log;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.GestionColaborador
{
    public class ServicioAplicacionVendedor : IServicioAplicacionVendedor
    {
        private readonly IRepositorioAlmacen _IRepositorioAlmacen;
        private readonly IRepositorioEstadoVendedor _IRepositorioEstadoVendedor;
        private readonly IRepositorioUsuarioSistema _IIRepositorioUsuarioSistema;
        private readonly IRepositorioVendedor _IRepositorioVendedor;

        public ServicioAplicacionVendedor(IRepositorioAlmacen pIRepositorioAlmacen,
                             IRepositorioEstadoVendedor pIRepositorioEstadoVendedor,
                             IRepositorioUsuarioSistema pIRepositorioUsuarioSistema,
                             IRepositorioVendedor pIRepositorioVendedor)
        {
            if (pIRepositorioAlmacen == null)
                throw new ArgumentNullException("IRepositorioAlmacen Nulo En ServicioAplicacionInicioSession");            

            if (pIRepositorioEstadoVendedor == null)
                throw new ArgumentNullException("IRepositorioEstadoVendedor Nulo En ServicioAplicacionInicioSession");

            if (pIRepositorioUsuarioSistema == null)
                throw new ArgumentNullException("IRepositorioUsuarioSistema Nulo En ServicioAplicacionInicioSession");

            if (pIRepositorioVendedor == null)
                throw new ArgumentNullException("pIRepositorioVendedor Nulo En ServicioAplicacionInicioSession");                


            _IRepositorioAlmacen = pIRepositorioAlmacen;
            _IRepositorioEstadoVendedor = pIRepositorioEstadoVendedor;
            _IIRepositorioUsuarioSistema = pIRepositorioUsuarioSistema;
            _IRepositorioVendedor = pIRepositorioVendedor;
        }

        public ResultadoServicio<VendedorDTO> AgregarNuevoUsuarioVendedor(VendedorDTO pVendedor)
        {
            if (pVendedor == null || String.IsNullOrEmpty(pVendedor.CodigoVendedor))
            {
                throw new ArgumentException(Mensajes.advertencia_DatosDeVendedorOCodigoDeVendedorInvalido);
            }
                
            //Validaciones
            var almacen =_IRepositorioAlmacen.ObtenerPorCodigo(pVendedor.CodigoAlmacen);
            if (almacen == null)
            {                
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_AlmacenAsociadoAlVendedorNoExiste);
                throw new ArgumentException(Mensajes.advertencia_AlmacenAsociadoAlVendedorNoExiste);
            }                

            var estadoVendedor =_IRepositorioEstadoVendedor.ObtenerPorCodigo(pVendedor.CodigoEstadoVendedor);
            if (estadoVendedor == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_EstadoDeVendedorAsociadoAlVendedorNoExiste);
                throw new ArgumentException(Mensajes.advertencia_EstadoDeVendedorAsociadoAlVendedorNoExiste);
            }                

            var usuarioSistemaRegistrador =_IIRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario(pVendedor.CodigoUsuarioSistema);
            if (usuarioSistemaRegistrador == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_UsuarioSistemaCreadorNuevoVendedorNoExiste);
                throw new ArgumentException(Mensajes.advertencia_UsuarioSistemaCreadorNuevoVendedorNoExiste);
            }                

            var usuarioSistemaDelAcceso =_IIRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario("VENDPLAYA");
            if (usuarioSistemaDelAcceso == null)
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_UsuarioSistemaAccesoNuevoVendedorNoExiste);
                throw new ArgumentException(Mensajes.advertencia_UsuarioSistemaAccesoNuevoVendedorNoExiste);
            }

            VendedorDireccion direccionVendedor = new VendedorDireccion(pVendedor.DireccionPrimeroPais,
                                                                        pVendedor.DireccionPrimeroDepartamento,
                                                                        pVendedor.DireccionPrimeroProvincia,
                                                                        pVendedor.DireccionPrimeroDistrito,
                                                                        pVendedor.DireccionPrimeroUbicacion);                                          

            var nuevoVendedor = CrearNuevoVendedor(pVendedor, almacen, estadoVendedor, usuarioSistemaRegistrador,
                                                    usuarioSistemaDelAcceso, direccionVendedor);

            GrabarNuevoVendedor(nuevoVendedor);

            if (nuevoVendedor != null)
            {
                return new ResultadoServicio<VendedorDTO>(7,Mensajes.advertencia_VendedorCreadoSatisfactoriamente,
                        string.Empty, nuevoVendedor.ProyectadoComo<VendedorDTO>(), null);
            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_CreacionNuevoVendedorFallo);                
                return new ResultadoServicio<VendedorDTO>(7,Mensajes.advertencia_CreacionNuevoVendedorFallo,
                        string.Empty, nuevoVendedor.ProyectadoComo<VendedorDTO>(), null);
            }
        }


        Vendedor CrearNuevoVendedor(VendedorDTO pVendedorDTO, Almacen pAlmacen,
                                    EstadoVendedor pEstadoVendedor,    
                                    UsuarioSistema pUsuarioSistema,
                                    UsuarioSistema pUsuarioSistemaAcceso,
                                    VendedorDireccion pDireccionPrimero)
        {
            Vendedor nuevoVendedor = VendedorFactory.CrearVendedor(pVendedorDTO.NombresVendedor, 
                                    pVendedorDTO.DocumentoIdentidad, pVendedorDTO.Telefono, 
                                    pVendedorDTO.Sexo, pVendedorDTO.FechaInicio,
                                    pVendedorDTO.CodigoVendedor,  pVendedorDTO.Clave,
                                    pVendedorDTO.FechaNacimiento, pAlmacen, pEstadoVendedor,
                                    pUsuarioSistema, pUsuarioSistemaAcceso,  pDireccionPrimero);
            return nuevoVendedor;

        }


        void GrabarNuevoVendedor(Vendedor pVendedor)
        {
            _IRepositorioVendedor.Agregar(pVendedor);
        }
    }
}
        