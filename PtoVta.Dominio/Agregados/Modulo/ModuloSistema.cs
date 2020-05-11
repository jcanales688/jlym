using System;
using System.Collections.Generic;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Modulo
{
    //CLASE ROOT
    public class ModuloSistema:Entidad
    {
        HashSet<VentanaUsuario> _lineasVentanaUsuario;

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



        public VentanaUsuario AgregarNuevaVentanaUsuario(
            string pCodigoVentanaUsuario, string pNombreVentana, string pTipoVentana
             ,string  pCodigoModuloSistema
            )
        {

            if (String.IsNullOrWhiteSpace(pCodigoVentanaUsuario))
                throw new ArgumentNullException("Codigo Ventana No Puede Ser Nulo");


            if (String.IsNullOrWhiteSpace(pNombreVentana))
                throw new ArgumentNullException("Nombre Ventana No Puede Ser Nulo");

            if (String.IsNullOrWhiteSpace(pTipoVentana))
                throw new ArgumentNullException("Tipo De Ventana No Puede Ser Nulo");


            var _VentanaUsuario = new VentanaUsuario()
            {
                ModuloSistemaId = this.Id,
                CodigoModuloSistema = pCodigoModuloSistema,
                CodigoVentanaUsuario = pCodigoVentanaUsuario,
                NombreVentana = pNombreVentana,
                TipoVentana = pTipoVentana
            };


            //Establecer la identidad
            _VentanaUsuario.GenerarNuevaIdentidad();

            this.VentanasUsuario.Add(_VentanaUsuario);

            return _VentanaUsuario;
        }

        // public DerechoAccesoUsuario AgregarDerechoAccesoUsuario(
        //             int pDerechoConsultar, int pDerechoInsertar, int pDerechoActualizar,
        //             int pDerechoEliminar, int pDerechoImprimir, int pDerechoAnular, int pDerechoEmitir,
        //             Guid pUsuarioSistemaId
        //     )
        // {
        //     return _VentanaUsuario.AgregarNuevoDerechoAccesoUsuario(pDerechoConsultar, pDerechoInsertar, pDerechoActualizar,
        //                             pDerechoEliminar, pDerechoImprimir,pDerechoAnular, pDerechoEmitir, pUsuarioSistemaId);
        // }

        // public VentanaUsuario AgregarNuevaVentanaUsuario()
        // {
        //     this.VentanasUsuario.Add(_VentanaUsuario);

        //     return _VentanaUsuario;
        // }        
    }
}
