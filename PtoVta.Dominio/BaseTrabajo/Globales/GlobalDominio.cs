using System;

namespace PtoVta.Dominio.BaseTrabajo.Globales
{
    public class GlobalDominio
    {
        public struct EnumMoneda
        {
            public const string CodigoMonedaBase = "PEN";
            public const string CodigoMonedaExtranjera = "USD";
        }

        public struct EnumEstadoDocumento
        {
            public const string CodigoEstadoDocumentoPorDefecto = "OK";    
            public const string CodigoEstadoDocumentoAnulado = "AN";    
            public const string CodigoEstadoDocumentoPendiente = "PE";    
        }

        public struct EnumTipoPago
        {
            public const string TipoPagoPorDefecto = "01";    
        }

        public struct EnumTipoNegocio
        {
            public const string TipoNegocioDesactivado = "0";    
            public const string TipoNegocioEESS = "1";    
            public const string TipoNegocioRetail = "2";                
            public const string TipoNegocioOficina = "3";                
        }
        
        public struct EnumModoTipoVenta
        {
            public const string ModoTipoVentaManual = "M";
            public const string ModoTipoVentaAutomatico = "A";
        }

        public struct EmunCondicionPago
        {
            public const string CondicionPagoPagoContraentrega = "00";
        }

        public struct EnumGenerales
        {
            public const int AnchoTicket = 40;
        }

