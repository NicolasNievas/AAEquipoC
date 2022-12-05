using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Prog2.Dominio
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string NombreCompleto { get; set; }
        public int Clase { get; set; }

        public Persona()
        {

        }

        public Persona(int idPersona, string nombreCompleto, int clase)
        {
            IdPersona = idPersona;
            NombreCompleto = nombreCompleto;
            Clase = clase;
        }
        public override string ToString()
        {
            return NombreCompleto;
        }
    }
}
