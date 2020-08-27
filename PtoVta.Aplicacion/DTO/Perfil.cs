using System;
using AutoMapper;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Aplicacion.DTO.Configuraciones;
using PtoVta.Aplicacion.DTO.Inventarios;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Aplicacion.DTO.Usuario;
using PtoVta.Aplicacion.DTO.Ventas;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.CuentasPorCobrar;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.Agregados.Ventas;

namespace PtoVta.Aplicacion.DTO
{
    class Perfil : Profile
    {

        protected override void Configure()
        {
            //...............Root
            Mapper.CreateMap<DerechoAccesoUsuario, DerechoAccesoUsuarioDTO>();
            Mapper.CreateMap<ModuloSistema, ModuloSistemaDTO>();
            Mapper.CreateMap<VentanaUsuario, VentanaUsuarioDTO>();
            Mapper.CreateMap<UsuarioSistema, UsuarioSistemaDTO>();                
            Mapper.CreateMap<Vendedor, VendedorDTO>();    
            Mapper.CreateMap<ArticuloAlterno, ArticuloAlternoDTO>();                
            Mapper.CreateMap<ArticuloDetalle, ArticuloDetalleDTO>();    
            Mapper.CreateMap<Articulo, ArticuloDTO>();   
            Mapper.CreateMap<CategoriaArticulo, CategoriaArticuloDTO>();    
            Mapper.CreateMap<SubCategoriaArticulo, SubCategoriaArticuloDTO>();          
            Mapper.CreateMap<Cliente, ClienteDTO>();          
            Mapper.CreateMap<CondicionPago, CondicionPagoDTO>();          
            Mapper.CreateMap<DiaDePago, DiaDePagoDTO>();          
            Mapper.CreateMap<ClientePlaca, ClientePlacaDTO>();          
            Mapper.CreateMap<DocumentoLibre, DocumentoLibreDTO>();          
            Mapper.CreateMap<ClienteLimiteCredito , ClienteLimiteCreditoDTO>();    
            Mapper.CreateMap<Cliente, ResultadoClienteGrabadoDTO>();  
            Mapper.CreateMap<Cliente, ClienteListadoDTO>();

            Mapper.CreateMap<ConfiguracionPuntoVenta, ConfiguracionPuntoVentaDTO>();

            Mapper.CreateMap<PedidoEESS, PedidoEESSDTO>();
            Mapper.CreateMap<PedidoEESSDetalle, PedidoEESSDetalleDTO>();
            Mapper.CreateMap<PedidoEESSConVale, PedidoEESSConValeDTO>();
            Mapper.CreateMap<PedidoEESS, ResultadoPedidoEESSGrabadoDTO>();
            Mapper.CreateMap<PedidoEESS, PedidoEESSListadoDTO>();

            Mapper.CreateMap<PedidoRetail, PedidoRetailDTO>();            
            Mapper.CreateMap<PedidoRetailDetalle, PedidoRetailDetalleDTO>();            
            Mapper.CreateMap<PedidoRetailConVale, PedidoRetailConValeDTO>();            
            Mapper.CreateMap<PedidoRetailConTarjeta, PedidoRetailConTarjetaDTO>();  
            Mapper.CreateMap<PedidoRetail, ResultadoPedidoRetailGrabadoDTO>();
            Mapper.CreateMap<PedidoRetail, PedidoRetailListadoDTO>();

            Mapper.CreateMap<Venta, VentaDTO>();
            Mapper.CreateMap<TipoPago, TipoPagoDTO>();
            Mapper.CreateMap<VentaDetalle, VentaDetalleDTO>();
            Mapper.CreateMap<VentaConTarjeta, VentaConTarjetaDTO>();
            Mapper.CreateMap<VentaConVale, VentaConValeDTO>();
            Mapper.CreateMap<DocumentoAnticipado, DocumentoAnticipadoDTO>();
            Mapper.CreateMap<CuentaPorCobrar, CuentaPorCobrarDTO>();
            Mapper.CreateMap<Venta, ResultadoVentaGrabadaDTO>();
            Mapper.CreateMap<Venta, VentaListadoDTO>();
        }
    }
}