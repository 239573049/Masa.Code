using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.CodeDom.Compiler;

namespace Code.Desktop;

public partial class DesktopForm : Form
{
    public DesktopForm()
    {
        InitializeComponent();

        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"))
                .Build();
        var language = configuration.GetSection(nameof(LanguageOptions));
        services.AddSingleton(language.Get<Code.Shared.Options.LanguageOptions[]>());
        services.AddWindowsFormsBlazorWebView();
        services.AddCodeShared();
        blazorWeb.HostPage = "wwwroot\\index.html";
        blazorWeb.Services = services.BuildServiceProvider();
        blazorWeb.RootComponents.Add<Main>("#app");
    }
}
