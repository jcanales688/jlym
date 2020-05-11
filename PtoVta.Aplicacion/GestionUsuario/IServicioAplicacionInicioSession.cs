using System;
using PtoVta.Aplicacion.DTO.Modulo;

namespace PtoVta.Aplicacion.GestionUsuario
{
    public interface IServicioAplicacionInicioSession : IDisposable
    {
        ModuloSistemaDTO GestionInicioSesion(string pUsuario, string pClave, string pCodigoModuloSistema);        
    }
}
