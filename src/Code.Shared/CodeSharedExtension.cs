using Code.Shared.JsInterop;
using Masa.Blazor.Extensions.Languages.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Token.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class CodeSharedExtension
{
    public static IServiceCollection AddCodeShared(this IServiceCollection services)
    {
        services.AddSemiDesignBlazorMonacoEditor();
        services.AddMasaBlazor();
        services.AddEventBus();
        services.AddScoped<HelperJsInterop>();
        services.AddScoped<CodeComponentJsInterop>();
        _ = Task.Run(async () =>
        {
            RazorCompile.Initialized(await GetReference(), await GetRazorExtension());
            // 添加编译时全局引用
            CompileRazorProjectFileSystem.AddGlobalUsing("@using Masa.Blazor");
            CompileRazorProjectFileSystem.AddGlobalUsing("@using BlazorComponent");
        });

        return services;
    }

    static async Task<List<PortableExecutableReference>?> GetReference()
    {
        #region Server
    
        var portableExecutableReferences = new List<PortableExecutableReference>();
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                // Server is running on the Server and you can get the file directly if you're a Hybrid like Maui Wpf you don't need to get the file through HttpClient and you can get the file directly like server
                portableExecutableReferences?.Add(MetadataReference.CreateFromFile(asm.Location));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    
        #endregion
   
        // As a result of WebAssembly and Server return to PortableExecutableReference mechanism are different need to separate processing
        return portableExecutableReferences;
    }

    static async Task<List<RazorExtension>> GetRazorExtension()
    {
        var razorExtension = typeof(CodeSharedExtension).Assembly.GetReferencedAssemblies().Select(asm => new AssemblyExtension(asm.FullName, AppDomain.CurrentDomain.Load(asm.FullName))).Cast<RazorExtension>().ToList();

        return razorExtension;
    }
}