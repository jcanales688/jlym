using System;

namespace PtoVta.Infraestructura.Transversales.Adaptador
{
    public static class TipoAdaptadorFactory
    {
        static ITipoAdaptadorFactory _actualITipoAdaptadorFactory = null;

        //Establecer el actual tipo adapter factory
        //adaptarFactory: El generador de adaptadores a establecer
        public static void EstablecerActual(ITipoAdaptadorFactory adaptarFactory)
        {
            _actualITipoAdaptadorFactory = adaptarFactory;
        }

        //Crear un nuevo tipo de adaptador desde la actual Factory
        //retorna: tipo adaptador creado
        public static ITipoAdaptador CrearAdaptador()
        {
            return _actualITipoAdaptadorFactory.Crear();
        }
    }
}
