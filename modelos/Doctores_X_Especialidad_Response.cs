using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class Doctores_X_Especialidad_Response
    {
        public int IdDoctor { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string Especialidad { get; set; } = null!;

    }
}
