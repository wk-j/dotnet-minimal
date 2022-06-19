namespace MyWeb.Modules;

using System.Linq;

public interface IModule
{
    IServiceCollection RegisterModules(IServiceCollection builder);
    WebApplication MapEndpoints(WebApplication endpoints);
}

public static class ModuleExtensions
{
    static readonly List<IModule> _registeredModules = new List<IModule>();

    public static IServiceCollection RegisterModules(this IServiceCollection services)
    {
        var modules = DiscoverModules();
        foreach (var module in modules)
        {
            module.RegisterModules(services);
            _registeredModules.Add(module);
        }

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        foreach (var module in _registeredModules)
        {
            module.MapEndpoints(app);
        }
        return app;
    }

    private static IEnumerable<IModule> DiscoverModules()
    {
        return typeof(IModule).Assembly
            .GetTypes()
            .Where(x => x.IsClass && x.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
    }
}