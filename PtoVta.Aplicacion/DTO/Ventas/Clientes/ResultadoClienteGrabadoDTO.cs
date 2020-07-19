using System;


namespace PtoVta.Aplicacion.DTO.Ventas
{
    public class ResultadoClienteGrabadoDTO
    {
        public string CodigoCliente { get; set; }
        public string CodigoContable { get; set; }
        public string Ruc { get; set; }
        public string NombresORazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public Nullable<DateTime> FechaNacimiento { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public int DiasDeGracia { get; set; }
        public decimal MontoLimiteCredito { get; set; }
        public decimal Deuda { get; set; }
        public int EsAfecto { get; set; }
        public int ControlarSaldoDisponible { get; set; }       
    }
}