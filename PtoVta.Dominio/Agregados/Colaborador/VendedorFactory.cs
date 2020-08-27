using System;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Usuario;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public static class VendedorFactory
    {
        public static Vendedor CrearVendedor(
                                             string pNombresVendedor,
                                             string pDocumentoIdentidad,
                                             string pTelefono,
                                             string pSexo,
                                             DateTime pFechaInicio,
                                             string pCodigoVendedor,
                                             string pClave,
                                             DateTime pFechaNacimiento,
                                             Almacen pAlmacen,
                                             EstadoVendedor pEstadoVendedor,    
                                             UsuarioSistema pUsuarioSistema,
                                             UsuarioSistema pUsuarioSistemaAcceso,
                                             VendedorDireccion pDireccionPrimero
            )
        {
            var vendedor = new Vendedor(pNombresVendedor, pDocumentoIdentidad, pTelefono,
                                        pSexo, pFechaInicio, pCodigoVendedor,
                                        pClave, pFechaNacimiento);

            // Value Object
            vendedor.Direccion = pDireccionPrimero;

            // vendedor.Habilitar();
            vendedor.EstablecerAlmacenDeVendedor(pAlmacen);
            vendedor.EstablecerEstadoVendedorDeVendedor(pEstadoVendedor);
            vendedor.EstablecerUsuarioSistemaDeVendedor(pUsuarioSistema);
            vendedor.EstablecerUsuarioSistemaAccesoDeVendedor(pUsuarioSistemaAcceso);

            return vendedor;
        }
    }    
}