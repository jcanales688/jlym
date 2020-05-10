using System;
using AutoMapper;
using PtoVta.Infraestructura.Transversales.Adaptador;

namespace PtoVta.Infraestructura.TransversalesNET.Adaptador
{
    public class AutomapperTipoAdaptador : ITipoAdaptador
    {
        public TDestino Adapt<TOrigen, TDestino>(TOrigen origen)
            where TOrigen : class
            where TDestino : class, new()
        {
            return Mapper.Map<TOrigen, TDestino>(origen);
        }

        public TDestino Adapt<TDestino>(object origen) where TDestino : class, new()
        {
            return Mapper.Map<TDestino>(origen);
        }        
    }
}
