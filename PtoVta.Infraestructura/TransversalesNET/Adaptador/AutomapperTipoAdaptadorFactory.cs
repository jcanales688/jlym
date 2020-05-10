using System;
using PtoVta.Infraestructura.Transversales.Adaptador;

namespace PtoVta.Infraestructura.TransversalesNET.Adaptador
{
    public class AutomapperTipoAdaptadorFactory
        : ITipoAdaptadorFactory
    {
        //Crear un nuevo tipo de adaptadores AutoMapper
        public AutomapperTipoAdaptadorFactory()
        {
            //escanear todas las assemblies buscando Perfil AutoMapper 
            var profiles = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .SelectMany(a => a.GetTypes())
                                    .Where(t => t.BaseType == typeof(Profile));

            Mapper.Initialize(cfg =>
            {
                foreach (var item in profiles)
                {
                    if (item.FullName != "AutoMapper.SelfProfiler`2")
                        cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });
            

            //var perfiles = AppDomain.CurrentDomain
            //    .GetAssemblies()
            //    .SelectMany(a => a.GetTypes())
            //    .Where(t => t.BaseType == typeof(Profile));

            //Mapper.Initialize(cfg =>
            //    {
            //        foreach (var item in perfiles)
            //        {
            //            if (item.FullName != "AutoMapper.SelfProfiler`2")
            //                cfg.AddProfile(Activator.CreateInstance(item) as Profile);
            //        }
            //    });
        }

        public ITipoAdaptador Crear()
        {
            return new AutomapperTipoAdaptador();
        }        
    }
}
