using System;
using Autofac;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.GestionClientes;
using PtoVta.Aplicacion.GestionColaborador;
using PtoVta.Aplicacion.GestionConfiguraciones;
using PtoVta.Aplicacion.GestionInventarios;
using PtoVta.Aplicacion.GestionParametros;
using PtoVta.Aplicacion.GestionPedidos;
using PtoVta.Aplicacion.GestionUsuario;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Configuraciones;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Modulo;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;
using PtoVta.Dominio.Agregados.Ventas;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Configuraciones;
using PtoVta.Infraestructura.Repositorios.Inventarios;
using PtoVta.Infraestructura.Repositorios.Modulo;
using PtoVta.Infraestructura.Repositorios.Parametros;
using PtoVta.Infraestructura.Repositorios.Usuario;
using PtoVta.Infraestructura.Repositorios.Ventas;
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
            public string QueriesConnectionString { get; }

            public ApplicationModule(string qconstr)
            {
                QueriesConnectionString = qconstr;

            }

            protected override void Load(ContainerBuilder builder)
            {
                builder.Register(c => new RepositorioVendedor(QueriesConnectionString))
                    .As<IRepositorioVendedor>()
                    .InstancePerLifetimeScope();

                builder.Register(c => new RepositorioModuloSistema(QueriesConnectionString))
                    .As<IRepositorioModuloSistema>()
                    .InstancePerLifetimeScope();

                builder.Register(c => new RepositorioUsuarioSistema(QueriesConnectionString))
                    .As<IRepositorioUsuarioSistema>()
                    .InstancePerLifetimeScope();       

                builder.Register(c => new RepositorioEstadoVendedor(QueriesConnectionString))
                    .As<IRepositorioEstadoVendedor>()
                    .InstancePerLifetimeScope();        

                builder.Register(c => new RepositorioArticulo(QueriesConnectionString))
                    .As<IRepositorioArticulo>()
                    .InstancePerLifetimeScope();       

                builder.Register(c => new RepositorioAlmacen(QueriesConnectionString))
                    .As<IRepositorioAlmacen>()
                    .InstancePerLifetimeScope();       

                builder.Register(c => new RepositorioCategoriaArticulo(QueriesConnectionString))
                    .As<IRepositorioCategoriaArticulo>()
                    .InstancePerLifetimeScope();  

                builder.Register(c => new RepositorioCliente(QueriesConnectionString))
                    .As<IRepositorioCliente>()
                    .InstancePerLifetimeScope();                      

                builder.Register(c => new RepositorioConfiguracionPuntoVenta(QueriesConnectionString))
                                    .As<IRepositorioConfiguracionPuntoVenta>()
                                    .InstancePerLifetimeScope();   

                builder.Register(c => new RepositorioConfiguracionFormatoTicket(QueriesConnectionString))
                    .As<IRepositorioConfiguracionFormatoTicket>()
                    .InstancePerLifetimeScope();    

                builder.Register(c => new RepositorioConfiguracionGeneral(QueriesConnectionString))
                    .As<IRepositorioConfiguracionGeneral>()
                    .InstancePerLifetimeScope();    

                builder.Register(c => new RepositorioConfiguracionInventario(QueriesConnectionString))
                    .As<IRepositorioConfiguracionInventario>()
                    .InstancePerLifetimeScope();                                                                                           
                                                                                                                                          
                builder.Register(c => new RepositorioConfiguracionVenta(QueriesConnectionString))
                    .As<IRepositorioConfiguracionVenta>()
                    .InstancePerLifetimeScope();  

                builder.Register(c => new RepositorioListaPrecioCliente(QueriesConnectionString))
                    .As<IRepositorioListaPrecioCliente>()
                    .InstancePerLifetimeScope(); 

                builder.Register(c => new RepositorioListaPrecioInventario(QueriesConnectionString))
                    .As<IRepositorioListaPrecioInventario>()
                    .InstancePerLifetimeScope(); 

                builder.Register(c => new RepositorioPedidoEESS(QueriesConnectionString))
                    .As<IRepositorioPedidoEESS>()
                    .InstancePerLifetimeScope(); 

                builder.Register(c => new RepositorioPedidoRetail(QueriesConnectionString))
                    .As<IRepositorioPedidoRetail>()
                    .InstancePerLifetimeScope(); 

                builder.Register(c => new RepositorioMoneda(QueriesConnectionString))
                    .As<IRepositorioMoneda>()
                    .InstancePerLifetimeScope(); 

                builder.Register(c => new RepositorioTarjeta(QueriesConnectionString))
                    .As<IRepositorioTarjeta>()
                    .InstancePerLifetimeScope();                                                                                 
                                                        

                // builder.RegisterType<RepositorioVendedor>()
                //     .As<IRepositorioVendedor>()
                //     .InstancePerLifetimeScope();

                // builder.RegisterType<RepositorioModuloSistema>()
                //     .As<IRepositorioModuloSistema>()
                //     .InstancePerLifetimeScope();

                // builder.RegisterType<RepositorioUsuarioSistema>()
                //     .As<IRepositorioUsuarioSistema>()
                //     .InstancePerLifetimeScope();

                builder.RegisterType<ConfiguracionGlobalUnificado>()
                    .As<IConfiguracionGlobalUnificado>()
                    .InstancePerLifetimeScope();
                                    
                builder.RegisterType<ServicioDominioValidarUsuarioVendedor>()
                    .As<IServicioDominioValidarUsuarioVendedor>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<ServicioDominioValidarUsuarioSistema>()
                    .As<IServicioDominioValidarUsuarioSistema>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<ServicioDominioListaPrecios>()
                    .As<IServicioDominioListaPrecios>()
                    .InstancePerLifetimeScope();                    

               builder.RegisterType<ServicioAplicacionInicioSession>()
                    .As<IServicioAplicacionInicioSession>()
                    .InstancePerLifetimeScope();    

               builder.RegisterType<ServicioAplicacionVendedor>()
                    .As<IServicioAplicacionVendedor>()
                    .InstancePerLifetimeScope(); 

               builder.RegisterType<ServicioAplicacionArticulo>()
                    .As<IServicioAplicacionArticulo>()
                    .InstancePerLifetimeScope(); 

               builder.RegisterType<ServicioAplicacionParametros>()
                    .As<IServicioAplicacionParametros>()
                    .InstancePerLifetimeScope();                                         

               builder.RegisterType<ServicioAplicacionCliente>()
                    .As<IServicioAplicacionCliente>()
                    .InstancePerLifetimeScope();

               builder.RegisterType<ServicioAplicacionConfiguracion>()
                    .As<IServicioAplicacionConfiguracion>()
                    .InstancePerLifetimeScope();    

               builder.RegisterType<ServicioAplicacionPedidos>()
                    .As<IServicioAplicacionPedidos>()
                    .InstancePerLifetimeScope();                                

                                                                                                            
               builder.RegisterType<AutenticacionWindows>()
                    .As<IAutenticacion>()
                    .InstancePerLifetimeScope();    
                            

                LogFactory.EstablecerActual(new LogTrazaOrigenFactory());

                var adaptadorFactory = new AutomapperTipoAdaptadorFactory();
                TipoAdaptadorFactory.EstablecerActual(adaptadorFactory);  

                // builder.RegisterAssemblyTypes(typeof(CrearArticuloControladorComando).GetTypeInfo().Assembly)
                    //.AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

            }            
        }    
}