using System;
using System.Linq;
using System.Reflection;

namespace PtoVta.Dominio.BaseTrabajo
{

    public class ObjetoValor<TObjetoValor> : IEquatable<TObjetoValor>
        where TObjetoValor:ObjetoValor<TObjetoValor>
    {
        public bool Equals(TObjetoValor otro)
        {
            if ((object)otro == null)
                return false;

            if (Object.ReferenceEquals(this, otro))
                return true;

            //Comparar todas las propiedades públicas
            PropertyInfo[] propiedadesPublicas = this.GetType().GetProperties();

            if ((object)propiedadesPublicas != null &&
                propiedadesPublicas.Any())
            {
                    return propiedadesPublicas.All( p =>
                    {
                        var izquierda = p.GetValue(this, null);
                        var derecha = p.GetValue(otro,null);

                        if(typeof(TObjetoValor).IsAssignableFrom(izquierda.GetType()))
                        {
                            //no verificar auto referencias
                            return Object.ReferenceEquals(izquierda, derecha);
                        }
                        else
                            return izquierda.Equals(derecha);
                    });
            }
            else
                return true;

        }


        public override bool Equals(object obj)
        {
            if ((object)obj == null)
                return false;

            if(Object.ReferenceEquals(this, obj))
                return true;


            ObjetoValor<TObjetoValor> item = obj as ObjetoValor<TObjetoValor>;

            if ((object)item != null)
                return Equals((TObjetoValor)item);
            else
                return false;




        }

        public override int GetHashCode()
        {
            int codigoHash = 31;
            bool cambiarMultiplicador = false;
            int indice = 1;

            //Comparar todas las propiedades públicas
            PropertyInfo[] propiedadesPublicas = this.GetType().GetProperties();

            if ((object)propiedadesPublicas != null
                &&
                propiedadesPublicas.Any())
            {
                foreach (var item in propiedadesPublicas)
                {
                    object valor = item.GetValue(this, null);

                    if ((object)valor != null)
                    {
                        codigoHash = codigoHash * ((cambiarMultiplicador) ? 59 : 114) + valor.GetHashCode();

                        cambiarMultiplicador = !cambiarMultiplicador;

                    }
                    else
                        codigoHash = codigoHash ^ (indice * 13);//sólo para apoyo {"a",null,null,"a"} <> {null,"a","a",null}

                }
            }
            return codigoHash;

        }


        public static bool operator ==(ObjetoValor<TObjetoValor> izquierda, ObjetoValor<TObjetoValor> derecha)
        {
            if (Object.Equals(izquierda, null))
                return (Object.Equals(derecha, null)) ? true : false;
            else
                return izquierda.Equals(derecha);
        }


        public static bool operator !=(ObjetoValor<TObjetoValor> izquierda, ObjetoValor<TObjetoValor> derecha)
        {
            return !(izquierda == derecha);
        }

        
    }
}