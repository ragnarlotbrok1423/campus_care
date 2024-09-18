using campusCare.modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace campusCare.vistasModelos
{
    public partial class CreateUserViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<Tipaje> Tipajes { get; set; } = new ObservableCollection<Tipaje>
        {
            new Tipaje { DisplayText = "A+", Value = 2 },
            new Tipaje { DisplayText = "A-", Value = 6 },
            new Tipaje { DisplayText = "B+", Value = 4 },
            new Tipaje { DisplayText = "B-", Value = 8 },
            new Tipaje { DisplayText = "AB+", Value = 3 },
            new Tipaje { DisplayText = "AB-", Value = 7 },
            new Tipaje { DisplayText = "O+", Value = 1 },
            new Tipaje { DisplayText = "O-", Value = 5 }
        };


        [ObservableProperty]
        private Tipaje selectedTipaje;

        // Propiedad que contendrá el nuevo o el usuario a editar
        [ObservableProperty]
        private CreatePacienteDTO nuevoPaciente = new CreatePacienteDTO();

        [ObservableProperty]
        private int? pacienteId;

        [ObservableProperty]
        private UpdatePaciente usuarioAEditar = new UpdatePaciente();
        // Propiedad para saber si estamos editando


        // Comandos para crear y actualizar usuario
        public IAsyncRelayCommand CrearUsuarioCommand { get; }
        public IAsyncRelayCommand ActualizarUsuarioCommand { get; }

        public CreateUserViewModel()
        {
            NuevoPaciente = new CreatePacienteDTO();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://c66d-200-124-21-55.ngrok-free.app/") // Cambia esta URL si es necesario
            };

            CrearUsuarioCommand = new AsyncRelayCommand(CrearUsuarioAsync);
            ActualizarUsuarioCommand = new AsyncRelayCommand(ActualizarUsuarioAsync);
        }

        // Método para enviar los datos a la API (POST)
        private async Task CrearUsuarioAsync()
        {
            NuevoPaciente.Tipaje = SelectedTipaje.Value;

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Pacientes", NuevoPaciente);

                if (response.IsSuccessStatusCode)
                {
                    // Redirigir a la vista CRUD de pacientes
                    await Shell.Current.GoToAsync("///CRUDpaciente");
                }
                else
                {
                    // Manejar el error
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al crear usuario: {ex.Message}");
            }
        }

        // Método para actualizar los datos en la API (PUT)
        private async Task ActualizarUsuarioAsync()
        {
            if (PacienteId.HasValue)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"api/Pacientes/{PacienteId.Value}", NuevoPaciente);

                    if (response.IsSuccessStatusCode)
                    {
                        await Shell.Current.GoToAsync("///CRUDpaciente");
                    }
                    else
                    {
                        // Manejar el error
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al actualizar usuario: {ex.Message}");
                }
            }
        }
        public async Task CargarPacienteParaEdicionAsync(int id)
        {
            try
            {
                var paciente = await _httpClient.GetFromJsonAsync<CreatePacienteDTO>($"api/Pacientes/{id}");
                if (paciente != null)
                {
                    NuevoPaciente = paciente;
                    PacienteId = id; // Establecer el ID para actualizar
                    SelectedTipaje = Tipajes.FirstOrDefault(t => t.Value == paciente.Tipaje);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar paciente: {ex.Message}");
            }
        }

    }
}
