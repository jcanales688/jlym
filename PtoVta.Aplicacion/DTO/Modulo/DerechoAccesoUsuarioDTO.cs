using System;

namespace PtoVta.Aplicacion.DTO.Modulo
{
    public class DerechoAccesoUsuarioDTO
    {
        public Guid Id { get; set; }

        public int DerechoConsultar { get; set; }
        public int DerechoInsertar { get; set; }
        public int DerechoActualizar { get; set; }
        public int DerechoEliminar { get; set; }
        public int DerechoImprimir { get; set; }
        public int DerechoAnular { get; set; }
        public int DerechoEmitir { get; set; }


        public Guid UsuarioSistemaId { get; set; }
        public string UsuarioSistemaUsuarioDeSistema { get; set; }        
    }
}
