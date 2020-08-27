using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class ListaPrecioCliente: Entidad
    {
        HashSet<AsignacionListaPrecioCliente> _lineasAsignacionListaPrecioCliente;        
        HashSet<ListaPrecioClienteDetalle> _lineaListaPrecioClienteDetalle;

        public string CodigoListaPrecioCliente { get; set; }
        public string DescripcionListaPrecioCliente { get; set; }
        public DateTime FechaInicioPrecio { get; set; }
        public DateTime FechaFinPrecio { get; set; }
        public int PrimeraAprobacion { get; set; }
        public DateTime FechaHoraPrimeraAprobacion { get; set; }
        public int SegundaAprobacion { get; set; }
        public DateTime FechaHoraSegundaAprobacion { get; set; }
        public int EnviarAprobacion { get; set; }
        public int ModalidadDescuento { get; set; }

        public string CodigoMoneda { get; private set; }
        public string CodigoUsuarioDeSistemaCrea { get; private set; }
        public string CodigoUsuarioDeSistemaAprueba { get; private set; }
        public string CodigoAlmacen { get; private set; }
        public string CodigoAlmacenOrigen { get; private set; }

        public virtual Moneda Moneda { get; private set; }
        public virtual UsuarioSistema UsuarioSistemaCrea { get; private set; }
        public virtual UsuarioSistema UsuarioSistemaAprueba { get; private set; }
        public virtual Almacen Almacen { get; private set; }
        public virtual Almacen AlmacenOrigen { get; private set; }


        public virtual ICollection<ListaPrecioClienteDetalle> ListaPrecioClienteDetalles 
        {
            get
            {
                if (_lineaListaPrecioClienteDetalle == null)
                    _lineaListaPrecioClienteDetalle = new HashSet<ListaPrecioClienteDetalle>();

                return _lineaListaPrecioClienteDetalle;
            }
            set
            {
                _lineaListaPrecioClienteDetalle = new HashSet<ListaPrecioClienteDetalle>(value);
            }
        }

        public virtual ICollection<AsignacionListaPrecioCliente> AsignacionListasPrecioCliente
        {
            get
            {
                if (_lineasAsignacionListaPrecioCliente == null)
                    _lineasAsignacionListaPrecioCliente = new HashSet<AsignacionListaPrecioCliente>();

                return _lineasAsignacionListaPrecioCliente;
            }
            set
            {
                _lineasAsignacionListaPrecioCliente = new HashSet<AsignacionListaPrecioCliente>(value);
            }
        }        


        public ListaPrecioClienteDetalle AgregarNuevaListaPrecioClienteDetalle(int pSecuencia, decimal pMontoDescuento, 
                                decimal pPrecioAntesLista, decimal pNuevoPrecioCliente, string pDescripcionArticulo, 
                                string pCodigoArticulo)
        {
            if (string.IsNullOrEmpty(pCodigoArticulo.Trim())
                ||
                pMontoDescuento <= 0
                ||
                pPrecioAntesLista <= 0
                ||
                pNuevoPrecioCliente <= 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaListaPrecioClienteDetalle);


            var nuevaLineaListaPrecioClienteDetalle = new ListaPrecioClienteDetalle()
            {
                CodigoListaPrecioCliente = this.CodigoListaPrecioCliente,                
                CodigoAlmacen = this.CodigoAlmacen,
                CodigoArticulo = pCodigoArticulo.Trim(),
                Secuencia = pSecuencia,
                MontoDescuento = pMontoDescuento,
                PrecioAntesLista = pPrecioAntesLista,
                NuevoPrecioCliente = pNuevoPrecioCliente,
                DescripcionArticulo = pDescripcionArticulo

            };

            //Establecer la identidad
            // nuevaLineaListaPrecioClienteDetalle.GenerarNuevaIdentidad();

            this.ListaPrecioClienteDetalles.Add(nuevaLineaListaPrecioClienteDetalle);

            return nuevaLineaListaPrecioClienteDetalle;

        }


        public void EstablecerMonedaDeListaPrecioCliente(Moneda pMoneda)
        {
            if (pMoneda == null)
                throw new ArgumentException(Mensajes.excepcion_MonedaDeListaPrecioClienteNuloOTransitorio);

            //relacion
            this.CodigoMoneda = pMoneda.CodigoMoneda;
            this.Moneda = pMoneda;
        }

        public void EstablecerReferenciaMonedaDeListaPrecioCliente(string pCodigoMoneda)
        {
            if (!string.IsNullOrEmpty(pCodigoMoneda))
            {
                //relacion
                this.CodigoMoneda = pCodigoMoneda.Trim();
                this.Moneda = null;
            }
        }


        public void EstablecerUsuarioSistemaCreaDeListaPrecioCliente(UsuarioSistema pUsuarioSistemaCrea)
        {
            if (pUsuarioSistemaCrea == null)    
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaCreaDeListaPrecioClienteNuloOTransitorio);

            //relacion
            this.CodigoUsuarioDeSistemaCrea = pUsuarioSistemaCrea.CodigoUsuarioDeSistema;
            this.UsuarioSistemaCrea = pUsuarioSistemaCrea;
        }

        public void EstablecerReferenciaUsuarioSistemaCreaDeListaPrecioCliente(string pCodigoUsuarioDeSistemaCrea)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioDeSistemaCrea))
            {
                //relacion
                this.CodigoUsuarioDeSistemaCrea = pCodigoUsuarioDeSistemaCrea.Trim();
                this.UsuarioSistemaCrea = null;
            }
        }


        public void EstablecerUsuarioSistemaApruebaDeListaPrecioCliente(UsuarioSistema pUsuarioSistemaAprueba)
        {
            if (pUsuarioSistemaAprueba == null)
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaApruebaDeListaPrecioClienteNuloOTransitorio);

            //relacion
            this.CodigoUsuarioDeSistemaAprueba = pUsuarioSistemaAprueba.CodigoUsuarioDeSistema;
            this.UsuarioSistemaAprueba = pUsuarioSistemaAprueba;
        }

        public void EstablecerReferenciaUsuarioSistemaApruebaDeListaPrecioCliente(string pCodigoUsuarioSistemaAprueba)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioSistemaAprueba))
            {
                //relacion
                this.CodigoUsuarioDeSistemaAprueba = pCodigoUsuarioSistemaAprueba.Trim();
                this.UsuarioSistemaAprueba = null;
            }
        }

        public void EstablecerAlmacenDeListaPrecioCliente(Almacen pAlmacen)
        {
            if (pAlmacen == null)
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeListaPrecioClienteNuloOTransitorio);

            //relacion
            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeListaPrecioCliente(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {
                //relacion
                this.CodigoAlmacen = pCodigoAlmacen.Trim();
                this.Almacen = null;
            }
        }

        public void EstablecerAlmacenOrigenDeListaPrecioCliente(Almacen pAlmacenOrigen)
        {
            if (pAlmacenOrigen == null)
                throw new ArgumentException(Mensajes.excepcion_AlmacenOrigenDeListaPrecioClienteNuloOTransitorio);

            //relacion
            this.CodigoAlmacenOrigen = pAlmacenOrigen.CodigoAlmacen;
            this.AlmacenOrigen = pAlmacenOrigen;
        }

        public void EstablecerReferenciaAlmacenOrigenDeListaPrecioCliente(string pCodigoAlmacenOrigen)
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