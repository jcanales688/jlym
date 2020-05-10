using System;

namespace PtoVta.Infraestructura.Transversales.Adaptador
{
    public interface ITipoAdaptadorFactory
    {
        //Crear un tipo Adater
        //ITipoAdaptador creado
        ITipoAdaptador Crear();        
    }
}
