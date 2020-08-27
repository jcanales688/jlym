using System;
using System.Linq;
using PtoVta.Dominio.Agregados.Ventas;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class ServicioDominioListaPrecios : IServicioDominioListaPrecios
    {
        public decimal ObtenerPrecioVentaArticulo(Articulo pArticulo, ListaPrecioCliente pListaPrecioCliente,
                        ListaPrecioInventario pListaPrecioInventario, string pCodigoClienteDeListaPrecio,
                        string pCodigoClienteInterno, int pCantidadDecimalPrecio)
        {
            //Validacion de Articulo
            if (pArticulo == null)
                throw new InvalidOperationException(Mensajes.excepcion_ArticuloNoExisteEnServicioDominioListaPrecios);

            ////Validacion Listas Precios Disponibles
            //if (pListaPrecioCliente == null && pListaPrecioInventario == null)
            //    throw new InvalidOperationException(Mensajes.exception_CannotTransferMoneyWhenFromIsTheSameAsTo);

            //Validacion si articulo es habilitado
            if (!pArticulo.EsHabilitado)
                throw new InvalidOperationException(Mensajes.excepcion_ArticuloNoHabilitadoEnServicioDominioListaPrecios);

            //Precio por defecto
            decimal originalPrecioVenta = pArticulo.ArticuloDetalle.Precio;

            decimal nuevoPrecioVenta = 0;
            decimal precioVentaObtenido = originalPrecioVenta; //Valor por defecto

            //Lista Precio de Clientes-----------
            if (pListaPrecioCliente != null && pListaPrecioCliente.ListaPrecioClienteDetalles.Any())
            {
                Nullable<int> modalidadDescuento = pListaPrecioCliente.ModalidadDescuento;

                decimal montoDescuento = pListaPrecioCliente.ListaPrecioClienteDetalles.FirstOrDefault().MontoDescuento;

                ////Validacion que no sea un cliente interno
                if (pCodigoClienteDeListaPrecio != pCodigoClienteInterno)
                {
                    //descuento en monto
                    if (modalidadDescuento == 1)
                    {
                        nuevoPrecioVenta = originalPrecioVenta - montoDescuento;
                    }
                    //descuento en porcentaje
                    else if (modalidadDescuento == 3)
                    {
                        nuevoPrecioVenta =
                            originalPrecioVenta - Math.Round(originalPrecioVenta * (montoDescuento / 100), pCantidadDecimalPrecio);
                    }
                }

                if (nuevoPrecioVenta > 0)
                    precioVentaObtenido = nuevoPrecioVenta;

            }

            //Lista Precio Inventario;
            if (pListaPrecioInventario != null && pListaPrecioInventario.ListaPrecioInventarioDetalles.Any())
            {
                if (nuevoPrecioVenta == 0)
                {
                    nuevoPrecioVenta = pListaPrecioInventario.ListaPrecioInventarioDetalles.FirstOrDefault().NuevoPrecioInventario;

                    if (nuevoPrecioVenta > 0)
                        precioVentaObtenido = nuevoPrecioVenta;
                }
            }

            return precioVentaObtenido;
        }
    }
}