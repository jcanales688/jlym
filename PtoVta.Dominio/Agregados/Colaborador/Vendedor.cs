using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public class Vendedor: Entidad
    {
        bool _EsHabilitado;


        public string CodigoVendedor { get; set; }

        public string NombresVendedor { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaInicio { get; set; }        
        public DateTime FechaNacimiento { get; set; }    
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

        public virtual Almacen Almacen  { get; private set; }
        public virtual EstadoVendedor EstadoVendedor { get; private  set; }
        public virtual UsuarioSistema UsuarioSistema { get; private set; }
        public virtual UsuarioSistema UsuarioSistemaAcceso { get; private set; }
        public virtual VendedorDireccion Direccion { get; set; }


        public Vendedor(){}
        public Vendedor(string pNombresVendedor, string pDocumentoIdentidad, string pTelefono,
                        string pSexo, DateTime pFechaInicio, string pCodigoVendedor,
                        string pClave, DateTime pFechaNacimiento)
        {
            this.CodigoVendedor  = !string.IsNullOrEmpty(pCodigoVendedor)? pCodigoVendedor.Trim()
                                        : throw new ArgumentException(Mensajes.advertencia_CodigoVendedorNoPuedeSerNuloOVacio);
            this.NombresVendedor = !string.IsNullOrEmpty(pNombresVendedor) ? pNombresVendedor.Trim()
                                        : throw new ArgumentException(Mensajes.advertencia_NombresVendedorNoPuedeSerNuloOVacio);
            this.DocumentoIdentidad = !string.IsNullOrEmpty(pDocumentoIdentidad)? pDocumentoIdentidad.Trim()
                                        : throw new ArgumentException(Mensajes.advertencia_DocumentoIdentidadNoPuedeSerNuloOVacio);            
            this.Telefono = pTelefono; 
            this.Sexo = !string.IsNullOrEmpty(pSexo) ? pSexo.Trim()
                                        : throw new ArgumentException(Mensajes.advertencia_SexoNoPuedeSerNuloOVacio);                        
            this.FechaInicio = pFechaInicio;
            this.Clave = !string.IsNullOrEmpty(pClave) ? pClave.Trim()
                                        : throw new ArgumentException(Mensajes.advertencia_ClaveNoPuedeSerNuloOVacio);                                    
            this.FechaNacimiento = pFechaNacimiento;            
        }


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





        //Almacen
        public void EstablecerAlmacenDeVendedor(Almacen pAlmacen)
        {
            if (pAlmacen == null)        
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeVendedorEnEstadoNuloOTransitorio);
        
            //relacion
            this.CodigoAlmacen = pAlmacen.CodigoAlmacen;
            this.Almacen = pAlmacen;
        }

        public void EstablecerReferenciaAlmacenDeVendedor(string pCodigoAlmacenAlmacen)
        {
            if (string.IsNullOrEmpty(pCodigoAlmacenAlmacen.Trim()))
                throw new ArgumentException(Mensajes.excepcion_AlmacenDeVendedorEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoAlmacen = pCodigoAlmacenAlmacen.Trim();
            this.Almacen = null;            
        }


        //UsuarioSistema el que registra el vendedor
        public void EstablecerUsuarioSistemaDeVendedor(UsuarioSistema pUsuarioSistema)
        {
            if (pUsuarioSistema == null )            
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaDeVendedorEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoUsuarioSistema = pUsuarioSistema.CodigoUsuarioDeSistema;
            this.UsuarioSistema = pUsuarioSistema;
        }

        public void EstablecerReferenciaUsuarioSistemaDeVendedor(string pCodigoUsuarioSistema)
        {
            if (string.IsNullOrEmpty(pCodigoUsuarioSistema.Trim()))
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaDeVendedorEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoUsuarioSistema = pCodigoUsuarioSistema.Trim();
            this.UsuarioSistema = null;            
        }

        //EstadoVendedor
        public void EstablecerEstadoVendedorDeVendedor(EstadoVendedor pEstadoVendedor)
        {
            if (pEstadoVendedor == null)        
                throw new ArgumentException(Mensajes.excepcion_EstadoVendedorDeVendedorEnEstadoNuloOTransitorio);
    
            //relacion
            this.CodigoEstadoVendedor = pEstadoVendedor.CodigoEstadoVendedor;
            this.EstadoVendedor = pEstadoVendedor;
        }

        public void EstablecerReferenciaEstadoVendedorDeVendedor(string pCodigoEstadoVendedor)
        {
            if (string.IsNullOrEmpty(pCodigoEstadoVendedor.Trim()))
                throw new ArgumentException(Mensajes.excepcion_EstadoVendedorDeVendedorEnEstadoNuloOTransitorio);            

            //relacion
            this.CodigoEstadoVendedor = pCodigoEstadoVendedor.Trim();
            this.EstadoVendedor = null;            
        }




        //UsuarioSistema Acceso
        public void EstablecerUsuarioSistemaAccesoDeVendedor(UsuarioSistema pUsuarioSistemaAcceso)
        {
            if (pUsuarioSistemaAcceso == null)
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaAccesoDeVendedorEnEstadoNuloOTransitorio);
        
            //relacion
            this.CodigoUsuarioSistemaAcceso = pUsuarioSistemaAcceso.CodigoUsuarioDeSistema;
            this.UsuarioSistemaAcceso = pUsuarioSistemaAcceso;
        }

        public void EstablecerReferenciaUsuarioSistemaAccesoDeVendedor(string pCodigoUsuarioSistemaAcceso)
        {
            if (string.IsNullOrEmpty(pCodigoUsuarioSistemaAcceso.Trim()))
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaAccesoDeVendedorEnEstadoNuloOTransitorio);

            //relacion
            this.CodigoUsuarioSistemaAcceso = pCodigoUsuarioSistemaAcceso.Trim();
            this.UsuarioSistemaAcceso = null;            
        }        
    }
}
