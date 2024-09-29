using campusCare.modelos;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System;



namespace campusCare.vistasModelos
{
    class CitaMedicaService
    {
        HttpClient client;

        public CitaMedicaService()
        {
            this.client = new HttpClient();
        }
        public async Task CreateAppointmentAsync(CitaRequest cita)
        {
            ServerString server = new ServerString();
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, server.cabecera);
                message.Content = JsonContent.Create<CitaRequest>(cita);

                HttpResponseMessage response = await client.SendAsync(message);

                if (!response.IsSuccessStatusCode)
                {
                    // Manejar el error según sea necesario
                    throw new Exception($"Error al crear la cita: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
        }



    }
}
