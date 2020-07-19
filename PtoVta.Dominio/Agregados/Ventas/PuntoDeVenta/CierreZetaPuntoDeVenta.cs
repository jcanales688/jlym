using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Ventas
{
  public class CierreZetaPuntoDeVenta:Entidad
  {
        bool _EsHabilitado;

        HashSet<CierreZetaResumenArticulo> _lineasCierreZetaResumenArticulo;
        HashSet<CierreZetaResumenCara> _lineasCierreZetaResumenCara;
        HashSet<CierreZetaResumenCategoria> _lineasCierreZetaResumenCategoria;
        HashSet<CierreZetaResumenVendedor> _lineasCierreZetaResumenVendedor;


        public string CodigoCierreZetaPuntoDeVenta { get; set; } 
        //prop nativa
        public string Cabecera01 { get; set; }
        public string Cabecera02 { get; set; }
        public string Cabecera03 { get; set; }
        public string Cabecera04 { get; set; }
        public string Cabecera05 { get; set; }
        public string Cabecera06 { get; set; }
        public string NumeroSerieMaquinaRegistradora { get; set; }
        public decimal MontoTipoDeCambio { get; set; }
        public decimal NumeroTransaccion { get; set; }
        public decimal TicketBoletaInicial { get; set; }
        public decimal TicketBoletaFinal { get; set; }
        public decimal TicketFacturaInicial { get; set; }
        public decimal TicketFacturaFinal { get; set; }
        public decimal ValorVentaNacional { get; set; }
        public decimal NoAfectoNacional { get; set; }
        public decimal IgvNacional { get; set; }
        public decimal TotalNacional { get; set; }
        public int TicketsAnulados { get; set; }
        public decimal MontoTicketsAnuladosNacional { get; set; }
        public decimal TotalTicketBoletaNacional { get; set; }
        public decimal TotalTicketFacturaNacional { get; set; }
        public decimal TotalBoletaFacturaBase { get; set; }
        public decimal TotalEfectivoBase { get; set; }
        public decimal TotalEfectivoExtranjera { get; set; }
        public decimal TotalVueltoBase { get; set; }
        public decimal TotalVueltoExtranjera { get; set; }
        public decimal TotalNetoBase { get; set; }
        public decimal TotalNetoExtranjera { get; set; }
        public decimal TotalTarjetaBase { get; set; }
        public decimal TotalTarjetaExtranjera { get; set; }
        public decimal TotalCreditoBase { get; set; }
        public decimal TotalCreditoExtranjera { get; set; }


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


        //Propiedades nativas
        public DateTime FechaProcesoVentas { get; set; }                //prop param busqueda
        public string CodigoPuntoDeVenta { get; set; }             //prop param busqueda
        // public Guid ConfiguracionPuntoVentaId { get; set; }             //prop param busqueda
        public Nullable<DateTime> FechaCierreZeta { get; set; }
        public decimal TotalCierreZeta { get; set; }                    //prop nativa
        public int NumeroZetaPtoVta { get; set; }                     //prop nativa


        //prop general
        public int CodigoTipoNegocio { get; set; }
        public string DescripcionTipoNegocio { get; set; }



        public CierreZetaResumenArticulo AgregarNuevoResumenPorArticulo(string pCodigoArticulo, string pDescripcion,
                                        decimal pTotalNacional)
        {
            if (String.IsNullOrWhiteSpace(pCodigoArticulo)
                ||
                String.IsNullOrEmpty(pDescripcion)
                ||
                pTotalNacional <= 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaCierreZResumenPorArticulo);



            //Crear nuevo detalles venta
            var nuevaLineaResumenPorArticulo = new CierreZetaResumenArticulo()
            {
                CodigoCierreZetaPuntoDeVenta = this.CodigoCierreZetaPuntoDeVenta,

                CodigoArticulo = pCodigoArticulo,
                Descripcion = pDescripcion,
                TotalNacional = pTotalNacional
            };

            //Establecer la identidad
            nuevaLineaResumenPorArticulo.GenerarNuevaIdentidad();

            this.CierreZetaResumenPorArticulos.Add(nuevaLineaResumenPorArticulo);

            return nuevaLineaResumenPorArticulo;
        }


        public CierreZetaResumenCara AgregarNuevoResumenPorCara(string pCodigoCara, string pDescripcionCara,
                        string pCodigoArticulo,string pDescripcionArticulo, decimal pTotalNacional)
        {
            if (String.IsNullOrWhiteSpace(pCodigoCara)
                ||
                String.IsNullOrEmpty(pDescripcionCara)
                ||
                String.IsNullOrEmpty(pCodigoArticulo)
                ||
                String.IsNullOrEmpty(pDescripcionArticulo)
                ||
                pTotalNacional <= 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaCierreZResumenPorCara);



            //Crear nuevo detalles venta
            var nuevaLineaResumenPorCara = new CierreZetaResumenCara()
            {
                CodigoCierreZetaPuntoDeVenta = this.CodigoCierreZetaPuntoDeVenta,

                CodigoCara = pCodigoCara,
                DescripcionCara = pDescripcionCara,
                CodigoArticulo = pCodigoArticulo,
                DescripcionArticulo = pDescripcionArticulo,
                TotalNacional = pTotalNacional
            };

            //Establecer la identidad
            nuevaLineaResumenPorCara.GenerarNuevaIdentidad();

            this.CierreZetaResumenPorCaras.Add(nuevaLineaResumenPorCara);

            return nuevaLineaResumenPorCara;
        }


        public CierreZetaResumenCategoria AgregarNuevoResumenPorCategoria(string pCodigoCategoria, 
                                          string pDescripcion,decimal pTotalNacional)
        {
            if (String.IsNullOrWhiteSpace(pCodigoCategoria)
                ||
                String.IsNullOrEmpty(pDescripcion)
                ||
                pTotalNacional < 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaCierreZResumenPorCategoria);


            //Crear nuevo detalles venta
            var nuevaLineaResumenPorCategoria = new CierreZetaResumenCategoria()
            {
                CodigoCierreZetaPuntoDeVenta = this.CodigoCierreZetaPuntoDeVenta,

                CodigoCategoria = pCodigoCategoria,
                Descripcion = pDescripcion,
                TotalNacional = pTotalNacional
            };

            //Establecer la identidad
            nuevaLineaResumenPorCategoria.GenerarNuevaIdentidad();

            this.CierreZetaResumenPorCategorias.Add(nuevaLineaResumenPorCategoria);

            return nuevaLineaResumenPorCategoria;
        }


        public CierreZetaResumenVendedor AgregarNuevoResumenPorVendedor(string pCodigoVendedor, 
                                                  string pNombres,decimal pTotalNacional)
        {
            if (String.IsNullOrWhiteSpace(pCodigoVendedor)
                ||
                String.IsNullOrEmpty(pNombres)
                ||
                pTotalNacional < 0
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaCierreZResumenPorVendedor);


            //Crear nuevo detalles venta
            var nuevaLineaResumenPorVendedor = new CierreZetaResumenVendedor()
            {
                CodigoCierreZetaPuntoDeVenta = this.CodigoCierreZetaPuntoDeVenta,

                CodigoVendedor = pCodigoVendedor,
                Nombres = pNombres,
                TotalNacional = pTotalNacional
            };

            //Establecer la identidad
            nuevaLineaResumenPorVendedor.GenerarNuevaIdentidad();

            this.CierreZetaResumenPorVendedores.Add(nuevaLineaResumenPorVendedor);

            return nuevaLineaResumenPorVendedor;
        }



        public CierreZetaResumenArticulo TemporalAgregarNuevoResumenPorArticulo(string pCodigoArticulo, 
                                                      string pDescripcion,decimal pTotalNacional)
        {
            //Crear nuevo detalles venta
            var nuevaLineaResumenPorArticulo = new CierreZetaResumenArticulo()
            {
                CodigoCierreZetaPuntoDeVenta = this.CodigoCierreZetaPuntoDeVenta,

                CodigoArticulo = pCodigoArticulo,
                Descripcion = pDescripcion,
                TotalNacional = pTotalNacional
            };


            this.CierreZetaResumenPorArticulos.Add(nuevaLineaResumenPorArticulo);

            return nuevaLineaResumenPorArticulo;
        }


        public CierreZetaResumenCara TemporalAgregarNuevoResumenPorCara(string pCodigoCara, 
                    string pDescripcionCara,string pDescripcionArticulo,
                    decimal pTotalNacional)
        {

            //Crear nuevo detalles venta
            var nuevaLineaResumenPorCara = new CierreZetaResumenCara()
            {
                CodigoCierreZetaPuntoDeVenta = this.CodigoCierreZetaPuntoDeVenta,

                CodigoCara = pCodigoCara,
                DescripcionCara = pDescripcionCara,
                DescripcionArticulo = pDescripcionArticulo,
                TotalNacional = pTotalNacional
            };

            this.CierreZetaResumenPorCaras.Add(nuevaLineaResumenPorCara);

            return nuevaLineaResumenPorCara;
        }


        public CierreZetaResumenCategoria TemporalAgregarNuevoResumenPorCategoria(string pCodigoCategoria, 
                                      string pDescripcion,decimal pTotalNacional)
        {

            //Crear nuevo detalles venta
            var nuevaLineaResumenPorCategoria = new CierreZetaResumenCategoria()
            {
                CodigoCierreZetaPuntoDeVenta = this.CodigoCierreZetaPuntoDeVenta,

                CodigoCategoria = pCodigoCategoria,
                Descripcion = pDescripcion,
                TotalNacional = pTotalNacional
            };


            this.CierreZetaResumenPorCategorias.Add(nuevaLineaResumenPorCategoria);

            return nuevaLineaResumenPorCategoria;
        }


        public CierreZetaResumenVendedor TemporalAgregarNuevoResumenPorVendedor(string pCodigoVendedor, 
                                                    string pDescripcion,decimal pTotalNacional)
        {

            //Crear nuevo detalles venta
            var nuevaLineaResumenPorVendedor = new CierreZetaResumenVendedor()
            {
                CodigoCierreZetaPuntoDeVenta = this.CodigoCierreZetaPuntoDeVenta,

                CodigoVendedor = pCodigoVendedor,
                Nombres = pDescripcion,
                TotalNacional = pTotalNacional
            };


            this.CierreZetaResumenPorVendedores.Add(nuevaLineaResumenPorVendedor);

            return nuevaLineaResumenPorVendedor;
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


        public virtual ICollection<CierreZetaResumenArticulo> CierreZetaResumenPorArticulos
        {
            get
            {
                if (_lineasCierreZetaResumenArticulo == null)
                    _lineasCierreZetaResumenArticulo = new HashSet<CierreZetaResumenArticulo>();

                return _lineasCierreZetaResumenArticulo;
            }

            set
            {
                _lineasCierreZetaResumenArticulo = new HashSet<CierreZetaResumenArticulo>(value);
            }
        }

        public virtual ICollection<CierreZetaResumenCara> CierreZetaResumenPorCaras
        {
            get
            {
                if (_lineasCierreZetaResumenCara == null)
                    _lineasCierreZetaResumenCara = new HashSet<CierreZetaResumenCara>();

                return _lineasCierreZetaResumenCara;
            }

            set
            {
                _lineasCierreZetaResumenCara = new HashSet<CierreZetaResumenCara>(value);
            }
        }

        public virtual ICollection<CierreZetaResumenCategoria> CierreZetaResumenPorCategorias
        {
            get
            {
                if (_lineasCierreZetaResumenCategoria == null)
                    _lineasCierreZetaResumenCategoria = new HashSet<CierreZetaResumenCategoria>();

                return _lineasCierreZetaResumenCategoria;
            }

            set
            {
                _lineasCierreZetaResumenCategoria = new HashSet<CierreZetaResumenCategoria>(value);
            }
        }

        public virtual ICollection<CierreZetaResumenVendedor> CierreZetaResumenPorVendedores
        {
            get
            {
                if (_lineasCierreZetaResumenVendedor == null)
                    _lineasCierreZetaResumenVendedor = new HashSet<CierreZetaResumenVendedor>();

                return _lineasCierreZetaResumenVendedor;
            }

            set
            {
                _lineasCierreZetaResumenVendedor = new HashSet<CierreZetaResumenVendedor>(value);
            }
        }      
  }
}