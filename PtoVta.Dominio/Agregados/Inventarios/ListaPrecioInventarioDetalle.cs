using System;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class ListaPrecioInventarioDetalle : Entidad
    {
        public int Secuencia { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal PrecioAntesLista { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal NuevoPrecioInventario { get; set; }
        public decimal CostoReposicion { get; set; }
        public string CodigoArticulo { get; set; }

        public decimal CodigoListaPrecioInventario { get; set; }


        public Articulo Articulo { get; private set; }


        public void EstablecerArticuloDeListaPrecioInventarioDetalle(Articulo pArticulo)
        {
            if (pArticulo == null)
                throw new ArgumentException(Mensajes.excepcion_ArticuloDeListaPrecioInventarioDetalleEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoArticulo = pArticulo.CodigoArticulo;
            this.Articulo = pArticulo;
        }

        public void EstablecerReferenciaArticuloDeListaPrecioInventarioDetalle(string pCodigoArticulo)
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