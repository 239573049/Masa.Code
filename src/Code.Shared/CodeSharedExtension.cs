using Semi.Design.Blazor.Monaco.Editor;
using Token.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class CodeSharedExtension
{
    public static IServiceCollection AddCodeShared(this IServiceCollection services)
    {
        services.AddSemiDesignMonaco();
        services.AddMasaBlazor();
        services.AddEventBus();
        
        return services;
    }
}