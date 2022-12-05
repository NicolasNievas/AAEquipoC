using Final_Prog2.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Prog2.Servicios
{
    abstract class AbstractFactoryService
    {
        public abstract IServicio crearServicio();
    }
}
