using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public interface IRepositorioTarjeta : IRepositorio<Tarjeta>
    {
        Tarjeta ObtenerPorCodigo(string pCodigoTarjeta);   
    }
}