using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using campusCare.modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace campusCare.vistasModelos
{
    public partial class DoctorLoginViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private string _Cedula;
        public string Cedula
        {
            get => _Cedula;
            set => SetProperty(ref _Cedula, value);
        }
        private string _Contraseña;
        public string Contraseña
        {
            get => _Contraseña;
            set => SetProperty(ref _Contraseña, value);
        }
        public ICommand DoctorLoginCommand { get; }

        public DoctorLoginViewModel()
        {
            ServerString server = new ServerString();
            DoctorLoginCommand = new AsyncRelayCommand(ExecuteDoctorLoginCommand);
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };

        }
        public async Task ExecuteDoctorLoginCommand()
        {
            var loginData = new DoctorLoginDTO
            {
                Cedula = this.Cedula,
                Contraseña = this.Contraseña
            };
            var response = await _httpClient.PostAsJsonAsync("api/Doctores/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var doctorLoginResponse = await response.Content.ReadFromJsonAsync<DoctorLoginResponse>();
                if (doctorLoginResponse != null)
                {
                    Preferences.Set("IdDoctor", doctorLoginResponse.IdDoctores.Value);
                    Preferences.Set("NombreCompleto", doctorLoginResponse.NombreCompleto);
                    Preferences.Set("EspecialidadFk", doctorLoginResponse.EspecialidadFk.Value);
                    await Shell.Current.GoToAsync("///DoctorMainPage");
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error en el servidor", "OK");
            }
        }
    }
}
