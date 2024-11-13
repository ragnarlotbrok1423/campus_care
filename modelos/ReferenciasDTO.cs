using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    internal class ReferenciasDTO
    {
        public int Idreferencias { get; set; }

        public string Fecha { get; set; } = null!;

        public string CondicionMedica { get; set; } = null!;

        public string Sintomas { get; set; } = null!;

        public string Diagnostico { get; set; } = null!;

        public string Especialidad { get; set; } = null!;

        public string Pdf { get; set; } = null!;

        public UserDTO Paciente { get; set; } = null!;

        public DoctoresDTO Doctor { get; set; } = null!;
    }
}
