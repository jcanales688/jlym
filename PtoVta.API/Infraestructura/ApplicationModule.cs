using System;
using Autofac;
using PtoVta.Aplicacion.GestionUsuario;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Modulo;
using PtoVta.Infraestructura.Repositorios.Usuario;
using PtoVta.Infraestructura.Transversales.Adaptador;
using PtoVta.Infraestructura.Transversales.Autenticacion;
using PtoVta.Infraestructura.Transversales.Log;
using PtoVta.Infraestructura.TransversalesNET.Adaptador;
using PtoVta.Infraestructura.TransversalesNET.Autenticacion;
using PtoVta.Infraestructura.TransversalesNET.Log;

namespace PtoVta.API.Infraestructura
{
        public class ApplicationModule :Autofac.Module
        {
            public ApplicationModule()
            {

            }

            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<RepositorioVendedor>()
                    .As<IRepositorioVendedor>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<RepositorioModuloSistema>()
                    .As<IRepositorioModuloSistema>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<RepositorioUsuarioSistema>()
                .As<IRepositorioUsuarioSistema>()
                .InstancePerLifetimeScope();

                builder.RegisterType<ServicioDominioValidarUsuarioVendedor>()
                .As<IServicioDominioValidarUsuarioVendedor>()
                .InstancePerLifetimeScope();

                builder.RegisterType<ServicioDominioValidarUsuarioSistema>()
                .As<IServicioDominioValidarUsuarioSistema>()
                .InstancePerLifetimeScope();

               builder.RegisterType<ServicioAplicacionInicioSession>()
                .As<IServicioAplicacionInicioSession>()
                .InstancePerLifetimeScope();    

               builder.RegisterType<AutenticacionWindows>()
                .As<IAutenticacion>()
                .InstancePerLifetimeScope();    
                            

                LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

                var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
                TipoAdaptadorFactory.EstablecerActual(adaptadorFactory);  

                // builder.RegisterAssemblyTypes(typeof(CrearArticuloControladorComando).GetTypeInfo().Assembly)
                    //.AsClosedTypesOf(typeof(IIntegrationEventHandler<>))
                    ;

            }            
        }    
}