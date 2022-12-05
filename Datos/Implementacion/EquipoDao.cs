using Final_Prog2.Datos.Interfaces;
using Final_Prog2.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Prog2.Datos.Implementacion
{
    public class EquipoDao : IEquipoDao
    {
        public bool Save(Equipo equipo)
        {
            return HelperDao.ObtenerInstancia().GrabarEquipo(equipo, "SP_INSERTAR_EQUIPO", "SP_INSERTAR_DETALLES_EQUIPO");
        }

        public List<Persona> ToGetPersona()
        {
            List<Persona> listPersona = new List<Persona>();
            DataTable dt = HelperDao.ObtenerInstancia().cargarCombo("SP_CONSULTAR_PERSONAS");
            foreach(DataRow dr in dt.Rows)
            {
                Persona persona = new Persona();
                persona.IdPersona = (int)dr["id_persona"];
                persona.NombreCompleto = dr["nombre_completo"].ToString();
                persona.Clase = (int)dr["clase"];
                listPersona.Add(persona);
            }
            return listPersona;
        }
    }
}
