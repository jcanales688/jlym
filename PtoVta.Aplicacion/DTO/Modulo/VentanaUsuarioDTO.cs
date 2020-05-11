using System;
using System.Collections.Generic;

namespace PtoVta.Aplicacion.DTO.Modulo
{
    public class VentanaUsuarioDTO
    {
        // public Guid Id { get; set; }

        
        public string CodigoVentanaUsuario { get; set; }
        public string NombreVentana { get; set; }
        public string TipoVentana { get; set; }



        public List<DerechoAccesoUsuarioDTO> DerechosAccesoUsuario { get; set; }        
    }
}
