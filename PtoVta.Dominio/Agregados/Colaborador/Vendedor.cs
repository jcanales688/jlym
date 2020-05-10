using System;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public class Vendedor: Entidad
    {
        bool _EsHabilitado;




        public string NombresVendedor { get; set; }
        public string DocumentoIdentidad { get; set; }
     

        public string UsuarioVendedor { get; set; }
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




        public Guid AlmacenId { get; private set; }
        public Guid EstadoVendedorId { get;private set; }
        public Guid UsuarioSistemaId { get;private set; }
        public Guid UsuarioSistemaAccesoId { get; private set; }

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
            this.EstadoVendedorId = pEstadoVendedor.Id;
            this.EstadoVendedor = pEstadoVendedor;
        }

        public void EstablecerReferenciaEstadoVendedorDeVendedor(Guid pEstadoVendedorId)
        {
            if (pEstadoVendedorId != Guid.Empty)
            {
                //relacion
                this.EstadoVendedorId = pEstadoVendedorId;
                this.EstadoVendedor = null;
            }
        }




        //UsuarioSistema Acceso
        public void EstablecerUsuarioSistemaAccesoDeVendedor(UsuarioSistema pUsuarioSistemaAcceso)
        {
            if (pUsuarioSistemaAcceso == null || pUsuarioSistemaAcceso.EsTransitorio())
            {
                throw new ArgumentException("Mensajes.excepcion_UsuarioSistemaAccesoDeVendedorEnEstadoNuloOTransitorio");

            }

            //relacion
            this.UsuarioSistemaAccesoId = pUsuarioSistemaAcceso.Id;
            this.UsuarioSistemaAcceso = pUsuarioSistemaAcceso;
        }

        public void EstablecerReferenciaUsuarioSistemaAccesoDeVendedor(Guid pUsuarioSistemaAccesoId)
        {
            if (pUsuarioSistemaAccesoId != Guid.Empty)
            {
                //relacion
                this.UsuarioSistemaAccesoId = pUsuarioSistemaAccesoId;
                this.UsuarioSistemaAcceso = null;
            }
        }        
    }
}
