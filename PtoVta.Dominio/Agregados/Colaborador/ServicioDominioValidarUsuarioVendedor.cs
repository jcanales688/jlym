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
                throw new InvalidOperationException("Vendedor No Existe En ServicioDominio ValidarUsuarioVendedor");

            }

            if (pVendedor.Clave.Trim() != pClave.Trim())
            {
                //Clave Incorrecta
                throw new InvalidOperationException("Clave Incorrecta En ServicioDominio ValidarUsuarioVendedor");
            }

            if (pVendedor.EstadoVendedor.CodigoEstadoVendedor == "02")
            {
                throw new InvalidOperationException("Vendedor Inactivo En ServicioDominio ValidarUsuarioVendedor");
            }

            if (pVendedor.EstadoVendedor.CodigoEstadoVendedor == "03")
            {
                throw new InvalidOperationException("Vendedor Suspendido En ServicioDominio ValidarUsuarioVendedor");

            }


            estadoValidUsuEmpleado = true;

            return estadoValidUsuEmpleado;
        }        
    }
}