        public struct Mensajes
        {
            public const string advertencia_AdvertenciaVentaOTipoDocumentoInvalido = "Advertencia Venta O Tipo Documento Invalido";
            public const string advertencia_ConfiguracionDeAplicacionIncompleta = "Configuracion De Aplicacion Incompleta";
            public const string advertencia_ConfiguracionPuntoVentaAsociadoAVentaNoExiste = "Configuracion Punto Venta Asociado A Venta No Existe";
            public const string advertencia_AlmacenAsociadoAVentaNoExiste = "Almacen Asociado A Venta No Existe";
            public const string advertencia_TipoNegocioAsociadoAVentaNoExiste = "Tipo Negocio Asociado A Venta No Existe";
            public const string advertencia_UsuarioSistemaAsociadoAVentaNoExiste = "Usuario Sistema Asociado A Venta No Existe";
            public const string advertencia_TipoDocumentoAsociadoAVentaNoExiste = "Tipo Documento Asociado A Venta No Existe";
            public const string advertencia_EstadoDocumentoAsociadoAVentaNoExiste = "Estado Documento Asociado A Venta No Existe";
            public const string advertencia_CorrelativoDocumentoYaFueGenerado = "Correlativo Documento Ya Fue Generado";
            public const string advertencia_ClienteAsociadoAVentaNoExiste = "Cliente Asociado A Venta No Existe";
            public const string advertencia_ClaseTipoDeCambioAsociadoAVentaNoExiste = "Clase Tipo De Cambio Asociado A Venta No Existe";
            public const string advertencia_VendedorAsociadoAVentaNoExiste= "Vendedor Asociado A Venta No Existe";
            public const string advertencia_MonedaAsociadoAVentaNoExiste =  "Moneda Asociado A Venta No Existe";
            public const string advertencia_TipoDePagoAsociadoAVentaNoExiste =  "Tipo De Pago Asociado A Venta No Existe";
            public const string advertencia_TipoMovAlmacenVentasAsociadoAVentaNoExiste=  "Tipo Movimiento Almacen Ventas Asociado A Venta No Existe";
            public const string advertencia_EstadoDeDocumentoCuentasXCobrarAsociadoAVentaNoExiste=  "Estado De Documento Cuentas X Cobrar Asociado A Venta No Existe";
            public const string advertencia_FalloCreacionNuevaVentaEnVenta =  "Fallo Creacion Nueva Venta En Venta";
            public const string advertencia_ExitosaCreacionNuevaVentaEnVenta =  "Creacion Nueva Venta En Venta se realizo exitosamente";            
            public const string advertencia_CondicionPagoPorVentasAsociadoAVentaNoExiste =  "Condicion Pago Por Ventas Asociado A Venta No Existe";
            public const string advertencia_ClienteExcedeLimiteCredito =  "Cliente Excede Limite Credito";
            public const string advertencia_ArticuloAsociadoAVentaDetalleNoExiste =  "Articulo Asociado A Venta Detalle No Existe";
            public const string advertencia_MonedaAsociadoAPagoVentaConTarjetaNoExiste = "Moneda Asociado A Pago Venta Con Tarjeta No Existe";
            public const string advertencia_TarjetaAsociadoAPagoVentaConTarjetaNoExiste= "Tarjeta Asociado A Pago Venta Con Tarjeta No Existe";
            public const string advertencia_NoSeObtuvoPagoDeVentaAdelantada= "No Se Obtuvo Pago De Venta Adelantada";
            public const string advertencia_NoSeObtuvoVentaConNumeroDeDocumentoBuscado= "No Se ObtuvoVenta Con Numero De Documento Buscado";
            public const string advertencia_NoSeObtuvoResultadoDeConsultaTipoDocumento= "No Se Obtuvo Resultado De Consulta TipoDocumento";
            public const string advertencia_NoSeObtuvoResultadoDeConsultaArticuloAPersistir= "No Se Obtuvo Resultado De Consulta Articulo A Persistir";
            public const string advertencia_NoSeObtuvoParametroArticulosStockActualizado= "No Se Obtuvo Parametro Articulos Stock Actualizado";
            public const string advertencia_NoSeObtuvoParametroMovimientosDeAlmacen= "No Se Obtuvo Parametro Movimientos De Almacen";
            public const string excepcion_CantidadVentaExcedeStockDisponibleArticulo = "Cantidad Venta Excede Stock Disponible Articulo";
            public const string advertencia_ConsultaVentasPorClienteExitosa = "Consulta de ventas por cliente exitosa.";
            public const string advertencia_AlmacenAsociadoAlVendedorNoExiste = "Almacen asociado al vendedor no existe.";
            public const string advertencia_EstadoDeVendedorAsociadoAlVendedorNoExiste = "Estado de vendedor asociado al vendedor no existe.";
            public const string advertencia_DatosDeVendedorOCodigoDeVendedorInvalido = "Datos de vendedor o Codigo de vendedor invalido.";
            public const string advertencia_UsuarioSistemaCreadorNuevoVendedorNoExiste = "Usuario de Sistema que crea el nuevo vendedor no existe.";
            public const string advertencia_UsuarioSistemaAccesoNuevoVendedorNoExiste = "Usuario de Sistema de prvilegios asociado al nuevo vendedor no existe.";
            public const string advertencia_VendedorCreadoSatisfactoriamente = "Vendedor creado satisfactoriamente.";
            public const string advertencia_CreacionNuevoVendedorFallo = "Creacion de nuevo vendedor fallo.";             
            public const string advertencia_UsuarioOClaveNula = "No se puede validar Inicio de Sesion con Usuario o Clave nula.";           
            public const string advertencia_UsuarioVendedorInvalido = "Usuario de Vendedor invalido.";
            public const string advertencia_UsuarioSistemaDeVendedorInvalido = "Usuario Sistema de vendedor invalido.";
            public const string advertencia_UsuarioSistemaInvalido = "Usuario Sistema invalido.";
            public const string advertencia_UsuarioValido = "Usuario Valido.";
            public const string advertencia_UsuarioSistemaInvalidoYSinDerechos = "Usuario Sistema invalido y sin derechos.";
            
            public const string advertencia_NuevoCorrelativoDocumentoGeneradoIncorreactamente = "Nuevo correlativo documento generado incorreactamente.";
            public const string advertencia_CorrelativoDocumentoActualEncontradoIncorrecto = "Correlativo de documento actual encontrado, incorrecto.";
            public const string advertencia_MontoExcedeElVueltoOriginal = "Monto excede el vuelto original.";
            public const string advertencia_VendedorNoExiste = "Vendedor no existe.";
            public const string advertencia_ClaveIncorrecta = "Clave incorrecta.";
            public const string advertencia_VendedorInactivo = "Vendedor inactivo.";
            public const string advertencia_VendedorSuspendido = "Vendedor suspendido.";

