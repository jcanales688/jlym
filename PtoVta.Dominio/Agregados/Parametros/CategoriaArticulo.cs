using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class CategoriaArticulo : Entidad
    {
        HashSet<SubCategoriaArticulo> _lineasSubCategoriaArticulo;

        bool _EsHabilitado;

        public string CodigoCategoriaArticulo { get; set; }
        public string DescripcionCategoriaArticulo { get; set; }
        public string CodigoContable { get; set; }
        public string Comentario { get; set; }
        public byte[] Imagen{get; set; }


        public bool EsHabilitado
        {
            get
            {
                return _EsHabilitado;
            }
            private set
            {
                _EsHabilitado = value;
            }
        }

        public void Habilitar()
        {
            if (!EsHabilitado)
                this._EsHabilitado = true;

        }

        public void Deshabilitar()
        {
            if (EsHabilitado)
                this._EsHabilitado = false;
        }

        public string CodigoTipoNegocio { get;  set; }
        public virtual TipoNegocio TipoNegocio { get; private set; }

        public virtual ICollection<SubCategoriaArticulo> SubCategoriasArticulo 
        {
            get
            {
                if (_lineasSubCategoriaArticulo == null)
                    _lineasSubCategoriaArticulo = new HashSet<SubCategoriaArticulo>();

                return _lineasSubCategoriaArticulo;
            }
            set
            {
                _lineasSubCategoriaArticulo = new HashSet<SubCategoriaArticulo>(value);
            }
        }


    }
}    