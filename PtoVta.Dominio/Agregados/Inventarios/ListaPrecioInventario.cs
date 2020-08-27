using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Inventarios
{
    public class ListaPrecioInventario : Entidad
    {
        HashSet<ListaPrecioInventarioDetalle> _lineasListaPrecioInventarioDetalle;

        public decimal CodigoListaPrecioInventario { get; set; }

        public DateTime FechaHoraCrea { get; set; }
        public DateTime FechaHoraAprueba { get; set; }
        public DateTime FechaInicioPrecio { get; set; }
        public DateTime FechaFinPrecio { get; set; }
        public string HoraInicioPrecio { get; set; }
        public string HoraFinPrecio { get; set; }
        public bool EsActivo { get; set; }
        public bool EnviarAprobacion { get; set; } 

        public string CodigoTipoPrecioInventario { get; private set; }
        public string CodigoUsuarioDeSistemaCrea { get; private set; }
        public string CodigoUsuarioDeSistemaAprueba { get; private set; }
        public string CodigoAlmacen { get; private set; }
        public string CodigoAlmacenOrigen { get; private set; }

        public virtual TipoPrecioInventario TipoPrecioInventario { get; private set; }
        public virtual UsuarioSistema UsuarioDeSistemaCrea { get; private  set; }
        public virtual UsuarioSistema UsuarioDeSistemaAprueba { get; private  set; }
        public virtual Almacen Almacen { get; private  set; }
        public virtual Almacen AlmacenOrigen { get; private  set; }

        

        public virtual ICollection<ListaPrecioInventarioDetalle> ListaPrecioInventarioDetalles 
        {
            get
            {
                if (_lineasListaPrecioInventarioDetalle == null)
                    _lineasListaPrecioInventarioDetalle = new HashSet<ListaPrecioInventarioDetalle>();

                return _lineasListaPrecioInventarioDetalle;
            }
            set
            {
                _lineasListaPrecioInventarioDetalle = new HashSet<ListaPrecioInventarioDetalle>(value);
            }
        }

        public ListaPrecioInventarioDetalle AgregarNuevaListaPrecioInventarioDetalle(int pSecuencia, string pDescripcionArticulo, 
                                    decimal pPrecioAntesLista, decimal pMontoDescuento, decimal pNuevoPrecioInventario, 
                                    decimal pCostoReposicion,  string pCodigoArticulo)
        {
            if (string.IsNullOrEmpty(pCodigoArticulo.Trim())
                ||
                pPrecioAntesLista <= 0
                ||
                pMontoDescuento <= 0
                ||
                pNuevoPrecioInventario <= 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaListaPrecioInventarioDetalle);


            var nuevaLineaListaPrecioInventarioDetalle = new ListaPrecioInventarioDetalle()
            {
                CodigoListaPrecioInventario = this.CodigoListaPrecioInventario,
                Secuencia = pSecuencia,
                DescripcionArticulo = pDescripcionArticulo,
                PrecioAntesLista = pPrecioAntesLista,
                MontoDescuento = pMontoDescuento,
                NuevoPrecioInventario = pNuevoPrecioInventario,
                CostoReposicion = pCostoReposicion,
                CodigoArticulo = pCodigoArticulo
            };

            //Establecer la identidad
            // nuevaLineaListaPrecioInventarioDetalle.GenerarNuevaIdentidad();

            this.ListaPrecioInventarioDetalles.Add(nuevaLineaListaPrecioInventarioDetalle);

            return nuevaLineaListaPrecioInventarioDetalle;

        }

        public void EstablecerTipoPrecioInventarioDeListaPrecioInventario(TipoPrecioInventario pTipoPrecioInventario)
        {
            if (pTipoPrecioInventario == null)
                throw new ArgumentException(Mensajes.excepcion_TipoPrecioInventarioDeListaPrecioInventarioEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoTipoPrecioInventario = pTipoPrecioInventario.CodigoTipoPrecioInventario;
            this.TipoPrecioInventario = pTipoPrecioInventario;
        }

        public void EstablecerReferenciaTipoPrecioInventarioDeListaPrecioInventario(string pCodigoTipoPrecioInventario)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoPrecioInventario))
            {
                //relacion
                this.CodigoTipoPrecioInventario = pCodigoTipoPrecioInventario.Trim();
                this.TipoPrecioInventario = null;
            }
        }


        public void EstablecerUsuarioSistemaCreaDeListaPrecioInventario(UsuarioSistema pUsuarioSistemaCrea)
        {
            if (pUsuarioSistemaCrea == null)
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaCreaDeListaPrecioInventarioEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoUsuarioDeSistemaCrea = pUsuarioSistemaCrea.CodigoUsuarioDeSistema;
            this.UsuarioDeSistemaCrea = pUsuarioSistemaCrea;
        }

        public void EstablecerReferenciaUsuarioSistemaCreaDeListaPrecioInventario(string pCodigoUsuarioSistemaCrea)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioSistemaCrea))
            {
                //relacion
                this.CodigoUsuarioDeSistemaCrea = pCodigoUsuarioSistemaCrea.Trim();
                this.UsuarioDeSistemaCrea = null;
            }
        }


        public void EstablecerUsuarioSistemaApruebaDeListaPrecioInventario(UsuarioSistema pUsuarioSistemaAprueba)
        {
            if (pUsuarioSistemaAprueba == null)
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaApruebaDeListaPrecioInventarioEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoUsuarioDeSistemaAprueba = pUsuarioSistemaAprueba.CodigoUsuarioDeSistema;
            this.UsuarioDeSistemaAprueba = pUsuarioSistemaAprueba;
        }

        public void EstablecerReferenciaUsuarioSistemaApruebaDeListaPrecioInventario(string pCodigoUsuarioSistemaAprueba)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioSistemaAprueba))
            {
                //relacion
                this.CodigoUsuarioDeSistemaAprueba = pCodigoUsuarioSistemaAprueba.Trim();
                this.UsuarioDeSistemaAprueba = null;
            }
        }

        public void EstablecerAlmacenDeListaPrecioInventario(Almacen pAlmacen)
        {
            if (pAlmacen == null)
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeListaPrecioInventarioEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeListaPrecioInventario(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {
                //relacion
                this.CodigoAlmacen = pCodigoAlmacen.Trim();
                this.Almacen = null;
            }
        }

        public void EstablecerAlmacenOrigenDeListaPrecioInventario(Almacen pAlmacenOrigen)
        {
            if (pAlmacenOrigen == null)        
                throw new ArgumentException(Mensajes.excepcion_AlmacenOrigenDeListaPrecioInventarioEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoAlmacenOrigen = pAlmacenOrigen.CodigoAlmacen;
            this.AlmacenOrigen = pAlmacenOrigen;
        }

        public void EstablecerReferenciaAlmacenOrigenDeListaPrecioInventario(string pCodigoAlmacenOrigen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacenOrigen))
            {
                //relacion
                this.CodigoAlmacenOrigen = pCodigoAlmacenOrigen.Trim();
                this.AlmacenOrigen = null;
            }
        }        
    }
}
