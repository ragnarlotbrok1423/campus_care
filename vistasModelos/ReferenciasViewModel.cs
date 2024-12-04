using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using campusCare.modelos;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Threading.Tasks;

namespace campusCare.vistasModelos
{
    public partial class ReferenciasViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

       

        [ObservableProperty]
        private ObservableCollection<ReferenciasDTO> referencias = new ObservableCollection<ReferenciasDTO>();

        public IAsyncRelayCommand LoadReferenciasCommand { get; }

        public IRelayCommand<ReferenciasDTO> GenerarPdfCommand { get; }

        public IAsyncRelayCommand GeneratePdfCommand { get; }


        public ReferenciasViewModel()
        {
            ServerString server = new ServerString();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };
       
            LoadReferenciasCommand = new AsyncRelayCommand(LoadReferenciasAsync);
            GeneratePdfCommand = new AsyncRelayCommand(GeneratePdf);

        }
        private async Task GeneratePdf()
        {
            try
            {
                string filePath = @"C:\Users\erick\Downloads\referencias.pdf";
                string directoryPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directoryPath))
                {
                    Debug.WriteLine($"La carpeta de destino no existe: {directoryPath}");
                    return;
                }

                await GeneratePdfAsync(filePath);
                Debug.WriteLine("PDF generado exitosamente.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al generar el PDF: {ex.Message}");
            }
        }

        public async Task LoadReferenciasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Referencias/1");
                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Esta es la respuesta de la api para referencias {jsonString}");

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<ReferenciasDTO>>();

                if (result != null && result.Values != null)
                {
                    referencias.Clear();
                    foreach (var referencia in result.Values)
                    {
                        referencias.Add(referencia);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar las referencias : {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }


        public async Task GeneratePdfAsync(string filePath)
        {
            // Asegúrate de que tienes datos en `referencias`
            if (referencias.Count == 0)
            {
                Debug.WriteLine("No hay referencias para generar el PDF.");
                return;
            }

            using (var writer = new PdfWriter(filePath))
            using (var pdf = new PdfDocument(writer))
            {
                Document document = new Document(pdf);

                foreach (var referencia in referencias)
                {
                    // Agregar título
                    document.Add(new Paragraph($"Referencia ID: {referencia.Idreferencias}").SetFontSize(20));
                    document.Add(new Paragraph($"Fecha: {referencia.Fecha}"));
                    document.Add(new Paragraph($"Condición Médica: {referencia.CondicionMedica}"));
                    document.Add(new Paragraph($"Síntomas: {referencia.Sintomas}"));
                    document.Add(new Paragraph($"Diagnóstico: {referencia.Diagnostico}"));
                    document.Add(new Paragraph($"Especialidad: {referencia.Especialidad}"));

                    // Información del Doctor
                    if (referencia.Doctor != null)
                    {
                        document.Add(new Paragraph("Información del Doctor:"));
                        document.Add(new Paragraph($"Nombre Completo: {referencia.Doctor.NombreCompleto}"));
                        document.Add(new Paragraph($"Cédula: {referencia.Doctor.Cedula}"));
                    }

                    // Información del Paciente
                    if (referencia.Paciente != null)
                    {
                        document.Add(new Paragraph("Información del Paciente:"));
                        document.Add(new Paragraph($"Nombre: {referencia.Paciente.Nombre}"));
                        document.Add(new Paragraph($"Apellido: {referencia.Paciente.Apellido}"));
                        document.Add(new Paragraph($"Cédula: {referencia.Paciente.Cedula}"));
                    }

                    document.Add(new AreaBreak()); // Nueva página para la siguiente referencia
                }

                document.Close();
            }

            Debug.WriteLine($"PDF generado en: {filePath}");
        }
    }
}