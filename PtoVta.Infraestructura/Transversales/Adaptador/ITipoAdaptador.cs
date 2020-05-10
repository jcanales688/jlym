using System;

namespace PtoVta.Infraestructura.Transversales.Adaptador
{
    public interface ITipoAdaptador
    {
        //Adaptar un objeto de origen para una instancia de tipo objetivo
        //TOrigen:      Tipo de elemento de origen
        //TObjetivo:    tipo elemento destino
        //RESULTADO:    asigano , mapeado a
        TDestino Adapt<TOrigen, TDestino>(TOrigen origen)
            where TDestino : class, new()
            where TOrigen : class;


        //Adaptar un objeto origen para un tipo de instnace 
        //TObjetivo :   Tipo de elemento de destino
        //origen:       Instancia de adaptación
        //RESULTADO:    asigano , mapeado a
        TDestino Adapt<TDestino>(object origen)
            where TDestino : class, new();        
    }
}
