using System;
using PtoVta.Dominio.BaseTrabajo;
using static PtoVta.Dominio.BaseTrabajo.Globales.GlobalDominio;

namespace PtoVta.Dominio.Agregados.Colaborador
{
    public class VendedorDireccion :
                            ObjetoValor<VendedorDireccion>
    {
        public string Pais { get; private set; }
        public string Departamento { get; private set; }
        public string Provincia { get; private set; }
        public string Distrito { get; private set; }
        public string Ubicacion { get; set; }

        public VendedorDireccion() { }
        public VendedorDireccion(string pPais, string pDepartamento, string pProvincia,
                                                    string pDistrito, string pUbicacion)
        {
            this.Pais = pPais;
            this.Departamento = pDepartamento;
            this.Provincia = pProvincia;
            this.Distrito = pDistrito;
            this.Ubicacion = !string.IsNullOrEmpty(pUbicacion) ? pUbicacion.Trim()
                                : throw new ArgumentException(Mensajes.advertencia_UbicacionDeDireccionNoPuedeSerNuloOVacio);
        }


    }
}