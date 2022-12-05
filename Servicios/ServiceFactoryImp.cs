using Final_Prog2.Servicios.Implementacion;
using Final_Prog2.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Prog2.Servicios
{
    class ServiceFactoryImp : AbstractFactoryService
    {
        public override IServicio crearServicio()
        {
            return new ServicioEquipo();
        }
    }
}
