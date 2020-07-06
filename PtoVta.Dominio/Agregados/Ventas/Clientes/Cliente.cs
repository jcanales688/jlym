using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

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
        public int EsAfecto { get; set; }
        public int ControlarSaldoDisponible { get; set; }

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
        public string CodigoPais { get; private set; }
        public string CodigoDepartamento { get; private set; }
        public string CodigoDistrito { get; private set; }                
    

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
        public virtual Pais Pais { get; private set; }
        public virtual Departamento Departamento { get; private set; }
        // public virtual Provincia Provincia { get; private set; }
        public virtual Distrito Distrito { get; private set; }            

        
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




        //Moneda
        public void EstablecerMonedaDeCliente(Moneda pMoneda)
        {
            if (pMoneda == null)
            {
                throw new ArgumentException(Mensajes.excepcion_MonedaDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoMoneda = pMoneda.CodigoMoneda;
            this.Moneda = pMoneda;
        }

        public void EstablecerReferenciaMonedaDeCliente(string pCodigoMoneda)
        {
            if (!string.IsNullOrEmpty(pCodigoMoneda))
            {
                this.CodigoMoneda = pCodigoMoneda;
                this.Moneda = null;
            }
        }


        //ClaseTipoCambio
        public void EstablecerClaseTipoCambioDeCliente(ClaseTipoCambio pClaseTipoCambio)
        {
            if (pClaseTipoCambio == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ClaseTipoCambioDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoClaseTipoCambio = pClaseTipoCambio.CodigoClaseTipoCambio;
            this.ClaseTipoCambio = pClaseTipoCambio;
        }

        public void EstablecerReferenciaClaseTipoCambioDeCliente(string pCodigoClaseTipoCambio)
        {
            if (!string.IsNullOrEmpty(pCodigoClaseTipoCambio))
            {
                this.CodigoClaseTipoCambio = pCodigoClaseTipoCambio;
                this.ClaseTipoCambio = null;
            }
        }


      //TipoCliente
        public void EstablecerTipoClienteDeCliente(TipoCliente pTipoCliente)
        {
            if (pTipoCliente == null)
            {
                throw new ArgumentException(Mensajes.excepcion_TipoClienteDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoTipoCliente = pTipoCliente.CodigoTipoCliente;
            this.TipoCliente = pTipoCliente;
        }

        public void EstablecerReferenciaTipoClienteDeCliente(string pCodigoTipoCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoTipoCliente))
            {
                this.CodigoTipoCliente = pCodigoTipoCliente;
                this.TipoCliente = null;
            }
        }

        //ZonaCliente
        public void EstablecerZonaClienteDeCliente(ZonaCliente pZonaCliente)
        {
            if (pZonaCliente == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ZonaClienteDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoZonaCliente = pZonaCliente.CodigoZonaCliente;
            this.ZonaCliente = pZonaCliente;
        }

        public void EstablecerReferenciaZonaClienteDeCliente(string pCodigoZonaCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoZonaCliente))
            {
                this.CodigoZonaCliente = pCodigoZonaCliente;
                this.ZonaCliente = null;
            }
        }


      //DiaDePago
        public void EstablecerDiaDePagoDeCliente(DiaDePago pDiaDePago)
        {
            if (pDiaDePago == null)
            {
                throw new ArgumentException(Mensajes.excepcion_DiaDePagoDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoDiaDePago = pDiaDePago.CodigoDiaDePago;
            this.DiaDePago = pDiaDePago;
        }

        public void EstablecerReferenciaDiaDePagoDeCliente(string pCodigoDiaDePago)
        {
            if (!string.IsNullOrEmpty(pCodigoDiaDePago))
            {
                this.CodigoDiaDePago = pCodigoDiaDePago;
                this.DiaDePago = null;
            }
        }

        

        //Vendedor
        public void EstablecerVendedorDeCliente(Vendedor pVendedor)
        {
            if (pVendedor == null)
            {
                throw new ArgumentException(Mensajes.excepcion_VendedorDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoVendedor = pVendedor.CodigoVendedor;
            this.Vendedor = pVendedor;
        }

        public void EstablecerReferenciaVendedorDeCliente(string pCodigoVendedor)
        {
            if (!string.IsNullOrEmpty(pCodigoVendedor))
            {
                this.CodigoVendedor = pCodigoVendedor;
                this.Vendedor = null;
            }
        }

        //ImpuestoIgv
        public void EstablecerImpuestoIgvDeCliente(Impuesto pImpuestoIgv)
        {
            if (pImpuestoIgv == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ImpuestoIgvDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoImpuestoIgv = pImpuestoIgv.CodigoImpuesto;
            this.ImpuestoIgv = pImpuestoIgv;
        }

        public void EstablecerReferenciaImpuestoIgvDeCliente(string pCodigoImpuestoIgv)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIgv))
            {

                this.CodigoImpuestoIgv = pCodigoImpuestoIgv;
                this.ImpuestoIgv = null;
            }
        }


        //ImpuestoIsc
        public void EstablecerImpuestoIscDeCliente(Impuesto pImpuestoIsc)
        {
            if (pImpuestoIsc == null)
            {
                throw new ArgumentException(Mensajes.excepcion_ImpuestoIscDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoImpuestoIsc = pImpuestoIsc.CodigoImpuesto;
            this.ImpuestoIsc = pImpuestoIsc;
        }

        public void EstablecerReferenciaImpuestoIscDeCliente(string pCodigoImpuestoIsc)
        {
            if (!string.IsNullOrEmpty(pCodigoImpuestoIsc))
            {
                this.CodigoImpuestoIsc = pCodigoImpuestoIsc;
                this.ImpuestoIsc = null;
            }
        }


        //CondicionPagoDocumento
        public void EstablecerCondicionPagoDocumentoGeneradoDeCliente(CondicionPago pCondicionPagoDocumentoGenerado)
        {
            if (pCondicionPagoDocumentoGenerado == null)
            {
                throw new ArgumentException(Mensajes.excepcion_CondicionPagoDocumentoGeneradoDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoCondicionPagoDocumentoGenerado = pCondicionPagoDocumentoGenerado.CodigoCondicionPago;
            this.CondicionPagoDocumentoGenerado = pCondicionPagoDocumentoGenerado;
        }

        public void EstablecerReferenciaCondicionPagoDocumentoGeneradoDeCliente(string pCodigoCondicionPagoDocumentoGenerado)
        {
            if (!string.IsNullOrEmpty(pCodigoCondicionPagoDocumentoGenerado))
            {
                this.CodigoCondicionPagoDocumentoGenerado = pCodigoCondicionPagoDocumentoGenerado;
                this.CondicionPagoDocumentoGenerado = null;
            }
        }

        //CondicionPagoTicket
        public void EstablecerCondicionPagoTicketDeCliente(CondicionPago pCondicionPagoTicket)
        {
            if (pCondicionPagoTicket == null)
            {
                throw new ArgumentException(Mensajes.excepcion_CondicionPagoTicketDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoCondicionPagoTicket = pCondicionPagoTicket.CodigoCondicionPago;
            this.CondicionPagoTicket = pCondicionPagoTicket;
        }

        public void EstablecerReferenciaCondicionPagoTicketDeCliente(string pCodigoCondicionPagoTicket)
        {
            if (!string.IsNullOrEmpty(pCodigoCondicionPagoTicket))
            {
                this.CodigoCondicionPagoTicket = pCodigoCondicionPagoTicket;
                this.CondicionPagoTicket = null;
            }
        }


        //EstadoDeCliente
        public void EstablecerEstadoDeClienteDeCliente(EstadoDeCliente pEstadoDeCliente)
        {
            if (pEstadoDeCliente == null)
            {
                throw new ArgumentException(Mensajes.excepcion_EstadoDeClienteDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoEstadoDeCliente = pEstadoDeCliente.CodigoEstadoDeCliente;
            this.EstadoDeCliente = pEstadoDeCliente;
        }

        public void EstablecerReferenciaEstadoDeClienteDeCliente(string pCodigoEstadoDeCliente)
        {
            if (!string.IsNullOrEmpty(pCodigoEstadoDeCliente))
            {
                this.CodigoEstadoDeCliente= pCodigoEstadoDeCliente;
                this.EstadoDeCliente = null;
            }
        }

        //UsuarioSistema
        public void EstablecerUsuarioSistemaDeCliente(UsuarioSistema pUsuarioSistema)
        {
            if (pUsuarioSistema == null)
            {
                throw new ArgumentException(Mensajes.excepcion_UsuarioSistemaDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoUsuarioDeSistema = pUsuarioSistema.CodigoUsuarioDeSistema;
            this.UsuarioSistema = pUsuarioSistema;
        }

        public void EstablecerReferenciaUsuarioSistemaDeCliente(string pCodigoUsuarioDeSistema)
        {
            if (!string.IsNullOrEmpty(pCodigoUsuarioDeSistema))
            {
                this.CodigoUsuarioDeSistema = pCodigoUsuarioDeSistema;
                this.UsuarioSistema = null;
            }
        }


        //Pais
        public void EstablecerPaisDeCliente(Pais pPais)
        {
            if (pPais == null)
            {
                throw new ArgumentException(Mensajes.excepcion_PaisDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoPais = pPais.CodigoPais;
            this.Pais = pPais;
        }

        public void EstablecerReferenciaPaisDeCliente(string pCodigoPais)
        {
            if (!string.IsNullOrEmpty(pCodigoPais))
            {
                this.CodigoPais = pCodigoPais;
                this.Pais = null;
            }
        }


        //Departamento
        public void EstablecerDepartamentoDeCliente(Departamento pDepartamento)
        {
            if (pDepartamento == null)
            {
                throw new ArgumentException(Mensajes.excepcion_DepartamentoDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoDepartamento = pDepartamento.CodigoDepartamento;
            this.Departamento = pDepartamento;
        }

        public void EstablecerReferenciaDepartamentoDeCliente(string pCodigoDepartamento)
        {
            if (!string.IsNullOrEmpty(pCodigoDepartamento))
            {
                this.CodigoDepartamento = pCodigoDepartamento;
                this.Departamento = null;
            }
        }


        //Distrito
        public void EstablecerDistritoDeCliente(Distrito pDistrito)
        {
            if (pDistrito == null)
            {
                throw new ArgumentException(Mensajes.excepcion_DistritoDeClienteEnEstadoNuloOTransitorio);
            }

            this.CodigoDistrito = pDistrito.CodigoDistrito;
            this.Distrito = pDistrito;
        }

        public void EstablecerReferenciaDistritoDeCliente(string pCodigoDistrito)
        {
            if (!string.IsNullOrEmpty(pCodigoDistrito))
            {
                this.CodigoDistrito = pCodigoDistrito;
                this.Distrito = null;
            }
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