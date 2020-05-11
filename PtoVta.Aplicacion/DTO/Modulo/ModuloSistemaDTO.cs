using System;
using System.Collections.Generic;

namespace PtoVta.Aplicacion.DTO.Modulo
{
    public class ModuloSistemaDTO
    {
        // public Guid Id { get; set; }

        public string CodigoModuloSistema { get; set; }
        public string NombreModulo { get; set; }

        public List<VentanaUsuarioDTO> VentanasUsuario { get; set; }        
    }
}
