using System;
using System.Collections.Generic;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class ListaPrecioCliente: Entidad
    {
        bool _EsHabilitado;

        HashSet<ListaPrecioClienteDetalle> _lineaListaPrecioClienteDetalle;

        public string CodigoListaPrecioCliente { get; set; }
        public string DescripcionListaPrecioCliente { get; set; }
        public DateTime FechaInicioPrecio { get; set; }
        public DateTime FechaFinPrecio { get; set; }

        public int PrimeraAprobacion { get; set; }
        public DateTime FechaHoraPrimeraAprob { get; set; }
        public int SegundaAprobacion { get; set; }
        public DateTime FechaHoraSegundaAprob { get; set; }
        public int EnviarAprobacion { get; set; }
        public Nullable<int> ModalidadDescuento { get; set; }

        public bool EsHabilitado
        {
            get
            {
                return _EsHabilitado;
            }
            private set
            {
                _EsHabilitado = value;
            }
        }


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


        public void Habilitar()
        {
            if (!EsHabilitado)
                this._EsHabilitado = true;


        }

        public void Deshabilitar()
        {
            if (EsHabilitado)
                this._EsHabilitado = false;
        }

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
    }
}