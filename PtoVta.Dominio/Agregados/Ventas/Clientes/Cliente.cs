using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.MensajesDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class Cliente : Entidad
    {
        bool _EsHabilitado;

        HashSet<ClienteLimiteCredito> _lineasClienteLimiteCredito;
        HashSet<AsignacionListaPrecioCliente> _lineasAsignacionListaPrecioCliente;
        HashSet<DocumentoLibre> _lineasDocumentoLibre; 

        public string CodigoCliente { get; set; }

        public string CodigoContable { get; set; }
        public string Ruc { get; set; }
        public string NombresORazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public Nullable<DateTime> FechaNacimiento { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public int DiasDeGracia { get; set; }
        public decimal MontoLimiteCredito { get; set; }
        public decimal Deuda { get; set; }
        public Nullable<int> EsAfecto { get; set; }
        public Nullable<int> ControlarSaldoDispo { get; set; }

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
        public string CodigoClaseTipoCambio { get; private set; }
        public string  CodigoTipoCliente { get; private set; }
        public string CodigoZonaCliente { get; private set; }
        public string CodigoDiaDePago { get; private set; }
        public string CodigoVendedor { get; private set; }
        public string CodigoImpuestoIgv { get; private set; }
        public string CodigoImpuestoIsc { get; private set; }
        public string CodigoCondicionPagoDocumentoGenerado { get; private set; }
        public string CodigoCondicionPagoTicket { get; private set; }
        public string CodigoEstadoDeCliente { get; private set; }
        public string CodigoUsuarioDeSistema { get; private set; }

        public virtual Moneda Moneda { get; private set; }
        public virtual ClaseTipoCambio ClaseTipoCambio { get; private set; }
        public virtual TipoCliente TipoCliente { get; private set; }
        public virtual ZonaCliente ZonaCliente { get; private set; }
        public virtual DiaDePago DiaDePago { get; private set; }
        public virtual Vendedor Vendedor { get; private set; }
        public virtual Impuesto ImpuestoIgv { get; private set; }
        public virtual Impuesto ImpuestoIsc { get; private set; }
        public virtual CondicionPago CondicionPagoDocumentoGenerado { get; private set; }
        public virtual CondicionPago CondicionPagoTicket { get; private set; }
        public virtual EstadoDeCliente EstadoDeCliente { get; private set; }
        public virtual UsuarioSistema UsuarioSistema { get; private set; }


        public virtual ClienteDireccion DireccionPrimero { get; set; }
        public virtual ClienteDireccion DireccionSegundo { get; set; }



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

        public virtual ICollection<ClienteLimiteCredito> ClienteLimitesCredito 
        {
            get
            {
                if (_lineasClienteLimiteCredito == null)
                    _lineasClienteLimiteCredito = new HashSet<ClienteLimiteCredito>();

                return _lineasClienteLimiteCredito;
            }
            set
            {
                _lineasClienteLimiteCredito = new HashSet<ClienteLimiteCredito>(value);
            }
        }

        public virtual ICollection<DocumentoLibre> DocumentosLibre
        {
            get
            {
                if (_lineasDocumentoLibre == null)
                    _lineasDocumentoLibre = new HashSet<DocumentoLibre>();

                return _lineasDocumentoLibre;
            }
            set
            {
                _lineasDocumentoLibre = new HashSet<DocumentoLibre>(value);
            }
        }



        public AsignacionListaPrecioCliente AgregarNuevaAsignacionListaPrecioCliente(DateTime pFechaCreacion, string pCodigoAlmacen, 
                                                                        string pCodigoListaPrecioCliente, string pCodigoUsuarioDeSistema)
        {
            if (string.IsNullOrEmpty(pCodigoAlmacen)
                ||
                string.IsNullOrEmpty(pCodigoListaPrecioCliente)
                ||
                string.IsNullOrEmpty(pCodigoUsuarioDeSistema)
                ||
                pFechaCreacion == null
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaAsignacionListaPrecioCliente);


            var nuevaLineaAsignacionListaPrecioCliente = new AsignacionListaPrecioCliente()
            {
                CodigoCliente = this.CodigoCliente,
                CodigoAlmacen = pCodigoAlmacen,
                CodigoListaPrecioCliente = pCodigoListaPrecioCliente,
                CodigoUsuarioDeSistema = pCodigoUsuarioDeSistema,
                FechaCreacion = pFechaCreacion
            };

            //Establecer la identidad
            nuevaLineaAsignacionListaPrecioCliente.GenerarNuevaIdentidad();

            this.AsignacionListasPrecioCliente.Add(nuevaLineaAsignacionListaPrecioCliente);

            return nuevaLineaAsignacionListaPrecioCliente;


        }


        public ClienteLimiteCredito AgregarNuevoClienteLimiteCredito(decimal pPorcentajeLimite, decimal pMontoLimite,
                            decimal pDeuda, decimal pPorcentajeExcede, decimal pMontoExcedente,
                            string pCodigoAlmacen, string pCodigoUsuarioDeSistema)
        {
            if (string.IsNullOrEmpty(pCodigoAlmacen)
                ||
                string.IsNullOrEmpty(pCodigoUsuarioDeSistema)
                ||
                pPorcentajeLimite <= 0
                ||
                pMontoLimite <= 0
                ||
                pPorcentajeExcede < 0
                ||
                pMontoExcedente < 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaClienteLimiteCredito);



            var nuevaLineaClienteLimiteCredito = new ClienteLimiteCredito()
            {
                CodigoCliente = this.CodigoCliente,
                CodigoAlmacen = pCodigoAlmacen,
                CodigoUsuarioDeSistema   = pCodigoUsuarioDeSistema,
                PorcentajeLimite = pPorcentajeLimite,
                MontoLimite = pMontoLimite,
                Deuda = pDeuda,
                PorcentajeExcede =pPorcentajeExcede, 
                MontoExcedente = pMontoExcedente
            };

            //Establecer la identidad
            nuevaLineaClienteLimiteCredito.GenerarNuevaIdentidad();

            this.ClienteLimitesCredito.Add(nuevaLineaClienteLimiteCredito);

            return nuevaLineaClienteLimiteCredito;


        }


        public DocumentoLibre AgregarNuevoDocumentoLibre(decimal pNumeroDocumentoLibre, DateTime pFechaProcesoInicial, 
                                DateTime pFechaProcesoFinal, decimal pTotalLibre,string pCodigoAlmacen, string pCodigoUsuarioSistema)
        {
            if (string.IsNullOrEmpty(pCodigoAlmacen)
                ||
                string.IsNullOrEmpty(pCodigoUsuarioSistema)
                ||
                pNumeroDocumentoLibre <= 0
                ||
                pFechaProcesoInicial == null
                ||
                pFechaProcesoFinal == null
                ||
                pTotalLibre <= 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaDocumentoLibre);



            var nuevaLineaDocumentoLibre = new DocumentoLibre()
            {
                CodigoCliente = this.CodigoCliente,
                CodigoAlmacen = pCodigoAlmacen,
                CodigoUsuarioDeSistema = pCodigoUsuarioSistema,
                NumeroDocumentoLibre = pNumeroDocumentoLibre,
                FechaProcesoInicial = pFechaProcesoInicial,
                FechaProcesoFinal = pFechaProcesoFinal,
                TotalLibre = pTotalLibre
            };

            //Establecer la identidad
            nuevaLineaDocumentoLibre.GenerarNuevaIdentidad();

            this.DocumentosLibre.Add(nuevaLineaDocumentoLibre);

            return nuevaLineaDocumentoLibre;


        }




        public bool ValidarLimiteCredito(decimal montoVenta)
        {
            bool excedeLimiteCredito = false;

            if (montoVenta > (  this.ClienteLimitesCredito.Single().MontoLimite - 
                                this.ClienteLimitesCredito.Single().Deuda) + 
                                this.ClienteLimitesCredito.Single().MontoExcedente)
            {
                excedeLimiteCredito = true;
            }

            return excedeLimiteCredito;
        }
        

        public void ActualizarDeuda(decimal montoVenta)
        {
            this.ClienteLimitesCredito.Single().Deuda += montoVenta; 
        }        
    }
}