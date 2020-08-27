using System;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class ListaPrecioClienteDetalle: Entidad
    {
        public int Secuencia { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal PrecioAntesLista { get; set; }
        public decimal NuevoPrecioCliente { get; set; }
        public string DescripcionArticulo { get; set; }


        public string CodigoListaPrecioCliente { get; set; }

        public string CodigoAlmacen { get; set; }
        public string CodigoArticulo { get; set; }

        public Almacen Almacen { get; private set; }
        public Articulo Articulo { get; private set; }        



        public void EstablecerAlmacenDeListaPrecioClienteDetalle(Almacen pAlmacen)
        {
            if (pAlmacen == null)
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeListaPrecioClienteDetalleNuloOTransitorio);


            //relacion
            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeListaPrecioClienteDetalle(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {
                //relacion
                this.CodigoAlmacen = pCodigoAlmacen.Trim();
                this.Almacen = null;
            }
        }

        public void EstablecerArticuloDeListaPrecioClienteDetalle(Articulo pArticulo)
        {
            if (pArticulo == null)
                throw new ArgumentException(Mensajes.excepcion_ArticuloDeListaPrecioClienteDetalleNuloOTransitorio);

            //relacion
            this.CodigoArticulo = pArticulo.CodigoArticulo;
            this.Articulo = pArticulo;
        }

        public void EstablecerReferenciaArticuloDeListaPrecioClienteDetallee(string pCodigoArticulo)
        {
            if (!string.IsNullOrEmpty(pCodigoArticulo))
            {
                //relacion
                this.CodigoArticulo = pCodigoArticulo.Trim();
                this.Articulo = null;
            }
        }        
    }
}