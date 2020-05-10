using System;

namespace PtoVta.Dominio.BaseTrabajo
{
    public abstract class Entidad
    {
        int? _pedirCodigoHash;
        Guid _Id;

        //Obtener el identificador de objeto persistente
        public virtual Guid Id
        {
            get
            {
                return _Id;    
            }
            protected set
            {
                _Id = value;
            }
        }


        /*
            Compruebe si esta entidad es transitoria, es decir, sin identidad en este momento
            Verdadero si la entidad es transitorio, de lo falso 
         */
        public bool EsTransitorio()
        {
            return this.Id == Guid.Empty;
        }

        //Generar la identidad de esta entidad
        public void GenerarNuevaIdentidad()
        {
            if (EsTransitorio())
                this.Id = GeneradorIdentidad.NuevaGuidSecuencial();
        }

        //Cambiar de identidad actual de una nueva identidad no transitoria
        public void CambiarIdentidadActual(Guid identidad)
        {
            if (identidad != Guid.Empty)
                this.Id = identidad;

        }



        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Entidad))
                return false;

            if(Object.ReferenceEquals(this, obj))
                return true;

            Entidad item = (Entidad)obj;

            if(item.EsTransitorio() || this.EsTransitorio())
                return false;
            else
                return item.Id == this.Id;

        }


        public override int GetHashCode()
        {
            if (!EsTransitorio())
            {
                if (!_pedirCodigoHash.HasValue)
                    _pedirCodigoHash = this.Id.GetHashCode() ^ 31;//XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _pedirCodigoHash.Value;
            }
            else
                return base.GetHashCode();
        }



        public static bool operator ==(Entidad izquierda, Entidad derecha)
        {
            if (Object.Equals(izquierda, null))
                return (Object.Equals(derecha, null)) ? true : false;
            else
                return izquierda.Equals(derecha);

            
        }

        public static bool operator !=(Entidad izquierda, Entidad derecha)
        {
            return !(izquierda == derecha);
        }

    }
}
