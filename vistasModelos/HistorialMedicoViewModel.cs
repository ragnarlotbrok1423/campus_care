using campusCare.modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace campusCare.vistasModelos
{

    partial class HistorialMedicoViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private ObservableCollection<HistorialCitaMedica> historial = new ObservableCollection<HistorialCitaMedica>();


        public IAsyncRelayCommand LoadHistorialCommand { get; }


        public HistorialMedicoViewModel()
        {
            ServerString server = new ServerString();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };
            LoadHistorialCommand = new AsyncRelayCommand(LoadHistorialAsync);
            LoadHistorialCommand.Execute(null);
        }
        public async Task LoadHistorialAsync()
        {
            int userId = Preferences.Get("IdUsuario", 0); // 0 es el valor predeterminado si no existe el ID
            if (userId == 0)
            {
                Debug.WriteLine("No se ha encontrado el IdUsuario en las preferencias. El usuario no está logueado.");
                return; // O podrías redirigir al usuario a la página de login si es necesario
            }
            try
            {
                var response = await _httpClient.GetAsync($"api/CitasMedicas/paciente/{userId}");
                response.EnsureSuccessStatusCode();

                // Leer el contenido JSON como string
                var jsonString = await response.Content.ReadAsStringAsync(); ;
                Debug.WriteLine($"Respuesta de la API: {jsonString}");
                Debug.WriteLine($"Valor de userId: {userId}");

                // Deserializar el contenido JSON
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<HistorialCitaMedica>>();
                Debug.WriteLine($"result: {result}");
                if (result != null && result.Values != null)
                {
                    Historial.Clear();
                    foreach (var citaMedica in result.Values)
                    {
                        Historial.Add(citaMedica);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar usuarios: {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
