using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class RecetasDTO
    {
        public int IdRegistroDeEntrega { get; set; }
        public string FechaDeEntrega { get; set; } = null!;
        public int CantidadDeEntrega { get; set; }
        public string Observaciones { get; set; } = null!;
        public UserDTO Paciente { get; set; } = null!;
        public DoctoresDTO Doctor { get; set; } = null!;
        public MedicamentosDTO Medicamento { get; set; } = null!;
    }
    public class CreateRecetaDTO
    {
        public string FechaDeEntrega { get; set; } = null!;
        public int CantidadDeEntrega { get; set; }
        public string Observaciones { get; set; } = null!;
        public int IdPaciente { get; set; }
        public int IdDoctor { get; set; }
        public int IdMedicamento { get; set; }
    }
}
