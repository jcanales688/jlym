using System;
using PtoVta.Aplicacion.BaseTrabajo;
using PtoVta.Aplicacion.DTO.Colaborador;

namespace PtoVta.Aplicacion.GestionUsuario
{
    public interface IServicioAplicacionVendedor
    {
        ResultadoServicio<VendedorDTO> AgregarNuevoUsuarioVendedor(VendedorDTO pVendedor);        
    }    
}