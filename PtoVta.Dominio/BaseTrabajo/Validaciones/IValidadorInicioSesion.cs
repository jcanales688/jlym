using System;

namespace PtoVta.Dominio.BaseTrabajo.Validaciones
{
    public interface IValidadorInicioSesion: IDisposable   
    {
        bool ValidarInicioSesion(string pUsuario, string pClave);   
    }

}