using Final_Prog2.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Prog2.Datos.Interfaces
{
    public interface IEquipoDao
    {
        List<Persona> ToGetPersona();
        bool Save(Equipo equipo);
    }
}
