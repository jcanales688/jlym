using System;
using AutoMapper;
using PtoVta.Aplicacion.DTO.Colaborador;
using PtoVta.Aplicacion.DTO.Inventarios;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.DTO.Parametros;
using PtoVta.Aplicacion.DTO.Usuario;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;

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
        }
    }
}