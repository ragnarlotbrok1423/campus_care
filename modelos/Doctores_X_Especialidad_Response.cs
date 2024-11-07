using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class Doctores_X_Especialidad_Response
    {
        public int IdDoctores { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public Especialidad Especialidad { get; set; }

    }
}