            public const string excepcion_AlmacenDeVendedorEnEstadoNuloOTransitorio = "Almacen De Vendedor En Estado Nulo O Transitorio";
            public const string excepcion_UsuarioSistemaDeVendedorEnEstadoNuloOTransitorio = "Usuario Sistema De Vendedor En Estado Nulo o Transitorio";
            public const string excepcion_EstadoVendedorDeVendedorEnEstadoNuloOTransitorio = "Estado Vendedor De Vendedor En Estado Nulo O Transitorio";
            public const string excepcion_UsuarioSistemaAccesoDeVendedorEnEstadoNuloOTransitorio = "Usuario Sistema Acceso De Vendedor En Estado Nulo O Transitorio";
            public const string excepcion_AlmacenOrigenDeConfiguracionGeneralEnEstadoNuloOTransitorio = "Almacen Origen de Configuracion General en estado Nulo o Transitorio";
            public const string excepcion_MonedaBaseDeConfiguracionGeneralEnEstadoNuloOTransitorio =  "Moneda Base de Configuracion General en estado Nulo o Transitorio";     
            public const string excepcion_MonedaExtranjeraDeConfiguracionGeneralEnEstadoNuloOTransitorio = "Moneda Extranjera de Configuracion General en estado Nulo o Transitorio.";
            public const string excepcion_ClaseTipoCambioVentasDeConfiguracionGeneralEnEstadoNuloOTransitorio =  "Clase Tipo Cambio Ventas de Configuracion General en estado Nulo o Transitorio";
            public const string excepcion_ClaseTipoCambioOrigenDeConfiguracionGeneralEnEstadoNuloOTransitorio =  "Clase Tipo Cambio Origen De Configuracion General en estado Nulo o Transitorio";
            public const string excepcion_ClienteInternoDeConfiguracionGeneralEnEstadoNuloOTransitorio = "Cliente Interno de Configuracion General en estado Nulo o Transitorio";
            public const string excepcion_TipoPrecioInventarioActualizableDeConfiguracionGeneralEnEstadoNuloOTransitorio = "Tipo Precio Inventario Actualizable de Configuracion General En Estado Nulo o Transitorio";
            public const string excepcion_ImpuestoDeConfiguracionGeneralEnEstadoNuloOTransitorio  = "Impuesto de Configuracion General en estado Nulo o Transitorio";
            public const string excepcion_DatosNoValidosParaLineaCierreZetaPuntoDeVenta =  "Datos no validos para Linea Cierre Zeta Punto de Venta";
            public const string excepcion_MonedaCajaDeConfiguracionPuntoDeVentaNuloOTransitorio =  "Moneda Caja de Configuracion Punto de Venta nulo o Transitorio";
            public const string excepcion_TipoNegocioDeConfiguracionPuntoDeVentaNuloOTransitorio = "Tipo Negocio de Configuracion Punto de Venta nulo o Transitorio";
            public const string excepcion_AlmacenPuntoVentaDeConfiguracionPuntoDeVentaNuloOTransitorio =  "Almacen Punto Venta de Configuracion Punto de Venta nulo o Transitorio";
            public const string excepcion_TipoImpresoraDeConfiguracionPuntoDeVentaNuloOTransitorio = "Tipo Impresora de configuracion Punto de Venta nulo o Transitorio";
            public const string excepcion_EstadoDeDocumentoDefaultDeConfiguracionPuntoDeVentaNuloOTransitorio =  "Estado de Documento default de Configuracion Punto de Venta nulo o Transitorio";
            public const string excepcion_TipoPagoDefaultDeConfiguracionPuntoVentaNuloOTransitorio =  "Tipo Pago default de Configuracion Punto Venta nulo o Transitorio";
            public const string excepcion_EstadoDocumentoAnuladoDeConfiguracionPuntoVentaNuloOTransitorio = "Estado Documento Anulado de Configuracion Punto Venta nulo o Transitorio";
            public const string excepcion_MonedaDeCuentasPorCobrarEnEstadoNuloOTransitorio =   "Moneda de Cuentas por Cobrar en estado nulo o transitorio";
            public const string excepcion_ClaseTipoDeCambioDeCuentaPorCobrarEnEstadoNuloOTransitorio =  "Clase tipo de cambio de cuenta por cobrar en estado nulo o transitorio";
            public const string excepcion_EstadoDeDocumentoDeCuentaPorCobrarEnEstadoNuloOTransitorio =  "Estado de Documento de Cuenta Por Cobrar en estado nulo o transitorio";
            public const string excepcion_DiaDePagoDeCuentaPorCobrarEnEstadoNuloOTransitorio =   "Dia de Pago de Cuenta Por Cobrar en estado nulo o transitorio";
            public const string excepcion_AlmacenDeCuentaPorCobrarEnEstadoNuloOTransitorio = "Almacen de Cuenta por Cobrar en estado nulo o transitorio";
            public const string excepcion_UsuarioDeSistemaDeCuentaPorCobrarEnEstadoNuloOTransitorio = "Usuario de Sistema de cuenta por cobrar en estado nulo o transitorio";
            public const string excepcion_TipoDeDocumentoDeCuentaPorCobrarEnEstadoNuloOTransitorio = "Tipo de Documento de Cuenta por Cobrar en estado nulo o transitorio";
            public const string excepcion_TipoDocumentoDeDocumentoAnticipadoNuloOTransitorio = "Tipo Documento de Documento anticipado Nulo o Transitorio";
            public const string excepcion_AlmacenDeDocumentoAnticipadoNuloOTransitorio = "Almacen de Documento Anticipado Nulo O Transitorio";
            public const string excepcion_ArticuloDeVentaDetalleEnEstadoNuloOTransitorio =   "Articulo de Venta Detalle en estado Nulo o Transitorio";
            public const string excepcion_AlmacenDeMovimientoAlmacenNuloOTransitorio =  "Almacen de Movimiento Almacen Nulo o Transitorio";
            public const string excepcion_ArticuloDeMovimientoAlmacenNuloOTransitorio =  "Articulo de Movimiento Almacen Nulo o Transitorio";
            public const string excepcion_TipoMovimientoAlmacenDeMovimientoAlmacenNuloOTransitorio = "Tipo Movimiento Almacen de Movimiento Almacen Nulo o Transitorio";
            public const string excepcion_TipoDocumentoDeMovimientoAlmacenNuloOTransitorio =  "Tipo Documento de Movimiento Almacen nulo o Transitorio";
            public const string excepcion_NoSePuedeAsociarUsuarioSistemaTransitorioONulo =  "No Se Puede Asociar UsuarioSistema Transitorio O Nulo";
            public const string excepcion_CodigoVentanaNoPuedeSerNulo = "Codigo Ventana No Puede Ser Nulo";
            public const string excepcion_NombreVentanaNoPuedeSerNulo = "Nombre Ventana No Puede Ser Nulo";
            public const string excepcion_TipoDeVentanaNoPuedeSerNulo =  "Tipo De Ventana No Puede Ser Nulo";
            public const string excepcion_DatosNoValidosParaLineaDerechoAccesoUsuario =  "Datos No Validos Para Linea DerechoAccesoUsuario";
            public const string excepcion_DatosNoValidosParaLineaTipoDeCambio = "Datos no validos para linea Tipo De Cambio";
            public const string excepcion_DatosNoValidosParaLineaCorrelativoDocumento = "Datos no validos para Linea Correlativo Documento";
            public const string validacion_CodigoTipoMovimientoAlmacenDeTipoMovimientoAlmacenVacioONulo = "";
            public const string validacion_DescripcionTipoMovimientoAlmacenDeTipoMovimientoAlmacenVacioONulo= "";
            public const string validacion_IngresoOSalidaDeTipoMovimientoAlmacenMenorACero = "";
            public const string validacion_EsValorizadoDeTipoMovimientoAlmacenMenorACero = "";
            public const string validacion_ValorizadoPorPrecioVoCostoRepDeTipoMovimientoAlmacenMenorACero = "";
            public const string validacion_CostoPromedioDeTipoMovimientoAlmacenMenorACero = "";
            public const string validacion_EsTipoIngresoPorCompraDeTipoMovimientoAlmacenMenorACero = "";
            public const string validacion_RequiereProveedorDeTipoMovimientoAlmacenMenorACero = "";
            public const string validacion_EnCalculoCostoPromedioDeTipoMovimientoAlmacenMenorACero = "";
            public const string validacion_DescripcionAbreviadaDeTipoMovimientoAlmacenVacioONulo = "";
            public const string validacion_CodigoTipoNegocioDeTipoNegocioVacioONulo = "";
            public const string validacion_DescripcionTipoNegocioDeTipoNegocioVacioONulo = "";
            public const string validacion_ClaveNulaOVacia =  "Clave nula o vacia";
            public const string validacion_NoExistenModulosDeSistemaAsignadosAlUsuario =  "No existen modulos de sistema asignados al usuario";
            public const string excepcion_VendedorsinUsuarioDeSistemaAsignado =  "Vendedor sin Usuario de Sistema asignado";
            public const string excepcion_UsuarioDeSistemaDeVendedorInactivo =  "Usuario de Sistema de vendedor inactivo";
            public const string excepcion_UsuarioDeSistemaDeVendedorSinPrivilegiosAsignados = "Usuario de Sistema de vendedor sin privilegios asignados";
            public const string excepcion_UsuarioDeSistemaDeVendedorSinPrivilegios =  "Usuario de Sistema de vendedor sin privilegios";
            public const string excepcion_MontoNacionalPagadoInsuficiente = "Monto nacional pagado insuficiente";
            public const string excepcion_MontoExtranjeraPagadoInsuficiente = "Monto extranjera pagado insuficiente";
            public const string excepcion_DatosNoValidosParaLineaVentaDetalle = "Datos no validos para Linea Venta Detalle";
            public const string excepcion_DatosNoValidosParaLineaVentaConTarjeta =  "Datos no validos para Linea Venta con Tarjeta";
            public const string excepcion_DatosNoValidosParaLineaVentaConVale =  "Datos no validos para Linea Venta con Vale";
            public const string excepcion_DatosNoValidosParaLineaDocumentoAnticipado = "Datos no validos para Linea Documento Anticipado";
            public const string excepcion_DatosNoValidosParaLineaCuentaPorCobrar = "Datos mo Validos para Linea Cuenta por Cobrar";
            public const string excepcion_MonedaDeVentaEnEstadoNuloOTransitorio =  "Moneda de Venta en estado nulo o Transitorio";
            public const string excepcion_ClaseTipoCambioDeVentaEnEstadoNuloOTransitorio = "Clase Tipo Cambio de Venta En Estado Nulo o Transitorio";
            public const string excepcion_ClienteDeVentaEnEstadoNuloOTransitorio =  "Cliente de Venta en estado nulo o Transitorio";
            public const string excepcion_TipoDocumentoDeVentaEnEstadoNuloOTransitorio =   "Tipo Documento de Venta en estado nulo o Transitorio";
            public const string excepcion_ImpuestoIgvDeVentaEnEstadoNuloOTransitorio = "Impuesto Igv De Venta en estado nulo o Transitorio";
            public const string excepcion_ImpuestoIscDeVentaEnEstadoNuloOTransitorio =  "Impuesto Isc de Venta en Estado Nulo o Transitorio";
            public const string excepcion_EstadoDocumentoDeVentaEnEstadoNuloOTransitorio =  "Estado Documento de Venta en estado Nulo o Transitorio";
            public const string excepcion_VendedorDeVentaEnEstadoNuloOTransitorio =  "Vendedor de Venta en estado Nulo o Transitorio";
            public const string excepcion_CondicionPagoDeVentaEnEstadoNuloOTransitorio =  "Condicion pago de Venta en estado Nulo o Transitorio";
            public const string excepcion_TipoPagoDeVentaEnEstadoNuloOTransitorio =  "Tipo Pago de Venta en estado Nulo o Transitorio";
            public const string excepcion_ConfiguracionPuntoVentaDeVentaEnEstadoNuloOTransitorio =  "Configuracion Punto Venta de Venta en estado Nulo o Transitorio";
            public const string excepcion_AlmacenDeVentaEnEstadoNuloOTransitorio =  "Almacen de Venta en estado Nulo o Transitorio";
            public const string excepcion_TipoNegocioDeVentaEnEstadoNuloOTransitorio = "Tipo Negocio de Venta en estado Nulo o Transitorio";
            public const string excepcion_UsuarioDeSistemaDeVentaEnEstadoNuloOTransitorio = "Usuario de Sistema de venta en estado Nulo o Transitorio";
            public const string excepcion_MonedaDeVentaConTarjetaEnEstadoNuloOTransitorio =  "Moneda de Venta con tarjeta en estado Nulo O Transitorio";
            public const string excepcion_TarjetaDeVentaConTarjetaEnEstadoNuloOTransitorio =  "Tarjeta de Venta con Tarjeta En Estado Nulo O Transitorio";
            public const string excepcion_TipoDocumentoDeVentaconTarjetaEnEstadoNuloOTransitorio = "Tipo Documento de Venta con Tarjeta en Estado Nulo o Transitorio";
            public const string excepcion_AlmacenDeVentaConTarjetaEnEstadoNuloOTransitorio = "Almacen de Venta con Tarjeta en estado Nulo o Transitorio";
            public const string excepcion_ClienteDeVentaConValeEnEstadoNuloOTransitorio =  "Cliente de Venta con Vale En Estado nulo o Transitorio";
            public const string excepcion_AlmacenDeVentaConValeEnEstadoNuloOTransitorio = "Almacen de venta con vale en estado Nulo o Transitorio";
            public const string excepcion_TipoDocumentoDeVentaConValeEnEstadoNuloOTransitorio =  "Tipo Documento de Venta con vale en estado Nulo O Transitorio";
            public const string excepcion_MonedaDeVentaConValeEnEstadoNuloOTransitorio = "Moneda de venta con vale en estado Nulo o Transitorio";
            public const string excepcion_MonedaDeVentaDetalleEnEstadoNuloOtransitorio = "Moneda de Venta Detalle en estado nulo o transitorio";
            public const string excepcion_EstadoDocumentoDeVentaDetalleEnEstadoNuloOTransitorio =  "Estado Documento de VentaDetalle en estado Nulo o Transitorio";
            public const string excepcion_DatosNoValidosParaLineaAsignacionListaPrecioCliente = "Datos no validos para Linea Asignacion Lista Precio Cliente";
            public const string excepcion_DatosNoValidosParaLineaClienteLimiteCredito = "Datos no validos para Linea Cliente Limite Credito";
            public const string excepcion_DatosNoValidosParaLineaDocumentoLibre = "Datos no validos Para Linea DocumentoLibre";
            public const string excepcion_DatosNoValidosParaLineaCierreZResumenPorArticulo = "Datos no validos para linea Cierre Z Resumen por Articulo";
            public const string excepcion_DatosNoValidosParaLineaCierreZResumenPorCara = "Datos no validos para Linea Cierre Z Resumen por Cara";
            public const string excepcion_DatosNoValidosParaLineaCierreZResumenPorCategoria = "Datos no validos para Linea Cierre Z Resumen por Categoria";
            public const string excepcion_DatosNoValidosParaLineaCierreZResumenPorVendedor = "Datos no validos para Linea Cierre Z Resumen Por Vendedor";
            public const string excepcion_CondicionPagoDocumentoGeneradoDeClienteEnEstadoNuloOTransitorio = "Condicion de Pago Documento Generado de Cliente en estado nulo o transitorio";
            public const string excepcion_CondicionPagoTicketDeClienteEnEstadoNuloOTransitorio = "Condicion de Pago Ticket de Cliente en estado nulo o transitorio";
            public const string excepcion_DiaDePagoDeClienteEnEstadoNuloOTransitorio = "Dia de Pago de Cliente en estado nulo o transitorio";

