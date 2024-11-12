using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class DoctoresDTO
    {
        public int IdDoctores { get; set; }

        public string NombreCompleto { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public EspecialidadesDTO Especialidad { get; set; }

        public string Diploma { get; set; } = null!;

        public string Perfil { get; set; } = null!;

        public InformacionMedicaDTO InformacionMedica { get; set; }
    }
    public class EspecialidadesDTO
    {
        public int IdEspecialidades { get; set; }

        public string Nombre { get; set; } = null!;
    }
}
