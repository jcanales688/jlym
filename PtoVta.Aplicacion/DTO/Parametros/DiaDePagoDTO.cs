using System;

namespace PtoVta.Aplicacion.DTO.Parametros
{
    public class DiaDePagoDTO
    {
        public string CodigoDiaDePago { get; set; }
        public int CombinaDia1 { get; set; }
        public int CombinaDia2 { get; set; }
        public int CombinaDia3 { get; set; }
        public int CombinaDia4 { get; set; }
        public string DescripcionDiaDePago { get; set; }
        public int D1Lunes { get; set; }
        public int D2Martes { get; set; }
        public int D3Miercoles { get; set; }
        public int D4Jueves { get; set; }
        public int D5Viernes { get; set; }
        public int D6Sabado { get; set; }
        public int D7Domingo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaActualiza { get; set; }
        public int EstadoSemana { get; set; }        
    }
}   