using FFImageLoading;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace campusCare
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .UseMauiApp<App>()
                 .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Bungee-Regular.ttf", "Bungee");
                    fonts.AddFont("Ubuntu-Bold.ttf", "Ubuntu");
                });



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
