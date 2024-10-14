using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.modelos
{
    public class LoginPacients
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
    }
    public class LoginResponse
    {
        public string? Message { get; set; }
        public int? IdUsuarios { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}

