using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Modulo
{
    //CLASE ROOT
    public class ModuloSistema:Entidad
    {
        HashSet<VentanaUsuario> _lineasVentanaUsuario;
        VentanaUsuario _VentanaUsuario = null;

        bool _EsHabilitado;
 
        public string CodigoModuloSistema { get; set; }
        public string NombreModulo { get; set; }

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


        public virtual ICollection<VentanaUsuario> VentanasUsuario 
        {
            get
            {
                if (_lineasVentanaUsuario == null)
                    _lineasVentanaUsuario = new HashSet<VentanaUsuario>();

                return _lineasVentanaUsuario;
            }
            set
            {
                _lineasVentanaUsuario = new HashSet<VentanaUsuario>(value);
            }
        
        }



        public void CrearNuevaVentanaUsuario(
            string pIdVentanaUsuario, string pNombreVentana, string pTipoVentana
            //, Guid pModuloSistemaId
            )
        {

            // var resultadoValidacion = new List<ValidationResult>();


            if (String.IsNullOrWhiteSpace(pIdVentanaUsuario))
                throw new ArgumentNullException("Mensajes.validacion_IdVentanaNoPuedeSerNulo");


            if (String.IsNullOrWhiteSpace(pNombreVentana))
                throw new ArgumentNullException("Mensajes.validacion_NombreVentanaNoPuedeSerNulo");

            if (String.IsNullOrWhiteSpace(pTipoVentana))
                throw new ArgumentNullException("Mensajes.validacion_TipoDeVentanaNoPuedeSerNulo");


            _VentanaUsuario = new VentanaUsuario()
            {
                ModuloSistemaId = this.Id,
                CodigoVentanaUsuario = pIdVentanaUsuario,
                NombreVentana = pNombreVentana,
                TipoVentana = pTipoVentana
            };


            //Establecer la identidad
            _VentanaUsuario.GenerarNuevaIdentidad();

        }

        public DerechoAccesoUsuario AgregarDerechoAccesoUsuario(
                    int pDerechoConsultar, int pDerechoInsertar, int pDerechoActualizar,
                    int pDerechoEliminar, int pDerechoImprimir, int pDerechoAnular, int pDerechoEmitir,
                    Guid pUsuarioSistemaId
            )
        {
            return _VentanaUsuario.AgregarNuevoDerechoAccesoUsuario(pDerechoConsultar, pDerechoInsertar, pDerechoActualizar,
                                    pDerechoEliminar, pDerechoImprimir,pDerechoAnular, pDerechoEmitir, pUsuarioSistemaId);
        }

        public VentanaUsuario AgregarNuevaVentanaUsuario()
        {
            this.VentanasUsuario.Add(_VentanaUsuario);

            return _VentanaUsuario;
        }        
    }
}
