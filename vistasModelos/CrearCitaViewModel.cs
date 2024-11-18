using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using campusCare.modelos;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Json;
using CommunityToolkit.Maui.Markup;
using System.Diagnostics;
using System.Windows.Input;
using System.Numerics;
using System.Text.Json;
using System.Globalization;



namespace campusCare.vistasModelos
{
    public partial class CrearCitaViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        public CrearCitaViewModel()
        {
            ServerString server = new ServerString();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };
            //creamos el objeto para crear una cita

            Especialidades = new ObservableCollection<EspecialidadDTO>();
            Doctores = new ObservableCollection<Doctores_X_Especialidad_Response>();
            TiposDeCita = new ObservableCollection<TiposConsultas>();
        }
        // funcion para crear una cita

        // se observa a "crearCita" con los datos requeridos para crear una cita


        [ObservableProperty]
        private ObservableCollection<EspecialidadDTO> especialidades;

        [ObservableProperty]
        private ObservableCollection<Doctores_X_Especialidad_Response> doctores;

        [ObservableProperty]
        private ObservableCollection<TiposConsultas> tiposDeCita;



        [ObservableProperty]
        private Doctores_X_Especialidad_Response doctorSeleccionado;

        [ObservableProperty]
        private TiposConsultas tipoCitaSeleccionado;

        [ObservableProperty]
        private DateTime fechaSeleccionada = DateTime.Today;

        [ObservableProperty]
        private TimeSpan horaSeleccionada = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private bool isDoctorPickerEnabled;


        private EspecialidadDTO especialidadSeleccionada;

        public EspecialidadDTO EspecialidadSeleccionada
        {
            get => especialidadSeleccionada;
            set
            {
                SetProperty(ref especialidadSeleccionada, value);
                OnEspecialidadChanged();
            }
        }



        [RelayCommand]
        private async Task CargarEspecialidadesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Especialidades");
                //leemos el contenido del json 
                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta de la API: {jsonString}");

                //deserializamos el json
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<EspecialidadDTO>>();
                if (result != null && result.Values != null)
                {
                    Especialidades.Clear();

                    foreach (var especialidad in result.Values)
                    {
                        Especialidades.Add(especialidad);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar las especialidades: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }
        public async void OnEspecialidadChanged()
        {
            if (EspecialidadSeleccionada != null)
            {
                try
                {
                    int idEspecialidad = EspecialidadSeleccionada.IdEspecialidades;
                    var doctoresList = await _httpClient.GetAsync($"api/Doctores/especialidad/{idEspecialidad}");
                    var jsonString = await doctoresList.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Respuesta de la API: {jsonString}");
                    var doctores = await doctoresList.Content.ReadFromJsonAsync<ApiResponse<Doctores_X_Especialidad_Response>>();
                    //var doctoresList = await _httpClient.GetFromJsonAsync<List<Doctores_X_Especialidad_Response>>($"api/Doctores/Especialidad{EspecialidadSeleccionada.IdEspecialidad}");
                    Doctores.Clear();
                    foreach (var doctor in doctores.Values)
                    {
                        Doctores.Add(doctor);
                    }
                    isDoctorPickerEnabled = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar los doctores: {ex.Message}");
                    isDoctorPickerEnabled = false;
                }
            }
            else
            {
                isDoctorPickerEnabled = false;
                Doctores.Clear();
            }
        }

        [RelayCommand]
        private async Task CargarDoctoresPorEspecialidadAsync()
        {
            if (EspecialidadSeleccionada == null) return;
            try
            {
                int idEspecialidad = EspecialidadSeleccionada.IdEspecialidades;
                var response = await _httpClient.GetAsync($"api/Doctores/especialidad/{idEspecialidad}");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta de la API: {jsonString}");
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<Doctores_X_Especialidad_Response>>();
                if (result != null && result.Values != null)
                {
                    Doctores.Clear();
                    foreach (var doctor in result.Values)
                    {
                        Doctores.Add(doctor);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar los doctores: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }


        }

        [RelayCommand]
        private async Task CargarTiposDeCitaAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/TiposConsultas");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta de la API: {jsonString}");
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<TiposConsultas>>();
                if (result != null && result.Values != null)
                {
                    TiposDeCita.Clear();
                    foreach (var tipoCita in result.Values)
                    {
                        TiposDeCita.Add(tipoCita);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar los tipos de cita: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        [RelayCommand]
        private async Task CrearCitaAsync()
        {
            if (EspecialidadSeleccionada == null || DoctorSeleccionado == null || TipoCitaSeleccionado == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }
            else
            {
                try
                {
                    var fechaHora = new DateTime(
                        FechaSeleccionada.Year,
                        FechaSeleccionada.Month,
                        FechaSeleccionada.Day,
                        HoraSeleccionada.Hours,
                        HoraSeleccionada.Minutes,
                        HoraSeleccionada.Seconds
                    ).ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                    var cita = new
                    {
                        idUsuario = Preferences.Get("IdUsuario", 0),
                        idDoctor = DoctorSeleccionado.IdDoctores,
                        fecha = fechaHora,
                        IdTipoConsulta = TipoCitaSeleccionado.IdtiposConsultas // Asegúrate que este es el nombre correcto de la propiedad
                    };
                    Debug.WriteLine($"Esta es cadena para el post {cita}");
                    var json = JsonSerializer.Serialize(cita);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Usa la URL base que ya está configurada en el HttpClient
                    var response = await _httpClient.PostAsync("api/CitasMedicas", content);

                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Éxito", "Cita agendada correctamente, debe esperar" +
                            "que el doctor acepte la cita", "OK");
                        await Shell.Current.GoToAsync("///HomePacient");
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo crear la cita: {errorMessage}", "OK");
                        Debug.WriteLine($"Error al crear la cita: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error de exepcion: {ex.Message}", "OK");
                    Debug.WriteLine($"Error al crear la cita: {ex.Message}");
                }
            }

        }
    }
}