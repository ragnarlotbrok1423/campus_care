using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class DoctorLoginDTO
    {
        public string Cedula { get; set; } = null!;

        public string Contraseña { get; set; } = null!;
    }
    public class DoctorLoginResponse
    {
        public int? IdDoctores { get; set; }

        public string NombreCompleto { get; set; } = null!;
        public int? EspecialidadFk { get; set; }
    }
}
