using campusCare.modelos;

namespace campusCare.Services
{
    interface ICitaMedica
    {
        void CreateAppointmentAsync(CitaRequest cita);
    }
}
