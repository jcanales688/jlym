using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Modulo
{
    public class VentanaUsuario :Entidad
    {
        HashSet<DerechoAccesoUsuario> _lineasDerechoAccesoUsuario;

        public string CodigoVentanaUsuario { get; set; }
        public string NombreVentana { get; set; }
        public string TipoVentana { get; set; }

        public string CodigoModuloSistema{get; set;}
        public Guid ModuloSistemaId { get; set; }



        public virtual ICollection<DerechoAccesoUsuario> DerechosAccesoUsuario
        {
            get
            {
                if (_lineasDerechoAccesoUsuario == null)
                    _lineasDerechoAccesoUsuario = new HashSet<DerechoAccesoUsuario>();

                return _lineasDerechoAccesoUsuario;
            }
            set
            {
                _lineasDerechoAccesoUsuario = new HashSet<DerechoAccesoUsuario>(value);
            }
        }


        public DerechoAccesoUsuario AgregarNuevoDerechoAccesoUsuario(
            int pDerechoConsultar, int pDerechoInsertar, int pDerechoActualizar,
            int pDerechoEliminar, int pDerechoImprimir, int pDerechoAnular, int pDerechoEmitir,
            string pCodigoUsuarioSistema)
        {

            if (string.IsNullOrEmpty(pCodigoUsuarioSistema)
               
                    ||

                    (
                        pDerechoConsultar == 0
                        &&
                        pDerechoInsertar == 0
                        &&
                        pDerechoActualizar == 0
                        &&
                        pDerechoEliminar == 0
                        &&
                        pDerechoImprimir == 0
                        &&
                        pDerechoAnular == 0
                        &&
                        pDerechoEmitir == 0
                    )
                )
                throw new ArgumentException(Mensajes.excepcion_DatosNoValidosParaLineaDerechoAccesoUsuario);



            var nuevaLineaDerechoAccesoUsuario = new DerechoAccesoUsuario()
            {
                CodigoVentanaUsuario = this.CodigoVentanaUsuario,
                CodigoUsuarioSistema = pCodigoUsuarioSistema,
                DerechoConsultar = pDerechoConsultar,
                DerechoInsertar = pDerechoInsertar,
                DerechoActualizar = pDerechoActualizar,
                DerechoEliminar = pDerechoEliminar,
                DerechoImprimir = pDerechoImprimir,
                DerechoAnular = pDerechoAnular,
                DerechoEmitir = pDerechoEmitir
            };

            //Establecer la identidad
            nuevaLineaDerechoAccesoUsuario.GenerarNuevaIdentidad();

            this.DerechosAccesoUsuario.Add(nuevaLineaDerechoAccesoUsuario);

            return nuevaLineaDerechoAccesoUsuario;

    
        }        
    }
}
