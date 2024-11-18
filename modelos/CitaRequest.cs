

namespace campusCare.modelos
{
    public class CitaRequest
    {
        public string Fecha { get; set; }
        public string? Descripcion { get; set; }
        public int IdTipoConsulta { get; set; }
        public int IdUsuario { get; set; }
        public int IdDoctor { get; set; }
    }

    public class PacientesByDoctor
    {
        public int IdTipoConsulta
        {
            get; set;
        }
        public TipoConsultaDTO TipoConsulta { get; set; }

        public UserDTO Usuarios { get; set; }

    }
}
