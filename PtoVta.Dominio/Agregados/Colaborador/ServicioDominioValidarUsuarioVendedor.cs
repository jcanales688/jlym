using System;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

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
                throw new InvalidOperationException(Mensajes.advertencia_VendedorNoExiste);

            }

            if (pVendedor.Clave.Trim() != pClave.Trim())
            {
                //Clave Incorrecta
                throw new InvalidOperationException(Mensajes.advertencia_ClaveIncorrecta);
            }

            if (pVendedor.EstadoVendedor.CodigoEstadoVendedor == "02")
            {
                throw new InvalidOperationException(Mensajes.advertencia_VendedorInactivo);
            }

            if (pVendedor.EstadoVendedor.CodigoEstadoVendedor == "03")
            {
                throw new InvalidOperationException(Mensajes.advertencia_VendedorSuspendido);

            }


            estadoValidUsuEmpleado = true;

            return estadoValidUsuEmpleado;
        }        
    }
}
