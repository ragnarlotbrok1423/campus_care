using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class CreatePacienteDTO
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public string? Alergia { get; set; }

        public string NumeroSecundario { get; set; } = null!;

        public int Tipaje { get; set; }
        public sbyte Visible { get; set; } = 1;
    }
    public class Tipaje
    {
        public string DisplayText { get; set; } // Texto que se muestra en el Picker
        public int Value { get; set; } // Valor que se envía a la API (de tipo int)

        public override string ToString()
        {
            return DisplayText; // Mostrar el DisplayText en el Picker
        }
    }



}
