using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    class HistorialCitaMedica
    {
        public int IdcitasMedicas { get; set; }
        public DateOnly Fecha { get; set; }
        public Servicios Servicios { get; set; }
        public TipoConsultaDTO TipoConsulta { get; set; }
        public Pacientes Pacientes { get; set; }
        public Doctores Doctores { get; set; }
    }
    class Servicios
    {
        public sbyte CertificadoBuenaSalud { get; set; }

        public float Peso { get; set; }

        public int? Inhaloterapias { get; set; }

        public string? Inyecciones { get; set; }

        public float? GlisemiaCapilar { get; set; }

        public string? ReferenciaMedica { get; set; }
    }
    public class TipoConsultaDTO
    {
        public string TipoConsulta { get; set; } = null!;
    }
    public class Pacientes
    {
        public int IdUsuarios { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;
        public sbyte Visible { get; set; }
    }
    public class Doctores
    {
        public int IdDoctores { get; set; }

        public string NombreCompleto { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string Especialidad { get; set; } = null!;

        public string Diploma { get; set; } = null!;

        public string Perfil { get; set; } = null!;
    }
}
