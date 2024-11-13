using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class DonantesDTO
    {
        public int IddonantesSangre { get; set; }

        public string Fecha { get; set; } = null!;

        public UserDTO Paciente { get; set; } = null!;

        public DoctoresDTO Doctor { get; set; } = null!;
    }
}
