using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class MedicamentosDTO
    {
        public int Idmedicamento { get; set; }

        public string Nombre { get; set; } = null!;

        public CategoriaDTO Categoria { get; set; } = null!;

        public int CantidadStock { get; set; }
    }
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; } = null!;
    }
}
