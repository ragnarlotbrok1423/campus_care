using campusCare.modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;


namespace campusCare.vistasModelos
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("$values")]
        public T[] Values { get; set; } = Array.Empty<T>();
    }

    public partial class UsuariosViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        public ICommand logOutCommand { get; }

        [ObservableProperty]
        private ObservableCollection<UserDTO> usuarios = new ObservableCollection<UserDTO>();

        public IAsyncRelayCommand LoadUsuariosCommand { get; }
        public IRelayCommand<int> CargarPacienteCommand { get; }
        public UsuariosViewModel()

        {
            logOutCommand = new AsyncRelayCommand(ExecuteLogOutCommandAsync);
            ServerString server = new ServerString();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };

            LoadUsuariosCommand = new AsyncRelayCommand(LoadUsuariosAsync);
            CargarPacienteCommand = new RelayCommand<int>(async (id) =>
            {
                // Llama a la vista de creación pasando el ID del paciente
                await Shell.Current.GoToAsync($"CreateUserPage?id={id}");
            });
        }
        private async Task ExecuteLogOutCommandAsync()
        {
            Preferences.Remove("IdUsuario");
            await Shell.Current.GoToAsync("///Login");
        }

        public async Task LoadUsuariosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Pacientes");
                response.EnsureSuccessStatusCode();

                // Leer el contenido JSON como string
                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta de la API: {jsonString}");

                // Deserializar el contenido JSON
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();

                if (result != null && result.Values != null)
                {
                    Usuarios.Clear();
                    foreach (var usuario in result.Values)
                    {
                        Usuarios.Add(usuario);
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
