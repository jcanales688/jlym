using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Modulo;

namespace PtoVta.Aplicacion.GestionUsuario
{
    public interface IServicioAplicacionInicioSession 
    // : IDisposable
    {
        ResultadoServicio<ModuloSistemaDTO>  GestionInicioSesion(string pUsuario, string pClave, string pCodigoModuloSistema);        
    }
}
