using System;

namespace PtoVta.Aplicacion.DTO.Colaborador
{
    public class VendedorDTO
    {
        public Guid Id { get; set; }

        public string CodigoVendedor { get; set; }
        public string NombresVendedor { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Clave { get; set; }
        public DateTime FechaNacimiento { get; set; }


        public string DireccionPrimeroPais { get; set; }
        public string DireccionPrimeroDepartamento { get; set; }
        public string DireccionPrimeroProvincia { get; set; }
        public string DireccionPrimeroDistrito { get; set; }
        public string DireccionPrimeroUbicacion { get; set; }

        public string CodigoAlmacen{ get; set; }
        public string CodigoEstadoVendedor{ get; set; }
        public string CodigoUsuarioSistema { get;set; }
        public string CodigoUsuarioSistemaAcceso { get;  set; }


    }

}