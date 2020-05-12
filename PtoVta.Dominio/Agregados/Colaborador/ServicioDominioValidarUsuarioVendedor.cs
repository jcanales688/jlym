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
                throw new InvalidOperationException("Vendedor no existe.");

            }

            if (pVendedor.Clave.Trim() != pClave.Trim())
            {
                //Clave Incorrecta
                throw new InvalidOperationException("Clave incorrecta.");
            }

            if (pVendedor.EstadoVendedor.CodigoEstadoVendedor == "02")
            {
                throw new InvalidOperationException("Vendedor inactivo.");
            }

            if (pVendedor.EstadoVendedor.CodigoEstadoVendedor == "03")
            {
                throw new InvalidOperationException("Vendedor suspendido.");

            }


            estadoValidUsuEmpleado = true;

            return estadoValidUsuEmpleado;
        }        
    }
}
