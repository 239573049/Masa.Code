using Code.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Code.App;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddCodeShared();
        builder.Services.AddMauiBlazorWebView();
        builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));
        var language = builder.Configuration.GetSection(nameof(LanguageOptions));

        builder.Services.AddSingleton(language.Get<LanguageOptions[]>());
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif


        return builder.Build();
    }
}
