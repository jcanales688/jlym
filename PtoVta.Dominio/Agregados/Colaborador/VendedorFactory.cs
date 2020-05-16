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
                                             string pUsuarioVendedor,
                                             string pClave,
                                             DateTime pFechaNacimiento,
                                             Almacen pAlmacen,
                                             EstadoVendedor pEstadoVendedor,    
                                             UsuarioSistema pUsuarioSistema,
                                             UsuarioSistema pUsuarioSistemaAcceso,
                                             VendedorDireccion pDireccionPrimero
            )
        {
            var vendedor = new Vendedor();

            vendedor.GenerarNuevaIdentidad();

            vendedor.NombresVendedor = pNombresVendedor;
            vendedor.DocumentoIdentidad = pDocumentoIdentidad;
            vendedor.Telefono = pTelefono; 
            vendedor.Sexo = pSexo;
            vendedor.FechaInicio = pFechaInicio;
            vendedor.Clave = pClave;
            vendedor.FechaNacimiento = pFechaNacimiento;


            // Value Object
            vendedor.Direccion = pDireccionPrimero;

            vendedor.Habilitar();

            vendedor.EstablecerAlmacenDeVendedor(pAlmacen);
            vendedor.EstablecerEstadoVendedorDeVendedor(pEstadoVendedor);
            vendedor.EstablecerUsuarioSistemaDeVendedor(pUsuarioSistema);
            vendedor.EstablecerUsuarioSistemaAccesoDeVendedor(pUsuarioSistemaAcceso);



            return vendedor;
        }
    }    
}