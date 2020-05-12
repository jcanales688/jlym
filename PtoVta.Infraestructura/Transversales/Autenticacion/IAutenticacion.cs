using System;

namespace PtoVta.Infraestructura.Transversales.Autenticacion
{
    public interface IAutenticacion
    // : IDisposable   
    {
        bool ValidarInicioSesion(string pUsuario, string pClave);   
    }

}