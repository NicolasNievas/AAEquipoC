using Final_Prog2.Datos.Implementacion;
using Final_Prog2.Datos.Interfaces;
using Final_Prog2.Dominio;
using Final_Prog2.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Prog2.Servicios.Implementacion
{
    public class ServicioEquipo : IServicio
    {
        private IEquipoDao dao;
        public ServicioEquipo()
        {
            dao = new EquipoDao();
        }

        public bool ConfirmarEquipo(Equipo equipo)
        {
            return dao.Save(equipo);
        }

        public List<Persona> ObtenerPersona()
        {
            return dao.ToGetPersona();
        }
    }
}
