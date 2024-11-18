using campusCare.modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace campusCare.vistasModelos
{
    public partial class CrearRecetasViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private string _Observaciones;


        public string Observaciones
        {
            get => _Observaciones;
            set => SetProperty(ref _Observaciones, value);
        }
        
        
        public CrearRecetasViewModel()
        {
            ServerString server = new ServerString();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };
            //creamos el objeto para crear una receta
            Medicamentos = new ObservableCollection<MedicamentosByCategoriaDTO>();
            Categorias = new ObservableCollection<CategoriaDTO>();
            Pacientes = new ObservableCollection<PacientesByDoctor>();
            

        }

        // vamos a crear los objetos para poder rastrear y almacenar la informacion 

        [ObservableProperty]
        private ObservableCollection<CategoriaDTO> categorias;

        [ObservableProperty]
        private ObservableCollection<MedicamentosByCategoriaDTO> medicamentos;

        [ObservableProperty]
        private DateTime fechaSeleccionada = DateTime.Today;

        [ObservableProperty]
        private int cantidadColocada;

        [ObservableProperty]
        private string observacionesColocadas;

        [ObservableProperty]
        private ObservableCollection<PacientesByDoctor> pacientes;



        [ObservableProperty]
        private MedicamentosByCategoriaDTO medicamentoSeleccionado;

        [ObservableProperty]
        private PacientesByDoctor pacienteSeleccionado;

        private CategoriaDTO categoriaSelecionada;

        public CategoriaDTO CategoriaSeleccionada
        {
            get => categoriaSelecionada;
            set
            {
                SetProperty(ref categoriaSelecionada, value);
                OnCategoriaChanged();
            }
        }



        [RelayCommand]
        private async Task CargarCategoriasAsync()
        {
            try
            {

                var response = await _httpClient.GetAsync("api/Categorias");
                // leemos el contenido del json
                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta de la Api: {jsonString}");

                //deserializamos el json
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<CategoriaDTO>>();
                Debug.WriteLine($"Esta es la conversion: {JsonSerializer.Serialize(result)}");
                if (result != null && result.Values != null)
                {
                    Categorias.Clear();
                    foreach (var categoria in result.Values)
                    {
                        Categorias.Add(categoria);
                    }
                }



            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar las categorias: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        public async void OnCategoriaChanged()
        {
            if (CategoriaSeleccionada != null)
            {
                try
                {

                    int idCategoria = CategoriaSeleccionada.IdCategoria;
                    var medicamentosList = await _httpClient.GetAsync($"api/Medicamentos/categoria/{idCategoria}");

                    var jsonString = await medicamentosList.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Respuesta de la API para los medicamentos: {jsonString}");
                    
                    var response = await medicamentosList.Content.ReadFromJsonAsync<ApiResponse<MedicamentosByCategoriaDTO>>();

                    if (response != null && response.Values != null)
                    {
                        Medicamentos.Clear();
                        foreach (var medicamentos in response.Values)
                        {
                            Medicamentos.Add(medicamentos);
                        }

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar los doctores: {ex.Message}");
                }
            }
        }

     
        [RelayCommand]
        private async Task LoadPacientesAsync()
        {
            try
            {
                var idDoctor = Preferences.Get("IdDoctor", 0);
                Debug.WriteLine($"Id para obtener los pacientes por doctor: {idDoctor}");

                var response = await _httpClient.GetAsync($"api/CitasMedicas/doctor/{idDoctor}");

                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta de la API para los pacientes: {jsonString}");

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PacientesByDoctor>>();
                Debug.WriteLine($"Resultado de la conversión: {JsonSerializer.Serialize(result)}");

                if (result != null && result.Values != null)
                {
                    Debug.WriteLine($"Número de pacientes obtenidos: {result.Values.Length}");
                    Pacientes.Clear();
                    foreach (var paciente in result.Values)
                    {
                        Debug.WriteLine($"Paciente deserializado: {JsonSerializer.Serialize(paciente)}");
                        Pacientes.Add(paciente);
                    }
                }
                else
                {
                    Debug.WriteLine("El resultado de la deserialización es nulo o no contiene valores.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar los pacientes: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        [RelayCommand]
        private async Task CrearRecetaAsync()
        {
            if (CategoriaSeleccionada == null || MedicamentoSeleccionado == null || PacienteSeleccionado == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }
            else
            {
                try
                {

                    var fecha = new DateTime(fechaSeleccionada.Year, fechaSeleccionada.Month, fechaSeleccionada.Day).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);


                    var receta = new
                    {
                        fechaDeEntrega = fecha,
                        cantidadEntregada = cantidadColocada,
                        observaciones = observacionesColocadas,
                        idPaciente = pacienteSeleccionado.Usuarios.IdUsuarios,
                        idDoctor = Preferences.Get("IdDoctor", 0),
                        idMedicamento = medicamentoSeleccionado.IdMedicamento

                    };


                    Debug.WriteLine($"Esta es cadena para el post {receta}");
                    var json = JsonSerializer.Serialize(receta);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("api/Recetas", content);

                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Éxito", "Receta agendada correctamente", "OK");

                        await Shell.Current.GoToAsync("///DoctorMainPage");
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo crear la receta: {errorMessage}", "OK");
                        Debug.WriteLine($"Error al crear la receta: {errorMessage}");
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