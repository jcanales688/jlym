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

        public ServicioAplicacionVendedor(IRepositorioAlmacen pIRepositorioAlmacen,
                             IRepositorioEstadoVendedor pIRepositorioEstadoVendedor,
                             IRepositorioUsuarioSistema pIRepositorioUsuarioSistema)
        {
            if (pIRepositorioAlmacen == null)
                throw new ArgumentNullException("IRepositorioAlmacen Nulo En ServicioAplicacionInicioSession");            

            if (pIRepositorioEstadoVendedor == null)
                throw new ArgumentNullException("IRepositorioEstadoVendedor Nulo En ServicioAplicacionInicioSession");

            if (pIRepositorioUsuarioSistema == null)
                throw new ArgumentNullException("IRepositorioUsuarioSistema Nulo En ServicioAplicacionInicioSession");


            _IRepositorioAlmacen = pIRepositorioAlmacen;
            _IRepositorioEstadoVendedor = pIRepositorioEstadoVendedor;
            _IIRepositorioUsuarioSistema = pIRepositorioUsuarioSistema;
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

            var nuevoVendedor = 



        }
    }
}