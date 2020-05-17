using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.Transversales.Log;

namespace PtoVta.Aplicacion.GestionUsuario
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
            string mensajeValidacion = string.Empty;

            if (pVendedor == null || String.IsNullOrEmpty(pVendedor.CodigoVendedor))
            {
                mensajeValidacion = "Datos de vendedor o Codigo de vendedor invalido";
                throw new ArgumentException(mensajeValidacion);
            }
                
            //Validaciones
            var almacen =_IRepositorioAlmacen.ObtenerPorCodigo(pVendedor.CodigoAlmacen);
            if (almacen == null)
            {
                mensajeValidacion = "Almacen asociado al vendedor no existe.";
                LogFactory.CrearLog().LogWarning(mensajeValidacion);
                throw new ArgumentException(mensajeValidacion);
            }                

            var estadoVendedor =_IRepositorioEstadoVendedor.ObtenerPorCodigo(pVendedor.CodigoEstadoVendedor);
            if (estadoVendedor == null)
            {
                mensajeValidacion = "Estado de vendedor asociado al vendedor no existe.";
                LogFactory.CrearLog().LogWarning(mensajeValidacion);
                throw new ArgumentException(mensajeValidacion);
            }                

            var usuarioSistemaRegistrador =_IIRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario(pVendedor.CodigoUsuarioSistema);
            if (estadoVendedor == null)
            {
                mensajeValidacion = "Estado de vendedor asociado al vendedor no existe.";
                LogFactory.CrearLog().LogWarning(mensajeValidacion);
                throw new ArgumentException(mensajeValidacion);
            }                

            var usuarioSistemaDelAcceso =_IIRepositorioUsuarioSistema.ObtenerUsuarioSistemaPorUsuario(pVendedor.CodigoUsuarioSistema);
            if (estadoVendedor == null)
            {
                mensajeValidacion = "Estado de vendedor asociado al vendedor no existe.";
                LogFactory.CrearLog().LogWarning(mensajeValidacion);
                throw new ArgumentException(mensajeValidacion);
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
                mensajeValidacion = "Vendedor creado satisfactoriamente.";
                
                return new ResultadoServicio<VendedorDTO>(7,mensajeValidacion,
                        string.Empty, nuevoVendedor.ProyectadoComo<VendedorDTO>(), null);
            }
            else
            {
                mensajeValidacion = "Creacion de nuevo vendedor fallo.";
                LogFactory.CrearLog().LogWarning(mensajeValidacion);
                
                return new ResultadoServicio<VendedorDTO>(7,mensajeValidacion,
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