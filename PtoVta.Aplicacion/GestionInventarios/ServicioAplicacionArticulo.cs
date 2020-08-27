using System;
using System.Linq;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Inventarios;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.Transversales.Log;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Aplicacion.GestionInventarios
{
    public class ServicioAplicacionArticulo : IServicioAplicacionArticulo
    {
        private readonly IRepositorioArticulo _IRepositorioArticulo;
        private IRepositorioListaPrecioCliente _IRepositorioListaPrecioCliente;
        private IRepositorioListaPrecioInventario _IRepositorioListaPrecioInventario;

         private IServicioDominioListaPrecios _IServicioDominioListaPrecios;
        private IConfiguracionGlobalUnificado _IConfiguracionGlobalUnificado;

        public ServicioAplicacionArticulo(IRepositorioArticulo pIRepositorioArticulo, 
                                        IRepositorioListaPrecioCliente pIRepositorioListaPrecioCliente,
                                        IRepositorioListaPrecioInventario pIRepositorioListaPrecioInventario, 
                                        IServicioDominioListaPrecios pIServicioDominioListaPrecios,
                                        IConfiguracionGlobalUnificado pIConfiguracionGlobalUnificado)
        {
            if (pIRepositorioArticulo == null)
                throw new ArgumentNullException("pIRepositorioArticulo Nulo En ServicioAplicacionArticulo");

            if (pIRepositorioListaPrecioCliente == null)
                throw new ArgumentNullException("pIRepositorioListaPrecioCliente Nulo En ServicioAplicacionArticulo");

            if (pIRepositorioListaPrecioInventario == null)
                throw new ArgumentNullException("pIRepositorioListaPrecioInventario Nulo En ServicioAplicacionArticulo");

            if (pIServicioDominioListaPrecios == null)
                throw new ArgumentNullException("pIServicioDominioListaPrecios Nulo En ServicioAplicacionArticulo");

            if (pIConfiguracionGlobalUnificado == null)
                throw new ArgumentNullException("pIConfiguracionGlobalUnificado Nulo En ServicioAplicacionArticulo");                                                                

            _IRepositorioArticulo = pIRepositorioArticulo;
            _IRepositorioListaPrecioCliente = pIRepositorioListaPrecioCliente;
            _IRepositorioListaPrecioInventario = pIRepositorioListaPrecioInventario;
            _IServicioDominioListaPrecios = pIServicioDominioListaPrecios;
            _IConfiguracionGlobalUnificado = pIConfiguracionGlobalUnificado;
        }


        public ResultadoServicio<ArticuloDTO> ObtenerPorCategoriaYSubcategoria(string pCodigoCategoria, string pCodigoSubCategoria, string pCodigoAlmacen)
        {
            var mensajeValidacion = string.Empty;
            var articulos = _IRepositorioArticulo.ObtenerPorCategoriaYSubcategoria(pCodigoCategoria, pCodigoSubCategoria, pCodigoAlmacen);

            if (articulos != null && articulos.Any())
            {
                mensajeValidacion = "Consulta de Articulos exitosa.";
                return new ResultadoServicio<ArticuloDTO>(7,mensajeValidacion,
                        string.Empty, null,  articulos.ProyectadoComoColeccion<ArticuloDTO>());
            }
            else
            {
                mensajeValidacion = "Consulta de Articulos fallida.";
                return new ResultadoServicio<ArticuloDTO>(6,mensajeValidacion,
                        string.Empty, null,  null);                
            }
        }
        

        public decimal ObtenerPrecioVentaDeArticulo(string pCodigoCliente, string pCodigoArticulo, string pCodigoAlmacen)
        {
            DateTime fechaProcesoVenta; 
            string codigoClienteInterno;
            int cantidadDecimalPrecio;

            var configuracionGlobal = _IConfiguracionGlobalUnificado.UnificarConfiguracionGlobal();
            if(configuracionGlobal == null){
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ConfiguracionGlobalInvalido);
                throw new ArgumentException(Mensajes.advertencia_ConfiguracionGlobalInvalido);
            }else{
                fechaProcesoVenta = configuracionGlobal.FechaProcesoVenta;
                codigoClienteInterno = configuracionGlobal.CodigoClienteInterno;
                cantidadDecimalPrecio = configuracionGlobal.CantidadDecimalPrecio;             
            } 

            Articulo articulo = _IRepositorioArticulo.ObtenerPorCodigo(pCodigoArticulo, pCodigoAlmacen);
            if (articulo != null)
            {
                //Obtener Lista Precio Clientes
                ListaPrecioCliente listaPrecioCliente =
                            _IRepositorioListaPrecioCliente.ObtenerListaPrecioCliente(pCodigoCliente, pCodigoArticulo, 
                                                                        pCodigoAlmacen, fechaProcesoVenta.ToString("yyyyMMdd"));

                //Obtener Lista Precio Inventarios
                ListaPrecioInventario listaPrecioInventario =
                            _IRepositorioListaPrecioInventario.ObtenerListaPrecioInventario(pCodigoArticulo, pCodigoAlmacen);

                return _IServicioDominioListaPrecios.ObtenerPrecioVentaArticulo(articulo, listaPrecioCliente, listaPrecioInventario, 
                                                                        pCodigoCliente, codigoClienteInterno, cantidadDecimalPrecio);

            }
            else
            {
                LogFactory.CrearLog().LogWarning(Mensajes.advertencia_ArticuloNoExiste, pCodigoArticulo);
                return 0;
            }
        }
    }
}