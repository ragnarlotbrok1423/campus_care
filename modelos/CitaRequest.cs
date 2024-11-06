

namespace campusCare.modelos
{
    public class CitaRequest
    {
        public string Fecha { get; set; }

        public int IdTipoConsulta { get; set; }
        public int IdUsuario { get; set; }
        public int IdDoctor { get; set; }
    }

}
