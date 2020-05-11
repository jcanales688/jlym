using System;
using PtoVta.Infraestructura.Transversales.Autenticacion;

namespace PtoVta.Infraestructura.TransversalesNET.Autenticacion
{
    public class AutenticacionWindows : IAutenticacion
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool ValidarInicioSesion(string pUsuario, string pClave)
        {
            throw new NotImplementedException();
        }
    }
}
