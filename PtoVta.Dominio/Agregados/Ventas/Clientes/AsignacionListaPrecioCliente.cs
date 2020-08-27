using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class AsignacionListaPrecioCliente : Entidad
    {
        public DateTime FechaCreacion { get; set; }

        public string CodigoCliente { get; set; }

        public string CodigoAlmacen { get; set; }
        public string CodigoListaPrecioCliente { get; set; }
        public string CodigoUsuarioDeSistema { get; set; }


        public virtual Almacen Almacen { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public virtual ListaPrecioCliente ListaPrecioCliente { get; private set; }
        public virtual UsuarioSistema UsuarioSistema { get; private set; }        


        //Almacen
        public void EstablecerAlmacenDeAsignacionListaPrecioCliente(Almacen pAlmacen)
        {
            if (pAlmacen == null)
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeAsignacionListaPrecioClienteNuloOTransitorio);



            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeAsignacionListaPrecioCliente(string pCodigoAlmacen)
        {
            if (!string.IsNullOrEmpty(pCodigoAlmacen))
            {
                this.CodigoAlmacen = pCodigoAlmacen.Trim();
                this.Almacen = null;
            }
        }

        //Cliente
        public void EstablecerClienteDeAsignacionListaPrecioCliente(Cliente pCliente)
        {
            if (pCliente == null)
                throw new ArgumentException(Mensajes.excepcion_ClienteDeAsignacionListaPrecioClienteNuloOTransitorio);

            this.CodigoCliente = pCliente.CodigoCliente;
            this.Cliente = pCliente;
        }

        public void EstablecerReferenciaClienteDeAsignacionListaPrecioCliente(string pCodigoCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoCliente))
            {
                this.CodigoCliente = pCodigoCliente.Trim();
                this.Cliente = null;
            }
        }


        //ListaPrecioCliente
        public void EstablecerListaPrecioClienteDeAsignacionListaPrecioCliente(ListaPrecioCliente pListaPrecioCliente)
        {
            if (pListaPrecioCliente == null)
                throw new ArgumentException(Mensajes.excepcion_ListaPrecioClienteDeAsignacionListaPrecioClienteNuloOTransitorio);

            this.CodigoListaPrecioCliente = pListaPrecioCliente.CodigoListaPrecioCliente;
            this.ListaPrecioCliente = pListaPrecioCliente;
        }

        public void EstablecerReferenciaListaPrecioClienteDeAsignacionListaPrecioCliente(string pCodigoListaPrecioCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoListaPrecioCliente))
            {
                this.CodigoListaPrecioCliente = pCodigoListaPrecioCliente.Trim();
                this.ListaPrecioCliente = null;
            }
        }

        //UsuarioSistema
        public void EstablecerUsuarioSistemaDeAsignacionListaPrecioCliente(UsuarioSistema pUsuarioSistema)
        {
            if (pUsuarioSistema == null)        
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaDeAsignacionListaPrecioClienteNuloOTransitorio);
            
            this.CodigoUsuarioDeSistema = pUsuarioSistema.CodigoUsuarioDeSistema;
            this.UsuarioSistema = pUsuarioSistema;
        }

        public void EstablecerReferenciaUsuarioSistemaDeAsignacionListaPrecioCliente(string pCodigoUsuarioSistema)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioSistema))
            {
                this.CodigoUsuarioDeSistema = pCodigoUsuarioSistema.Trim();
                this.UsuarioSistema = null;
            }
        }        
    }
}