using System;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public class Vendedor: Entidad
    {
        bool _EsHabilitado;


        public string CodigoVendedor { get; set; }

        public string NombresVendedor { get; set; }
        public string DocumentoIdentidad { get; set; }
     
        public string Clave { get; set; }




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



        public string CodigoAlmacen{ get; private set; }
        // public Guid AlmacenId { get; private set; }
        public string CodigoEstadoVendedor{ get; private set; }
        // public Guid EstadoVendedorId { get;private set; }
        public string CodigoUsuarioSistema { get;private set; }
        // public Guid UsuarioSistemaId { get;private set; }
        public string CodigoUsuarioSistemaAcceso { get; private set; }
        // public Guid UsuarioSistemaAccesoId { get; private set; }

        //public virtual Almacen Almacen  { get; private set; }
        public virtual EstadoVendedor EstadoVendedor { get; private  set; }
        //public virtual UsuarioSistema UsuarioSistema { get; private set; }
        public virtual UsuarioSistema UsuarioSistemaAcceso { get; private set; }



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







        //EstadoVendedor
        public void EstablecerEstadoVendedorDeVendedor(EstadoVendedor pEstadoVendedor)
        {
            if (pEstadoVendedor == null || pEstadoVendedor.EsTransitorio())
            {
                throw new ArgumentException("Mensajes.excepcion_EstadoVendedorDeVendedorEnEstadoNuloOTransitorio");

            }

            //relacion
            this.CodigoEstadoVendedor = pEstadoVendedor.CodigoEstadoVendedor;
            this.EstadoVendedor = pEstadoVendedor;
        }

        public void EstablecerReferenciaEstadoVendedorDeVendedor(string pCodigoEstadoVendedor)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoVendedor))
            {
                //relacion
                this.CodigoEstadoVendedor = pCodigoEstadoVendedor;
                this.EstadoVendedor = null;
            }
        }




        //UsuarioSistema Acceso
        public void EstablecerUsuarioSistemaAccesoDeVendedor(UsuarioSistema pUsuarioSistemaAcceso)
        {
            if (pUsuarioSistemaAcceso == null)
            {
                throw new ArgumentException("Mensajes.excepcion_UsuarioSistemaAccesoDeVendedorEnEstadoNuloOTransitorio");

            }

            //relacion
            this.CodigoUsuarioSistemaAcceso = pUsuarioSistemaAcceso.CodigoUsuarioDeSistema;
            this.UsuarioSistemaAcceso = pUsuarioSistemaAcceso;
        }

        public void EstablecerReferenciaUsuarioSistemaAccesoDeVendedor(string pCodigoUsuarioSistemaAcceso)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioSistemaAcceso))
            {
                //relacion
                this.CodigoUsuarioSistemaAcceso = pCodigoUsuarioSistemaAcceso;
                this.UsuarioSistemaAcceso = null;
            }
        }        
    }
}