            public const string validacion_CodigoPaisDePaisVacioONulo = "Codigo de Pais Vacio o Nulo";
            public const string validacion_DescripcionPaisDePaisVacioONulo = "Descripcion de Pais Vacio O Nulo";
            public const string excepcion_DatosNoValidosParaLineaDistrito = "Datos no Validos para linea distrito";
            public const string validacion_CodigoDepartamentoDeDepartamentoONulo = "Codigo de Departamento o Nulo";
            public const string validacion_DescripcionDepartamentoDeDepartamentoVacioONulo = "Descripcion de Departamento Vacio o Nulo";
            public const string validacion_CodigoDistritoDeDistritoVacioONulo  = "Codigo de Distrito Vacio o Nulo";
            public const string validacion_DescripcionDistritoDeDistritoVacioONulo = "Descripcion de Distrito Vacio o Nulo";

            public const string excepcion_MonedaDeClienteEnEstadoNuloOTransitorio = "Moneda de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_ClaseTipoCambioDeClienteEnEstadoNuloOTransitorio = "Clase Tipo Cambio de Cliente en estado Nulo o Transitorio";
            public const string excepcion_TipoClienteDeClienteEnEstadoNuloOTransitorio = "Tipo Cliente de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_ZonaClienteDeClienteEnEstadoNuloOTransitorio = "Zona Cliente de Cliente en estado Nulo o Transitorio";
            public const string excepcion_VendedorDeClienteEnEstadoNuloOTransitorio = "Vendedor de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_ImpuestoIgvDeClienteEnEstadoNuloOTransitorio = "Impuesto Igv de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_ImpuestoIscDeClienteEnEstadoNuloOTransitorio = "Impuesto Isc de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_EstadoDeClienteDeClienteEnEstadoNuloOTransitorio = "Estado de Cliente de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_UsuarioSistemaDeClienteEnEstadoNuloOTransitorio = "Usuario Sistema de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_PaisDeClienteEnEstadoNuloOTransitorio = "Pais de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_DepartamentoDeClienteEnEstadoNuloOTransitorio = "Departamento de Cliente en Estado Nulo o Transitorio";
            public const string excepcion_DistritoDeClienteEnEstadoNuloOTransitorio = "Distrito de Cliente en Estado Nulo o Transitorio";
            public const string advertencia_ExitosaCreacionNuevoPedidoEESS = "Creacion Nuevo pedido EESS se realizo exitosamente";
            public const string advertencia_ExitosaCreacionNuevoPedidoRetail = "Creacion Nuevo pedido Retail se realizo exitosamente";
            public const string advertencia_ConsultaPedidosEESSPorPuntoDeVentaExitosa = "Consulta de pedidos EESS por punto de venta exitosa.";
            public const string advertencia_ConsultaPedidosRetailPorPuntoDeVentaExitosa = "Consulta de pedidos retail por punto de venta exitosa.";
            public const string advertencia_ConsultaPedidoEESSPorNumeroPedidoExitosa = "Consulta de pedidos EESS por numero de pedido exitosa.";
            public const string advertencia_ConsultaPedidoRetailPorNumeroPedidoExitosa = "Consulta de pedidos retail por numero de pedido exitosa.";
            public const string advertencia_NoSePuedeGrabarPedidoEESSNulo = "No se puede grabar pedido EESS nulo";
            public const string advertencia_NoSePuedeGrabarPedidoRetailNulo = "No se puede grabar pedido retail nulo";
            public const string advertencia_FalloCreacionNuevaVentaAPartirDePedidoRetail = "Fallo Creacion Nueva Venta a Partir de Pedido Retail";
            public const string advertencia_FalloCreacionNuevaVentaAPartirDePedidoEESS = "Fallo Creacion Nueva Venta a Partir de Pedido EESS";
            public const string advertencia_ConsultaVentasPorAlmacenExitosa = "Consulta de ventas por almacen exitosa.";
            public const string excepcion_DatosNoValidosParaLineaClientePlaca = "Placa incorrecta para agregar linea placa de cliente";
            public const string advertencia_NoSePuedeGrabarClienteNulo = "No se puede grabar cliente nulo";
            public const string advertencia_ConsultaConfiguracionPuntoDeVentaExitosa = "Consulta Configuracion Punto de Venta Exitosa";
            public const string advertencia_ConsultaConfiguracionGlobalExitosa = "Consulta Configuracion Global del sistema Exitosa";
            public const string advertencia_ConsultaConfiguracionGeneralFallo = "Consulta Configuracion General Fallo";
            public const string advertencia_ConsultaConfiguracionVentaFallo = "Consulta Configuracion Venta Fallo";
            public const string advertencia_ConsultaConfiguracionInventarioFallo = "Consulta Configuracion Inventario Fallo";
            public const string advertencia_ConsultaConfiguracionFormatoTicketFallo = "Consulta Configuracion Formato de Ticket Fallo";


        }

    }
}