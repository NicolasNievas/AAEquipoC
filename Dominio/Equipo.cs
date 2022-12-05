using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Prog2.Dominio
{
    public class Equipo
    {
        public string Pais { get; set; }
        public string DirectorTecnico { get; set; }

        public List<Jugador> jugadores { get; set; }

        public Equipo()
        {
            jugadores = new List<Jugador>();
        }
        public void AgregarJugador(Jugador j)
        {
            jugadores.Add(j);
        }
        public void QuitarJugador(int i)
        {
            jugadores.RemoveAt(i);
        }
    }
}
