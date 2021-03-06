using System;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using Xunit;

namespace PtoVta.Infraestructura.Tests
{
    public class RepositorioVendedorTest
    {
        private readonly IRepositorioVendedor _IRepositorioVendedor;
        public RepositorioVendedorTest()
        {
            _IRepositorioVendedor = new RepositorioVendedor(ConfiguracionGlobal.CadenaConexionBd);
        }

        [Fact]
        public void ObtenerVendedorPorUsuario_Test()
        {
            var vendedor = _IRepositorioVendedor.ObtenerVendedorPorUsuario("42928283");
            
            Assert.False(vendedor == null);
        }

        [Fact]
        public void ObtenerPorCodigo_Test()
        {
            var vendedor = _IRepositorioVendedor.ObtenerPorCodigo("03876573");
            
            Assert.False(vendedor == null);
        }

        [Fact]
        public void Agregar_Test()
        {
            var vendedor = new Vendedor(){
                CodigoVendedor = "10412893",
                NombresVendedor = "FERNANDO ILLESCAS",
                DocumentoIdentidad = "10412893",
                Telefono = "5203125",
                Sexo="M",
                FechaInicio = DateTime.Now,
                FechaNacimiento = DateTime.Now,
                Clave = "123"
            };



            vendedor.EstablecerReferenciaAlmacenDeVendedor("24");
            vendedor.EstablecerReferenciaEstadoVendedorDeVendedor("01");
            vendedor.EstablecerReferenciaUsuarioSistemaDeVendedor("SYSADMIN");
            vendedor.EstablecerReferenciaUsuarioSistemaAccesoDeVendedor("VENDPLAYA");
            
            vendedor.Direccion = new VendedorDireccion("Peru", "Lima", "Lima", "Puente Piedra", "AV. 100");

            _IRepositorioVendedor.Agregar(vendedor);
            
            Assert.False(vendedor == null);
        }        
    }
}
