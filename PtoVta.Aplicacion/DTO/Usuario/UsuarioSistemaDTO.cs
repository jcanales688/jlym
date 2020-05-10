using System;

namespace PtoVta.Aplicacion.DTO.Usuario
{
    public class UsuarioSistemaDTO
    {
        public Guid Id { get; set; }

        public string UsuarioDeSistema { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string DescripcionUsuario { get; set; }
        public string Contraseña { get; set; }        
    }
}
