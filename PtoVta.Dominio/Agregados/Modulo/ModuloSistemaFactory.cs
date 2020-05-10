using System;

namespace PtoVta.Dominio.Agregados.Modulo
{
    public static class ModuloSistemaFactory
    {
        public static ModuloSistema CrearModuloSistema(string pCodigoModuloSistema, string pNombreModulo)
        {
            var moduloSistema = new ModuloSistema();

            moduloSistema.GenerarNuevaIdentidad();

            moduloSistema.CodigoModuloSistema = pCodigoModuloSistema;
            moduloSistema.NombreModulo = pNombreModulo;

            moduloSistema.Habilitar();

            return moduloSistema;

        }        
    }
}
