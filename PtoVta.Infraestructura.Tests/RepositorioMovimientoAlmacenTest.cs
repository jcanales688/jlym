using System;
using PtoVta.Dominio.Agregados.Colaborador;
using PtoVta.Dominio.Agregados.Inventarios;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Infraestructura.Repositorios.Colaborador;
using PtoVta.Infraestructura.Repositorios.Inventarios;
using Xunit;


namespace PtoVta.Infraestructura.Tests
{

    public class RepositorioMovimientoAlmacenTest
    {
        private readonly IRepositorioMovimientoAlmacen _IRepositorioMovimientoAlmacen;
        public RepositorioMovimientoAlmacenTest(){      
                _IRepositorioMovimientoAlmacen = new RepositorioMovimientoAlmacen(ConfiguracionGlobal.CadenaConexionBd);
        }


        [Fact]
        public void Agregar_Test()
        {   
            var movimientoAlmacen = new MovimientoAlmacen
            {
                CorrelativoMovimiento = "777",
                FechaDocumento = DateTime.Now,
                FechaProceso = DateTime.Now,
                MontoTipoDeCambio = 3.56M,
                FechaTipoDeCambio = DateTime.Now,
                Periodo = "202006",
                FlagEntradaSalida = 1,
                Cantidad = 7777.00M,
                CostoReposicionExtranjera = 13.78M,
                CostoReposicionNacional = 35.77M,
                EsArticuloFormula = true,
                Precio = 5.80M,
                DocumentoReferencia = "JL20000005",
                EnInventarioFisico = 1
            };

            movimientoAlmacen.EstablecerAlmacenDeMovimientoAlmacen(new Almacen{CodigoAlmacen = "24"});
            movimientoAlmacen.EstablecerArticuloDeMovimientoAlmacen(new Articulo{CodigoArticulo = "20101"});
            movimientoAlmacen.EstablecerTipoMovimientoAlmacenDeMovimientoAlmacen(new TipoMovimientoAlmacen{CodigoTipoMovimientoAlmacen = "301"});
            movimientoAlmacen.EstablecerTipoDocumentoDeMovimientoAlmacen(new TipoDocumento{CodigoTipoDocumento="12"});

            _IRepositorioMovimientoAlmacen.Agregar(movimientoAlmacen);



            Assert.True(movimientoAlmacen.CorrelativoMovimiento == "777");
        }
    }
}
