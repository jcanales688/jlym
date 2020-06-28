using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Parametros
{
    public class DiaDePago : Entidad
    {
        bool _EsHabilitado;


        public string CodigoDiaDePago { get; set; }
        public Nullable<int> CombinaDia1 { get; set; }
        public int CombinaDia2 { get; set; }
        public Nullable<int> CombinaDia3 { get; set; }
        public Nullable<int> CombinaDia4 { get; set; }
        public string DescripcionDiaDePago { get; set; }
        public Nullable<int> D1Lunes { get; set; }
        public Nullable<int> D2Martes { get; set; }
        public Nullable<int> D3Miercoles { get; set; }
        public Nullable<int> D4Jueves { get; set; }
        public Nullable<int> D5Viernes { get; set; }
        public Nullable<int> D6Sabado { get; set; }
        public Nullable<int> D7Domingo { get; set; }
        public Nullable<DateTime> FechaCreacion { get; set; }
        public Nullable<DateTime> FechaUltimaActualiza { get; set; }
        public Nullable<int> EstadoSemana { get; set; }
        //public string UsuarioSistemaCrea { get; set; }

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
    }
}