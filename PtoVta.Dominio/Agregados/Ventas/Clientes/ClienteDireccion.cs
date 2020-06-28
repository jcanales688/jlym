using System;
using PtoVta.Dominio.BaseTrabajo;

namespace PtoVta.Dominio.Agregados.Ventas
{
    public class ClienteDireccion :   ObjetoValor<ClienteDireccion>
    {
        public string Pais { get; private set; }
        public string Departamento { get; private set; }
        public string Provincia { get; private set; }
        public string Distrito { get; private set; }
        public string Ubicacion { get; set; }

        public ClienteDireccion() { }
        public ClienteDireccion(string pPais, string pDepartamento, string pProvincia, 
                                                    string pDistrito, string pUbicacion)
        {
            this.Pais  = pPais;
            this.Departamento = pDepartamento;
            this.Provincia= pProvincia;
            this.Distrito = pDistrito;
            this.Ubicacion = pUbicacion;
        }      
    }
}