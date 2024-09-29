using campusCare.modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace campusCare.vistasModelos
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private string _NombreUsuaruio;
        public string NombreUsuario
        {
            get => _NombreUsuaruio;
            set => SetProperty(ref _NombreUsuaruio, value);
        }
        private string _Contraseña;
        public string Contraseña
        {
            get => _Contraseña;
            set => SetProperty(ref _Contraseña, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            ServerString server = new ServerString();
            LoginCommand = new AsyncRelayCommand(ExecuteLoginCommand);
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };
        }

        public async Task ExecuteLoginCommand()
        {
            var loginData = new LoginPacients
            {
                NombreUsuario = this.NombreUsuario,
                Contraseña = this.Contraseña
            };
            var response = await _httpClient.PostAsJsonAsync("api/Pacientes/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                int IdUsuario = int.Parse(responseContent);
                string Nombre = responseContent;
                Preferences.Set("IdUsuario", IdUsuario);

                Debug.WriteLine($"Valor de userId: {IdUsuario}");
                if (IdUsuario == 4)
                {
                    await Shell.Current.GoToAsync("///CRUDpaciente");

                }
                else
                {
                    await Shell.Current.GoToAsync("///HomePacient");
                }



            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();


                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error inesperado", "OK");
            }


        }
    }


}
