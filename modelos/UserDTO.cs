

namespace campusCare.modelos
{
    public class UserDTO
    {
        public int IdUsuarios { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string NombreUsuario { get; set; }
        public InformacionMedicaDTO InformacionMedica { get; set; }
        public sbyte Visible { get; set; }

    }



    public class InformacionMedicaDTO
    {
        public string Alergia { get; set; }
        public string NumeroSecundario { get; set; }
        public TipajeSanguineoDTO TipajeSanguineo { get; set; }
    }

    public class TipajeSanguineoDTO
    {
        public string TipoSanguineo { get; set; }
    }
}
