namespace MyWeb.Modules.Orders;
public class OrderConfig
{
    public string ConnectionString { set; get; } = "db";
}

public class OrdersModule : IModule
{
    public WebApplication MapEndpoints(WebApplication endpoints)
    {
        endpoints.MapGet("/orders", () =>
        {
            return "Hello, world";
        });

        return endpoints;
    }

    public IServiceCollection RegisterModules(IServiceCollection builder)
    {
        builder.AddSingleton(new OrderConfig());
        return builder;
    }
}