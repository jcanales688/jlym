using System;
using AutoMapper;
using PtoVta.Aplicacion.DTO.Modulo;
using PtoVta.Aplicacion.DTO.Usuario;
using PtoVta.Dominio.Agregados.Modulo;
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
        }
    }
}