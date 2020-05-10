using System;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public interface IServicioDominioValidarUsuarioVendedor
    {
        bool ValidarUsuarioVendedor(Vendedor pVendedor, string pClave);
    }
}
