using System;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public class ServicioDominioValidarUsuarioVendedor : IServicioDominioValidarUsuarioVendedor
    {
       public bool ValidarUsuarioVendedor(Vendedor pVendedor, string pClave)
        {
            bool estadoValidUsuEmpleado = false;

            if (pVendedor == null)
            {
                //Usuario No Existe 
                throw new InvalidOperationException("Vendedor No Existe En ServicioDominioValidarUsuarioVendedor");

            }

            if (pVendedor.Clave.Trim() != pClave.Trim())
            {
                //Clave Incorrecta
                throw new InvalidOperationException("Mensajes.validacion_ClaveIncorrectaEnServicioDominioValidarUsuarioVendedor");
            }

            if (pVendedor.EstadoVendedor.CodigoEstadoVendedor == "02")
            {
                throw new InvalidOperationException("Mensajes.validacion_VendedorInactivoEnServicioDominioValidarUsuarioVendedor");
            }

            if (pVendedor.EstadoVendedor.CodigoEstadoVendedor == "03")
            {
                throw new InvalidOperationException("Mensajes.validacion_VendedorSuspendidoEnServicioDominioValidarUsuarioVendedor");

            }


            estadoValidUsuEmpleado = true;

            return estadoValidUsuEmpleado;
        }        
    }
}
